using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    private RectTransform _rectTransform;
    private bool _isDragging;
    private Vector2 _offsetVector;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();  
        _isDragging = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canDrag())
        {
            Vector2 mousePos = Input.mousePosition;
            _offsetVector = mousePos - _rectTransform.anchoredPosition;
            _isDragging = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            Vector2 mousePos = Input.mousePosition;
            _rectTransform.anchoredPosition = mousePos - _offsetVector;
        }
    }

    private bool canDrag() 
    { 
        Vector2 mousePos = Input.mousePosition;
        mousePos -= new Vector2(Screen.width/2, Screen.height/2);
        float rectMinX = _rectTransform.anchoredPosition.x - _rectTransform.sizeDelta.x/2;
        float rectMinY = _rectTransform.anchoredPosition.y - _rectTransform.sizeDelta.y/2;
        float rectMaxX = _rectTransform.anchoredPosition.x + _rectTransform.sizeDelta.x/2;
        float rectMaxY = _rectTransform.anchoredPosition.y + _rectTransform.sizeDelta.y/2;
        
        return ((mousePos.x >= rectMinX && mousePos.x <= rectMaxX) && (mousePos.y >= rectMinY && mousePos.y <= rectMaxY));
    }
}
