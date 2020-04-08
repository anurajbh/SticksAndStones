using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    //public GameObject Panel;

    public void hide()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void show()
    {
        gameObject.GetComponent<Image>().enabled = true;
    }
}
