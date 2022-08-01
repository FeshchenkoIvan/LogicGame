using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    public bool CameraRotation;
    public bool PhasePanelFigures;
    public float X = 0;
    public float Y = 0;

    float distanse;
    //int SpeedRotation=10;
    [SerializeField]
    private GameObject camera;
    void Start()
    {
        CameraRotation = true;
        PhasePanelFigures = false;
    }

    
    void Update()
    {
        if (CameraRotation)
        {
            if (Input.touchCount>0)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    if (Input.GetTouch(0).position.y < 320) PhasePanelFigures = true;

            if (!PhasePanelFigures)
            {
                Touch[] touches = Input.touches;
                if (touches.Length == 1)
                {
                    Y += touches[0].deltaPosition.x * 0.1f;
                    if (X - touches[0].deltaPosition.y * 0.1f < 90 && X - touches[0].deltaPosition.y * 0.1f > 0) //ограничение на угол
                        X -= touches[0].deltaPosition.y * 0.1f;
                }
                transform.rotation = Quaternion.Euler(X, Y, 0);


                if (touches.Length == 2)
                {
                    float distanseNow = System.Math.Abs(Vector2.Distance(touches[0].position, touches[1].position));

                    if (distanse == 0) { distanse = distanseNow; }
                    else
                    {
                        if (distanseNow < distanse)
                        {
                            Vector3 NewPosition = camera.transform.localPosition;
                            NewPosition.z -= (distanse - distanseNow) / 100;
                            if (NewPosition.z > -20 && NewPosition.z < -5)
                            {
                                camera.transform.localPosition = NewPosition;
                                distanse = distanseNow;
                            }
                        }
                        else
                        {
                            Vector3 NewPosition = camera.transform.localPosition;
                            NewPosition.z += (distanseNow - distanse) / 100;
                            if (NewPosition.z > -20 && NewPosition.z < -5)
                            {
                                camera.transform.localPosition = NewPosition;
                                distanse = distanseNow;
                            }
                        }
                    }
                }
                if (touches.Length < 2) { distanse = 0; }
            }

            if (Input.touchCount > 0)
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    PhasePanelFigures = false;
        }
    }


    public void SwitchCameraRotationActive()
    {
        CameraRotation = !CameraRotation;
    }

}
