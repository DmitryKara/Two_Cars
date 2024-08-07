using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Slider progressBar;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());

        GameOverManager.Instance?.RestartGame();
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            yield return null;
        }
    }
}