using UnityEngine;
using UnityEngine.Serialization;

public class CoinPickup : MonoBehaviour
{
    [FormerlySerializedAs("coinPickUpSFX")] [SerializeField] private AudioClip coinPickUpSfx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(Camera.main is null)) AudioSource.PlayClipAtPoint(coinPickUpSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
