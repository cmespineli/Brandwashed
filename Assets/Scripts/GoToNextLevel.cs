using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour
{
    private const string LastSceneKey = "LastScene";

    public void SceneTransition(string sceneName)
    {
        // Store the current scene before transitioning
        PlayerPrefs.SetString(LastSceneKey, SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }

    public void GoToLastScene()
    {
        // Retrieve the last scene and load it if it exists
        string lastScene = PlayerPrefs.GetString(LastSceneKey, "");

        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.LogWarning("No previous scene found!");
        }
    }
}
