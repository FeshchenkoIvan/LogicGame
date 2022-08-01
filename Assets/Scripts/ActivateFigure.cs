using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFigure : MonoBehaviour
{
    public bool active;
    public int indexFigure;
    public void GetActive(out bool b)
    {
        b = active;
    }
    void Start()
    {
        active = false;
        //transform.GetChild(0).gameObject.SetActive(true);
    }
    public void activateFigure(bool b)
    {
        active = b;
        GetComponent<ÑenteringObject>().enabled = b;
        transform.GetChild(0).gameObject.SetActive(b);
        if (b)
        {
            gameObject.layer = 0;
            GetComponent<Rigidbody>().mass = 0.0001f;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            GetComponent<Rigidbody>().mass = 1000f;
            gameObject.layer = 6;
        }
    }

}
