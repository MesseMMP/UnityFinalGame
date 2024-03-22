using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject playerPrefab; // Префаб игрока
    private GameObject _playerInstance; // Экземпляр игрока
    [SerializeField] public GameObject gameOverPanel; // Ссылка на панель проигрыша
    public float currentScore = 0;
    public float highScore = 0;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public TextMeshProUGUI endScoreText;

    private bool _gameEnded = false; // Флаг, указывающий, завершилась ли игра

    void Awake()
    {
        SpawnPlayer();
        highScore = PlayerPrefs.GetFloat("SaveScore");
    }

    void SpawnPlayer()
    {
        // Создаем игрока в начале карты
        _playerInstance = Instantiate(playerPrefab, new Vector3(0, 0, -13), Quaternion.identity);
    }

    public void EndGame()
    {
        if (_gameEnded) return;
        _gameEnded = true;
        endScoreText.text = "Your Score: " + (int)Math.Round(currentScore);
        // Показываем панель проигрыша
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Остановка времени в игре
    }

    // Метод для перезапуска игры
    public void RestartGame()
    {
        // Возвращаем Time.timeScale к его стандартному значению
        Time.timeScale = 1f;
        // Получаем название текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneName);
    }

    public void UpdateScore()
    {
        currentScore += Time.deltaTime; // Увеличиваем счетчик на 1 / 1000 каждый кадр
        scoreText.text = "Score: " + (int)Math.Round(currentScore);
        highScoreText.text = "HighScore: " + (int)Math.Round(highScore);
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if ((int)Math.Round(currentScore) <= (int)Math.Round(highScore)) return;
        highScore = currentScore;

        PlayerPrefs.SetFloat("SaveScore", highScore);
    }
}