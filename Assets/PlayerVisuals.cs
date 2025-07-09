using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class PlayerVisuals : MonoBehaviour
{
    
    private Player _parentScript;
    private Light2D _playerColor;
    private ParticleSystem _bloodParticles;
    private Dictionary<int, Color> _playerHealthColorTable = new Dictionary<int, Color>
    {
        { 0, Color.black},
        { 1, Color.cyan},
        { 2, Color.magenta }
    };

    private void Awake()
    {
        _parentScript = transform.parent.GetComponent<Player>();
        _playerColor = GetComponent<Light2D>();
        _playerColor.color = _playerHealthColorTable[2];
        _bloodParticles = GetComponent<ParticleSystem>();
    }
    
    private void OnEnable()
    {
        _parentScript.OnDamagePlayer += OnDamagePlayerVisuals;
        _parentScript.OnPlayerRespawn += OnPlayerRespawnVisuals;

    }
    private void OnDisable()
    {
        _parentScript.OnDamagePlayer -= OnDamagePlayerVisuals;
        _parentScript.OnPlayerRespawn -= OnPlayerRespawnVisuals;
    }

    void OnPlayerRespawnVisuals()
    {
        _playerColor.color = _playerHealthColorTable[2];
    }

    void OnDamagePlayerVisuals(int currenthealth)
    {
        if (currenthealth <= 0)
        {
            _playerColor.color = _playerHealthColorTable[0];
        }
        else if (currenthealth == 1)
        {
            _playerColor.color = _playerHealthColorTable[1];
        }
        else
        {
            _playerColor.color = _playerHealthColorTable[2];
        }
        _bloodParticles.transform.position = transform.position;
        _bloodParticles.Play();
    }


}
