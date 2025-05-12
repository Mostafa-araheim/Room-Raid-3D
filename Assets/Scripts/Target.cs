using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject OriginalOB;
    public GameObject ChangeOB;
    public Animator ANI;

    public float health = 100f;

    public bool animate;
    public bool replace;
    public bool destroy;

    public void TakeDamage(float amount)
    {
        Debug.Log($"i{this.name} got hit got hit");
        health -= amount;
        if (health <= 0f && animate)
        {
            Animate();
        }

        if (health <= 0f && replace)
        {
            Replace();
        }

        if (health <= 0f && destroy)
        {
            Destroy();
        }
    }

    void Animate()
    {
        ANI.SetBool("IsDead", true);
        StartCoroutine(WaitForDeathAnimation());
        //No Destroy here—let the Animator or other logic handle it if needed
    }
    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(3f); // Wait for 2 seconds (adjust to match your death animation length)
        if (animate)
        {
            Destroy();
        }
    }
    void Replace()
    {
        OriginalOB.SetActive(false);
        ChangeOB.SetActive(true);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}