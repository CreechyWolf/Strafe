using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayEndless()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void PlayRegular()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Mystic()
    {
        PlayerPrefs.SetString("SuperActive", "mystic");
    }

    public void Silent()
    {
        PlayerPrefs.SetString("SuperActive", "silent");
    }

    public void Transient()
    {
        PlayerPrefs.SetString("SuperActive", "transient");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
