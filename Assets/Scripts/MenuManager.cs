using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void EasyMode()
    {
        SceneManager.LoadScene(1);
    }

    public void MidMode()
    {
        SceneManager.LoadScene(2);
    }

    public void HardMode()
    {
        SceneManager.LoadScene(3);
    }
}
