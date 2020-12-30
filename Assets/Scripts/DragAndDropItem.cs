using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    private RectTransform _rectTransform;
    private bool _isDragging;
    private bool _canDrag;
    private Vector2 _offsetVector;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();  
        if (_rectTransform == null) 
        {
            Debug.Log("Rect transform is null!");
        }
        _isDragging = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canDrag)
        {
            Vector2 cursorPos =  Util.ConvertMousePosToCursorPos(Input.mousePosition); 
            _offsetVector = cursorPos - _rectTransform.anchoredPosition;
            _isDragging = true;

            if (GetComponent<InventoryItem>() != null && GetComponent<InventoryItem>().isPicked)
            {
                GetComponent<InventoryItem>().Drop();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            _isDragging = false;
            _canDrag = false;
        }

        if (_isDragging)
        {
            Vector2 cursorPos =  Util.ConvertMousePosToCursorPos(Input.mousePosition); 
            _rectTransform.anchoredPosition = cursorPos - _offsetVector;
        }
    }

    public void SetCanDrag(bool canDrag) 
    { 
        _canDrag = canDrag;
    }
}
