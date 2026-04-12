using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float initialVerticalSpeed = 4f;

    private Rigidbody2D _rb;
    private float _verticalVelocity;
    private bool _switchRequested;
    private const float InputBufferDuration = 0.08f;
    private float _inputBufferTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _verticalVelocity = initialVerticalSpeed;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            _inputBufferTime = InputBufferDuration;
            if (_inputBufferTime > 0f)
            {
                _inputBufferTime -= Time.deltaTime;
            }

            _switchRequested = true;
        }
    }

    private void FixedUpdate()
    {

        if (_inputBufferTime > 0f)
        {
            SwitchGravity();
            _switchRequested = false;
            _inputBufferTime = 0f;
        }

        float speed = GameManager.Instance.CurrentSpeed;
        _rb.velocity = new Vector2(speed, _verticalVelocity);
    }

    private void SwitchGravity()
    {
        _verticalVelocity = -_verticalVelocity;
        PlayerFeedback.Instance.PlaySwitchFeedback();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerFeedback.Instance.PlayDeathFeedback();
    }
}
