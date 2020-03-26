using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    public GameObject playerController;

    public static bool GameIsPaused = false;
    public static bool IsInSelection = false;
    public static bool IsInventoryMode = false;
    public static bool IsOptionsMode = false;
    public static bool IsQuitMode = false;
    //public InventoryObject inventory;
    //public GameObject itemEntry;
    public GameObject menuItemsUI;
    public GameObject inventoryUI;

    //private GameObject itemEntries;
    //private GameObject scrollbar;
    private int cursorIndex;
    //private int scrollbarTopIndex;
    //private int scrollbarBottomIndex;

    private int timeSinceInput;
    private int numCyclesBetweenInput = 35;

    // Start is called before the first frame update
    void Start() {
        Resume();
        //itemEntries = inventoryUI.transform.GetChild(1).GetChild(0).gameObject;
        //scrollbar = inventoryUI.transform.GetChild(3).gameObject;
        //Debug.Log(scrollbar.name);
        //CreateItemEntries();
        //ClearDescriptionBox();
        cursorIndex = 0;
        //ResetScrollbar();
        timeSinceInput = 0;

    }

    // Update is called once per frame
    void Update() {
        //UpdateDisplay();

        if (Input.GetKeyDown(KeyCode.P)) {
            if (GameIsPaused && !IsInSelection) {
                if (playerController != null) {
                    playerController.GetComponent<PlayerMovement>().inMenu = false;
                }
                Resume();
            }
            else if (!GameIsPaused && !IsInSelection) {
                if (playerController != null) {
                    playerController.GetComponent<PlayerMovement>().inMenu = true;
                }
                Pause();
            }
        }

        if (timeSinceInput > 0) {
            timeSinceInput -= 1;
        }

        if (GameIsPaused && !IsInSelection) {
            if (Input.GetAxisRaw("Vertical") == -1 && timeSinceInput == 0) {
                MoveCursorDown();
                timeSinceInput = numCyclesBetweenInput;
            }
            else if (Input.GetAxisRaw("Vertical") == 1 && timeSinceInput == 0) {
                MoveCursorUp();
                timeSinceInput = numCyclesBetweenInput;
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                IsInSelection = true;
                MenuSelect();
            }
        }

        if (IsInventoryMode) {
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                HideInventory();
            }
        } else if (IsOptionsMode) {  // TODO: Fix
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                IsOptionsMode = false;
                menuItemsUI.transform.GetChild(cursorIndex).GetComponent<UnityEngine.UI.Image>().enabled = false;
                IsInSelection = false;
            }
        } else if (IsQuitMode) {  // TODO: Fix
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                IsQuitMode = false;
                menuItemsUI.transform.GetChild(cursorIndex).GetComponent<UnityEngine.UI.Image>().enabled = false;
                IsInSelection = false;
            }
        }
    }

    //void UpdateDisplay() {
    //    for (int i = 0; i < inventory.Container.Count; i++) {
    //        string itemName = inventory.Container[i].item.itemName;
    //        GameObject entryI = itemEntries.transform.GetChild(i).gameObject;
    //        entryI.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = itemName;
    //    }
    //}

    void MenuSelect() {
        menuItemsUI.transform.GetChild(cursorIndex).GetComponent<UnityEngine.UI.Image>().enabled = true;
        if (cursorIndex == 0) {    // Resume
            Resume();
        } else if (cursorIndex == 1) {    // Inventory
            ShowInventory();
        } else if (cursorIndex == 2) {    // Options
            ShowOptions();
        } else if (cursorIndex == 3) {    // Quit
            QuitGame();
        }
    }

    void ShowInventory() {
        inventoryUI.SetActive(true);
        IsInventoryMode = true;
        this.GetComponent<DisplayInventory>().OpenInventory();
    }
    void ShowOptions() {
        IsOptionsMode = true;
    }
    void QuitGame() {
        IsQuitMode = true;
    }

    void HideInventory() {
        if (this.GetComponent<DisplayInventory>().isControlInInventory && this.GetComponent<DisplayInventory>().timeSinceInput == 0) {
            inventoryUI.SetActive(false);
            this.GetComponent<DisplayInventory>().CloseInventory();
            IsInventoryMode = false;
            menuItemsUI.transform.GetChild(cursorIndex).GetComponent<UnityEngine.UI.Image>().enabled = false;
            IsInSelection = false;
        }
    }

    void MoveCursorUp() {
        if (cursorIndex > 0) {
            GameObject entryPrev = menuItemsUI.transform.GetChild(cursorIndex).gameObject;
            entryPrev.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
            cursorIndex -= 1;
            GameObject entryCurr = menuItemsUI.transform.GetChild(cursorIndex).gameObject;
            entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ">";
        }
    }

    void MoveCursorDown() {
        if (cursorIndex < 3) {
            GameObject entryPrev = menuItemsUI.transform.GetChild(cursorIndex).gameObject;
            entryPrev.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
            cursorIndex += 1;
            GameObject entryCurr = menuItemsUI.transform.GetChild(cursorIndex).gameObject;
            entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ">";
        }
    }

    void Resume() {
        menuItemsUI.transform.parent.gameObject.SetActive(false);
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        GameObject entryCurr = menuItemsUI.transform.GetChild(cursorIndex).gameObject;
        entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        GameIsPaused = false;
        IsInSelection = false;
    }

    void Pause() {
        menuItemsUI.transform.parent.gameObject.SetActive(true);
        Time.timeScale = 0f;
        cursorIndex = 0;

        GameObject resumeItem = menuItemsUI.transform.GetChild(0).gameObject;
        resumeItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ">";
        menuItemsUI.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = false;

        GameObject inventoryItem = menuItemsUI.transform.GetChild(1).gameObject;
        inventoryItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        menuItemsUI.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().enabled = false;

        GameObject optionsItem = menuItemsUI.transform.GetChild(2).gameObject;
        optionsItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        menuItemsUI.transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().enabled = false;

        GameObject quitItem = menuItemsUI.transform.GetChild(3).gameObject;
        quitItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        menuItemsUI.transform.GetChild(3).GetComponent<UnityEngine.UI.Image>().enabled = false;

        GameIsPaused = true;
        IsInSelection = false;
    }
}
