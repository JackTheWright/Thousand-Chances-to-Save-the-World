using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButton : MonoBehaviour
{
    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
