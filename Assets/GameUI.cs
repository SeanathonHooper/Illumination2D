using System.Collections;
using TMPro;
using UnityEngine;
public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI subText;
    private Player _player;

    public void Setup(Player player)
    {
        _player = player;
        player.OnPlayerDeath += PlayerOnPlayerDeath;
    }

    private void OnDisable()
    {
        _player.OnPlayerDeath -= PlayerOnPlayerDeath;
    }


    private void PlayerOnPlayerDeath()
    {
        StartCoroutine(RespawnTimer());
    }
    
    public IEnumerator RespawnTimer()
    {
        int countdown = 0;
        while (countdown < 3)
        {
            mainText.text = "RESPAWNING IN: " + (3 - countdown).ToString() + " SECONDS";
            countdown++;
            yield return new WaitForSeconds(1.0f);
        }
        mainText.text = "";
    }
}
