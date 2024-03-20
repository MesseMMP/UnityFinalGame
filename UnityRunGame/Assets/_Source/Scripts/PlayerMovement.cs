using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _playerSpeed = 0;
    [SerializeField] private Lane _currentLane;
    [SerializeField] private float _laneWidth;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody _rigidbody; // Ссылка на компонент Rigidbody
    [SerializeField] private bool _isOnRoad;

    void Start()
    {
        _playerAnimator.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        // Получаем текущее положение игрока
        Vector3 currentPosition = transform.position;

        // Игрок постоянно бежит вперед
        currentPosition += transform.forward * Time.deltaTime * _playerSpeed;

        // Движение влево при нажатии клавиши "A"
        if (Input.GetKeyDown(KeyCode.A) && _currentLane is Lane.Middle or Lane.Right)
        {
            _currentLane -= 1;
            currentPosition += Vector3.left * _laneWidth;
        }

        // Движение вправо при нажатии клавиши "D"
        if (Input.GetKeyDown(KeyCode.D) && _currentLane is Lane.Left or Lane.Middle)
        {
            _currentLane += 1;
            currentPosition += Vector3.right * _laneWidth;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isOnRoad)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isOnRoad = false;
            _playerAnimator.SetBool("isJumping", true);
        }

        // Обновляем позицию игрока
        transform.position = currentPosition;
    }

    // Проверка на столкновение с дорогой
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            _isOnRoad = true;
            _playerAnimator.SetBool("isJumping", false);
        }
    }
}