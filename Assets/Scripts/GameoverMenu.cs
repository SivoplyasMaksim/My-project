using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu Instance { get; private set; }

    [Header("UI Элементы")]
    [SerializeField] private GameObject menuPanel; // Панель меню
    [SerializeField] private TextMeshProUGUI finalScoreText; // Текст с итоговым счётом
    [SerializeField] private Button restartButton; // Кнопка рестарта
    [SerializeField] private Button quitButton; // Кнопка выхода

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

        // Скрываем меню при старте
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        // Назначаем обработчики кнопок
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    public void ShowMenu()
    {

        // ✅ Показываем панель
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
            Debug.Log("Панель активирована");
        }
        else
        {
            Debug.LogError("menuPanel НЕ НАЗНАЧЕН!");
        }

        // ✅ Показываем текст счёта
        if (finalScoreText != null && ScoreManager.Instance != null)
        {
            finalScoreText.text = $"Итоговый счёт: {ScoreManager.Instance.GetScore()}";
            finalScoreText.gameObject.SetActive(true);
            Debug.Log("Текст счёта обновлён");
        }

        // ✅ ПРИНУДИТЕЛЬНО показываем кнопки
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
            restartButton.interactable = true;
            Debug.Log("Кнопка Restart активирована");
        }
        else
        {
            Debug.LogError("restartButton НЕ НАЗНАЧЕН!");
        }

        if (quitButton != null)
        {
            quitButton.gameObject.SetActive(true);
            quitButton.interactable = true;
            Debug.Log("Кнопка Quit активирована");
        }
        else
        {
            Debug.LogError("quitButton НЕ НАЗНАЧЕН!");
        }
    }

    private void RestartGame()
    {
        // Возвращаем время
        Time.timeScale = 1f;

        // Перезагружаем сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        // В редакторе Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // В сборке
        Application.Quit();
#endif
    }
}