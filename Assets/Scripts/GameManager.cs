using Unity.Cinemachine;
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
   }
}
