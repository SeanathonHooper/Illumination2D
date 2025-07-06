using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Light2D playerColor;
    [SerializeField] private bool isInvulnerable = false;
    [SerializeField] private GameObject cameraPrefab;
    
    private int _playerCurrentHealth;
    private int _playerMaxHealth = 2;
    
    private Vector3 _playerCheckpointLocation;
    
    private CinemachineCamera _levelCam;
    
    
    //Maps the PlayerHealthState to what color they should be
    private Dictionary<int, Color> _playerHealthColorLook = new Dictionary<int, Color>
    {
        { 0, Color.black },
        { 1, Color.cyan},
        { 2, Color.magenta }
    };
    
    public void DamagePlayer(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }
        
        _playerCurrentHealth -= damage;
        
        if (_playerCurrentHealth <= 0)
        {
            playerColor.color = _playerHealthColorLook[0];
            notifyOnPlayerDeath();
        }
        else if (_playerCurrentHealth >= 2)
        {
            playerColor.color = _playerHealthColorLook[2];
        }
        else
        {
            playerColor.color = _playerHealthColorLook[1];
        }
    }

    //Function that runs when the player dies
    //Tells the game manager to start the respawn timer
    private void notifyOnPlayerDeath()
    {
        _levelCam.Follow = null;
        gameObject.SetActive(false);
        GameManager.Instance.StartCoroutine(GameManager.Instance.waitForRespawn());
    }

    //Re-enables player object to continue playing.
    public void RespawnPlayer()
    {
        MovePlayerToCheckpoint();
        _playerCurrentHealth = _playerMaxHealth;
        gameObject.SetActive(true);
        playerColor.color = _playerHealthColorLook[_playerCurrentHealth];
        _levelCam.Follow = transform;
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
            _playerCurrentHealth = _playerMaxHealth;
            playerColor.color = _playerHealthColorLook[_playerCurrentHealth];
            _levelCam = Instantiate(cameraPrefab, transform.position, transform.rotation).GetComponent<CinemachineCamera>();
            _levelCam.Follow = transform;
    }


}
