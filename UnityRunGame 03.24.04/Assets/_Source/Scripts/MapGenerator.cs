using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<GameObject> _mapPrefabs;
    private float _spawnPosition = 0f;
    private int _numberMaps = 3;
    private float _mapLength = 30;
    private List<GameObject> _activeMaps = new();


    // Генерируем сразу карту из нескольких участков
    private void Start()
    {
        for (int i = 0; i < _numberMaps; i++)
        {
            SpawnMap(Random.Range(0, _mapPrefabs.Count));
        }
    }

    // Если дошли до конца карты, добавляем новый кусок карты
    private void Update()
    {
        if (!(_playerTransform.position.z - _mapLength - 5 > _spawnPosition - _numberMaps * _mapLength)) return;
        SpawnMap(Random.Range(0, _mapPrefabs.Count));
        // Чтобы не забивать сцену кучей объектов, удаляем пройденные участки карты
        DeleteMap();
    }

    private void SpawnMap(int mapIndex)
    {
        GameObject newMap = Instantiate(_mapPrefabs[mapIndex], transform.forward * _spawnPosition, transform.rotation);
        _activeMaps.Add(newMap);
        // Новые участки карты спавнятся после созданных
        _spawnPosition += _mapLength;
    }

    private void DeleteMap()
    {
        if (_activeMaps.Count != 0)
        {
            Destroy(_activeMaps[0]);
            _activeMaps.RemoveAt(0);
        }
    }
}