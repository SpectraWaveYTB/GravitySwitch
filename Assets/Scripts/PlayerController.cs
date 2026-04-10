using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float initialVerticalSpeed = 4f;

    private Rigidbody2D _rb;
    private float _verticalVelocity;
    private bool _switchRequested;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _verticalVelocity = initialVerticalSpeed;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            _switchRequested = true;
        }
    }

    private void FixedUpdate()
    {
        float speed = GameManager.Instance.CurrentSpeed;
        if (_switchRequested)
        {
            SwitchGravity();
            _switchRequested = false;
            _rb.velocity = new Vector2(speed, _verticalVelocity);
        }
    }

    private void SwitchGravity()
    {
        _verticalVelocity = -_verticalVelocity;
        PlayerFeedback.Instance.PlaySwitchFeedback();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.RestartGame();
        PlayerFeedback.Instance.PlayDeathFeedback();
    }
}
