using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;

    void LateUpdate()
    {
        // Камера следует за игроком
        transform.position = _player.position;
    }
}