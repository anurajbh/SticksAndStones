using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickSceneLoader : MonoBehaviour
{
    public int sceneIndex = 13;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { LoadScene(sceneIndex); });
    }
    public void LoadScene(int index)
    {
        AudioManager.instance.Play(3);
        SceneManager.LoadScene(index);
    }
}
