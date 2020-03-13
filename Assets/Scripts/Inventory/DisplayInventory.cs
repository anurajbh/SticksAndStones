using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public bool isControlInInventory = false;
    public bool isControlInSlots = false;
    public bool isControlInConfirm = false;

    public InventoryObject inventory;
    public GameObject itemEntry;
    public GameObject inventoryUI;

    private GameObject itemEntries;
    private GameObject scrollbar;
    private static int itemCursorIndex;
    private int scrollbarTopIndex;
    private int scrollbarBottomIndex;

    private static GameObject inventoryOptions;
    private static int optionsCursorIndex;

    private static GameObject confirmation;
    private static int confirmationCursorIndex;

    public int timeSinceInput;
    private int numCyclesBetweenInput = 45;
    
    // Start is called before the first frame update
    void Start()
    {
        isControlInInventory = false;
        isControlInSlots = false;
        isControlInSlots = false;
        CloseInventory();
        inventoryOptions = inventoryUI.transform.GetChild(4).gameObject;
        itemEntries = inventoryUI.transform.GetChild(1).GetChild(0).gameObject;
        confirmation = inventoryUI.transform.GetChild(5).gameObject;
        confirmation.SetActive(false);
        scrollbar = inventoryUI.transform.GetChild(3).gameObject;
        Debug.Log(scrollbar.name);
        CreateItemEntries();
        ClearDescriptionBox();
        itemCursorIndex = 0;
        optionsCursorIndex = 0;
        confirmationCursorIndex = 0;
        ResetScrollbar();
        timeSinceInput = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();

        //if (Input.GetKeyDown(KeyCode.I)) {
        //    if (GameIsPaused) {
        //        Resume();
        //    } else {
        //        Pause();
        //    }
        //}

        if (timeSinceInput > 0) {
            timeSinceInput -= 1;
        }

        if (isControlInInventory) {
            if (Input.GetAxisRaw("Horizontal") == -1 && timeSinceInput == 0) {
                MoveOptionsCursorLeft();
                timeSinceInput = numCyclesBetweenInput;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1 && timeSinceInput == 0) {
                MoveOptionsCursorRight();
                timeSinceInput = numCyclesBetweenInput;
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                InventoryOptionSelect();
            }
        }

        if (isControlInSlots) {
            if (Input.GetAxisRaw("Vertical") == -1 && timeSinceInput == 0) {
                MoveItemsCursorDown();
                SetDescriptionBox();
                timeSinceInput = numCyclesBetweenInput;
            } else if (Input.GetAxisRaw("Vertical") == 1 && timeSinceInput == 0) {
                MoveItemsCursorUp();
                SetDescriptionBox();
                timeSinceInput = numCyclesBetweenInput;
            } else if (Input.GetKeyDown(KeyCode.Backspace) && timeSinceInput == 0) {
                BackToOptionSelect();
            } else if (Input.GetKeyDown(KeyCode.Space) && timeSinceInput == 0) {
                ItemSelect();
            }
        }

        if (isControlInConfirm) {
            if (Input.GetKeyDown(KeyCode.Space) && timeSinceInput == 0) {
                BackToItemSelect();
            }
        }
    }

    void CreateItemEntries() {
        if (itemEntries != null) {
            for (int i = 0; i < inventory.maxInventorySize; i++) {
                if (itemEntry != null) {
                    GameObject entry = Instantiate(itemEntry);
                    entry.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
                    entry.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "";
                    entry.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = "";
                    entry.transform.SetParent(itemEntries.transform);
                }
            }
        }
    }

    void ClearDescriptionBox() {
        GameObject description = inventoryUI.transform.GetChild(2).GetChild(0).gameObject;
        description.GetComponent<UnityEngine.UI.Text>().text = "";
    }
    void SetDescriptionBox() {
        string itemDescription = inventory.Container[itemCursorIndex].item.description;
        GameObject descriptionText = inventoryUI.transform.GetChild(2).GetChild(0).gameObject;
        descriptionText.GetComponent<UnityEngine.UI.Text>().text = itemDescription;
    }

    void UpdateDisplay() {
        if (itemEntries != null) {
            for (int i = 0; i < inventory.Container.Count; i++) {
                string itemName = inventory.Container[i].item.itemName;
                int quantity = inventory.Container[i].quantity;
                GameObject entryI = itemEntries.transform.GetChild(i).gameObject;
                entryI.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = itemName;
                entryI.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = quantity.ToString();
            }
        }
    }

    void MoveOptionsCursorLeft() {
        if (optionsCursorIndex > 0) {
            inventoryOptions.transform.GetChild(optionsCursorIndex).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "";
            optionsCursorIndex -= 1;
            inventoryOptions.transform.GetChild(optionsCursorIndex).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = ">";
        }
    }

    void MoveOptionsCursorRight() {
        if (optionsCursorIndex < 2) {
            inventoryOptions.transform.GetChild(optionsCursorIndex).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "";
            optionsCursorIndex += 1;
            inventoryOptions.transform.GetChild(optionsCursorIndex).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = ">";
        }
    }

    void MoveItemsCursorUp() {
        if (itemCursorIndex > 0) {
            GameObject entryPrev = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
            entryPrev.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
            itemCursorIndex -= 1;
            GameObject entryCurr = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
            entryCurr.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = ">";

            if (itemCursorIndex < scrollbarTopIndex) {
                scrollbarTopIndex -= 1;
                scrollbarBottomIndex -= 1;
                float newScrollbarValue = 1f - (scrollbarTopIndex * (1f / 14f));
                scrollbar.GetComponent<UnityEngine.UI.Scrollbar>().value = newScrollbarValue;
            }

        }
    }

    void MoveItemsCursorDown() {
        if (itemCursorIndex < inventory.Container.Count - 1) {
            GameObject entryPrev = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
            entryPrev.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
            itemCursorIndex += 1;
            GameObject entryCurr = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
            entryCurr.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = ">";

            if (itemCursorIndex > scrollbarBottomIndex) {
                scrollbarTopIndex += 1;
                scrollbarBottomIndex += 1;
                float newScrollbarValue = 1f - (scrollbarTopIndex * (1f / 14f));
                scrollbar.GetComponent<UnityEngine.UI.Scrollbar>().value = newScrollbarValue;
            }
        }
    }

    void InventoryOptionSelect() {
        inventoryOptions.transform.GetChild(optionsCursorIndex).GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = false;
        isControlInInventory = false;
        isControlInSlots = true;
        isControlInConfirm = false;
        if (inventory.Container.Count > 0) {
            resetItemCursor();
            SetDescriptionBox();
        }
        timeSinceInput = numCyclesBetweenInput;
    }

    void ItemSelect() {
        GameObject entryCurr = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
        entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
        isControlInInventory = false;
        isControlInSlots = false;
        isControlInConfirm = true;
        confirmation.SetActive(true);
        confirmationCursorIndex = 0;
        confirmation.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
        timeSinceInput = numCyclesBetweenInput;
    }

    //void ConfirmSelect() {
    //    inventoryOptions.transform.GetChild(2).GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = false;
    //    isControlInInventory = false;
    //    isControlInSlots = true;
    //}

    void BackToOptionSelect() {
        isControlInConfirm = false;
        isControlInSlots = false;
        GameObject entryCurr = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
        entryCurr.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
        inventoryOptions.transform.GetChild(optionsCursorIndex).GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
        ClearDescriptionBox();
        timeSinceInput = numCyclesBetweenInput;
        isControlInInventory = true;
    }

    void BackToItemSelect() {
        isControlInInventory = false;
        isControlInSlots = true;
        isControlInConfirm = false;
        GameObject entryCurr = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
        entryCurr.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = false;
        confirmation.SetActive(false);
        timeSinceInput = numCyclesBetweenInput;
    }

    void resetItemCursor() {
        GameObject entryPrev = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
        entryPrev.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
        itemCursorIndex = 0;
        GameObject entryFirst = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
        entryFirst.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = ">";
    }

    void ResetScrollbar() {
        scrollbarTopIndex = 0;
        scrollbarBottomIndex = 5;
        scrollbar.GetComponent<UnityEngine.UI.Scrollbar>().value = 1f;
    }

    public void CloseInventory() {
        inventoryUI.SetActive(false);
        if (inventory.Container.Count > 0) {
            GameObject entryCurr = itemEntries.transform.GetChild(itemCursorIndex).gameObject;
            entryCurr.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
        }
        if (inventoryOptions != null) {
            inventoryOptions.transform.GetChild(optionsCursorIndex).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "";
        }
        isControlInInventory = false;
        isControlInSlots = false;
        isControlInConfirm = false;
    }

    public void OpenInventory() {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;

        confirmation.SetActive(false);

        optionsCursorIndex = 0;
        if (inventoryOptions != null) {
            inventoryOptions.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = ">";
            inventoryOptions.transform.GetChild(1).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "";
            inventoryOptions.transform.GetChild(2).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "";

            inventoryOptions.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
            inventoryOptions.transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
            inventoryOptions.transform.GetChild(2).GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
        }

        itemCursorIndex = 0;
        ResetScrollbar();
        ClearDescriptionBox();
        //if (inventory.Container.Count > 0) {
        //    GameObject entry0 = itemEntries.transform.GetChild(0).gameObject;
        //    entry0.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = ">";
        //    SetDescriptionBox();
        //}

        isControlInInventory = true;
        isControlInSlots = false;
        isControlInConfirm = false;
    }
}
