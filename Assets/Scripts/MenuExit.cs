using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuExit : MonoBehaviour
{
    public void doExitGame() 
    {
        Debug.Log("quit");
        Application.Quit(); 
    }
}
