using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] private CheckCollisionPoint[] checkCollisionPoints;
    private GameObject PanelCompleteLevel;
    private int CountBoxCollisions;
    void Start()
    {
        PanelCompleteLevel = GameObject.Find("PanelCompleteLevel");
        CountBoxCollisions = checkCollisionPoints.Length;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            CheckLevelComplete();
    }
    void CheckLevelComplete()
    {
        for (int i = 0; i < CountBoxCollisions; i++)
        {
            if (checkCollisionPoints[i].collision == false)
                break;
            else
                if (i == CountBoxCollisions - 1)
                    if (CountBoxCollisions == AllCubesInScene())
                    {
                        if (DataHolder._levelStart == PlayerPrefs.GetInt("Classic"))
                        {
                            PlayerPrefs.SetInt("Classic", DataHolder._levelStart + 1);
                        }
                        PanelCompleteLevel.transform.GetChild(0).gameObject.SetActive(true);
                        transform.gameObject.SetActive(false);
                    }
        }
    }
    int AllCubesInScene()
    {
        int count = 0;
        var figures = GameObject.FindGameObjectsWithTag("Figure");
        foreach (var figure in figures)
        {
            if (figure.transform.position.y > 0)
            {
                if (figure.GetComponent<ActivateFigure>().indexFigure == 1)
                    count += 3;
                else
                    count += 4;
            }
        }
        return count;
    }
}
