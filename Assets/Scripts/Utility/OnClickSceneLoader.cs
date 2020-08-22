using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickSceneLoader : MonoBehaviour
{
    public string sceneName;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { LoadScene(sceneName); });
    }
    public void LoadScene(string sceneName)
    {
        AudioManager.instance.Play(3);
        SceneManager.LoadScene(sceneName);
    }
}
