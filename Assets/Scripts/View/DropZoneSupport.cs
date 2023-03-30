using UnityEngine;
using UnityEngine.EventSystems;

namespace List.View
{
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
}