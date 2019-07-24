using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    #region UI Seerializefield
    public GameObject game;
    [SerializeField]
    public GameObject startPanel;
    [SerializeField]
    public GameObject gameOverPanel;
    [SerializeField]
    public GameObject gamePanel;
    [SerializeField]
    public Text UIbestScore;
    [SerializeField]
    public GameObject instructionPanel;
    [SerializeField]
    public GameObject isExitPanel;
    #endregion

    #region Animator SerializeFeild
    [SerializeField]
    private Animator startPanelAnimator;
    #endregion

    private  bool isGameStart = false;
    public float score { get; private set; }
     
    private int maxScore = 0;
    private float scoreForPowerCube = 1f;
    private float scoreForPowerDestruction = 2f;
    private float scoreForMeteor = 1f;
    private bool isExitPanelActive = false;

    private void Start()
    {
        game.SetActive(false);
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        instructionPanel.SetActive(false);
        isExitPanel.SetActive(false);
        isExitPanelActive = false;
        maxScore = PlayerPrefs.GetInt("best", 0);
        UIbestScore.text = "Best : " + maxScore;
    }
    private void OnEnable()
    {
        PowerCube.UpdateCollectedScore += UpdateCollectibleScore;
        Meteor.UpdateMeteorScore += UpdateMeteorScore;
        CollisionDetection.OnPoweredUpCollision += UpdateDestructibleScore;
    }
    private void OnDisable()
    {
        PowerCube.UpdateCollectedScore -= UpdateCollectibleScore;
        Meteor.UpdateMeteorScore -= UpdateMeteorScore;
        CollisionDetection.OnPoweredUpCollision -= UpdateDestructibleScore;
    }

    private void Update()
    {
        if (isGameStart)
        {
            UpdateRegularScore();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !isExitPanelActive && isGameStart)
        {
            Debug.Log("escape");
            isExitPanelActive = true;
            isExitPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isExitPanelActive && isGameStart)
        {
            Debug.Log("No escape");
            OnNo();
        }
    }
    #region UIUpdater
    private void UpdateRegularScore()
    {
        score += Time.deltaTime;
        if (score > maxScore)
        {
            maxScore = (int)score;
            UIbestScore.text = "Best : " + maxScore;
            PlayerPrefs.SetInt("best", maxScore);
        }
    }
    private void UpdateMeteorScore()
    {
        //add pop ups effect on score;
        if(isGameStart)
        {
            score += scoreForMeteor;
        }        
    }
    private void UpdateDestructibleScore()
    {
        //add pop ups effect on score;
        if(isGameStart)
        {
            score += scoreForPowerDestruction;
        }
    }
    private void UpdateCollectibleScore()
    {
        //add pop ups effect on score;
       if(isGameStart)
        {
            score += scoreForPowerCube;
        }
    }
    #endregion

    #region PanelManager
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        isGameStart = false;
    }
    public void OnStart()
    {
        startPanelAnimator.SetTrigger("startPanelActive");
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        game.SetActive(true);
        isGameStart = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void OnTutorial()
    {
        instructionPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void OnBack()
    {
        instructionPanel.SetActive(false);
        startPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        Time.timeScale = 1;
        isGameStart = false;
        game.SetActive(false);
    }

    public void OnYes()
    {
        Application.Quit();
    }
    public void OnNo()
    {
        isExitPanelActive = false;
        Time.timeScale = 1;
        isExitPanel.SetActive(false);
    }

    public void OnQuit()
    {
        startPanelAnimator.SetTrigger("startPanelActive");
        Application.Quit();
    }
    #endregion
}
