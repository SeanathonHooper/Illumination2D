using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    [SerializeField] private int spikeDamage = 1;
    private Dictionary<Player, float> _touchingCollider;

    private void Awake()
    {
        _touchingCollider = new Dictionary<Player, float>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && !_touchingCollider.ContainsKey(player))
        {
            _touchingCollider.Add(player, 0f);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _touchingCollider.Remove(player);
        }
    }

    private void Update()
    {
        foreach (var key in _touchingCollider.ToList())
        {
            if (key.Value <= 0)
            {
                if (key.Key.GetCurrentHealth() <= 1)
                {
                    _touchingCollider.Remove(key.Key);
                    key.Key.DamagePlayer(spikeDamage);
                    continue;
                }
                key.Key.DamagePlayer(spikeDamage);
                _touchingCollider[key.Key] = 1f;
            }
            else
            {
                _touchingCollider[key.Key] = key.Value - Time.deltaTime;
            }
        }
    }
}
