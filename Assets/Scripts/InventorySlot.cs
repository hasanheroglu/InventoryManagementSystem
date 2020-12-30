using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private int _posX;
    private int _posY;

    public bool isEmpty;

    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(int posX, int posY)
    {
        _posX = posX;
        _posY = posY;
    }

    public Vector2 AreAdjacentInventorySlotsEmpty(int itemPosX, int itemPosY, int itemWidth, int itemHeight)
    {  
        GameObject inventory = gameObject.transform.parent.gameObject;
        InventoryController inventoryController = inventory.GetComponent<InventoryController>();
        
        Debug.Log("ItemPosX: " + itemPosX + " ItemPosY: " + itemPosY);
        if ((_posX - itemPosX < 0 || _posX + (itemWidth - itemPosX) > inventoryController.GetWidth()) 
            || (_posY - itemPosY < 0 || _posY + (itemHeight - itemPosY) > inventoryController.GetHeight())) 
        {
            Debug.Log("Out of bounds!");
            return new Vector2(-1, -1);
        }

        for (var i=_posX-itemPosX; i<_posX-itemPosX+itemWidth; i++) 
        {
            for (var j=_posY-itemPosY; j<_posY-itemPosY+itemHeight; j++)
            {
                if (!inventoryController.IsInventorySlotEmpty(i, j))
                {
                    return new Vector2(-1, -1);
                }
            }
        }

        return inventoryController.GetInventorySlot(_posX-itemPosX, _posY-itemPosY).GetComponent<RectTransform>().anchoredPosition;
    }

    public void FillInventorySlots(int itemPosX, int itemPosY, int itemWidth, int itemHeight)
    {
        GameObject inventory = gameObject.transform.parent.gameObject;
        InventoryController inventoryController = inventory.GetComponent<InventoryController>();
        
        Debug.Log("ItemPosX: " + itemPosX + " ItemPosY: " + itemPosY);
        if ((_posX - itemPosX < 0 || _posX + (itemWidth - itemPosX) > inventoryController.GetWidth()) 
            || (_posY - itemPosY < 0 || _posY + (itemHeight - itemPosY) > inventoryController.GetHeight())) 
        {
            Debug.Log("Out of bounds!");
        }

        for (var i=_posX-itemPosX; i<_posX-itemPosX+itemWidth; i++) 
        {
            for (var j=_posY-itemPosY; j<_posY-itemPosY+itemHeight; j++)
            {
                GameObject slot = inventoryController.GetInventorySlot(i, j);
                slot.GetComponent<InventorySlot>().isEmpty = false;
                slot.SetActive(false);
            }
        }
    } 
}
