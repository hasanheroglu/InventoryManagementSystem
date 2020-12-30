using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    private List<GameObject> _inventorySlots;

    // Start is called before the first frame update
    void Awake()
    {
        _inventorySlots = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void AddInventorySlot(GameObject inventorySlot)
    {
        _inventorySlots.Add(inventorySlot);
    }

    public bool IsInventorySlotEmpty(int posX, int posY)
    {
        return _inventorySlots[posX + posY * _width].GetComponent<InventorySlot>().isEmpty;
    }

    public GameObject GetInventorySlot(int posX, int posY)
    {
        return _inventorySlots[posX + posY * _width];
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public void EmptyItemSlots(Vector2 inventoryAnchoredPos, int itemWidth, int itemHeight)
    {
        Debug.Log("inventoryAnchoredPosX: " + inventoryAnchoredPos.x + " inventoryAnchoredPosY: " + inventoryAnchoredPos.y);
        int posX = (int) inventoryAnchoredPos.x;
        int posY = (int) inventoryAnchoredPos.y;

        Debug.Log("posX: " + posX + " posY: " + posY);
        for (var i=posX; i<posX+itemWidth; i++) 
        {
            for (var j=posY; j<posY+itemHeight; j++)
            {
                GameObject slot = GetInventorySlot(i, j);
                slot.GetComponent<InventorySlot>().isEmpty = true;
                slot.SetActive(true);
            }
        }
    }
}
