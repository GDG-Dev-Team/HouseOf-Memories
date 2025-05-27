using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PopupManager : MonoBehaviour
{

    [SerializeField]public GameObject popupPanel;

    private bool isPopupOpen = false;

    void Update()
    {
        // Show popup when pressing P
        if (Input.GetKeyDown(KeyCode.P))
        {
            popupPanel.SetActive(true);
            isPopupOpen = true;
        }

        // Hide popup if clicking outside
        if (isPopupOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(popupPanel))
            {
                popupPanel.SetActive(false);
                isPopupOpen = false;
            }
        }
    }
    //no comment
    // Checks if the pointer is over the popup panel or its children
    private bool IsPointerOverUIObject(GameObject target)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var result in raycastResults)
        {
            if (result.gameObject == target || result.gameObject.transform.IsChildOf(target.transform))
                return true;
        }

        return false;
    }
}
