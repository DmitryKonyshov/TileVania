using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] private float runSpeed = 5f;
    
    //State

    //Cached component reference
    private Rigidbody2D _myRigidBody;
    private Animator _myAnimator;
    private static readonly int Running = Animator.StringToHash("Running");

    //Message then methods
    private void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        FlipSprite();
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

    private void FlipSprite()
    {
        var playerHasHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.y) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_myRigidBody.velocity.x), 1f);
        }
    }
}
