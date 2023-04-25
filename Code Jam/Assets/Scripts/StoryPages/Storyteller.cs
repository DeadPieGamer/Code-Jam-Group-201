using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyteller : MonoBehaviour
{
    [SerializeField, Tooltip("An array of all the story pages")] private StoryPage[] fullStory;
    [SerializeField, Tooltip("The arrow to illustrate turning pages")] private StoryPage toNextArrow;

    [SerializeField, Tooltip("How long an image is shown before the next one appears")] private float baseWaitLength = 1f;
    [SerializeField, Tooltip("The prefab that is to render story pages")] private GameObject pageRenderer;
    [SerializeField, Tooltip("The audiosource that will narrate the text")] private AudioSource narratorSource;

    private Queue<AudioClip> narrationQueue = new Queue<AudioClip>();
    
    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowNextPicture(fullStory[currentPage]));
    }

    /// <summary>
    /// Turns over to the next page
    /// </summary>
    public void TurnPage()
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
    public void SkipWait()
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

        // Get the amount of time left in the current clip being played
        float currentClipLength = narratorSource.clip.length;
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
        GameObject currentPage = Instantiate(pageRenderer, Vector3.zero, Quaternion.identity);
        currentPage.GetComponent<SpriteRenderer>().sprite = sprite;
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
            PlayQueuedClips(currentClip.length);
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
        yield return new WaitForSeconds(page.clip == null ? baseWaitLength : page.clip.length);

        // If there are still pages left, show the next page
        if (currentPage < fullStory.Length)
        {
            StartCoroutine(ShowNextPicture(fullStory[currentPage]));
        }
    }
}
