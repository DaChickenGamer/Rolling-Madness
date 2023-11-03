using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void OnContinue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnStop()
    {
        Application.Quit();
    }
}
