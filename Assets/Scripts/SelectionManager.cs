using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    public bool onTarget;
    public GameObject interaction_Info_UI;
    Text interaction_text;
    public bool canBeStored;
    private void Awake()
    {
        if (instance is not null && instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
        }
    }
    private void Start()
    {
        canBeStored = false;
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            if (selectionTransform.GetComponent<InteractableObject>() 
                && selectionTransform.GetComponent<InteractableObject>().playerInRange)
            {
                onTarget = true;
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();

                interaction_Info_UI.SetActive(true);
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }

        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}