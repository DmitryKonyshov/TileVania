using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    private Rigidbody2D _myRigidBody;
    
    private void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        FlipSprite();
    }
    
    private void Run()
    {
        var controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var playerVelocity = new Vector2(controlThrow * runSpeed, _myRigidBody.velocity.y);
        _myRigidBody.velocity = playerVelocity;
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
