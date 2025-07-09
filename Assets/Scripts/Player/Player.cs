using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isInvulnerable = false;
    private bool _isDead = false;
    private int _playerCurrentHealth;
    private const int PlayerMaxHealth = 2;
    
    private Vector3 _playerCheckpointLocation;
    


    public delegate void DamagePlayerHandler(int currentHealth);
    public event DamagePlayerHandler OnDamagePlayer;
    
    public void DamagePlayer(int damage)
    {
        if (_isInvulnerable || _isDead)
        {
            return;
        }
        
        _playerCurrentHealth -= damage;
        if (_playerCurrentHealth <= 0)
        {
            notifyOnPlayerDeath();
        }
        OnDamagePlayer?.Invoke(_playerCurrentHealth);
        
    }

    //Function that runs when the player dies
    //Tells the game manager to start the respawn timer
    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler OnPlayerDeath;
    private void notifyOnPlayerDeath()
    {
        _isDead = true;
        OnPlayerDeath?.Invoke();
        GameManager.Instance.StartCoroutine(GameManager.Instance.WaitForRespawn());
    }

    public delegate void PlayerRespawnHandler();
    public event PlayerRespawnHandler OnPlayerRespawn;

    //Re-enables player object to continue playing.
    public void RespawnPlayer()
    {
        _isDead = false;
        MovePlayerToCheckpoint();
        OnPlayerRespawn?.Invoke();
        _playerCurrentHealth = PlayerMaxHealth;
        
    }
    //Updates which checkpoint the player is at
    public void SetPlayerCheckpoint(Vector3 checkpoint)
    {
        _playerCheckpointLocation = checkpoint;
    }
    //Move the player to their current checkpoint
    private void MovePlayerToCheckpoint()
    {
        transform.position = _playerCheckpointLocation;
    }
    

    public int GetCurrentHealth()
    {
        return _playerCurrentHealth;
    }

    private void Awake()
    {
        _playerCurrentHealth = PlayerMaxHealth;
        
    }

}
