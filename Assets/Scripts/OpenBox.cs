using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxScript : MonoBehaviour
{
    public Animator boxOB;
    public AudioSource openSound;

    public bool inReach;
    public bool isOpen;
    void Start()
    {
        inReach = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inReach = false;
    }
    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            openSound.Play();
            boxOB.SetBool("open", true);
            isOpen = true;
        }

        //else if (inReach && Input.GetKeyDown(KeyCode.E))
        //{
        //    openText.SetActive(false);
        //}

        if (isOpen)
        {
            boxOB.GetComponent<BoxCollider>().enabled = false;
            boxOB.GetComponent<OpenBoxScript>().enabled = false;
        }
    }
}