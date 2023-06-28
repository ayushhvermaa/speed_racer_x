using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text stageText;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    private float highScore = 0;
    private float score = 0;
    public GameObject heartContainer;
    public GameObject heartPrefab;
    private int currentLives = 3;
    private int currentStage = 1;
    private float startTime;
    private float stageDuration = 15f;
    private float nextStageTime;
    private bool gameRunning = true; 
    private void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        startTime = Time.time;
        nextStageTime = startTime + stageDuration;
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        CreateHearts();
        UpdateStageText();
    
    }

    private void Update()
    {
        if (gameRunning)
        {
            updateTime();
            updateScoreText();
            updateHighScore();
            updateHighScoreText();

            if (Time.time >= nextStageTime)
            {
                currentStage++;
                nextStageTime += stageDuration;
                UpdateStageText();
                UpdateStageDifficulty();
            }
        }
    }
    private void updateTime()
    {
        float goTime = Time.time - startTime;
        int minutes = (int)(goTime / 60);
        int seconds = (int)(goTime % 60);
        int milliseconds = (int)((goTime * 1000) % 1000);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
    public void updateScore() {
        score = score + 1;
    }
    private void updateScoreText() {
        scoreText.text = "Score: " + score;
    }
    private void updateHighScoreText() {
        highScoreText.text = "High Score: " + highScore;
    }
    private void updateHighScore() {
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);

        }
    }

    private void CreateHearts()
    {
        for (int i = 0; i < currentLives; i++)
        {
            Instantiate(heartPrefab, heartContainer.transform);
        }
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            Destroy(heartContainer.transform.GetChild(currentLives).gameObject);

            if (currentLives == 0)
            {
                StopGame();
            }
        }
    }
    private void StopGame()
    {
        gameRunning = false;
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 1f;
        updateHighScore();
        updateHighScoreText();
    }

    private void UpdateStageText()
    {
        stageText.text = "Stage: " + currentStage;
    }

    private void UpdateStageDifficulty()
    {
    float scrollSpeedIncrease = 0.2f;
    float maxScrollSpeed = 3f;
    float playerSpeedIncrease = 2f;
    float playerSpeedLimit = 40f;
    float blockerVolumeChange = 0.2f;
    float blockerLimit = 0.4f;
    RoadMove roadMove = FindObjectOfType<RoadMove>();
    PlayerController playerController = FindObjectOfType<PlayerController>();
    BlockerManager blockerManager = FindObjectOfType<BlockerManager>();
    blockerManager.UpdateSpawnVolume(blockerVolumeChange, blockerLimit);
    roadMove.UpdateScrollSpeed(scrollSpeedIncrease, maxScrollSpeed);
    playerController.UpdatePlayerSpeed(playerSpeedIncrease, playerSpeedLimit);
    }
}