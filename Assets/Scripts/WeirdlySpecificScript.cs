using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeirdlySpecificScript : MonoBehaviour
{
    public int buildIndex;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Cancel")!=0)
        {
            AudioManager.instance.Play(0);
            SceneManager.LoadScene(buildIndex);
        }
    }
}
