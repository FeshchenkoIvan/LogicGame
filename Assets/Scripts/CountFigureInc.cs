using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountFigureInc : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ButtonFigure[] buttonFigures;
    void Start()
    {
        
    }

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
