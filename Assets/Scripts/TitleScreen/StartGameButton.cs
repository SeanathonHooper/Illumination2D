using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void LoadSceneFromButton(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
