using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame(int number)
    {
        SceneManager.LoadScene(number);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
