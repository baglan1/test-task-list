using UnityEngine;
using UnityEngine.EventSystems;

namespace List.View
{
    /// <summary>
    /// Component for a GO that acts as a proxy for DropZones.
    /// </summary>
    public class DropZoneSupport : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] DropZone dropZone;

        /// <inheritdoc />
        public void OnPointerEnter(PointerEventData eventData)
        {
            dropZone.OnPointerEnter(eventData);
        }

        /// <inheritdoc />
        public void OnPointerExit(PointerEventData eventData)
        {
            dropZone.OnPointerExit(eventData);
        }
    }
}