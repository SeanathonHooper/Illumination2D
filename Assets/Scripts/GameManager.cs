using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject playerPrefab;
   [SerializeField] private GameObject cameraPrefab;
   [SerializeField] private GameObject uiCanvasPrefab;
   [SerializeField] private GameObject mainTextPrefab;
   [SerializeField] private Transform levelStartPoint;

   private CinemachineCamera _levelCam;
   private Player _playerEntity;
   private Canvas _canvas;
   private TextMeshProUGUI _mainText;

   private void Awake()
   {
      _playerEntity = Instantiate(playerPrefab, levelStartPoint.position, levelStartPoint.rotation).GetComponent<Player>();
      _levelCam = Instantiate(cameraPrefab, new Vector3(0,0,-10), levelStartPoint.rotation).GetComponent<CinemachineCamera>();
      _levelCam.Follow = _playerEntity.transform;
      _playerEntity.SetPlayerCheckpoint(levelStartPoint.position);
      _canvas = Instantiate(uiCanvasPrefab, Vector3.zero, levelStartPoint.rotation).GetComponent<Canvas>();
      _mainText = Instantiate(mainTextPrefab, _canvas.transform).GetComponent<TextMeshProUGUI>();
      _playerEntity.OnPlayerDeath += PlayerEntityOnPlayerDeath;
   }

   private void OnDestroy()
   {
      //Unsubscribes from player death event just in case the game manager somehow gets destroyed
      //(You probably have bigger problems at that point)
      _playerEntity.OnPlayerDeath -= PlayerEntityOnPlayerDeath;
   }

   private void PlayerEntityOnPlayerDeath()
   {
      _levelCam.Follow = null;
      StartCoroutine(_waitForRespawn());
   }

   IEnumerator _waitForRespawn()
   {
      int countdown = 0;
      while (countdown < 3)
      {
         _mainText.text = "RESPAWNING IN: " + (3 - countdown).ToString() + " SECONDS";
         countdown++;
         yield return new WaitForSeconds(1.0f);
      }
      _mainText.text = "";
      _playerEntity.RespawnPlayer();
      _levelCam.Follow = _playerEntity.transform;
   }
}
