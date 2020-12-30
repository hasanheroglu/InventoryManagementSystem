using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public bool isPicked;

    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    private RectTransform _rectTransform;

    private Vector2 _inventoryAnchoredPos;
    private InventoryController _inventoryController;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.sizeDelta = new Vector2(_width * 50, _height * 50);
        isPicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public void Pick(Vector2 inventoryAnchoredPos, InventoryController inventoryController)
    {
        _inventoryAnchoredPos = inventoryAnchoredPos;
        _inventoryController = inventoryController;
        isPicked = true;
    }

    public void Drop()
    {
        _inventoryController.EmptyItemSlots(_inventoryAnchoredPos, _width, _height);
        _inventoryController = null;
        isPicked = false;
    }
}
