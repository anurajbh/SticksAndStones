using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : PlayerTransition
{
    public string sceneName;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(transform.parent.gameObject);
        DontDestroyOnLoad(canvasGroup.transform.parent.gameObject);
    }

    public override IEnumerator Blackout()
    {
        yield return StartCoroutine("DoFade");
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine("MovePlayer");
        yield return new WaitForSeconds(3f);
        Reload();
        yield return new WaitForSeconds(3f);
        Destroy(canvasGroup.transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
    }
}
