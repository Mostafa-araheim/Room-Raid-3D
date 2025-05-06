using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    //Overlook Of Variables:

    // - rotationAngle      : How much you want your door to open
    // - rotationSpeed      : How fast the door should open (1 = slow, 10 = fast, and everything in between)
    // - interactionRange   : How close you want your player to be to detect interactions
    // - interactionKey     : The key you want your player to use to interact

    // - openSound          : Sound for opening the door
    // - openSound          : Sound for closing the door

    // *** Make Sure You Have An Audio Source On This Door Object ***

    // - rotateOnX          : Rotates the door on the X value
    // - rotateOnY          : Rotates the door on the Y value
    // - rotateOnZ          : Rotates the door on the Z value

    


    [Header("*** Door Settings ***")]
    public float rotationAngle = 90f; 
    public float rotationSpeed = 3.5f;
    public float interactionRange = 4f;
    public KeyCode interactionKey = KeyCode.E;

    [Header("*** Audio Settings ***")]
    public AudioClip openSound;
    public AudioClip closeSound;

    [Header("*** Rotation Axis ***")]
    public bool rotateOnX = false;
    public bool rotateOnY = true; 
    public bool rotateOnZ = false;



    private Transform door;
    private AudioSource audioSource;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private bool isInteracting = false;

    private Transform player;
    private Camera mainCamera;

    void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        door = this.transform;

        if (playerObject == null || cameraObject == null)
        {
            Debug.LogError("Player or MainCamera not found! Check your tags.");
            return;
        }

        player = playerObject.transform;
        mainCamera = cameraObject.GetComponent<Camera>();

        closedRotation = door.rotation;

        openRotation = CalculateOpenRotation();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (mainCamera == null || player == null || door == null)
        {
            Debug.LogError("References are missing! Ensure all required objects are assigned.");
            return;
        }

        if (Input.GetKeyDown(interactionKey) && !isInteracting)
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactionRange, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
            {
                Debug.Log($"Raycast hit: {hit.transform.name}");
                if (hit.transform == door)
                {
                    isOpen = !isOpen;

                    PlaySound(isOpen);

                    StartCoroutine(RotateDoor(isOpen));
                }
            }
        }
    }

    private Quaternion CalculateOpenRotation()
    {
        Vector3 rotationAxis = Vector3.zero;

        if (rotateOnX) rotationAxis = Vector3.right;
        if (rotateOnY) rotationAxis = Vector3.up;
        if (rotateOnZ) rotationAxis = Vector3.forward;

        return closedRotation * Quaternion.AngleAxis(rotationAngle, rotationAxis);
    }

    private System.Collections.IEnumerator RotateDoor(bool open)
    {
        isInteracting = true;

        if (open)
            openRotation = CalculateOpenRotation();

        Quaternion targetRotation = open ? openRotation : closedRotation;

        while (Quaternion.Angle(door.rotation, targetRotation) > 0.01f)
        {
            door.rotation = Quaternion.Lerp(door.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        door.rotation = targetRotation;
        isInteracting = false;
    }

    private void PlaySound(bool opening)
    {
        if (audioSource != null)
        {
            audioSource.clip = opening ? openSound : closeSound;
            audioSource.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (mainCamera != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(mainCamera.transform.position, mainCamera.transform.position + mainCamera.transform.forward * interactionRange);
        }
    }
}
