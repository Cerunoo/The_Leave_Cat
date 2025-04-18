using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _maxTilt = 5;
    [SerializeField] private float _tiltSpeed = 20;

    // [Header("Particles")]
    // [SerializeField] private ParticleSystem _moveParticles;

    // [Header("Audio Clips"), SerializeField]
    // private AudioClip[] _footsteps;

    private AudioSource sz;

    private Animator _anim;
    // private AudioSource _source;
    private IPlayerController _player;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        // _source = GetComponent<AudioSource>();
        _player = GetComponentInParent<IPlayerController>();

        sz = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleMove();
        HandleCharacterTilt();
    }

    public bool play = false;
    private void HandleMove()
    {
        var inputStrength = Mathf.Abs(_player.FrameDirection.x);
        _anim.SetBool(isRunningKey, !_player.IsMild ? inputStrength > 0 : false);
        _anim.SetBool(isMildKey, _player.IsMild ? inputStrength > 0 : false);
        // _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);

        if (inputStrength != 0 && !play)
        {
            sz.Play();
            play = true;
        }
        if (inputStrength == 0 && play)
        {
            sz.Stop();
            play = false;
        }
    }

    private void HandleCharacterTilt()
    {
        var runningTilt = Quaternion.Euler(0, 0, _maxTilt * _player.FrameDirection.x);
        _anim.transform.up = Vector3.RotateTowards(_anim.transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
    }

    private static readonly int isRunningKey = Animator.StringToHash("isRunning");
    private static readonly int isMildKey = Animator.StringToHash("isMild");
}