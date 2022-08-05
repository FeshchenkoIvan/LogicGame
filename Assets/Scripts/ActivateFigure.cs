using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFigure : MonoBehaviour
{
    public bool active;
    public int indexFigure;
    void Start()
    {
        active = false;
    }
    public void GetActive(out bool _bool)
    {
        _bool = active;
    }
    public void activateFigure(bool _bool)
    {
        active = _bool;
        GetComponent<CenteringObject>().enabled = _bool;
        transform.GetChild(0).gameObject.SetActive(_bool);
        if (_bool)
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
