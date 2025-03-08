using System;
using System.Collections;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    private bool _isPushed;
    private Vector3 _buttonPushDownSpeed = new Vector3(0f, 0.0008f, 0f);
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _isPushed = false;
    }

    private void Update()
    {
        if (!_isPushed)
        {
            TryPushButton();
        }
    }

    public delegate void ButtonPushed();
    public event ButtonPushed OnButtonPushedEvent;

    private void OnButtonPushed()
    {
        _isPushed = true;
        StartCoroutine(_buttonPushedColorTransition());
        OnButtonPushedEvent?.Invoke();
    }

    IEnumerator _buttonPushedColorTransition()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            _spriteRenderer.color = Color.Lerp(Color.red, Color.green, elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        _spriteRenderer.color = Color.green;
    }

    private void TryPushButton()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.2f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.up * 1.2f, Color.green);
        
        if (hit && !_isPushed)
        {
            if (transform.localPosition.y > 0.35f && !_isPushed)
            {
                transform.localPosition -= _buttonPushDownSpeed;
            }
            else if (transform.localPosition.y <= 0.35f)
            {
                OnButtonPushed();
            }
        }
        else if (!hit && !_isPushed)
        { 
            if (transform.localPosition.y < 0.6f && !_isPushed)
            {
                transform.localPosition += _buttonPushDownSpeed;
                
            }
        }
    }
}
