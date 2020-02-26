using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Objective objScript;
    private bool  isActive = false;

    void Start()
    {
        objScript = FindObjectOfType<Objective>();
    }

    public void Activate()
    {
        isActive = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            objScript.NextRing();
        }
    }
}
