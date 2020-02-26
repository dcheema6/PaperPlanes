using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlane : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.forward * 3 * Time.deltaTime; 
    }
}
