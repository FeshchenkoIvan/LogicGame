using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    public void OnClickButton()
    {
        DataHolder.menu_levels = true;
        SceneManager.LoadScene(0);
    }
}
