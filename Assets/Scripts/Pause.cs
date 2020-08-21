using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    //Scribbles
    public GameObject Scrib1;
    public GameObject Scrib2;
    public GameObject Scrib3;
    public GameObject Scrib4;

    //Inventory items
    public GameObject Inven1;
    public GameObject Inven2;
    public GameObject Inven3;
    public GameObject Inven4;
    public GameObject Inven5;
    public GameObject Inven6;
    public Inventory2D inventory2D;
    


    void Start() {
        Scrib1.SetActive(false);
        Scrib2.SetActive(false);
        Scrib3.SetActive(false);
        Scrib4.SetActive(false);
        setScribbles();
        //displayInventory();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                setScribbles();
                PauseGame();
            }
        }

    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false; 
    }

    void PauseGame() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void setScribbles() {
        int lvl = SMPlayerStats.Instance.anxiety;

        if (lvl < 3) {
            return;
        }
        if (lvl >= 3) {
            Scrib1.SetActive(true);
        } 
        if (lvl >= 5) {
            Scrib2.SetActive(true);
        }
        if (lvl >= 7) {
            Scrib3.SetActive(true);
        }
        if (lvl >= 9) {
            Scrib4.SetActive(true);
        }
    }

    /*private void displayInventory() {
        Inven1.SetActive(false);
        Inven2.SetActive(false);
        Inven3.SetActive(false);
        Inven4.SetActive(false);
        Inven5.SetActive(false);
        Inven6.SetActive(false);

        int pos = 0;
        foreach (Item2D item2D in inventory2D.GetItemList()) {
            String name = item2D.getName();
            switch(name) {
                case 1:
                    setPosition(GameObject, pos);
                    break;
                case 2:
                    setPosition(GameObject, pos);
                    break;
                case 3:
                    setPosition(GameObject, pos);
                    break;
                case 4:
                    setPosition(GameObject, pos);
                    break;
                case 5:
                    setPosition(GameObject, pos);
                    break;
                case 6:
                    setPosition(GameObject, pos);
                    break;
            }
            pos++;
        }
    }

    private void setPosition(GameObject g, int pos) {
        switch(pos) {
            case 1:
                GameObject.transform.position = new Vector3(x, y, z);
                break;
            case 2:
                GameObject.transform.position = new Vector3(x, y, z);
                break;
            case 3:
                GameObject.transform.position = new Vector3(x, y, z);
                break;
            case 4:
                GameObject.transform.position = new Vector3(x, y, z);
                break;
            case 5:
                GameObject.transform.position = new Vector3(x, y, z);
                break;
            case 6:
                GameObject.transform.position = new Vector3(x, y, z);
                break;
        }
    }
    
    
    */

    public void LoadMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /*public void LoadOptions() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Options"); 
    }*/
}
