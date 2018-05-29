using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Content:
//    Constants
//    Private Statics
//    Unity Events
//    Public Methods:
//        GetVectorChar
//        SetVectorMoveChar
//        SetStartCharPosition


public class CharController : MonoBehaviour {


    #region Constants
    private static string EPISODE_GAME_KEY = "EpisodeGame";
    #endregion


    #region Private Statics
    private static float speedRotationStick = 10f;
    private static Vector3 StartPositionMoveHero = 
        new Vector3(-3.45f, 0f, 0f);
    private static Vector3 VectorMoveHero = new Vector3(
        StartPositionMoveHero.x,
        StartPositionMoveHero.y,
        StartPositionMoveHero.z);
    #endregion


    #region Unity Events
    void Start()
    {
        SetStartCharPosition();
	}
	

	void Update()
    {
        if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(0) 
            || PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(7))
        {
            transform.position = VectorMoveHero;
        }
        if (!transform.position.Equals(VectorMoveHero))
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                VectorMoveHero, 
                Time.deltaTime * speedRotationStick);
        }
        else if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(4))
        {
            StickController.DeleteObjectStick();
            StickController.CreateObjectStick(VectorMoveHero);
            PlatformController.ChangeColumns();
        }
    }
    #endregion


    #region Public Methods
    public static Vector3 GetVectorChar()
    {
        return VectorMoveHero;
    }


    public static void SetVectorMoveChar(float VectorX)
    {
        VectorMoveHero.x = VectorX + VectorMoveHero.x;
    }


    public static void SetStartCharPosition()
    {
        VectorMoveHero.x = StartPositionMoveHero.x;
    }
    #endregion


}
