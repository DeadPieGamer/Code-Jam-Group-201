using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingletonSettings : MonoBehaviour
{
    public static SingletonSettings instance;

    [SerializeField, Tooltip("What Book Scenes are called")] private string bookNamingConvention = "Book";

    [SerializeField, Tooltip("The pause menu")] private GameObject pauseMenu;
    [SerializeField, Tooltip("The pause button")] private Button pauseButton;

    float lastTimeScale = 0f;
    
    private bool canPause = true;

    // Start is called before the first frame update
    void Start()
    {
        // If this script doesn't already exist
        if (instance == null)
        {
            // Make this one gameObject exist throughout all scenes
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            // If this already exists, kill any extra copies of it that may appear
            Destroy(gameObject);
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        // Pause or unpause the timescale
        float currentTimeScale = Time.timeScale;
        Time.timeScale = lastTimeScale;
        lastTimeScale = currentTimeScale;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If it's a book scene, don't appear. Else appear in the corner
        canPause = !scene.name.Contains(bookNamingConvention);
        pauseButton.interactable = canPause;
    }

    #region Scene Loading Events Setup
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion
}
