using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().DamagePlayer(3);
        }
    }
}
