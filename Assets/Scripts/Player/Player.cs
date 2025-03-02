using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Light2D _playerColor;

    private int _playerCurrentHealth;
    private int _playerMaxHealth = 2;
    private Vector3 _playerCheckpointLocation;
    
    public enum PlayerHealthState
    {
        Dead,
        Weak,
        Healthy
    }

    private Dictionary<PlayerHealthState, Color> _healthStateColorLookup = new Dictionary<PlayerHealthState, Color>
    {
        { PlayerHealthState.Dead, Color.black },
        { PlayerHealthState.Weak, Color.white },
        { PlayerHealthState.Healthy, new Color(1, .37254f, .85882f) }
    };

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

    public void SetPlayerCheckpoint(Vector3 checkpoint)
    {
        _playerCheckpointLocation = checkpoint;
    }

    public void MovePlayerToCheckpoint()
    {
        transform.position = _playerCheckpointLocation;
    }

    private void Awake()
    {
            _playerCurrentHealth = _playerMaxHealth;
            _playerHealthState = PlayerHealthState.Healthy;
            _playerColor.color = _healthStateColorLookup[_playerHealthState];
    }


}
