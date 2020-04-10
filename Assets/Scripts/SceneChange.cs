using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("OutsideTest");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("Floor2");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("Floor1");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("OutsideTest");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
