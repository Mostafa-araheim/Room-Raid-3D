using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public AudioSource turnOnSound;
    public AudioSource turnOffSound;
    private bool isOn; // Tracks flashlight state (ON/OFF)

    void Start()
    {
        isOn = false;
        flashlight.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn; // Toggle state

            flashlight.SetActive(isOn);

            // Play the correct sound
            if (isOn) turnOnSound.Play();
            else turnOffSound.Play();
        }
    }
}