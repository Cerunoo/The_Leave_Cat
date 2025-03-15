using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // [Header("Particles")]
    // [SerializeField] private ParticleSystem _moveParticles;

    // [Header("Audio Clips"), SerializeField]
    // private AudioClip[] _footsteps;

    private Animator _anim;
    // private AudioSource _source;
    private IPlayerController _player;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        // _source = GetComponent<AudioSource>();
        _player = GetComponentInParent<IPlayerController>();
    }

    private void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        var inputStrength = Mathf.Abs(_player.FrameDirection.x);
        _anim.SetBool(isRunningKey, !_player.IsMild ? inputStrength > 0 : false);
        _anim.SetBool(isMildKey, _player.IsMild ? inputStrength > 0 : false);
        // _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);
    }

    private static readonly int isRunningKey = Animator.StringToHash("isRunning");
    private static readonly int isMildKey = Animator.StringToHash("isMild");
}