using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int buildindex;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(buildindex);
    }
}
