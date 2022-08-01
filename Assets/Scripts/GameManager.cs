using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Levels;
    private int levelNumber;
    private void Awake()
    {
        levelNumber = DataHolder._levelStart;
        Levels[levelNumber].SetActive(true);
    }
}
