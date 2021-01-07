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
    }
    
    private void Run()
    {
        var controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var playerVelocity = new Vector2(controlThrow * runSpeed, _myRigidBody.velocity.y);
        _myRigidBody.velocity = playerVelocity;
    }
}
