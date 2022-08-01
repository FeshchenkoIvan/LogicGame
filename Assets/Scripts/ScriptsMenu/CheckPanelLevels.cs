using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPanelLevels : MonoBehaviour
{
    [SerializeField] GameObject PanelMain;
    [SerializeField] GameObject PanelMode;
    [SerializeField] GameObject PanelClassic;
    void Start()
    {
        if (DataHolder.menu_levels)
        {
            PanelClassic.SetActive(true);
            PanelMode.SetActive(false);
            PanelMain.SetActive(false);
        }
    }


}
