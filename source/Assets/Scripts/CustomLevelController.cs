using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;

//TO ALL WHO ATTEMPT TO UNDERSTAND THIS SCRIPT:
//Good luck. From here on out, there are very few comments.

// 12/07/2024 Wow I really should have put some comments here. I'm gonna have to spend a while studying this code before I can update anything in it...

public class CustomLevelController : MonoBehaviour
{
    public GameObject wall;
    public GameObject checkpoint;
    public GameObject torch;
    public GameObject start;

    public Material SelectedObjectNormalMaterial;
    public Material SelectedObjectMaterial;

    public TMP_Text InfoText;

    public bool MovingObject;
    public int SelectedObjectNumber;

    public GameObject Player;
    public PlayerMovement PlayerMovement;
    public BoxCollider PlayerBoxCollider;
    public Camera playerCamera;

    public TMP_Dropdown ObjectTypeDropdown;

    public TMP_InputField XPositionTextbox;
    public TMP_InputField YPositionTextbox;
    public TMP_InputField ZPositionTextbox;
    public TMP_InputField XRotationTextbox;
    public TMP_InputField YRotationTextbox;
    public TMP_InputField ZRotationTextbox;
    public TMP_InputField XScaleTextbox;
    public TMP_InputField YScaleTextbox;
    public TMP_InputField ZScaleTextbox;
    public TMP_InputField ObjectTypeTextbox;

    public Transform testTransform;

    public List<float> objectPositionX;
    public List<float> objectPositionY;
    public List<float> objectPositionZ;

    public List<float> objectRotationX;
    public List<float> objectRotationY;
    public List<float> objectRotationZ;

    public List<float> objectScaleX;
    public List<float> objectScaleY;
    public List<float> objectScaleZ;

    public List<string> objectType;
    
    [Serializable]
    public class JsonLevelDataClass
    {
        public List<float> objectPositionX;
        public List<float> objectPositionY;
        public List<float> objectPositionZ;

        public List<float> objectRotationX;
        public List<float> objectRotationY;
        public List<float> objectRotationZ;

        public List<float> objectScaleX;
        public List<float> objectScaleY;
        public List<float> objectScaleZ;

        public List<string> objectType;
    }

    void Start()
    {
        LoadLevel();
    }

