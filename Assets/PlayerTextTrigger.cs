using UnityEngine;


public class PlayerTextTrigger : MonoBehaviour
{
    [SerializeField] private string subTextString;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            GameManager.Instance.SetUISubtext(subTextString);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            GameManager.Instance.SetUISubtext("");
        }
    }
}
