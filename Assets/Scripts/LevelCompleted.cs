using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField] private CheckCollisionPoint[] checkCollisionPoints;
    private GameObject PanelCompleteLevel;
    private bool levelCompleted;
    private int CountBoxCollisions;
    void Start()
    {
        PanelCompleteLevel = GameObject.Find("PanelCompleteLevel");
        CountBoxCollisions = checkCollisionPoints.Length;
        levelCompleted = false;
    }
    int AllCubesInScene()
    {
        int count = 0;
        var figures = GameObject.FindGameObjectsWithTag("Figure");
        foreach (var figure in figures)
        {
            if (figure.transform.position.y>0)
            {
                if (figure.GetComponent<ActivateFigure>().indexFigure==1)
                    count += 3;
                else
                    count += 4;
            }
        }
        return count;
    }
    void Update()
    {
        if (levelCompleted)
        {
            if (DataHolder._levelStart == PlayerPrefs.GetInt("Classic"))
            {
                PlayerPrefs.SetInt("Classic", DataHolder._levelStart + 1);////[0] это первый уровень в PlayerPrefs
            }
            PanelCompleteLevel.transform.GetChild(0).gameObject.SetActive(true);
            //transform.root.gameObject.SetActive(false);
            transform.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))//(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //levelCompleted = false;
            for (int i = 0; i < CountBoxCollisions; i++)
            {
                if (checkCollisionPoints[i].collision == false)
                    break;
                else
                    if (i== CountBoxCollisions - 1)
                        if (CountBoxCollisions == AllCubesInScene())
                            levelCompleted = true;
            }
            Debug.Log("levelCompleted=" + levelCompleted + ", AllCubes=" + AllCubesInScene());
        }
    }

    
}
