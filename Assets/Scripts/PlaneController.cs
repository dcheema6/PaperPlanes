using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private CharacterController controller;

    private float baseSpeed = 10.0f;
    private float rotXSpeed = 6.0f;
    private float rotYSpeed = 3.0f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        GameObject trail = Instantiate(GameManager.Instance.planeTrails[SaveManager.Instance.game.activeTrail]);
        trail.transform.SetParent(transform.GetChild(0));
        trail.transform.localEulerAngles = Vector3.forward*-90.0f;
    }

    void Update()
    {
        Vector3 velocity = transform.forward * baseSpeed;
        Vector3 input = GameManager.Instance.GetPlayerInput();

        // Get direction change
        Vector3 yaw = input.y * transform.right * rotXSpeed * Time.deltaTime;
        Vector3 pitch = input.z * transform.up * rotYSpeed * Time.deltaTime;
        Vector3 direction = yaw + pitch;

        // Prevent plane from doing a loop
        float maxX = Quaternion.LookRotation(velocity + direction).eulerAngles.x;
        // If not going too far up/down, add to speed vector
        if (!(maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290))
        {
            velocity += direction;
            transform.rotation = Quaternion.LookRotation(velocity);
            controller.Move(velocity*Time.deltaTime);
        }
    }
}
