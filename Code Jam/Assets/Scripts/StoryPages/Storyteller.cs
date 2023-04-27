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

    private List<GameObject> currentRenders = new List<GameObject>();

    private int currentPage = 0;
    private int currentImage = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Begin showing the page
        StartCoroutine(ShowNextPicture(fullStory[currentPage].storyImages[currentImage]));
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

        // If there are still images (or the arrow) left on page, skip to the final image. Else turn the page
        if (currentImage <= fullStory[currentPage].storyImages.Length)
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
        // If there are still pages left, do what needs to be done to show the next page
        if (currentPage < fullStory.Length - 1)
        {
            // Go up a page
            currentPage++;

            // Destroy all currently rendered images
            foreach (GameObject render in currentRenders)
            {
                Destroy(render);
            }
            currentRenders.Clear();

            currentImage = 0;

            // Begin showing the next page
            StartCoroutine(ShowNextPicture(fullStory[currentPage].storyImages[currentImage]));
        }
        else
        {
            Debug.Log("Done showing these pages");
        }
    }

    /// <summary>
    /// Skips waiting for the individual drawing to show up to finish a page
    /// </summary>
    private void SkipWait()
    {
        // End all current coroutines (the stuff currently showing the next page n such
        StopAllCoroutines();

        // Go through the rest of the pages
        while (currentImage < fullStory[currentPage].storyImages.Length)
        {
            // Show every page
            ShowDrawing(fullStory[currentPage].storyImages[currentImage].pageGraphic);
            // Queue the narration
            narrationQueue.Enqueue(fullStory[currentPage].storyImages[currentImage].clip);
            currentImage++;
        }
        // Show the to next arrow
        currentImage++;
        ShowDrawing(toNextArrow);

        // Get the amount of time left in the current clip being played
        float currentClipLength = narratorSource.clip != null ? narratorSource.clip.length : 0f;
        currentClipLength -= narratorSource.time;
        // Clamp minimum length
        if (currentClipLength <= 0.001f) currentClipLength = 0.001f;

        // If there are any queued clips left
        if (narrationQueue.Count > 0)
        {
            // Begin playing queued clips
            StartCoroutine(PlayQueuedClips(currentClipLength));
        }
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
        currentRenderer.sortingOrder = currentImage;

        // Remember the game object for later cleaning
        currentRenders.Add(currentRenderer.gameObject);
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
    private IEnumerator ShowNextPicture(StoryImage page)
    {
        // Show the page
        ShowDrawing(page.pageGraphic);
        // Play the connected audio clip
        NarrateClip(page.clip);
        currentImage++;

        // Wait either until audio clip is over, or the default wait have passed, if no audio clip is attached to the page
        yield return new WaitForSeconds(page.clip == null ? ( page.specialWaitTime != 0 ? page.specialWaitTime : baseWaitLength ) : page.clip.length);

        // If there are still images left, show the next image
        if (currentImage < fullStory[currentPage].storyImages.Length)
        {
            StartCoroutine(ShowNextPicture(fullStory[currentPage].storyImages[currentImage]));
        }
        else // If there are no pages left, show the turn page arrow
        {
            currentImage++;
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

            //// If the player wishes to use advanced options, make them interactable
            //EditorGUI.BeginDisabledGroup(!storyteller.useAdvanced);
            //DrawAdvanced(serializedObject, storyteller.useAdvanced);
            //// Don't make anything else uninteractable
            //if (!storyteller.useAdvanced)
            //{
            //    EditorGUI.EndDisabledGroup();
            //}
        }

        /// <summary>
        /// Draws the Advanced Menu in the inspector
        /// </summary>
        /// <param name="serializedObject"></param>
        //private static void DrawAdvanced(SerializedObject serializedObject, bool useAdvanced)
        //{
        //    // Some space
        //    EditorGUILayout.Space();

        //    // Get the bool
        //    bool showAdvanced = serializedObject.FindProperty("showAdvanced").boolValue;

        //    // Begin checking for changes in the foldout
        //    EditorGUI.BeginChangeCheck();
        //    // Begins a field for dropdown. If using advanced options it is toggleable, else it is collapsed
        //    showAdvanced = EditorGUILayout.Foldout(useAdvanced ? showAdvanced : false, "Advanced", true);

        //    // If the foldout is changed, register it
        //    if (EditorGUI.EndChangeCheck())
        //    {
        //        serializedObject.FindProperty("showAdvanced").boolValue = showAdvanced;
        //    }

        //    // If the ShowAdvanced dropdown menu is to be open, show the following
        //    if (showAdvanced)
        //    {
        //        EditorGUI.indentLevel++;

        //        // Property field to assign and unassign materials to the outlinedMaterial
        //        EditorGUILayout.PropertyField(serializedObject.FindProperty("outlinedMaterial"));
        //        // Property field to assign and unassign materials to the outlinedMaterial
        //        EditorGUILayout.PropertyField(serializedObject.FindProperty("basicMaterial"));
        //        // Some spacing
        //        EditorGUILayout.Space();
        //        // Show the Outline Actions
        //        EditorGUILayout.PropertyField(serializedObject.FindProperty("outlineActions"));

        //        EditorGUI.indentLevel--;
        //    }

        //    // Apply all changes made
        //    serializedObject.ApplyModifiedProperties();
        //}
    }
#endif
    #endregion
}
