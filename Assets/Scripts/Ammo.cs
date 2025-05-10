using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private GameObject theAmmo;
    public GameObject weaponOB;

    public AudioSource pickUpSound;

    public int ammoBoxAmount;

    public bool inreach;

    private void Start()
    {
        theAmmo = this.gameObject; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inreach = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inreach = false;
        }
    }



    void Update()
    {
        if(inreach && Input.GetKeyDown(KeyCode.E))
        {
            weaponOB.GetComponent<GunSystem>().ammoCache += ammoBoxAmount;
            //pickUpText.SetActive(false);
            theAmmo.SetActive(false);
            pickUpSound.Play();
        }
    }
}
