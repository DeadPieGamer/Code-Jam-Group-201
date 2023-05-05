using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slicable_Cast : MonoBehaviour
{
    [SerializeField] private GameObject unslicedObject;
    private GameObject slicedObject;

    private Rigidbody2D m_rigibody;
    private Collider2D m_collider;

    [SerializeField] private LoadScene sceneManager;
    private float wait = 1.5f;

    private void Awake()
    {
        m_rigibody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
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
    }
    
}
//Inspo from 1 Minute Unity youtube.com/watch?v=muPZvw7CU-0&t=509s