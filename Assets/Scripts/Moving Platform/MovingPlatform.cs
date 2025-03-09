using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float platformMoveSpeed;

    [SerializeField] private Transform platformTargetPosition;
    [SerializeField] private Transform platformStartingPosition;

    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, platformTargetPosition.localPosition, platformMoveSpeed * Time.deltaTime);
        if (transform.localPosition == platformTargetPosition.localPosition)
        {
            transform.localPosition = platformStartingPosition.localPosition;
        }
    }
}
