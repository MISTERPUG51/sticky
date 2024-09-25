using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;

//TO ALL WHO ATTEMPT TO UNDERSTAND THIS SCRIPT:
//Good luck. From here on out, there are very few comments.



public class CustomLevelController : MonoBehaviour
{
    public GameObject wall;
    public GameObject checkpoint;
    public GameObject torch;
    public GameObject start;

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
                        SelectedObjectNumber = int.Parse(hit.collider.name.Substring(11));
                    }
                }
                
            }
        }


        if (Input.GetMouseButtonUp(1))
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
            }
        }
    }

    public void LoadLevel()
    {
        if (!System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/level.json"))
        {
            System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/level.json", "");
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
        SaveLevel();
        int StartPointObjectNumber = objectType.IndexOf("start");
        GameObject StartPointObject = GameObject.Find("levelObject" +  StartPointObjectNumber);
        StartPointObject.GetComponent<MeshRenderer>().enabled = false;
        Player.transform.position = new Vector3(StartPointObject.transform.position.x, StartPointObject.transform.position.y, StartPointObject.transform.position.z);
        PlayerMovement.rb.useGravity = true;
        PlayerBoxCollider.enabled = true;
    }

    public void HelpButtonClicked()
    {
        UnityEngine.Application.OpenURL("https://github.com/MISTERPUG51/sticky/blob/main/LevelEditor.md");
    }
    public void CreateObject()
    {
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
        MovingObject = true;
    }

    public void DestroyAllLevelObjects()
    {
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
        DestroyAllLevelObjects();
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
        int numberOfObjects = objectPositionX.Count;
        int currentObject = 0;
        while (currentObject < numberOfObjects)
        {
            Debug.Log("Destroying levelObject" + currentObject);
            InstantiateLevelObject(currentObject);
            currentObject++;
        }
    }

    public void SetButtonClicked()
    {
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
