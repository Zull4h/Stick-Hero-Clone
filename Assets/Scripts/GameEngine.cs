using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Content:
//    Constants
//    Private Statics
//    Private
//    Unity Events
//    Public Methods:
//        ClearGameOverPanel
//    Private Methods:
//        UpdateScores
//        ShowGameOverPanel


public class GameEngine : MonoBehaviour {


    #region Constants
    private static string EPISODE_GAME_KEY = "EpisodeGame";
    private static string SOUND_KEY = "SoundEffects";
    private static string HIGH_SCORE_KEY = "HighScore";
    #endregion


    #region Private Statics
    private static Transform stickTransform;
    private static Transform firstPlatformTransform;
    private static Transform secondPlatformTransform;
    private static int currentScore;
    private static int highestScore;
    #endregion


    #region Private
    private float StickLength;
    private float DistanceBetweenPlatforms;
    private float WidthOfSecondPlatform;
    private float MaxStickLength;
    private float MinStickLength;
    private GameObject gameOverPanel;
    private Text gameOverPanelCurrentScoreText;
    private Text currentScoreText;
    private Text higestScoreText;
    private AudioClip deathSound;
    private AudioClip successSound;
    private AudioClip bonusSound;
    private AudioClip stickGrowSound;
    private AudioSource audioSource;
    #endregion


    #region Unity Events
    void Start()
    {
        gameOverPanel = GameObject.Find("GameOverScreen");

        currentScoreText = 
            GameObject.Find("CurrentScoreInGame").GetComponent<Text>();

        gameOverPanelCurrentScoreText = 
            GameObject.Find("CurrentScore").GetComponent<Text>();

        higestScoreText = GameObject.Find("HighScore").GetComponent<Text>();

        audioSource = 
            GameObject.Find("EventSystem").GetComponent<AudioSource>();

        deathSound = Resources.Load("death", typeof(AudioClip)) as AudioClip;

        successSound = 
            Resources.Load("success", typeof(AudioClip)) as AudioClip;

        bonusSound = Resources.Load("bonus", typeof(AudioClip)) as AudioClip;
        audioSource.Stop();
        higestScoreText.text = "High:" + Environment.NewLine;
        gameOverPanelCurrentScoreText.text = "Current:" + Environment.NewLine;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        currentScore = 0;
    }


    void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt(EPISODE_GAME_KEY));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(0))
            {
                PlayerPrefs.SetInt(EPISODE_GAME_KEY, 1);
                if (PlayerPrefs.GetInt(SOUND_KEY).Equals(0))
                {
                    audioSource.Play();
                }
                return;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (PlayerPrefs.GetInt(EPISODE_GAME_KEY).Equals(1))
            {
                audioSource.Stop();
                PlayerPrefs.SetInt(EPISODE_GAME_KEY, 2);
                return;

            }
        }
        switch (PlayerPrefs.GetInt(EPISODE_GAME_KEY))
        {
            case 3:
                stickTransform = StickController.GetTransformStick();

                firstPlatformTransform =
                    PlatformController.GetTransformOfPlatform(0);

                secondPlatformTransform = 
                    PlatformController.GetTransformOfPlatform(1);

                StickLength = stickTransform.localScale.y;
                DistanceBetweenPlatforms = Mathf.Abs(
                    secondPlatformTransform.position.x 
                    - firstPlatformTransform.position.x);

                WidthOfSecondPlatform = (secondPlatformTransform.localScale.x);
                MaxStickLength = DistanceBetweenPlatforms;

                MinStickLength = 
                    DistanceBetweenPlatforms - WidthOfSecondPlatform;

                if (MinStickLength <= StickLength 
                    && StickLength <= MaxStickLength)
                {
                    CharController.SetVectorMoveChar(MaxStickLength);
                    if (MaxStickLength - 0.4f 
                        * WidthOfSecondPlatform 
                        > StickLength && MaxStickLength - 0.6f 
                        * WidthOfSecondPlatform < StickLength)
                    {
                        currentScore++;
                        if (PlayerPrefs.GetInt(SOUND_KEY).Equals(0))
                        {
                            audioSource.PlayOneShot(bonusSound);
                        }
                    }
                    else
                    {
                        if (PlayerPrefs.GetInt(SOUND_KEY).Equals(0))
                        {
                            audioSource.PlayOneShot(successSound);
                        }
                    }
                    currentScore++;
                    UpdateScores(currentScore);    
                    PlayerPrefs.SetInt(EPISODE_GAME_KEY, 4);
                }
                else
                {
                    ShowGameOverPanel(currentScore);
                    currentScore = 0;
                    if (PlayerPrefs.GetInt(SOUND_KEY).Equals(0))
                    {
                        audioSource.PlayOneShot(deathSound);
                    }
                    PlayerPrefs.SetInt(EPISODE_GAME_KEY, 6);
                }
                break;
            case 5:
                StickController.SetSpeedGrowStick(0);
                StickController.SetSpeedRotationStick(0);
                PlayerPrefs.SetInt(EPISODE_GAME_KEY, 0);
                break;
        }
    }
    #endregion


    #region Public Methods
    public void ClearGameOverPanel()
    {
        higestScoreText.text = "High:" + Environment.NewLine;
        gameOverPanelCurrentScoreText.text = "Current:" + Environment.NewLine;
    }
    #endregion


    #region Private Methods
    private void UpdateScores(int score)
    {
        currentScoreText.text = score.ToString();
    }
    

    private void ShowGameOverPanel(int score)
    {
        if (PlayerPrefs.GetInt(HIGH_SCORE_KEY) < score)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        }
        gameOverPanelCurrentScoreText.text += score.ToString();
        higestScoreText.text += PlayerPrefs.GetInt(HIGH_SCORE_KEY).ToString();
        currentScoreText.text = null;
        gameOverPanel.SetActive(true);
    }
    #endregion


}
