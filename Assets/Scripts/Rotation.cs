using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private int SpeedRotation = 20;
    [SerializeField]
    private GameObject PanelUI;

    public Transform Figure;
    public Transform Camera;
    public Vector3 V_rotation;
    public Vector3 Cam_otnos_mir;
    public bool figureActive;
    public bool rotate;
    public bool ueba = false;
    private SwipeControl swipeControl;
    void Start()
    {
        figureActive = false;
        Cam_otnos_mir = new Vector3(0, 0, 0);
        rotate = false;
        V_rotation = new Vector3(0,0,0);
        swipeControl = PanelUI.GetComponent<SwipeControl>();
        //gameObject.GetComponent<СenteringObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (figureActive)
        {
            if (!rotate)
            {
                //string command = PanelUI.GetComponent<SwipeControl>().CommandRotation;
                if (swipeControl.CommandRotation != "")
                {
                    switch (swipeControl.CommandRotation)
                    {
                        case "up":
                            rotate = true;
                            Debug.Log("Команда вверх");
                            ComputeRotationUpDown(1);
                            //GetComponent<СenteringObject>().CalibrationHeight();
                            break;
                        case "down":
                            rotate = true;
                            Debug.Log("Команда вниз");
                            ComputeRotationUpDown(-1);
                            //GetComponent<СenteringObject>().CalibrationHeight();
                            break;
                        case "right":
                            V_rotation.y -= 90;
                            rotate = true;
                            break;
                        case "left":
                            V_rotation.y += 90;
                            rotate = true;
                            break;
                        default:
                            Debug.Log("Нет команды");
                            break;
                    }
                    PanelUI.GetComponent<SwipeControl>().CommandRotation = "";
                }
            }
            else
            {
                if (transform.rotation != Quaternion.Euler(V_rotation))
                {
                    Figure.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(V_rotation), SpeedRotation * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(V_rotation);
                    rotate = false;
                    GetComponent<СenteringObject>().CalibrationHeight();
                }
            }
        }
    }
    void ComputeRotationUpDown(int invers)
    {
        Vector3 V_camera = transform.position - Camera.position;

        if (System.Math.Abs(Vector3.Dot(V_camera, Vector3.right)) < System.Math.Abs(Vector3.Dot(V_camera, Vector3.forward)))
        {
            if (Vector3.Dot(Camera.transform.right, Vector3.right)>0)
                Cam_otnos_mir = new Vector3(1, 0, 0);
            else
                Cam_otnos_mir = new Vector3(-1, 0, 0);
        }
        else
        {
            if (Vector3.Dot(Camera.transform.right, Vector3.forward) > 0)
                Cam_otnos_mir = new Vector3(0, 0, 1);
            else
                Cam_otnos_mir = new Vector3(0, 0, -1);
        }

        if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)))
        {
            if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
            {//X
                Debug.Log("Вращение по оси Х");
                ComputeRotateX(invers);
            }
            else
            {//Y
                Debug.Log("Вращение по оси Y");
                ComputeRotateY(invers);
            }
        }
        else
            if (System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
            {//Z
                Debug.Log("Вращение по оси Z");
                if (Vector3.Dot(transform.forward, Cam_otnos_mir) < 0)
                {
                    invers *= -1;
                    Debug.Log("Инвертируем, tr.z направлен в противоположную сторону от:" + Cam_otnos_mir);
                }
                V_rotation.z += 90 * invers;
            }
            else//Y
            {
                Debug.Log("Вращение по оси Y");
                ComputeRotateY(invers);
            }
    }
    private void ComputeRotateY(int invers)
    {
        if (V_rotation.z % 180 == 0)
        {
            if (System.Math.Abs(V_rotation.z % 360) == 180)
            {
                if (!ueba)
                {
                    ueba = !ueba;
                }
            }

            if (Vector3.Dot(transform.forward, Vector3.up) < 0)
            {
                V_rotation.y -= 90;
                V_rotation.z -= 90;
            }
            else
            {
                V_rotation.y += 90;
                V_rotation.z -= 90;
            }
        }

        if (Vector3.Dot(transform.up, Cam_otnos_mir) < 0)
        {
            invers *= -1;
            Debug.Log("Инвертируем, tr.y направлен в противоположную сторону от:" + Cam_otnos_mir);
        }

        //V_rotation.x += 90 * invers;

        if (ueba)
        {
            V_rotation.x -= 90 * invers;
        }
        else
            V_rotation.x += 90 * invers;
    }
    private void ComputeRotateX(int invers)
    {
        if (V_rotation.z % 180 != 0)
        {
            if (Vector3.Dot(transform.forward, Vector3.up) < 0)
            {
                V_rotation.y -= 90;
                V_rotation.z -= 90;
            }
            else
            {
                V_rotation.y -= 90;
                V_rotation.z += 90;
            }
        }

        if (System.Math.Abs(V_rotation.z % 360) == 180)
            invers *= -1;

        if (Vector3.Dot(transform.right, Cam_otnos_mir) < 0)
        {
            invers *= -1;
            //Debug.Log("Инвертируем, tr.x направлен в противоположную сторону от:" + Cam_otnos_mir);
        }
        //Debug.Log("invers=" + invers);
        V_rotation.x += 90 * invers;
    }
    void Info()
    {
        Vector3 V_camera = transform.position - Camera.position;
        //V_camera.y = 0;

        if (System.Math.Abs(Vector3.Dot(V_camera, Vector3.right)) < System.Math.Abs(Vector3.Dot(V_camera, Vector3.forward)))
        {
            Cam_otnos_mir = new Vector3(1, 0, 0);
        }
        else
        {
            Cam_otnos_mir = new Vector3(0, 0, 1);
        }

        if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)))
        {
            if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
            {
                if (Vector3.Dot(transform.right, Cam_otnos_mir) > 0)
                {
                    Debug.Log("Вращаем по X, dot>0");
                }
                else
                {
                    Debug.Log("Вращаем по X, dot<0");
                }

            }
            else
            {
                Debug.Log("Вращаем по Y");
            }

        }
        else
            if (System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
        {
            if (Vector3.Dot(transform.forward, Cam_otnos_mir) > 0)
            {
                Debug.Log("Вращаем по Z, dot>0");
            }
            else
            {
                Debug.Log("Вращаем по Z, dot<0");
            }
        }
        else
        {
            Debug.Log("Вращаем по Y");
        }
    }
}
//if (Input.GetKeyDown(KeyCode.UpArrow))
//{
//    rotate = true;
//    Debug.Log("Команда вверх");
//    ComputeRotationUpDown(1);
//}

//if (Input.GetKeyDown(KeyCode.DownArrow))
//{
//    rotate = true;
//    Debug.Log("Команда вниз");
//    ComputeRotationUpDown(-1);
//}


//if (Input.GetKeyDown(KeyCode.RightArrow))
//{
//    V_rotation.y -= 90;
//    rotate = true;
//}

//if (Input.GetKeyDown(KeyCode.LeftArrow))
//{
//    V_rotation.y += 90;
//    rotate = true;
//}