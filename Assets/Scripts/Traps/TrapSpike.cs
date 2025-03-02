using System;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            other.gameObject.GetComponent<Player>().DamagePlayer(1);
        }
    }
}