    void Update()
    {
        int numberOfObjects = objectPositionX.Count;
        int currentObject = 10000;
        while (currentObject < numberOfObjects)
        {
            GameObject levelObject = GameObject.Find("levelObject" + currentObject);
            objectPositionX[currentObject] = levelObject.transform.position.x;
            objectPositionY[currentObject] = levelObject.transform.position.y;
            objectPositionZ[currentObject] = levelObject.transform.position.z;
            objectRotationX[currentObject] = levelObject.transform.rotation.x;
            objectRotationY[currentObject] = levelObject.transform.rotation.y;
            objectRotationZ[currentObject] = levelObject.transform.rotation.z;
            objectScaleX[currentObject] = levelObject.transform.localScale.x;
            objectScaleY[currentObject] = levelObject.transform.localScale.y;
            objectScaleZ[currentObject] = levelObject.transform.localScale.z;
            currentObject++;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name.Length > 11)
                {
                    Debug.Log("Name length greater than 11.");
                    Debug.Log(hit.collider.name.Substring(0, 11));
                    if (hit.collider.name.Substring(0,11) == "levelObject")
                    {
                        //Sets the material of the previously selected object back to normal
                        GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material = SelectedObjectNormalMaterial;
                        //Sets the SelectedObjectNumber to the number of the object that was clicked on.
                        SelectedObjectNumber = int.Parse(hit.collider.name.Substring(11));
                        //Sets SelectedObjectNormalMaterial to the current material of the object that was just selected. SelectedObjectNormalMaterial is used to restore the selected object's material to normal when a new object is selected.
                        SelectedObjectNormalMaterial = GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material;
                    }
                }
                
            }
        }


        if (Input.GetKeyUp("e"))
        {
            if (MovingObject)
            {
                MovingObject = false;
            } else
            {
                MovingObject = true;
            }
        }

        if (Input.GetKeyUp("r"))
        {
            objectRotationY[SelectedObjectNumber] = objectRotationY[SelectedObjectNumber] + 45;
            if (objectRotationY[SelectedObjectNumber] == 360)
            {
                objectRotationY[SelectedObjectNumber] = 0;
            }
            UpdateCurrentObject(SelectedObjectNumber);
        }

        if (Input.GetKeyUp(";"))
        {
            SaveLevel();
        }
        if (Input.GetKeyUp("'"))
        {
            LoadLevel();
        }

    }

    public void FixedUpdate()
    {
        //Sets the currently selected object's material to the selected object material.
        GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material = SelectedObjectMaterial;

        //This crap caused lots of weird behavior when it was being run in Update(), and putting it in FixedUpdate() seemed to fix it.
        if (MovingObject)
        {
            GameObject.Destroy(GameObject.Find("levelObject" + SelectedObjectNumber));

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                objectPositionX[SelectedObjectNumber] = (float)Math.Round(hit.point.x * 2, MidpointRounding.AwayFromZero) / 2;
                objectPositionY[SelectedObjectNumber] = 1;
                objectPositionZ[SelectedObjectNumber] = (float)Math.Round(hit.point.z * 2, MidpointRounding.AwayFromZero) / 2;
                UpdateCurrentObject(SelectedObjectNumber);
                GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material = SelectedObjectMaterial;
            }
        }
    }

    public void LoadLevel()
    {
        if (!System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/level.json"))
        {
            //Creates a level with a single object becuase the editor does not like levels without any objects.
            System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/level.json", "{\"objectPositionX\":[-12.0],\"objectPositionY\":[1.0],\"objectPositionZ\":[-11.0],\"objectRotationX\":[0.0],\"objectRotationY\":[0.0],\"objectRotationZ\":[0.0],\"objectScaleX\":[1.0],\"objectScaleY\":[1.0],\"objectScaleZ\":[1.0],\"objectType\":[\"wall\"]}");
        }
        string json = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/level.json");
        JsonLevelDataClass levelData = JsonUtility.FromJson<JsonLevelDataClass>(json);
        objectPositionX = levelData.objectPositionX;
        objectPositionY = levelData.objectPositionY;
        objectPositionZ = levelData.objectPositionZ;
        objectRotationX = levelData.objectRotationX;
        objectRotationY = levelData.objectRotationY;
        objectRotationZ = levelData.objectRotationZ;
        objectScaleX = levelData.objectScaleX;
        objectScaleY = levelData.objectScaleY;
        objectScaleZ = levelData.objectScaleZ;
        objectType = levelData.objectType;

        int numberOfObjects = objectPositionX.Count;
        int currentObject = 0;
        while (currentObject < numberOfObjects)
        {
            Debug.Log("Creating levelObject" + currentObject);
            InstantiateLevelObject(currentObject);
            currentObject++;
        }
        //Sets SelectedObjectNormalMaterial to the current material of the object that was just selected. SelectedObjectNormalMaterial is used to restore the selected object's material to normal when a new object is selected.
        SelectedObjectNormalMaterial = GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material;
    }

    public void InstantiateLevelObject(int currentObject)
    {
        if (objectType[currentObject] == "start")
        {
            GameObject levelObject = GameObject.Instantiate(start);
            levelObject.transform.position = new Vector3(objectPositionX[currentObject], objectPositionY[currentObject], objectPositionZ[currentObject]);
            levelObject.transform.localEulerAngles = new Vector3(objectRotationX[currentObject], objectRotationY[currentObject], objectRotationZ[currentObject]);
            levelObject.transform.localScale = new Vector3(objectScaleX[currentObject], objectScaleY[currentObject], objectScaleZ[currentObject]);
            levelObject.name = "levelObject" + currentObject;
        }
        if (objectType[currentObject] == "wall")
        {
            GameObject levelObject = GameObject.Instantiate(wall);
            levelObject.transform.position = new Vector3(objectPositionX[currentObject], objectPositionY[currentObject], objectPositionZ[currentObject]);
            levelObject.transform.localEulerAngles = new Vector3(objectRotationX[currentObject], objectRotationY[currentObject], objectRotationZ[currentObject]);
            levelObject.transform.localScale = new Vector3(objectScaleX[currentObject], objectScaleY[currentObject], objectScaleZ[currentObject]);
            levelObject.name = "levelObject" + currentObject;
        }
        if (objectType[currentObject] == "torch")
        {
            GameObject levelObject = GameObject.Instantiate(torch);
            levelObject.transform.position = new Vector3(objectPositionX[currentObject], objectPositionY[currentObject], objectPositionZ[currentObject]);
            levelObject.transform.localEulerAngles = new Vector3(objectRotationX[currentObject], objectRotationY[currentObject], objectRotationZ[currentObject]);
            levelObject.transform.localScale = new Vector3(objectScaleX[currentObject], objectScaleY[currentObject], objectScaleZ[currentObject]);
            levelObject.name = "levelObject" + currentObject;
        }
        if (objectType[currentObject] == "checkpoint")
        {
            GameObject levelObject = GameObject.Instantiate(checkpoint);
            levelObject.transform.position = new Vector3(objectPositionX[currentObject], objectPositionY[currentObject], objectPositionZ[currentObject]);
            levelObject.transform.localEulerAngles = new Vector3(objectRotationX[currentObject], objectRotationY[currentObject], objectRotationZ[currentObject]);
            levelObject.transform.localScale = new Vector3(objectScaleX[currentObject], objectScaleY[currentObject], objectScaleZ[currentObject]);
            levelObject.name = "levelObject" + currentObject;
        }
    }

    public void SaveLevel()
    {
        JsonLevelDataClass levelData = new JsonLevelDataClass
        {
            objectPositionX = objectPositionX,
            objectPositionY = objectPositionY,
            objectPositionZ = objectPositionZ,
            objectRotationX = objectRotationX,
            objectRotationY = objectRotationY,
            objectRotationZ = objectRotationZ,
            objectScaleX = objectScaleX,
            objectScaleY = objectScaleY,
            objectScaleZ = objectScaleZ,
            objectType = objectType,
        };
        string json = JsonUtility.ToJson(levelData);
        System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/level.json", json);

    }

    public void PlayLevel()
    {
        if (objectType.Contains("start"))
        {
            SaveLevel();
            int StartPointObjectNumber = objectType.IndexOf("start");
            GameObject StartPointObject = GameObject.Find("levelObject" + StartPointObjectNumber);
            StartPointObject.GetComponent<MeshRenderer>().enabled = false;
            StartPointObject.GetComponent<BoxCollider>().enabled = false;
            Player.transform.position = new Vector3(StartPointObject.transform.position.x, StartPointObject.transform.position.y, StartPointObject.transform.position.z);
            PlayerMovement.rb.useGravity = true;
            PlayerBoxCollider.enabled = true;
        } else
        {
            InfoText.text = "You cannot play this level because there is no start point. Add a start point object to the level.";
        }
        
    }

    public void HelpButtonClicked()
    {
        UnityEngine.Application.OpenURL("https://github.com/MISTERPUG51/sticky/blob/main/LevelEditor.md");
    }
    public void CreateObject()
    {
        Debug.Log("Currently selected object: " + "levelObject" + SelectedObjectNumber);
        //Sets the material of the previously selected object back to normal
        GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material = SelectedObjectNormalMaterial;

        SelectedObjectNumber = objectPositionX.Count;
        Debug.Log(SelectedObjectNumber);
        objectPositionX.Add(0f);
        objectPositionY.Add(1f);
        objectPositionZ.Add(0f);
        objectRotationX.Add(0f);
        objectRotationY.Add(0f);
        objectRotationZ.Add(0f);
        objectScaleX.Add(1f);
        objectScaleY.Add(1f);
        objectScaleZ.Add(1f);
        if (ObjectTypeDropdown.value == 3)
        {
            objectType.Add("start");
        }
        if (ObjectTypeDropdown.value == 0)
        {
            objectType.Add("wall");
        }
        if (ObjectTypeDropdown.value == 1)
        {
            objectType.Add("torch");
        }
        if (ObjectTypeDropdown.value == 2)
        {
            objectType.Add("checkpoint");
        }


        InstantiateLevelObject(SelectedObjectNumber);
        //Sets SelectedObjectNormalMaterial to the current material of the object that was just selected. SelectedObjectNormalMaterial is used to restore the selected object's material to normal when a new object is selected.
        SelectedObjectNormalMaterial = GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material;
        MovingObject = true;
    }

    public void DestroyAllLevelObjects()
    {
        //Pretty self explanatory. This just deletes all level objects.
        int numberOfObjects = objectPositionX.Count;
        int currentObject = 0;
        while (currentObject < numberOfObjects)
        {
            Debug.Log("Destroying levelObject" + currentObject);
            GameObject.Destroy(GameObject.Find("levelObject" + currentObject));
            currentObject++;
        }
    }

    public void DeleteSelectedObject()
    {
        Debug.Log("objectPositionX.Count = " + objectPositionX.Count);
        if (objectPositionX.Count == 1)
        {
            //This triggers when there is only one object left.
            InfoText.text = "There must always be at least one object in the level. To delete this object, you must first create another one.";
        }
        else
        {
            //If there is more than one object, the currently selected one will be deleted.

            //Destroys all objects in the level.
            DestroyAllLevelObjects();

            //Removes all the data for the currently selected object from the level data.
            objectPositionX.RemoveAt(SelectedObjectNumber);
            objectPositionY.RemoveAt(SelectedObjectNumber);
            objectPositionZ.RemoveAt(SelectedObjectNumber);
            objectRotationX.RemoveAt(SelectedObjectNumber);
            objectRotationY.RemoveAt(SelectedObjectNumber);
            objectRotationZ.RemoveAt(SelectedObjectNumber);
            objectScaleX.RemoveAt(SelectedObjectNumber);
            objectScaleY.RemoveAt(SelectedObjectNumber);
            objectScaleZ.RemoveAt(SelectedObjectNumber);
            objectType.RemoveAt(SelectedObjectNumber);

            //Recreates all the remaining levelObjects.
            int numberOfObjects = objectPositionX.Count;
            int currentObject = 0;
            while (currentObject < numberOfObjects)
            {
                Debug.Log("Destroying levelObject" + currentObject);
                InstantiateLevelObject(currentObject);
                currentObject++;
            }
            SelectedObjectNumber = 0;
            //Sets SelectedObjectNormalMaterial to the current material of the object that was just selected. SelectedObjectNormalMaterial is used to restore the selected object's material to normal when a new object is selected.
            SelectedObjectNormalMaterial = GameObject.Find("levelObject" + SelectedObjectNumber).GetComponent<Renderer>().material;
        }
    }

    public void SetButtonClicked()
    {
        //I don't think any of the code in here is used anymore, but I'm leaving it here because something might break if I remove it.
        Debug.Log(SelectedObjectNumber);
        Debug.Log("Length of list: " + objectPositionX.Count.ToString());
        if (SelectedObjectNumber >= objectPositionX.Count)
        {
            Debug.Log("Adding object.");
            objectPositionX.Add(0f);
            objectPositionY.Add(0f);
            objectPositionZ.Add(0f);
            objectRotationX.Add(0f);
            objectRotationY.Add(0f);
            objectRotationZ.Add(0f);
            objectScaleX.Add(0f);
            objectScaleY.Add(0f);
            objectScaleZ.Add(0f);
            objectType.Add("wall");
        }


        objectPositionX[SelectedObjectNumber] = float.Parse(XPositionTextbox.text);
        objectPositionY[SelectedObjectNumber] = float.Parse(YPositionTextbox.text);
        objectPositionZ[SelectedObjectNumber] = float.Parse (ZPositionTextbox.text);
        objectRotationX[SelectedObjectNumber] = float.Parse(XRotationTextbox.text);
        objectRotationY[SelectedObjectNumber] = float.Parse(YRotationTextbox.text);
        objectRotationZ[SelectedObjectNumber] = float.Parse(ZRotationTextbox.text);
        objectScaleX[SelectedObjectNumber] = float.Parse(XScaleTextbox.text);
        objectScaleY[SelectedObjectNumber] = float.Parse(YScaleTextbox.text);
        objectScaleZ[SelectedObjectNumber] = float.Parse(ZScaleTextbox.text);
        objectType[SelectedObjectNumber] = ObjectTypeTextbox.text;
        UpdateCurrentObject(SelectedObjectNumber);

    }


    public void UpdateCurrentObject(int currentObject)
    {
        GameObject objectToDestroy = GameObject.Find("levelObject" + currentObject);
        GameObject.Destroy(objectToDestroy);
        InstantiateLevelObject(currentObject);
    }
}
