using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public Inventory2D inventory2D;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public void SetInventory(Inventory2D inventory2D)
    {
        this.inventory2D = inventory2D;
        RefreshInventory2DItems();
    }
    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = transform.Find("ItemSlotTemplate");
    }
    public void RefreshInventory2DItems()
    {
        int x = 0;
        int y = 0;
        float itemCellSize = 30f;
        foreach (Item2D item2D in inventory2D.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemCellSize, y * itemCellSize);
            x++;
            if(x>4)
            {
                x = 0;
                y++;
            }
        }
    }
   /* private void Update()
    {
        SetInventory(PlayerStats.Instance.inventory2D);
    }*/
}
