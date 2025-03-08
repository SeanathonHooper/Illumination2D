using System;
using System.Collections;
using UnityEngine;

public class ButtonReactorWallFloor : MonoBehaviour
{
    [SerializeField] PushButton pushButton;
    [SerializeField] private Transform moveToLocation;
    private float _moveToSpeed = 5f;

    private void Awake()
    {
        pushButton.OnButtonPushedEvent += OnButtonPushed;
    }

    private void OnDestroy()
    {
        pushButton.OnButtonPushedEvent -= OnButtonPushed;
    }

    private void OnButtonPushed()
    {
        StartCoroutine(MoveTowardsStopPoint());
    }

    IEnumerator MoveTowardsStopPoint()
    {
        while (transform.localPosition != moveToLocation.localPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveToLocation.localPosition, _moveToSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = moveToLocation.localPosition;
    }
}
