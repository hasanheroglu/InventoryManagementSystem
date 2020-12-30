using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private GameObject _inventorySlot;
    [SerializeField]
    private GameObject _inventoryWindow;

    private GameObject _window;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            if(_window != null) 
            {
                GameObject.Destroy(_window);
                _window = null;
            }
            _window = CreateInventoryWindow(Input.mousePosition, _width, _height);
        }
    }

    public GameObject CreateInventoryWindow(Vector3 position, int width, int height) 
    {
        GameObject window = GameObject.Instantiate(_inventoryWindow, position, Quaternion.identity, _canvas.transform);
        GameObject panel = window.transform.Find("Panel").gameObject;
        RectTransform windowRect = window.GetComponent<RectTransform>();
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        RectTransform slotRect = _inventorySlot.GetComponent<RectTransform>();
        windowRect.sizeDelta = new Vector2(width*slotRect.rect.width, height*slotRect.rect.height);
        panelRect.sizeDelta = new Vector2(width*slotRect.rect.width, height*slotRect.rect.height);
        for (var i=0; i<height; i++) {
            for (var j=0; j<width; j++) { 
                CreateInventorySlot(j, i, window.transform);
            }
        }
        return window;
    }

    public void CreateInventorySlot(int xPos, int yPos, Transform parent) 
    { 
        GameObject slot = GameObject.Instantiate(_inventorySlot, parent);
        slot.GetComponent<InventorySlot>().SetPosition(xPos, yPos);
        RectTransform slotRect = slot.GetComponent<RectTransform>();
        float slotWidth = slotRect.rect.width;
        float slotHeight = slotRect.rect.height;
        slotRect.anchoredPosition = new Vector2(xPos*slotWidth, yPos*slotHeight);
        Debug.Log("Parent name: " + parent.gameObject.name);
        parent.gameObject.GetComponent<InventoryController>().AddInventorySlot(slot);
    }
}
