using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;

    private UIManager _uIManager;
    private GameObject _inventoryWindow;

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            if(_inventoryWindow != null) 
            {
                GameObject.Destroy(_inventoryWindow);
                _inventoryWindow = null;
            }
            _inventoryWindow = _uIManager.CreateInventoryWindow(Input.mousePosition, _width, _height);
        }
    }
}
