using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFigure : MonoBehaviour
{
    [SerializeField]
    private SwipeControl swipeControl;
    [SerializeField]
    private GameObject figure;
    private Button thisButton;
    public int count;
    private Text textCount;
    void Start()
    {
        thisButton = GetComponent<Button>();
        count = 1;
        textCount = transform.GetChild(0).GetComponent<Text>();
        textCount.text = count.ToString();
    }
    public void OnTouchDown()
    {
        if (GetComponent<Button>().interactable)
        {
            Debug.Log("Зажата кнопка:" + thisButton.name);
            swipeControl.figureSelectedButton = figure;
        }
    }
    public void OnTouchUp()
    {
        if (GetComponent<Button>().interactable)
        {
            if (swipeControl.checkAddFigure == true)
            {
                count--;
                textCount.text = count.ToString();
                swipeControl.figureSelectedButton = null;
                swipeControl.checkAddFigure = false;

                if (count == 0){ GetComponent<Button>().interactable = false;}
            }
        }
    }

    public void CountInc()
    {
        count++;
        textCount.text = count.ToString();
        GetComponent<Button>().interactable = true;
    }

}
