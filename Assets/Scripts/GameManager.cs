using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;


public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject playerPrefab;
   [SerializeField] private GameObject uiCanvasPrefab;
   [SerializeField] private GameObject uiMainTextPrefab;
   [SerializeField] private GameObject uiSubTextPrefab;
   [SerializeField] private Transform levelStartPoint;
   
   
   private Player _playerEntity;
   private Canvas _canvas;
   private TextMeshProUGUI _mainText;
   private TextMeshProUGUI _subText;
   
   public static GameManager Instance;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      _playerEntity = Instantiate(playerPrefab, levelStartPoint.position, levelStartPoint.rotation).GetComponent<Player>();

      _playerEntity.SetPlayerCheckpoint(levelStartPoint.position);
      _canvas = Instantiate(uiCanvasPrefab, Vector3.zero, levelStartPoint.rotation).GetComponent<Canvas>();
      _mainText = Instantiate(uiMainTextPrefab, _canvas.transform).GetComponent<TextMeshProUGUI>();
      _subText = Instantiate(uiSubTextPrefab, _canvas.transform).GetComponent<TextMeshProUGUI>();
   }

   public void SetUISubtext(string text)
   {
      _subText.text = text;
   }
   

   public IEnumerator waitForRespawn()
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
   }
}
