using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingletonSettings : MonoBehaviour
{
    public static SingletonSettings instance;

    // Change: The prefab of this had previously set this string to something else, where it should've been "Book"
    [SerializeField, Tooltip("What Book Scenes are called")] private string bookNamingConvention = "Book";

    [SerializeField, Tooltip("The pause menu")] private GameObject pauseMenu;
    // Removed an unused reference

    float lastTimeScale = 0f;
    
    private bool isBookScene = true;
    private AudioSource narrationSource;

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

        // If this is a book scene, pause and unpause the narration
        if (isBookScene)
        {
            if (pauseMenu.activeSelf)
            {
                narrationSource.Pause();
            }
            else
            {
                narrationSource.UnPause();
            }
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If it's a book scene, don't appear. Else appear in the corner

        // Change: Removed an excalamtion mark
        isBookScene = scene.name.Contains(bookNamingConvention);
        //pauseButton.interactable = !isBookScene;
        if (isBookScene) // If this is a book scene, get the narration audio player
        {
            // There is techincally a magical variable here, but as this is not to be referenced at any other point, I chose to just let it be
            narrationSource = GameObject.FindGameObjectWithTag("Storyinator").GetComponent<AudioSource>();
        }
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
