using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    private Vector3 startPoint;
    private Quaternion startRotation;

    private Vector3 endPoint;
    private Quaternion endRotation;

    public Transform shopWayPoint;
    public Transform levelWayPoint;

    void Start()
    {
        startPoint = endPoint = transform.localPosition;
        startRotation = endRotation = transform.localRotation;
    }

    void Update()
    {
        Vector3 input = GameManager.Instance.GetPlayerInput();

        transform.localPosition = Vector3.Lerp(transform.localPosition, endPoint + input, 0.1f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotation, 0.1f);
    }

    public void NavigateToMainMenu()
    {
        endPoint = startPoint;
        endRotation = startRotation;
    }

    public void NavigateToShop()
    {
        endPoint = shopWayPoint.localPosition;
        endRotation = shopWayPoint.localRotation;
    }

    public void NavigateToLevel()
    {
        endPoint = levelWayPoint.localPosition;
        endRotation = levelWayPoint.localRotation;
    }
}
