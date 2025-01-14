using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public bool isGameActive = false;

    public int score = 0;
    public int[] scoreThresholds = { 10000, 20000, 30000, 50000 };
    public float baseStepDelay = 1f; 
    public float stepDelayMultiplier = 0.8f; 

    public TMPro.TextMeshProUGUI scoreText;

    private float currentStepDelay;

    private void Start()
    {
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0f;

        currentStepDelay = baseStepDelay;
        UpdateUI();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        isGameActive = true;
        Time.timeScale = 1f;
    }

    public void GameOverScreen()
    {
        gameOverPanel.SetActive(true);
        isGameActive = false;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        isGameActive = false;
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void AddScore(int clearedLines)
    {
        if (clearedLines == 1)
            score += 100;
        else if (clearedLines == 2)
            score += 300;
        else if (clearedLines == 3)
            score += 500;
        else if (clearedLines >= 4)
            score += 800;

        UpdateDifficulty();  
        UpdateUI();  
    }

    private void UpdateDifficulty()
    {
        foreach (int threshold in scoreThresholds)
        {
            if (score >= threshold)
            {
                currentStepDelay *= stepDelayMultiplier;

                FindObjectOfType<Piece>().stepDelay = currentStepDelay;
            }
        }
    }

    private void UpdateUI()
    {
        Debug.Log($"Score: {score}");
        scoreText.text = "Score: " + score.ToString();
    }
}
