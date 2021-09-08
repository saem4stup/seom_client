using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void MakeBOGEUMScene()
    {
        SceneManager.LoadScene("MakeBOGEUM");
    }
    public void LoadPageScene()
    {
        SceneManager.LoadScene("Page");
    }
}
