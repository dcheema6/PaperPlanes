using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SaveManager.Instance.game.gold++;
        SaveManager.Instance.Save();
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Rotate(Vector3.up*90*Time.deltaTime);
    }
}
