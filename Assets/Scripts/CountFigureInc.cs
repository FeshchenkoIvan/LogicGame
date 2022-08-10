using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountFigureInc : MonoBehaviour
{
    [SerializeField] private ButtonFigure[] buttonFigures;
    public void CountInc(int index)
    {
        transform.GetChild(index - 1).GetComponent<ButtonFigure>().CountInc();
    }
    public void ResetCountAll()
    {
        foreach (var item in buttonFigures)
        {
            if (item.count==0)
                item.CountInc();
        }
    }
}
