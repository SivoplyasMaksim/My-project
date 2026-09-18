using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private int highScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // ✅ Загружаем рекорд из памяти
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Очки: {currentScore}";
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    // ✅ Вызывается при смерти игрока
    public void SaveHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log($"🏆 Новый рекорд: {highScore}");
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }
}