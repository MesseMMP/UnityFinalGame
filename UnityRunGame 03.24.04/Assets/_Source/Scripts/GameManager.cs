using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab; // Префаб игрока
    private GameObject _playerInstance; // Экземпляр игрока

    void Awake()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        // Создаем игрока в начальной позиции (0, 0, 0)
        _playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }
}