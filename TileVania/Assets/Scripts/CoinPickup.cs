using UnityEngine;
using UnityEngine.Serialization;

public class CoinPickup : MonoBehaviour
{
    [FormerlySerializedAs("coinPickUpSFX")] [SerializeField] private AudioClip coinPickUpSfx;
    [SerializeField] private int pointsForCoinPickUp = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickUp);
        if (!(Camera.main is null)) AudioSource.PlayClipAtPoint(coinPickUpSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
