using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject _player;

    void LateUpdate()
    {
        // Находим объект игрока на сцене
        _player = GameObject.FindGameObjectWithTag("PlayerTag");

        // Проверяем, что ссылка на игрока не пуста
        if (_player != null)
        {
            // Камера следует за игроком
            transform.position = _player.transform.position;
        }
    }
}