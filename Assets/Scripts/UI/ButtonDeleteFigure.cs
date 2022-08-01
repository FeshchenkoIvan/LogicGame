using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDeleteFigure : MonoBehaviour
{
    [SerializeField]
    private ButtonObjectRotation buttonObjectRotation;
    [SerializeField]
    public SwipeControl swipeControl;
    [SerializeField]
    private CountFigureInc countFigureInc;
    private void Start()
    {
        GetComponent<Button>().interactable = false;
    }

    public void OnMouseUpAsButton()
    {
        buttonObjectRotation.ActiveButtonObjRotation(false);
        ActiveButtonDeleteFigure(false);
        DeleteFigure();
    }
    public void ActiveButtonDeleteFigure(bool Bool)
    {
        GetComponent<Button>().interactable = Bool;
    }

    private void DeleteFigure()
    {
        if (swipeControl.ActiveFigureName!="")
        {
            GameObject figure = GameObject.Find(swipeControl.ActiveFigureName);
            swipeControl.ActiveFigureName = "";
            countFigureInc.CountInc(figure.GetComponent<ActivateFigure>().indexFigure);
            figure.GetComponent<Rotation>().figureActive = false;
            figure.GetComponent<ÑenteringObject>().enabled = false;
            figure.GetComponent<ActivateFigure>().activateFigure(false);
            figure.GetComponent<Rigidbody>().isKinematic = true;
            figure.transform.position = new Vector3(0, -100, 0);
            //figure.SetActive(false);


        }
    }
}
