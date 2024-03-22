using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject playerPrefab; // Префаб игрока
    private GameObject _playerInstance; // Экземпляр игрока
    [SerializeField] public GameObject gameOverPanel; // Ссылка на панель проигрыша
    private ulong _currentScore = 0;
    [SerializeField] public TextMeshProUGUI score;
    [SerializeField] public TextMeshProUGUI highScore;
    [SerializeField] public TextMeshProUGUI endScore;

    private bool _gameEnded = false; // Флаг, указывающий, завершилась ли игра

    void Awake()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        // Создаем игрока в начале карты
        _playerInstance = Instantiate(playerPrefab, new Vector3(0, 0, -13), Quaternion.identity);
    }

    public void EndGame()
    {
        if (!_gameEnded)
        {
            _gameEnded = true;
            endScore.text = "Your Score: " + _currentScore / 1000;
            // Показываем панель проигрыша
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Остановка времени в игре
        }
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
        ++_currentScore;
        score.text = "Score: " + _currentScore / 1000;
    }
}