using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    public GameObject hud;
    public GameObject inv;
    public GameObject deathScreen;
    private GameObject player;
    private GameObject camera; 

    public float health = 100f;



    void Start()
    {
        deathScreen.SetActive(false);
        player = this.gameObject;
        camera = FindFirstObjectByType<MouseLook>().gameObject;
    }



    void Update()
    {

        if(health <= 0)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            camera.GetComponent<MouseLook>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            hud.SetActive(false);
            inv.SetActive(false);
            deathScreen.SetActive(true);
        }

        if (health > 100)
        {
            health = 100;
        }
        
    }
}
