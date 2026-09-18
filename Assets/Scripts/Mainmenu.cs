using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    [Header("UI Элементы")]
    [SerializeField] private TextMeshProUGUI highScoreText; // Текст рекорда
    [SerializeField] private Button startButton;            // Кнопка Старт
    [SerializeField] private Button quitButton;             // Кнопка Выход

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

        // Назначаем обработчики кнопок
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }

        // Показываем рекорд
        UpdateHighScoreText();
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = $"Рекорд: {highScore}";
        }
    }

    private void StartGame()
    {
        Debug.Log("Запуск игры...");

        // ✅ Возвращаем время (на случай если было остановлено)
        Time.timeScale = 1f;

        // Загружаем игровую сцену
        // Замените "SampleScene" на название вашей игровой сцен
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGame()
    {
        Debug.Log("Выход из игры...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}