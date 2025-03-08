using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PushButton : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private Light2D _light2D;
    
    private Vector3 _buttonDownPosition = new Vector3(0, 0.35f, 0);
    private Vector3 _buttonUpPosition = new Vector3(0, 0.6f, 0);
    private float _pressSpeed = 0.5f;
    private bool _isPushed;

    public enum PushButtonType
    {
        Player,
        PhysicsObjects
    }


    
    [SerializeField] PushButtonType _buttonType = PushButtonType.Player;
    
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
            _light2D.color = Color.Lerp(Color.red, Color.green, elapsedTime);
            elapsedTime += Time.deltaTime * 3;

            yield return null;
        }
        _spriteRenderer.color = Color.green;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light2D = GetComponent<Light2D>();
    }

    private void TryPushButton()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.8f, LayerMask.GetMask(_buttonType.ToString()));
        Debug.DrawRay(transform.position, Vector2.up * 0.8f, Color.green);
        
        if (hit)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _buttonDownPosition, _pressSpeed * Time.deltaTime); 
            _spriteRenderer.color = Color.Lerp(Color.red, Color.green, _pressSpeed * Time.deltaTime);
        }
        else
        { 
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _buttonUpPosition, _pressSpeed * Time.deltaTime); 
        }

        if (transform.localPosition == _buttonDownPosition)
        {
            OnButtonPushed();
        }
    }
    
    private void Update()
    {
        if (!_isPushed)
        {
            TryPushButton();
        }
    }


}
