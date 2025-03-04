using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject playerPrefab;
   [SerializeField] private GameObject cameraPrefab;
   [SerializeField] private Transform levelStartPoint;

   private CinemachineCamera _levelCam;
   private Player _playerEntity;

   private void Awake()
   {
      _playerEntity = Instantiate(playerPrefab, levelStartPoint.position, levelStartPoint.rotation).GetComponent<Player>();
      _levelCam = Instantiate(cameraPrefab, new Vector3(0,0,-10), levelStartPoint.rotation).GetComponent<CinemachineCamera>();
      _levelCam.Follow = _playerEntity.transform;
      _playerEntity.SetPlayerCheckpoint(levelStartPoint.position);
      _playerEntity.OnPlayerDeath += PlayerEntityOnPlayerDeath;
   }

   private void OnDestroy()
   {
      //Just in case the game manager somehow gets destroyed (You probably have bigger problems at that point)
      _playerEntity.OnPlayerDeath -= PlayerEntityOnPlayerDeath;
   }

   private void PlayerEntityOnPlayerDeath()
   {
      _levelCam.Follow = null;
      StartCoroutine(_waitForRespawn());
   }

   IEnumerator _waitForRespawn()
   {
      yield return new WaitForSeconds(3.0f);
      _playerEntity.RespawnPlayer();
      _levelCam.Follow = _playerEntity.transform;
   }
}
