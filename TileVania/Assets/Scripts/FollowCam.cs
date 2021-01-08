using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Player _player;

    // Start is called before the first frame update
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        var position = _player.transform.position;
        var newCamPos = new Vector2(position.x, position.y);
        var transform1 = transform;
        transform1.position = new Vector3(newCamPos.x, newCamPos.y, transform1.position.z);
    }
}
