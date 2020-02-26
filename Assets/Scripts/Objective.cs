using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{    
    public Material activeRing;
    public Material inactiveRing;
    public Material finalRing;

    private List<Transform> rings = new List<Transform>();
    private int ringsPassed = 0;

    private void Start()
    {
        foreach(Transform t in transform)
        {
            rings.Add(t);
            t.GetComponent<MeshRenderer>().material = inactiveRing;
        }

        if (rings.Count == 0)
        {
            Debug.Log("No rings assigned to the objective");
            return;
        }

        rings[ringsPassed].GetComponent<MeshRenderer>().material = activeRing;
        rings[ringsPassed].GetComponent<Ring>().Activate();
    }

    public void NextRing()
    {
        ringsPassed++;
        if (ringsPassed == rings.Count)
        {
            LevelComplete();
            return;
        }

        if (ringsPassed == rings.Count - 1)
            rings[ringsPassed].GetComponent<MeshRenderer>().material = finalRing;
        else
            rings[ringsPassed].GetComponent<MeshRenderer>().material = activeRing;
        rings[ringsPassed].GetComponent<Ring>().Activate();
    }

    private void LevelComplete()
    {
        FindObjectOfType<GameController>().CompleteLevel();
    }
}
