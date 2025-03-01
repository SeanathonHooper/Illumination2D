using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private Light2D _playerColor;

    private int _playerCurrentHealth;
    private int _playerMaxHealth = 2;

    
    
    public enum PlayerHealthState
    {
        Dead,
        Weak,
        Healthy
    }

    private Dictionary<PlayerHealthState, Color> _healthStateColorLookup = new Dictionary<PlayerHealthState, Color>();
    private PlayerHealthState _playerHealthState;
    
    public void DamagePlayer(int damage)
    {
        _playerCurrentHealth -= damage;
        if (_playerCurrentHealth <= 0)
        {
            SetPlayerState(PlayerHealthState.Dead);
        }
        else if (_playerCurrentHealth == 1)
        {
            SetPlayerState(PlayerHealthState.Weak);
        }
        else
        {
            SetPlayerState(PlayerHealthState.Healthy);
        }
    }

    private void SetPlayerState(PlayerHealthState newHealthState)
    {
        _playerHealthState = newHealthState;
        _playerColor.color = _healthStateColorLookup[_playerHealthState];
    }

    private void Awake()
    {
        _healthStateColorLookup.Add(PlayerHealthState.Dead, Color.red);
        _healthStateColorLookup.Add(PlayerHealthState.Weak, Color.yellow);
        _healthStateColorLookup.Add(PlayerHealthState.Healthy, Color.white);
        _playerCurrentHealth = _playerMaxHealth;
        _playerHealthState = PlayerHealthState.Healthy;
        _playerColor.color = _healthStateColorLookup[_playerHealthState];
    }
}
