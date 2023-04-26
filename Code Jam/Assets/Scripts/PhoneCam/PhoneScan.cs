using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhoneScan : MonoBehaviour
{
    [SerializeField] private GameObject scanner;
    [SerializeField] private Animator animator;
    [SerializeField] private LoadScene sceneMang;

    private void Start()
    {
        animator = scanner.GetComponent<Animator>();
    }
    
    
    public void buttonScan()
    {
        StartCoroutine(StartScan());
    }

    private IEnumerator StartScan()
    {
        animator.SetBool("Scanning", true);
        yield return new WaitForSeconds(10f);
        animator.SetBool("Scanning",false);
        yield return new WaitForSeconds(5f);
        sceneMang.LoadNextScene();
    }
}
