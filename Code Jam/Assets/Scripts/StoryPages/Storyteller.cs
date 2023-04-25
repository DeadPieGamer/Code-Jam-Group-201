using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class Storyteller : MonoBehaviour
{
    [SerializeField, Tooltip("An array of all the story pages")] private StoryPage[] fullStory;
    [SerializeField, Tooltip("The arrow to illustrate turning pages")] private Sprite toNextArrow;

    [SerializeField, Tooltip("How long an image is shown before the next one appears")] private float baseWaitLength = 1f;
    [SerializeField, Tooltip("The prefab that is to render story pages")] private GameObject pageRenderer;
    [SerializeField, Tooltip("The audiosource that will narrate the text")] private AudioSource narratorSource;

    private Queue<AudioClip> narrationQueue = new Queue<AudioClip>();
    
    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Begin showing the page
        StartCoroutine(ShowNextPicture(fullStory[currentPage]));
    }

    /// <summary>
    /// Registers input and translates it into function
    /// </summary>
    /// <param name="context"></param>
    public void GotInput(InputAction.CallbackContext context)
    {
        // Only do stuff if context.performed
        if (!context.performed)
        {
            return;
        }

        // If there are still images left, skip to the final image. Else turn the page
        if (currentPage < fullStory.Length)
        {
            Debug.Log("Skipping");
            SkipWait();
        }
        else
        {
            Debug.Log("Turning page");
            TurnPage();
        }
    }

    /// <summary>
    /// Turns over to the next page
    /// </summary>
    private void TurnPage()
    {
        // End all current coroutines (the stuff currently playing the next narration n such)
        StopAllCoroutines();
        // Empty the narration queue and stop the current narration
        narrationQueue.Clear();
        narratorSource.Stop();
    }

    /// <summary>
    /// Skips waiting for the individual drawing to show up to finish a page
    /// </summary>
    private void SkipWait()
    {
        // End all current coroutines (the stuff currently showing the next page n such
        StopAllCoroutines();

        // Go through the rest of the pages
        while (currentPage < fullStory.Length)
        {
            // Show every page
            ShowDrawing(fullStory[currentPage].pageGraphic);
            // Queue the narration
            narrationQueue.Enqueue(fullStory[currentPage].clip);
            currentPage++;
        }
        // Show the to next arrow
        ShowDrawing(toNextArrow);

        // Get the amount of time left in the current clip being played
        float currentClipLength = narratorSource.clip != null ? narratorSource.clip.length : 0f;
        currentClipLength -= narratorSource.time;
        // Clamp minimum length
        if (currentClipLength <= 0.001f) currentClipLength = 0.001f;
        // Begin playing queued clips
        StartCoroutine(PlayQueuedClips(currentClipLength));
    }

    /// <summary>
    /// Instantiates a new drawing to also show that
    /// </summary>
    /// <param name="sprite"></param>
    private void ShowDrawing(Sprite sprite)
    {
        // Instantiate the new drawing and remember its sprite renderer
        SpriteRenderer currentRenderer = Instantiate(pageRenderer, Vector3.zero, Quaternion.identity).GetComponent<SpriteRenderer>();
        currentRenderer.sprite = sprite;
        currentRenderer.sortingOrder = currentPage;
    }

    /// <summary>
    /// Begins playing the clip it has been fed
    /// </summary>
    /// <param name="clip"></param>
    private void NarrateClip(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        narratorSource.clip = clip;
        narratorSource.Play();
    }

    /// <summary>
    /// Plays all queued narrations in order
    /// </summary>
    /// <param name="waitLength"></param>
    /// <returns></returns>
    private IEnumerator PlayQueuedClips(float waitLength)
    {
        yield return new WaitForSeconds(waitLength);
        AudioClip currentClip = narrationQueue.Dequeue();
        NarrateClip(currentClip);

        // If there are any clips left, repeat this process
        if (narrationQueue.Count > 0)
        {
            StartCoroutine(PlayQueuedClips(currentClip != null ? currentClip.length : 0.001f));
        }
    }

    /// <summary>
    /// Shows all pictures in order
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    private IEnumerator ShowNextPicture(StoryPage page)
    {
        // Show the page
        ShowDrawing(page.pageGraphic);
        // Play the connected audio clip
        NarrateClip(page.clip);
        currentPage++;

        // Wait either until audio clip is over, or the default wait have passed, if no audio clip is attached to the page
        yield return new WaitForSeconds(page.clip == null ? ( page.specialWaitTime != 0 ? page.specialWaitTime : baseWaitLength ) : page.clip.length);

        // If there are still pages left, show the next page
        if (currentPage < fullStory.Length)
        {
            StartCoroutine(ShowNextPicture(fullStory[currentPage]));
        }
        else // If there are no pages left, show the turn page arrow
        {
            ShowDrawing(toNextArrow);
        }
    }

    #region Editor
#if UNITY_EDITOR

    // Custom editor inspired by Kap Koder
    // https://www.youtube.com/watch?v=RImM7XYdeAc

    [CustomEditor(typeof(Storyteller))]
    public class StorytellerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Renders the base thing
            base.OnInspectorGUI();

            // Get the DummyOutlineHandler to check whether advanced should be used or not
            Storyteller storyteller = (Storyteller)target;


        }
    }
#endif
    #endregion
}
