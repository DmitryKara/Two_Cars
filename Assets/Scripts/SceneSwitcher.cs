using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void Switcher(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
