using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuScript : MonoBehaviour
{
    public void ButtonReplay()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

}
