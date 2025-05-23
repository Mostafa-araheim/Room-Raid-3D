using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSwitch : MonoBehaviour
{
    public GameObject object01;
    public GameObject object02;
    void Start()
    {
        object01.SetActive(true);
        object02.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            object01.SetActive(true);
            object02.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            object01.SetActive(false);
            object02.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            object01.SetActive(false);
            object02.SetActive(false);
        }
    }
}