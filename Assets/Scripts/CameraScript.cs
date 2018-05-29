using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Content:
//    Constants
//    Private Statics
//    Unity Events
//    Events Handlers:
//        StartButton_OnClick
//        MainMenu_OnEnter


public class CameraScript : MonoBehaviour {


    #region Constants
    private static string EPISODE_GAME_KEY = "EpisodeGame";
    #endregion


    #region Private Statics
    private static float cameraSpeed = 10f;
    private static GameObject charObj;
    private static Vector3 charPosition;
    private static float cameraSize;
    #endregion


    #region Unity Events
    void Start()
    {
        MainMenu_OnEnter();
    }


    void Update()
    {
        if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(0))
        {
            transform.position = new Vector3(
                charObj.transform.position.x + 2f, 
                charObj.transform.position.y + 1f, 
                gameObject.transform.position.z);

        }
        else
        {
            if (!PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(7))
            {
                charPosition = new Vector3(
                    charObj.transform.position.x + 2,
                    charObj.transform.position.y + 1,
                    gameObject.transform.position.z);

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    charPosition, 
                    Time.deltaTime * cameraSpeed);
            }
            if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(7))
            {
                transform.position = new Vector3(
                    charObj.transform.position.x + 0.5f,
                    charObj.transform.position.y,
                    gameObject.transform.position.z);
            }
        }
    }
    #endregion


    #region Event Handlers
    public void StartButton_OnClick()
    {
        Camera.main.orthographicSize = cameraSize;
        PlayerPrefs.SetInt(EPISODE_GAME_KEY, 0);
        PlatformController.BuildSecondPlatform();
        StickController.SetSpeedGrowStick(0);
        StickController.SetSpeedRotationStick(0);
        StickController.CreateObjectStick(CharController.GetVectorChar());
    }


    public void MainMenu_OnEnter()
    {
        cameraSize = 3.5f;
        charObj = GameObject.Find("Player");
        CharController.SetStartCharPosition();
        Camera.main.orthographicSize = 1f;

        transform.position = new Vector3(
            charObj.transform.position.x + 0.5f,
            charObj.transform.position.y,
            gameObject.transform.position.z);

        PlayerPrefs.SetInt(EPISODE_GAME_KEY, 7);
        StickController.DeleteObjectStick();
        PlatformController.DeletePlatform(0);
        PlatformController.DeletePlatform(1);
        PlatformController.BuildFirstPlatform();
    }
    #endregion


}
