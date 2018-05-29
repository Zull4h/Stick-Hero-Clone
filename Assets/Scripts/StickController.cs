using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Content:
//    Constants
//    Private Statics
//    Unity Events
//    Public Methods:
//        SetSpeedGrowStick
//        SetSpeedRotationStick
//        CreateObjectStick
//        GetTransformStick
//        DeleteObjectStick


public class StickController : MonoBehaviour {


    #region Constants
    private static string EPISODE_GAME_KEY = "EpisodeGame";
    #endregion


    #region Private Statics
    private static float speedGrowStick = 0f;
    private static float speedRotationStick = 0f;
    private static GameObject stickObj;
    private static GameObject stickPrefab;
    #endregion


    #region Unity Events
    void Start()
    {
        speedGrowStick = 0f;
        speedRotationStick = 0f;
        stickPrefab = 
            Resources.Load("StickPrefab", typeof(GameObject)) as GameObject;
    }


    void Update()
    {
        if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(1))
        {
            speedGrowStick += 0.1f;
            stickObj.transform.localScale = 
                new Vector3(0.06f, speedGrowStick, 1);
        }
        else if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(2))
        {
            speedRotationStick -= 2f;
            stickObj.transform.localEulerAngles = 
                new Vector3(0, 0, speedRotationStick);
            if (speedRotationStick <= -90f)
            {
                PlayerPrefs.SetInt(EPISODE_GAME_KEY, 3);
            }
        }
    }
    #endregion


    #region Public Methods
    public static void SetSpeedGrowStick(float speed)
    {
        speedGrowStick = speed;
    }


    public static void SetSpeedRotationStick(float speed)
    {
        speedRotationStick = speed;
    }


    public static void CreateObjectStick(Vector3 CharPosition)
    {
        stickObj = Instantiate(
            stickPrefab,
            new Vector3(
                CharPosition.x + 0.45f,
                CharPosition.y - 0.26f,
                CharPosition.z),
            Quaternion.identity) as GameObject;
    }


    public static Transform GetTransformStick()
    {
        return stickObj.transform;
    }


    public static void DeleteObjectStick()
    {
        Destroy(stickObj);
    }
    #endregion


}
