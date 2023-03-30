using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace List.View
{
	/// <summary>
	/// Component for making a UI GO draggable.
	/// </summary>
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        [SerializeField] GameObject placeholderPrefab;
        [SerializeField] Image image;

        private Transform originalParent = null;
        private int originalIndex;
        private bool returnToOriginal = false;
        private Vector2 diff;

        static GameObject placeholder = null;

        static Color draggedColor = new Color(.80f, .80f, .80f, 1f);
        Color defaultColor;
        Coroutine moveToPlaceholderCoroutine;

        void Start()
        {
            defaultColor = image.color;

            if (placeholder == null)
            {
                placeholder = Instantiate(placeholderPrefab, transform.parent);
                placeholder.SetActive(false);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (moveToPlaceholderCoroutine is not null)
            {
                StopCoroutine(moveToPlaceholderCoroutine);
                moveToPlaceholderCoroutine = null;
            }

            originalParent = this.transform.parent;
            originalIndex = this.transform.GetSiblingIndex();

            // same location as the dragable
            placeholder.SetActive(true);
            placeholder.transform.SetParent(originalParent);
            placeholder.transform.SetSiblingIndex(originalIndex);

            Canvas canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));

            this.transform.SetParent(canvas.transform);
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;

            image.color = draggedColor;
            diff = transform.position - new Vector3(eventData.position.x, eventData.position.y, 0f);
        }

        public void OnDrag(PointerEventData eventData)
        {

            this.transform.position = eventData.position + diff;
            var parent = placeholder.transform.parent;
            if (parent != null && !returnToOriginal)
            {
                int newSiblingIndex = parent.childCount;
                Transform closestChild = null;
                float distance = float.PositiveInfinity;

                for (int i = 0; i < parent.childCount; i++)
                {
                    var child = parent.GetChild(i);
                    var tempDist = Vector3.Distance(child.position, transform.position);

                    if (tempDist < distance)
                    {
                        closestChild = child;
                        distance = tempDist;
                    }
                }

                newSiblingIndex = closestChild.GetSiblingIndex();
                placeholder.transform.SetSiblingIndex(newSiblingIndex);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            StartCoroutine(MoveToPlaceholder());
        }

        public void EnteringDropZone(Transform dropZone)
        {
            returnToOriginal = false;
            placeholder.transform.SetParent(dropZone);
        }

        public void LeavingDropZone(Transform dropZone)
        {
            placeholder.transform.SetParent(originalParent);
            returnToOriginal = true;
        }

        public ElementView GetElementView()
        {
            return gameObject.GetComponent<ElementView>();
        }

        IEnumerator MoveToPlaceholder()
        {
            var placeholderPosition = placeholder.transform.position;
            var threshold = 5f;

            while (Vector3.Distance(placeholderPosition, transform.position) > threshold)
            {
                var newPosition = Vector3.Lerp(transform.position, placeholderPosition, 15f * Time.deltaTime);

                transform.position = newPosition;
                yield return null;
            }

            SetPositionToPlaceholder();
        }

        void SetPositionToPlaceholder()
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            this.transform.SetParent(placeholder.transform.parent);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

            Canvas canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
            placeholder.transform.SetParent(canvas.transform);
            placeholder.SetActive(false);

            image.color = defaultColor;
        }
    }
}