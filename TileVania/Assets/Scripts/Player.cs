using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Vector2 deathKick = new Vector2(12f, 12f);
    
    //State
    private bool _isAlive = true;

    //Cached component reference
    private Rigidbody2D _myRigidBody;
    private Animator _myAnimator;
    private CapsuleCollider2D _myBodyCollider2D;
    private BoxCollider2D _myFeet;
    private float _gravityScaleAtStart;
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Climbing = Animator.StringToHash("Climbing");
    private static readonly int Dying = Animator.StringToHash("Dying");

    //Message then methods
    private void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        _myFeet = GetComponent<BoxCollider2D>();
        _gravityScaleAtStart = _myRigidBody.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isAlive) return;
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void Run()
    {
        var controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var velocity = _myRigidBody.velocity;
        var playerVelocity = new Vector2(controlThrow * runSpeed, velocity.y);
        velocity = playerVelocity;
        _myRigidBody.velocity = velocity;

        var playerHasHorizontalSpeed = Mathf.Abs(velocity.y) > Mathf.Epsilon;
        _myAnimator.SetBool(Running, playerHasHorizontalSpeed);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ClimbLadder()
    {
        // ReSharper disable once HeapView.ObjectAllocation
        if (_myFeet.IsTouchingLayers(LayerMask.GetMask($"Climbing")))
        {
            var controlTrow = CrossPlatformInputManager.GetAxis("Vertical");
            var velocity = _myRigidBody.velocity;
            var climbVelocity = new Vector2(velocity.x, controlTrow * climbSpeed);
            velocity = climbVelocity;
            _myRigidBody.velocity = velocity;
            _myRigidBody.gravityScale = 0f;
            var playerHasVerticalSpeed = Mathf.Abs(velocity.y) > Mathf.Epsilon;
            _myAnimator.SetBool(Climbing, playerHasVerticalSpeed);
        }
        else
        {
            _myAnimator.SetBool(Climbing, false);
            _myRigidBody.gravityScale = _gravityScaleAtStart;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Jump()
    {
        // ReSharper disable once HeapView.ObjectAllocation
        if (!_myFeet.IsTouchingLayers(LayerMask.GetMask($"Ground"))) return;
        if (!CrossPlatformInputManager.GetButtonDown("Jump")) return;
        var jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
        _myRigidBody.velocity += jumpVelocityToAdd;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Die()
    {
        // ReSharper disable once HeapView.ObjectAllocation
        if (!_myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", $"Hazards"))) return;
        _isAlive = false;
        _myAnimator.SetTrigger(Dying);
        GetComponent<Rigidbody2D>().velocity = deathKick;
    }

    private void FlipSprite()
    {
        var playerHasHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.y) > Mathf.Epsilon;
        if (!playerHasHorizontalSpeed) return;
        transform.localScale = new Vector2(Mathf.Sign(_myRigidBody.velocity.x), 1f);
    }
}
