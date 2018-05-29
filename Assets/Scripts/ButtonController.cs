using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Content:
//    Constants
//    Private Statics
//    Private
//    Unity Events
//    Events Handlers:
//        AudioButton_OnClick
//        ExitButton_OnClick
//        RestartButton_OnClick


public class ButtonController : MonoBehaviour {


    #region Constants
    private static string EPISODE_GAME_KEY = "EpisodeGame";
    private static string SOUND_KEY = "SoundEffects";
    #endregion


    #region Private Statics
    private static Sprite soundOnSprite;
    private static Sprite soundOffSprite;
    #endregion


    #region Private
    private Button soundEffectBtn;
    #endregion


    #region Unity Events
    void Start()
    {
        soundEffectBtn = 
            GameObject.Find("SoundEffectBtn").GetComponent<Button>();

        soundOnSprite = 
            Resources.Load("SoundOn", typeof(Sprite)) as Sprite;

        soundOffSprite =
            Resources.Load("SoundOff", typeof(Sprite)) as Sprite;

        if (PlayerPrefs.GetInt(SOUND_KEY).Equals(0))
        {
            soundEffectBtn.image.sprite = soundOnSprite;
        }
        else
        {
            soundEffectBtn.image.sprite = soundOffSprite;
        }

    }
    #endregion


    #region Event Handlers
    public void AudioButton_OnClick()
    {
        if (PlayerPrefs.GetInt(SOUND_KEY).Equals(0))
        {
            soundEffectBtn.image.sprite = soundOffSprite;
            PlayerPrefs.SetInt(SOUND_KEY, 1);
        }
        else
        {
            soundEffectBtn.image.sprite = soundOnSprite;
            PlayerPrefs.SetInt(SOUND_KEY, 0);

        }
    }


    public void ExitButton_OnClick()
    {
        Application.Quit();
    }


    public void RestartButton_OnClick()
    {
        PlayerPrefs.SetInt(EPISODE_GAME_KEY, 0);
        CharController.SetStartCharPosition();
        StickController.DeleteObjectStick();
        PlatformController.DeletePlatform(0);
        PlatformController.DeletePlatform(1);
        PlatformController.BuildSecondPlatform();
        PlatformController.BuildFirstPlatform();
        StickController.SetSpeedGrowStick(0);
        StickController.SetSpeedRotationStick(0);
        StickController.CreateObjectStick(CharController.GetVectorChar());
    }
    #endregion


}
