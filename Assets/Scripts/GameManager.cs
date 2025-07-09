using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject playerPrefab;
   [SerializeField] private GameObject uiCanvasPrefab;
   [SerializeField] private Transform levelStartPoint;
   [SerializeField] private GameObject cameraPrefab;

   private Player _playerEntity;
   Canvas _canvas;

   
   public static GameManager Instance;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      
      _playerEntity = Instantiate(playerPrefab, levelStartPoint.position, levelStartPoint.rotation).GetComponent<Player>();
      _playerEntity.SetPlayerCheckpoint(levelStartPoint.position);
      Instantiate(cameraPrefab, transform.position, transform.rotation).GetComponent<PlayerCamera>().Setup(_playerEntity);
      Instantiate(uiCanvasPrefab, Vector3.zero, levelStartPoint.rotation).GetComponent<GameUI>().Setup(_playerEntity);

   }

   public void OnEnable()
   {
      _playerEntity.OnPlayerDeath += PlayerEntityOnPlayerDeath;

   }
   
   public void OnDisable()
   {
      _playerEntity.OnPlayerDeath -= PlayerEntityOnPlayerDeath;
   }

   void PlayerEntityOnPlayerDeath()
   {
      StartCoroutine(WaitForRespawn());
   }
   
   
   public IEnumerator WaitForRespawn()
   {
      yield return new WaitForSeconds(3);
      _playerEntity.RespawnPlayer();
   }
}
