using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 0;
    [SerializeField] private float _sideMoveSize = 0;
    [SerializeField] private float _roadWidth = 2.25f;

    // Update is called once per frame
    void Update()
    {
        // Получаем текущее положение игрока
        Vector3 currentPosition = transform.position;

        // Игрок постоянно бежит вперед
        currentPosition += transform.forward * Time.deltaTime * _playerSpeed;

        // Движение влево при нажатии клавиши "A"
        if (Input.GetKey(KeyCode.A))
        {
            currentPosition.x -= _sideMoveSize * Time.deltaTime;
        }
        // Движение вправо при нажатии клавиши "D"
        else if (Input.GetKey(KeyCode.D))
        {
            currentPosition.x += _sideMoveSize * Time.deltaTime;
        }

        // Ограничиваем движения игрока в бок(чтобы не велетел за карту)

        currentPosition.x = Mathf.Clamp(currentPosition.x, -_roadWidth, _roadWidth);

        // Обновляем позицию игрока
        transform.position = currentPosition;
    }
}