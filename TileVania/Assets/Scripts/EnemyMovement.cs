using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D _myRigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _myRigidBody.velocity = IsFacingRight() ? new Vector2(moveSpeed, 0f) : new Vector2(-moveSpeed, 0f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_myRigidBody.velocity.x)), 1f);
    }
}
