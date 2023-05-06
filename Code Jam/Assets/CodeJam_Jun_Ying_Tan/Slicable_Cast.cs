using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class Slicable_Cast : MonoBehaviour
{
    [SerializeField] private GameObject unslicedObject;
    private GameObject slicedObject;

    private Rigidbody2D m_rigibody;
    private Collider2D m_collider;

    [SerializeField] private LoadScene sceneManager;
    private float wait = 1.5f;
    public GameObject snap;

    // Variables used to in Instantiate the gameobject snap
    private int canvasFlip = 90;
    private int imageFlip = 180;
   

    private void Awake()
    {
        m_rigibody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        //getting the gameobject with the tag Writeable and asigning it to out gameobject
        snap = GameObject.FindGameObjectWithTag("Writeable");
        //instantiating 
        Instantiate(snap, transform.position, Quaternion.Euler(canvasFlip, imageFlip, 0));
    }

    public void setSlicedObject(GameObject slicedObject)
    {
        this.slicedObject = slicedObject;
    }

    public GameObject getSlicedObject()
    {
        return slicedObject;
    }

    public void Slice()
    {
        //Debug.Log("slice!");
        unslicedObject.SetActive(false);
        slicedObject.SetActive(true);
        if (m_collider != null)
        {
            m_collider.enabled = false;
            for (int i = 0; i < slicedObject.transform.childCount; i++)
            {
                Rigidbody2D sliceRigidbody = slicedObject.transform.GetChild(i).GetComponent<Rigidbody2D>();
                sliceRigidbody.velocity = m_rigibody.velocity;
            }
            m_rigibody.bodyType = RigidbodyType2D.Static;
        }
        
        StartCoroutine(loadnext());
    }

    private IEnumerator loadnext()
    {
        yield return new WaitForSeconds(wait);
        sceneManager.LoadNextScene();
            Destroy(snap);
    }
    
}
//Inspo from 1 Minute Unity youtube.com/watch?v=muPZvw7CU-0&t=509s