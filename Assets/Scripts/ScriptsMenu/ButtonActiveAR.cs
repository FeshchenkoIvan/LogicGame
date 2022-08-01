using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActiveAR : MonoBehaviour
{
    //[SerializeField] private DataHolder dataHolder;
    private bool ARmod;
    public Text TextAR;
    void Start()
    {
        if (DataHolder.ARmod)
        {
            ARmod = true;
            TextAR.color = Color.green;
        }
        else
        {
            ARmod = false;
            TextAR.color = Color.red;
        }
        //DataHolder.ARmod = false;
    }

    public void OnButtonClick()
    {
        if (ARmod==false)
        {
            ARmod = true;
            TextAR.color = Color.green;
            DataHolder.ARmod = true;
        }
        else
        {
            ARmod = false;
            TextAR.color = Color.red;
            DataHolder.ARmod = false;
        }
    }
}
