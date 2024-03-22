using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private AnimationClip _turnAnimationClip;
    [SerializeField] private float _playerSpeed = 0;
    [SerializeField] private Lane _currentLane;
    [SerializeField] private float _laneWidth;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody _rigidbody; // Ссылка на компонент Rigidbody
    private bool _isOnRoad = true;
    private bool _hasPlayedTurnAnimation = false;
    
    void Start()
    {
        // Начать с анимации разворота
        StartCoroutine(StartRunningAfterDelay(_turnAnimationClip.length)); // Запускаем корутину с задержкой на длительность анимации разворота
    }

    IEnumerator StartRunningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ждем указанное количество времени (длительность анимации разворота)
        _playerAnimator.SetBool("isRunning", true);
        _hasPlayedTurnAnimation = true;
    }

    
    void Update()
    {

        // Проверяем, была ли воспроизведена анимация разворота
        if (_hasPlayedTurnAnimation)
        {
            // Получаем текущее положение игрока
            Vector3 currentPosition = transform.position;

            // Игрок бежит вперед
            currentPosition += transform.forward * Time.deltaTime * _playerSpeed;

            // Движение влево при нажатии клавиши "A"
            if (Input.GetKeyDown(KeyCode.A) && (_currentLane == Lane.Middle || _currentLane == Lane.Right))
            {
                _currentLane -= 1;
                currentPosition += Vector3.left * _laneWidth;
            }

            // Движение вправо при нажатии клавиши "D"
            if (Input.GetKeyDown(KeyCode.D) && (_currentLane == Lane.Left || _currentLane == Lane.Middle))
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

    // Метод для проверки, проигрывается ли в данный момент заданная анимация
    private bool IsAnimationPlaying(string animationName)
    {
        // Получаем информацию о текущем состоянии аниматора
        AnimatorStateInfo stateInfo = _playerAnimator.GetCurrentAnimatorStateInfo(0);

        // Проверяем, проигрывается ли анимация с заданным именем
        return stateInfo.IsName(animationName);
    }
}