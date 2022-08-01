using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObjectRotation : MonoBehaviour
{
    
    public bool ButObjRotationPress;
    [SerializeField]
    public GameObject ButCamRotation;
    [SerializeField]
    public GameObject PanelUI;


    private void Start()
    {
        GetComponent<Button>().interactable=false;
        ButObjRotationPress = false;
    }
    private void OnMouseDown()
    {

    }
    private void OnMouseUp()
    {

    }

    public void OnMouseUpAsButton()
    {
        if (GetComponent<Button>().interactable)
        {
            ButObjRotationPress = !ButObjRotationPress;
            if (ButObjRotationPress)
            {
                PanelUI.GetComponent<SwipeControl>().ObjectRotation = true;
                GetComponent<Image>().color = Color.green;
                if (ButCamRotation.GetComponent<ButtonCameraRotation>().ButCamPress)
                {
                    ButCamRotation.GetComponent<ButtonCameraRotation>().OnMouseUpAsButton();
                }
            }
            else
            {
                PanelUI.GetComponent<SwipeControl>().ObjectRotation = false;
                GetComponent<Image>().color = Color.white;
            }
        }

    }

    //отключение(включение)кнопки в зависимости от активного объекта
    public void ActiveButtonObjRotation(bool Bool)
    {
        GetComponent<Button>().interactable = Bool;
        if (!Bool)
        {
            GetComponent<Image>().color = Color.white;
            PanelUI.GetComponent<SwipeControl>().ObjectRotation = false;
        }
    }

}
