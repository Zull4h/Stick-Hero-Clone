using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Content:
//    Constants
//    Private Statics
//    Unity Events
//    Public Methods:
//        BuildSecondPlatform
//        BuildFirstPlatform
//        ChangeColumns
//        GetTransformOfPlatform
//        DeletePlatform
//    Private Methods:
//        BuildPlatform


public class PlatformController : MonoBehaviour {


    #region Constants
    private static string EPISODE_GAME_KEY = "EpisodeGame";
    #endregion


    #region Private Statics
    private static GameObject firstPlatform;
    private static GameObject secondPlatform;
    private static Vector3 charPosition;
    #endregion


    #region Unity Events
    void Start()
    {
        charPosition = CharController.GetVectorChar();
    }
    #endregion


    #region Public Methods
    public static void BuildSecondPlatform()
    {
        secondPlatform = BuildPlatform(1);
    }


    public static void BuildFirstPlatform()
    {
        firstPlatform = BuildPlatform(0);
    }


    public static void ChangeColumns()
    {
        charPosition = CharController.GetVectorChar();
        Destroy(firstPlatform);
        firstPlatform = secondPlatform;
        secondPlatform = BuildPlatform(1);
        PlayerPrefs.SetInt(EPISODE_GAME_KEY, 5);
    }


    public static Transform GetTransformOfPlatform(int count)
    {
        if (count == 0)
        {
            return firstPlatform.transform;
        }
        else
        {
            return secondPlatform.transform;
        }
    }


    public static void DeletePlatform(int numb) 
    {
        if (numb == 0)
        {
            Destroy(firstPlatform);
        }
        else
        {
            Destroy(secondPlatform);
        }
    }
    #endregion


    #region Private Methods
    private static GameObject BuildPlatform(int numberOfPlatform)
    {
        if (numberOfPlatform == 0)
        {
            charPosition = CharController.GetVectorChar();
            GameObject platform = 
                Instantiate(
                    Resources.Load("PlatformPrefab"),
                    new Vector3(
                        charPosition.x + 0.45f,
                        charPosition.y - 2.2f,
                        charPosition.z),
                    Quaternion.identity) as GameObject;
            platform.transform.localScale = new Vector3(1.5f, 4f, 1f);
            return platform;
        }
        else
        {
            charPosition = CharController.GetVectorChar();
            GameObject platform =
                Instantiate(
                    Resources.Load("PlatformPrefab"),
                    new Vector3(
                        Random.Range(3f, 5f) + charPosition.x + 0.45f,
                        charPosition.y - 2.2f, 
                        charPosition.z), 
                    Quaternion.identity) as GameObject;
            platform.transform.localScale =
                new Vector3(Random.Range(0.75f, 2.5f), 4f, 1f);
            return platform;
        }
    }
    #endregion


}
