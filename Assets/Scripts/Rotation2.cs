using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2 : MonoBehaviour
{
    public Transform Figure;
    public Transform Camera;
    private Vector3 V_rotation;
    private Quaternion V_quaternion;
    public Vector3 Cam_otnos_mir;
    private bool rotate;
    //private char axis;
    [SerializeField]
    private int SpeedRotation = 20;
    void Start()
    {
        Cam_otnos_mir = new Vector3(0, 0, 0);
        rotate = false;
        V_rotation = new Vector3(0, 0, 0);
        //V_quaternion = Quaternion.identity;
        V_quaternion = transform.rotation;
        //Info();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotate)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //transform.rotation = Quaternion.AngleAxis(V_rotation.x, Vector3.right);
                //transform.rotation *= Quaternion.Euler(V_rotation);

                V_rotation.x += 90;
                V_quaternion = Quaternion.AngleAxis(V_rotation.x, transform.right);
                rotate = true;
                Debug.Log("V_rotation=" + V_rotation);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                V_rotation.x -= 90;
                V_quaternion = Quaternion.AngleAxis(V_rotation.x, transform.right);
                rotate = true;
                Debug.Log("V_rotation=" + V_rotation);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                V_rotation.y -= 90;
                V_quaternion = Quaternion.AngleAxis(V_rotation.y, transform.up);
                rotate = true;
                Debug.Log("V_rotation=" + V_rotation);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                V_rotation.y += 90;
                //transform.rotation = Quaternion.AngleAxis(V_rotation.x, Vector3.right);
                V_quaternion = Quaternion.AngleAxis(V_rotation.y, transform.up);
                rotate = true;
                Debug.Log("V_rotation=" + V_rotation);
            }

        }
        else
        {
            if (transform.rotation != V_quaternion)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, V_quaternion, SpeedRotation * Time.deltaTime);
                transform.rotation = V_quaternion;
            }
            else
            {
                transform.rotation = V_quaternion;
                rotate = false;
                //Info();
            }
        }


    }


    //void Info()//int invers)
    //{
    //    Vector3 V_camera = transform.position - Camera.position;
    //    //V_camera.y = 0;

    //    if (System.Math.Abs(Vector3.Dot(V_camera, Vector3.right)) < System.Math.Abs(Vector3.Dot(V_camera, Vector3.forward)))
    //    {
    //        Cam_otnos_mir = new Vector3(1, 0, 0);
    //    }
    //    else
    //    {
    //        Cam_otnos_mir = new Vector3(0, 0, 1);
    //    }

    //    if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)))
    //    {
    //        if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
    //        {
    //            Debug.Log("Вращаем по X");
    //        }
    //        else
    //        {
    //            Debug.Log("Вращаем по Y");
    //        }

    //    }
    //    else
    //        if (System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
    //    {
    //        Debug.Log("Вращаем по Z");
    //    }
    //    else
    //    {
    //        Debug.Log("Вращаем по Y");
    //    }
    //}

    //void ComputeRotationUpDown(int invers)
    //{
    //    Vector3 V_camera = transform.position - Camera.position;
    //    //V_camera.y = 0;

    //    if (System.Math.Abs(Vector3.Dot(V_camera, Vector3.right)) < System.Math.Abs(Vector3.Dot(V_camera, Vector3.forward)))
    //    {
    //        Cam_otnos_mir = new Vector3(1, 0, 0);
    //    }
    //    else
    //    {
    //        Cam_otnos_mir = new Vector3(0, 0, 1);
    //    }

    //    if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)))
    //    {
    //        if (System.Math.Abs(Vector3.Dot(transform.right, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
    //        {
    //            //axis = 'x';
    //            if (Vector3.Dot(transform.right, Cam_otnos_mir) > 0)
    //            {
    //                V_rotation.x += 90 * invers;
    //            }
    //            else V_rotation.x -= 90 * invers;
    //            //V_rotation.x += 90 * invers;
    //        }
    //        else
    //        {
    //            if (Vector3.Dot(transform.up, Cam_otnos_mir) > 0)
    //            {
    //                V_rotation.x += 90 * invers;
    //            }
    //            else V_rotation.x -= 90 * invers;
    //            //axis = 'y'; 
    //            //V_rotation.x += 90 * invers;
    //        }

    //    }
    //    else
    //        if (System.Math.Abs(Vector3.Dot(transform.forward, Cam_otnos_mir)) > System.Math.Abs(Vector3.Dot(transform.up, Cam_otnos_mir)))
    //    {
    //        if (Vector3.Dot(transform.up, Cam_otnos_mir) > 0)
    //        {
    //            //V_rotation.z += 90 * invers;
    //        }
    //        //else V_rotation.z -= 90 * invers;
    //    }
    //    else
    //    {
    //        if (Vector3.Dot(transform.up, Cam_otnos_mir) > 0)
    //        {
    //            V_rotation.x += 90 * invers;
    //        }
    //        else V_rotation.x -= 90 * invers;
    //        //V_rotation.x += 90 * invers;
    //        //axis = 'y'; 
    //    }
    //}
}

