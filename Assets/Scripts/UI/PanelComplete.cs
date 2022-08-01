using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PanelComplete : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private CountFigureInc countFigureInc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextLevel()
    {
        //DataHolder._levelStart++;
        var levelComplete = DataHolder._levelStart;
        DataHolder._levelStart = DataHolder._levelStart+1;
        if (DataHolder.ARmod)
        {
            var Figures = GameObject.FindGameObjectsWithTag("Figure");
            foreach (var figure in Figures)
            {
                figure.transform.position = new Vector3(0, -100, 0);
            }
            gameManager.transform.GetChild(levelComplete+1).gameObject.SetActive(true);
            countFigureInc.ResetCountAll();//обновляем количесво используемых фигур(по одной)
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
            SceneManager.LoadScene(1);
    }
    public void RepeatLevel()
    {
        if (DataHolder.ARmod)
        {
            var Figures = GameObject.FindGameObjectsWithTag("Figure");
            foreach (var figure in Figures)
            {
                figure.transform.position = new Vector3(0,-100,0);
            }
            gameManager.transform.GetChild(DataHolder._levelStart).gameObject.SetActive(true);
            countFigureInc.ResetCountAll();
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
            SceneManager.LoadScene(1);
    }
}
