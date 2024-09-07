using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //This script controls more than the pause menu. Good luck to those who are trying to make sense of this game. Not all scripts are thoroughly commented.
    public PlayerMovement PlayerMovement;
    public GameObject PauseMenuUI;
    public Stopwatch GameStopwatch = Stopwatch.StartNew();
    public TMP_Text StopwatchText;
    public SaveHandler SaveHandler;
    public GameObject EndOfLevelUI;
    public TMP_Text LevelCompleteText;

    private void Start()
    {
        //Ensures that the stopwatch is at 0.
        GameStopwatch.Restart();
    }
    void Update()
    {
        //If the escape key is pressed, pause the game.
        if (Input.GetKey("escape"))
        {
            PauseGame();
        }

        //Convert the stopwatch time in milliseconds to human readable text.
        TimeSpan t = TimeSpan.FromMilliseconds(GameStopwatch.ElapsedMilliseconds);
        string GameStopWatchTimeFormattedToText = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
        //Display that text.
        StopwatchText.text = GameStopWatchTimeFormattedToText;
    }

    public void PauseGame()
    {
        //Freezes the position of the player.
        PlayerMovement.rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        //Prevents the player from moving.
        PlayerMovement.MovementEnabled = false;
        //Shows the pause menu.
        PauseMenuUI.SetActive(true);
        //Stops the stopwatch/
        GameStopwatch.Stop();
    }
    public void ResumeGame()
    {
        //starts the stopwatch.
        GameStopwatch.Start();
        //Unfreezes player position.
        PlayerMovement.rb.constraints = RigidbodyConstraints.FreezeRotation;
        //Allows the player to move again.
        PlayerMovement.MovementEnabled = true;
        //Hide the pause menu.
        PauseMenuUI.SetActive(false);
    }

    public void LevelCompleted()
    {
        //Same as the PauseGame() function, except the EndOfLevelUI is shown instead of the pause menu.
        GameStopwatch.Stop();
        PlayerMovement.rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        PlayerMovement.MovementEnabled = false;
        EndOfLevelUI.SetActive(true);

        //Sets personal best time for level 1 if necessary.
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            UnityEngine.Debug.Log("Active scene name = Level1");
            if (SaveHandler.Level1Time == 0)
            {
                UnityEngine.Debug.Log("Level1Time = 0");
                SaveHandler.Level1Time = GameStopwatch.ElapsedMilliseconds;
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
            }
            else if (GameStopwatch.ElapsedMilliseconds < SaveHandler.Level1Time)
            {
                UnityEngine.Debug.Log("Record time");
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
                SaveHandler.Level1Time = GameStopwatch.ElapsedMilliseconds;
            }
        }

        //Ditto for level 2.
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            UnityEngine.Debug.Log("Active scene name = Level2");
            if (SaveHandler.Level2Time == 0)
            {
                UnityEngine.Debug.Log("Level2Time = 0");
                SaveHandler.Level2Time = GameStopwatch.ElapsedMilliseconds;
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
            }
            else if (GameStopwatch.ElapsedMilliseconds < SaveHandler.Level2Time)
            {
                UnityEngine.Debug.Log("Record time");
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
                SaveHandler.Level2Time = GameStopwatch.ElapsedMilliseconds;
            }
        }

        //Ditto for level 3.
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            UnityEngine.Debug.Log("Active scene name = Level3");
            if (SaveHandler.Level3Time == 0)
            {
                UnityEngine.Debug.Log("Level3Time = 0");
                SaveHandler.Level3Time = GameStopwatch.ElapsedMilliseconds;
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
            }
            else if (GameStopwatch.ElapsedMilliseconds < SaveHandler.Level3Time)
            {
                UnityEngine.Debug.Log("Record time");
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
                SaveHandler.Level3Time = GameStopwatch.ElapsedMilliseconds;
            }
        }

        //Ditto for level 4.
        if (SceneManager.GetActiveScene().name == "Level4")
        {
            UnityEngine.Debug.Log("Active scene name = Level4");
            if (SaveHandler.Level4Time == 0)
            {
                UnityEngine.Debug.Log("Level4Time = 0");
                SaveHandler.Level4Time = GameStopwatch.ElapsedMilliseconds;
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
            }
            else if (GameStopwatch.ElapsedMilliseconds < SaveHandler.Level4Time)
            {
                UnityEngine.Debug.Log("Record time");
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
                SaveHandler.Level4Time = GameStopwatch.ElapsedMilliseconds;
            }
        }

        //Ditto for level 5.
        if (SceneManager.GetActiveScene().name == "Level5")
        {
            UnityEngine.Debug.Log("Active scene name = Level5");
            if (SaveHandler.Level5Time == 0)
            {
                UnityEngine.Debug.Log("Level5Time = 0");
                SaveHandler.Level5Time = GameStopwatch.ElapsedMilliseconds;
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
            }
            else if (GameStopwatch.ElapsedMilliseconds < SaveHandler.Level5Time)
            {
                UnityEngine.Debug.Log("Record time");
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
                SaveHandler.Level5Time = GameStopwatch.ElapsedMilliseconds;

            }
        }

        //Ditto for level 6.
        if (SceneManager.GetActiveScene().name == "Level6")
        {
            UnityEngine.Debug.Log("Active scene name = Level6");
            if (SaveHandler.Level6Time == 0)
            {
                UnityEngine.Debug.Log("Level6Time = 0");
                SaveHandler.Level6Time = GameStopwatch.ElapsedMilliseconds;
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
            }
            else if (GameStopwatch.ElapsedMilliseconds < SaveHandler.Level6Time)
            {
                UnityEngine.Debug.Log("Record time");
                LevelCompleteText.text = "Level Complete!<br>You beat your personal best time";
                SaveHandler.Level6Time = GameStopwatch.ElapsedMilliseconds;
            }
        }


        UnityEngine.Debug.Log("about to save");
        SaveHandler.SaveData();
    }

    public void MainMenu()
    {
        //If you can't figure out what this does, you're a moron.
        PlayerPrefs.DeleteKey("MusicTime");
        SceneManager.LoadScene("SampleScene");
    }
}
