using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineCamera _playerCam;
    private Player _playerToFollow;

    private void Awake()
    {
        _playerCam = GetComponent<CinemachineCamera>();
    }

    public void Setup(Player playerToFollow)
    {
        _playerToFollow = playerToFollow;
        playerToFollow.OnPlayerDeath += PlayerToFollowOnPlayerDeath;
        playerToFollow.OnPlayerRespawn += PlayerToFollowOnPlayerRespawn;
        PlayerToFollowOnPlayerRespawn();
    }

    private void OnDisable()
    {
        _playerToFollow.OnPlayerDeath -= PlayerToFollowOnPlayerDeath;
        _playerToFollow.OnPlayerRespawn -= PlayerToFollowOnPlayerRespawn;
    }

    private void PlayerToFollowOnPlayerRespawn()
    {
        _playerCam.Follow = _playerToFollow.transform;
    }

    private void PlayerToFollowOnPlayerDeath()
    {
        _playerCam.Follow = null;
    }
    
}
