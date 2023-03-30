using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneSupport : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] DropZone dropZone;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dropZone.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropZone.OnPointerExit(eventData);
    }
}
