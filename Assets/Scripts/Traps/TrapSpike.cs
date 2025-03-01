using System;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerState>())
        {
            other.gameObject.GetComponent<PlayerState>().DamagePlayer(1);
        }
    }
}
