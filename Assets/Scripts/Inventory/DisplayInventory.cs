using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public InventoryObject inventory;
    public GameObject itemEntry;
    public GameObject inventoryUI;

    private GameObject itemEntries;
    private GameObject scrollbar;
    private int cursorIndex;
    private int scrollbarTopIndex;
    private int scrollbarBottomIndex;

    private int timeSinceInput;
    private int numCyclesBetweenInput = 45;
    
    // Start is called before the first frame update
    void Start()
    {
        Resume();
        itemEntries = inventoryUI.transform.GetChild(1).GetChild(0).gameObject;
        scrollbar = inventoryUI.transform.GetChild(3).gameObject;
        Debug.Log(scrollbar.name);
        CreateItemEntries();
        ClearDescriptionBox();
        cursorIndex = 0;
        ResetScrollbar();
        timeSinceInput = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();

        if (Input.GetKeyDown(KeyCode.I)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }

        if (timeSinceInput > 0) {
            timeSinceInput -= 1;
        }

        if (GameIsPaused) {
            if (Input.GetAxisRaw("Vertical") == -1 && timeSinceInput == 0) {
                MoveCursorDown();
                SetDescriptionBox();
                timeSinceInput = numCyclesBetweenInput;
            } else if (Input.GetAxisRaw("Vertical") == 1 && timeSinceInput == 0) {
                MoveCursorUp();
                SetDescriptionBox();
                timeSinceInput = numCyclesBetweenInput;
            }
        }
    }

    void CreateItemEntries() {
        for (int i = 0; i < inventory.maxInventorySize; i++) {
            if (itemEntry != null) {
                GameObject entry = Instantiate(itemEntry);
                entry.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
                entry.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
                entry.transform.SetParent(itemEntries.transform);
            }
        }
    }

    void ClearDescriptionBox() {
        GameObject description = inventoryUI.transform.GetChild(2).GetChild(0).gameObject;
        description.GetComponent<UnityEngine.UI.Text>().text = "";
    }
    void SetDescriptionBox() {
        string itemDescription = inventory.Container[cursorIndex].item.description;
        GameObject descriptionText = inventoryUI.transform.GetChild(2).GetChild(0).gameObject;
        descriptionText.GetComponent<UnityEngine.UI.Text>().text = itemDescription;
    }

    void UpdateDisplay() {
        for (int i = 0; i < inventory.Container.Count; i++) {
            string itemName = inventory.Container[i].item.itemName;
            GameObject entryI = itemEntries.transform.GetChild(i).gameObject;
            entryI.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = itemName;
        }
    }

    void MoveCursorUp() {
        if (cursorIndex > 0) {
            GameObject entryPrev = itemEntries.transform.GetChild(cursorIndex).gameObject;
            entryPrev.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
            cursorIndex -= 1;
            GameObject entryCurr = itemEntries.transform.GetChild(cursorIndex).gameObject;
            entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ">";

            if (cursorIndex < scrollbarTopIndex) {
                scrollbarTopIndex -= 1;
                scrollbarBottomIndex -= 1;
                float newScrollbarValue = 1f - (scrollbarTopIndex * (1f / 13f));
                scrollbar.GetComponent<UnityEngine.UI.Scrollbar>().value = newScrollbarValue;
            }

        }
    }

    void MoveCursorDown() {
        if (cursorIndex < inventory.Container.Count - 1) {
            GameObject entryPrev = itemEntries.transform.GetChild(cursorIndex).gameObject;
            entryPrev.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
            cursorIndex += 1;
            GameObject entryCurr = itemEntries.transform.GetChild(cursorIndex).gameObject;
            entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ">";

            if (cursorIndex > scrollbarBottomIndex) {
                scrollbarTopIndex += 1;
                scrollbarBottomIndex += 1;
                float newScrollbarValue = 1f - (scrollbarTopIndex * (1f / 13f));
                scrollbar.GetComponent<UnityEngine.UI.Scrollbar>().value = newScrollbarValue;
            }
        }
    }

    void ResetScrollbar() {
        scrollbarTopIndex = 0;
        scrollbarBottomIndex = 6;
        scrollbar.GetComponent<UnityEngine.UI.Scrollbar>().value = 1f;
    }

    void Resume() {
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        if (inventory.Container.Count > 0) {
            GameObject entryCurr = itemEntries.transform.GetChild(cursorIndex).gameObject;
            entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        }
        GameIsPaused = false;
    }

    void Pause() {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        cursorIndex = 0;
        ResetScrollbar();
        if (inventory.Container.Count > 0) {
            GameObject entry0 = itemEntries.transform.GetChild(0).gameObject;
            entry0.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ">";
            SetDescriptionBox();
        }
        GameIsPaused = true;
    }
}
