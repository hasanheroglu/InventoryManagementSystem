using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private GameObject _inventorySlot;
    [SerializeField]
    private GameObject _inventoryWindow;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
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
        for (var i=0; i<width; i++) {
            for (var j=0; j<height; j++) { 
                CreateInventorySlot(i, j, window.transform);
            }
        }
        return window;
    }

    public void CreateInventorySlot(int xPos, int yPos, Transform parent) 
    { 
        GameObject slot = GameObject.Instantiate(_inventorySlot, parent);
        RectTransform slotRect = slot.GetComponent<RectTransform>();
        float slotWidth = slotRect.rect.width;
        float slotHeight = slotRect.rect.height;
        slotRect.anchoredPosition = new Vector2(xPos*slotWidth, yPos*slotHeight);
    }
}
