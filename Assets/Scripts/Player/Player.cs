using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] private Light2D _playerColor;

    private int _playerCurrentHealth;
    private int _playerMaxHealth = 2;
    private Vector3 _playerCheckpointLocation;
    
    // Possible health states
    public enum PlayerHealthState
    {
        Dead,
        Weak,
        Healthy
    }
    
    //Maps the PlayerHealthState to what color they should be
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

    //Pretty much just changes the player color, also calls the player
    //Death event
    public void SetPlayerState(PlayerHealthState newHealthState)
    {
        _playerHealthState = newHealthState;
        _playerColor.color = _healthStateColorLookup[_playerHealthState];
        if (newHealthState == PlayerHealthState.Dead)
        {
            notifyOnPlayerDeath();
        }
    }

    //Setting up player death event
    public delegate void PLayerDeath();
    public event PLayerDeath OnPlayerDeath;

    //Function that runs when the player dies
    //Tells the game manager to tell the camera to stop following the player basically
    private void notifyOnPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    //Re-enables player object to continue playing.
    public void RespawnPlayer()
    {
        MovePlayerToCheckpoint();
        _playerCurrentHealth = _playerMaxHealth;
        gameObject.SetActive(true);
        SetPlayerState(PlayerHealthState.Healthy);
    }
    //Updates which checkpoint the player is at
    public void SetPlayerCheckpoint(Vector3 checkpoint)
    {
        _playerCheckpointLocation = checkpoint;
    }
    //Move the player to their current checkpoint
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
