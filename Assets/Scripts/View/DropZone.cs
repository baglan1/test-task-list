using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DropZone : MonoBehaviour, IDropHandler
{
    public UnityEvent<ElementView> OnLeaveEvent = new UnityEvent<ElementView>();
    public UnityEvent<ElementView> OnEnterEvent = new UnityEvent<ElementView>();

	public void OnPointerEnter(PointerEventData eventData) {
		if (eventData.pointerDrag == null) {
			return;
		}

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.EnteringDropZone (this.transform);
            OnEnterEvent.Invoke(d.GetElementView());
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {

		if (eventData.pointerDrag == null) {
			return;
		}

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.LeavingDropZone(this.transform);
            OnLeaveEvent.Invoke(d.GetElementView());
		}
	}
	
	public void OnDrop(PointerEventData eventData) {
		//Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

//		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		//if(d != null) {
		//	d.targetDropZone = this.transform;
//		}
	}
}