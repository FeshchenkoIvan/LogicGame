using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCameraRotation : MonoBehaviour
{
    public bool ButCamPress;
    [SerializeField]
    public GameObject ButObjRotation;
    [SerializeField]
    public GameObject Panel;
    [SerializeField]
    public GameObject Camera;
    private void Start()
    {
        GetComponent<Image>().color = Color.green;
        ButCamPress = true;
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseUp()
    {
        
    }

    public void OnMouseUpAsButton()
    {
        ButCamPress = !ButCamPress;
        if (ButCamPress)
        {
            GetComponent<Image>().color = Color.green;
            if (ButObjRotation.GetComponent<ButtonObjectRotation>().ButObjRotationPress)
            {
                ButObjRotation.GetComponent<ButtonObjectRotation>().OnMouseUpAsButton();
            }
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }

        Panel.GetComponent<SwipeControl>().CameraRotation = ButCamPress;
        Camera.GetComponent<CameraMove>().CameraRotation = ButCamPress;
    }
}
