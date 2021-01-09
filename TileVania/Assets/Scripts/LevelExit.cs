using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelExit : MonoBehaviour
{
    [FormerlySerializedAs("LevelLoadDelay")] [SerializeField] private float levelLoadDelay = 2f;
    [FormerlySerializedAs("LevelExitSlowMoFactor")] [SerializeField] private float levelExitSlowMoFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }
    
    private IEnumerator LoadNextLevel()
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        Time.timeScale = levelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

