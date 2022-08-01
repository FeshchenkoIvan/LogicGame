using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button[] ButtonsClassicLevel;
    private int ProgressClassic;
    private void Awake()
    {
        ProgressClassic=PlayerPrefs.GetInt("Classic", 0);
        for (int i = 0; i <= ProgressClassic; i++)
        {
            ButtonsClassicLevel[i].interactable=true;
        }
    }

    public void clearDataProgressing()
    {
        PlayerPrefs.SetInt("Classic", 0);
        for (int i = 1; i < ButtonsClassicLevel.Length; i++)
        {
            ButtonsClassicLevel[i].interactable = false;
        }
    }
}
