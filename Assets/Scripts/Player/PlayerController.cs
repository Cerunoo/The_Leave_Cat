using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private ScriptableStats _stats;

    [Header("States")]
    public bool facingRight;
    public bool isMild;

    private FrameInput _frameInput;

    private Rigidbody2D _rb;

    private float _frameVelocity;

    #region Interface

    public Vector2 FrameDirection => new Vector2(_frameInput.Horizontal, _rb.linearVelocity.y);
    public bool IsMild => isMild;

    #endregion

    private void Awake()
    {
        Instance = this;

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        _frameInput = new FrameInput
        {
            Horizontal = inputHorizontal != 0 ? inputHorizontal : Mathf.Lerp(_frameInput.Horizontal, inputHorizontal, Time.fixedDeltaTime * 10f),
        };

        if (_stats.SnapInput)
        _frameInput.Horizontal = Mathf.Abs(_frameInput.Horizontal) < _stats.HorizontalDeadZoneThreshold ? 0 : _frameInput.Horizontal;
    }

    private void FixedUpdate()
    {
        HandleDirection();

        ApplyMovement();
    }

    #region Horizontal

    private void HandleDirection()
    {
        if (_frameInput.Horizontal == 0)
        {
            var deceleration = _stats.GroundDeceleration;
            _frameVelocity = Mathf.MoveTowards(_frameVelocity, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            float speed = !isMild ? _stats.NormalSpeed : _stats.MildSpeed;
            _frameVelocity = Mathf.MoveTowards(_frameVelocity, _frameInput.Horizontal * speed, _stats.Acceleration * Time.fixedDeltaTime);
        }

        if (_frameInput.Horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (_frameInput.Horizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;

        DangerNotify.Instance.Flip();
    }

    #endregion

    private void ApplyMovement() => _rb.linearVelocity = new Vector2(_frameVelocity, _rb.linearVelocity.y);
}

public struct FrameInput
{
    public float Horizontal;
}

public interface IPlayerController
{
    public Vector2 FrameDirection { get; }
    public bool IsMild { get; }
}