using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadLevel : MonoBehaviour
{
    [SerializeField] private int level; //[0] это первый уровень
    public void OnClickButton()
    {
        DataHolder._levelStart = level;
        if (DataHolder.ARmod)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(1);
    }
}
