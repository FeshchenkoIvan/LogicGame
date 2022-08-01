using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СenteringObject : MonoBehaviour
{
    float x, y, z;
    public bool offset;
    public float offset_x;
    public float offset_z;
    public bool Collision;
    Vector3 savePosition;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void FixedUpdate()
    {
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (offset)
            {
                СalibrationOffsetCenter();
            }
            else
            {
                СalibrationCenter();
            }
        }
    }
    public void CalibrationHeight2(int height) //калибровка по высоте
    {
        if (offset)
        {
            if (System.Math.Abs(Vector3.Dot(transform.right, Vector3.up)) >= 0.99)
            {
                y = height-0.5f;
            }
            else
            {
                if (System.Math.Abs(Vector3.Dot(transform.forward, Vector3.up)) >= 0.99 && offset_z != 0)
                {
                    y = CorrectPositionOffset(height);
                }
                else
                {
                    y = height;
                }
            }
        }
        else
        {
            y = height;
        }

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public void CalibrationHeight() //калибровка по высоте
    {
        if (System.Math.Abs(Vector3.Dot(transform.right, Vector3.up)) >= 0.99)
        {
            y = 0.5f;
        }
        else
        {
            if (System.Math.Abs(Vector3.Dot(transform.forward, Vector3.up)) >= 0.99 && offset_z != 0)
            {
                y = 1.5f;
            }
            else
            {
                y = 1;
            }
        }
        transform.position = new Vector3(x, y, z);
    }
    void СalibrationOffsetCenter() 
    {
        if (System.Math.Abs(Vector3.Dot(transform.up, Vector3.up)) >= 0.99) //y || Y?
        {
            if (offset_z == 0 && offset) //если это фигура 1
            {
                if (System.Math.Abs(Vector3.Dot(transform.forward, Vector3.forward)) >= 0.99)
                {
                    x = CorrectPositionOffset(transform.position.x);
                    z = CorrectPosition(transform.position.z);
                }
                else
                {
                    z = CorrectPositionOffset(transform.position.z);
                    x = CorrectPosition(transform.position.x);
                }
            }
            else
            {
                z = CorrectPositionOffset(transform.position.z);
                x = CorrectPositionOffset(transform.position.x);
            }
        }
        else
        {
            if (System.Math.Abs(Vector3.Dot(transform.right, Vector3.up)) >= 0.99) // x || Y?
            {
                if (offset_z == 0 && offset)
                {
                    x = CorrectPosition(transform.position.x);
                    z = CorrectPosition(transform.position.z);
                }
                else
                {
                    if (System.Math.Abs(Vector3.Dot(transform.forward, Vector3.forward)) >= 0.99) // z || Z?
                    {
                        x = CorrectPosition(transform.position.x);
                        z = CorrectPositionOffset(transform.position.z);
                    }
                    else
                    {
                        z = CorrectPosition(transform.position.z);
                        x = CorrectPositionOffset(transform.position.x);
                    }
                }
            }
            else // z || Y!
            {
                if (System.Math.Abs(Vector3.Dot(transform.right, Vector3.right)) >= 0.99) // x || X?
                {
                    x = CorrectPositionOffset(transform.position.x);
                    z = CorrectPosition(transform.position.z);
                }
                else
                {
                    x = CorrectPosition(transform.position.x);
                    z = CorrectPositionOffset(transform.position.z);
                }
            }
        }
        //y = CorrectPosition(transform.position.y);
        y = transform.position.y;
        transform.position = new Vector3(x, y, z);
        //CalibrationHeight();
    }
    void СalibrationCenter()
    {
        x = CorrectPosition(transform.position.x);
        y = CorrectPosition(transform.position.y);
        z = CorrectPosition(transform.position.z);
        transform.position = new Vector3(x, y, z);
    }
    float CorrectPosition(float posAxis)
    {
        if (System.Math.Abs(posAxis) % 1 < 0.5) return (int)posAxis;
        else
            if (posAxis > 0) return (int)posAxis + 1;
            else return (int)posAxis - 1;
    }
    float CorrectPositionOffset(float posAxis)
    {
        if (posAxis > 0) return (int)posAxis + 0.5f;
        else return (int)posAxis - 0.5f;
    }
}
