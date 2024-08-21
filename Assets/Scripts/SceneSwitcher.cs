using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void Switcher(string sceneName)
    {
        SceneLoader.LoadSceneWithTransition(sceneName);
    }
}
