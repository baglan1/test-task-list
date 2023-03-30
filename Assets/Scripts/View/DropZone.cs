using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace List.View
{
    /// <summary>
    /// Component for a GO that is a parent for draggables.
    /// </summary>
    public class DropZone : MonoBehaviour, IDropHandler
    {
        /// Events that are invoked when a draggable has entered or existed the GO.
        public UnityEvent<ElementView> OnLeaveEvent = new UnityEvent<ElementView>();
        public UnityEvent<ElementView> OnEnterEvent = new UnityEvent<ElementView>();

        /// <inheritdoc />
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null)
            {
                d.EnteringDropZone(this.transform);
                OnEnterEvent.Invoke(d.GetElementView());
            }
        }

        /// <inheritdoc />
        public void OnPointerExit(PointerEventData eventData)
        {

            if (eventData.pointerDrag == null)
            {
                return;
            }

            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null)
            {
                d.LeavingDropZone(this.transform);
                OnLeaveEvent.Invoke(d.GetElementView());
            }
        }

        /// <inheritdoc />
        public void OnDrop(PointerEventData eventData)
        {
            
        }
    }
}