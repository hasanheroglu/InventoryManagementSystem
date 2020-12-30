using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{

    private RectTransform _rectTransform;
    private GraphicRaycaster _graphicRaycaster;
    private PointerEventData _pointerEventData;
    private EventSystem _eventSystem;

    private bool _isDragging;
    
    private GameObject _draggedObject;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = transform.parent.gameObject;
        GameObject eventSystem = GameObject.Find("EventSystem");
        _graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        _eventSystem = eventSystem.GetComponent<EventSystem>();
        _rectTransform = GetComponent<RectTransform>();
        _isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            List<RaycastResult> results = GetRaycastResults();

            GameObject topWindow = null;
            int index = -1;

            foreach (RaycastResult result in results)
            {
                GameObject window = result.gameObject.transform.gameObject;
                
                if (window.GetComponent<DragAndDropItem>() == null) 
                {
                    window = result.gameObject.transform.parent.gameObject;
                }

                if (window.GetComponent<DragAndDropItem>() == null)
                {
                    continue;
                }

                int siblingIndex = window.GetComponent<RectTransform>().GetSiblingIndex();
                
                if (siblingIndex > index) 
                {
                    topWindow = window;
                    index = siblingIndex;
                }
            }

            if (topWindow == null)  
            {
                return;
            }

            if (topWindow.GetComponent<DragAndDropItem>() != null) 
            {
                topWindow.GetComponent<DragAndDropItem>().SetCanDrag(true);
                topWindow.GetComponent<RectTransform>().SetAsLastSibling();
                _draggedObject = topWindow;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            List<RaycastResult> results = GetRaycastResults();

            GameObject topWindow = null;
            int index = -1;

            foreach (RaycastResult result in results)
            {
                GameObject window = result.gameObject.transform.gameObject;
                
                if (window.GetComponent<InventorySlot>() == null) 
                {
                    window = result.gameObject.transform.parent.gameObject;
                }

                if (window.GetComponent<InventorySlot>() == null)
                {
                    continue;
                }

                int siblingIndex = window.GetComponent<RectTransform>().GetSiblingIndex();
                Debug.Log("Sibling index: " + siblingIndex);

                if (siblingIndex > index) 
                {
                    topWindow = window;
                    index = siblingIndex;
                }
            }

            if (topWindow == null)  
            {
                Debug.Log("No inventory slot!");
                return;
            }
            
            InventoryItem inventoryItem = null;
            
            if (_draggedObject != null)
            {
                _draggedObject.transform.SetParent(transform.parent);
                inventoryItem = _draggedObject.GetComponent<InventoryItem>();
            }

            if (topWindow.GetComponent<InventorySlot>() != null && inventoryItem != null)   
            {
                Vector2 cursorPos = Util.ConvertMousePosToCursorPos(Input.mousePosition);
                Vector2 itemPos = _draggedObject.GetComponent<RectTransform>().anchoredPosition;
                Vector2 posDiff = cursorPos - itemPos;
                Vector2 startPos = topWindow.GetComponent<InventorySlot>().AreAdjacentInventorySlotsEmpty((int)posDiff.x/50, (int)posDiff.y/50, inventoryItem.GetWidth(), inventoryItem.GetHeight());
                if (startPos.x >= 0) 
                {
                    topWindow.GetComponent<InventorySlot>().FillInventorySlots((int)posDiff.x/50, (int)posDiff.y/50, inventoryItem.GetWidth(), inventoryItem.GetHeight());
                    _draggedObject.GetComponent<InventoryItem>().Pick(startPos/50, topWindow.gameObject.transform.parent.gameObject.GetComponent<InventoryController>());
                    _draggedObject.transform.SetParent(topWindow.gameObject.transform.parent);
                    _draggedObject.GetComponent<RectTransform>().anchoredPosition = startPos;
                }
            }
        }

        _rectTransform.anchoredPosition = Util.ConvertMousePosToCursorPos(Input.mousePosition);
    }

    private List<RaycastResult> GetRaycastResults() 
    {
        _pointerEventData = new PointerEventData(_eventSystem);
        _pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEventData, results);

        return results;
    }

}
