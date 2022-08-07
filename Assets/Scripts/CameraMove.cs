using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private float sensitivity= 0.1f;
    public bool CameraRotation;
    private Touch[] touches;
    private bool PhasePanelFigures;
    private float X = 0;
    private float Y = 0;
    private float distance;

    void Start()
    {
        CameraRotation = true;
        PhasePanelFigures = false;
    }

    void Update()
    {
        if (CameraRotation)
        {
            CheckClickPanelFigures();
            if (!PhasePanelFigures)
            {
                touches = Input.touches;
                if (touches.Length == 1)
                    RotationCamera();

                if (touches.Length == 2)
                    ZoomingCamera();
                else
                    distance = 0;
            }
        }
    }

    void CheckClickPanelFigures()
    {
        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(0).position.y < 320) PhasePanelFigures = true;
            }
            else 
                if (Input.GetTouch(0).phase == TouchPhase.Ended) PhasePanelFigures = false;
    }

    void RotationCamera()
    {
        Y += touches[0].deltaPosition.x * sensitivity;
        if (X - touches[0].deltaPosition.y * sensitivity < 90 && X - touches[0].deltaPosition.y * sensitivity > 0)
            X -= touches[0].deltaPosition.y * sensitivity;
        transform.rotation = Quaternion.Euler(X, Y, 0);
    }

    void ZoomingCamera()
    {
        float distanceNow = System.Math.Abs(Vector2.Distance(touches[0].position, touches[1].position));

        if (distance == 0) { distance = distanceNow; }
        else
        {
            if (distanceNow < distance)
            {
                Vector3 NewPosition = Camera.transform.localPosition;
                NewPosition.z -= (distance - distanceNow) / 100;
                if (NewPosition.z > -20 && NewPosition.z < -5)
                {
                    Camera.transform.localPosition = NewPosition;
                    distance = distanceNow;
                }
            }
            else
            {
                Vector3 NewPosition = Camera.transform.localPosition;
                NewPosition.z += (distanceNow - distance) / 100;
                if (NewPosition.z > -20 && NewPosition.z < -5)
                {
                    Camera.transform.localPosition = NewPosition;
                    distance = distanceNow;
                }
            }
        }
    }
}
