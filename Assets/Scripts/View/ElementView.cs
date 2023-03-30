using List.Model;
using TMPro;
using UnityEngine;

namespace List.View
{
    public class ElementView : MonoBehaviour
    {
        [SerializeField] TMP_Text textComponent;

        [SerializeField] Draggable draggable;

        MyElement element;
        public MyElement Element
        {
            get
            {
                return element;
            }
        }

        public void SetElement(MyElement element)
        {
            var text = element.Text + " : " + element.Number.ToString();

            textComponent.text = text;

            this.element = element;
        }

        public Draggable GetDraggable()
        {
            return draggable;
        }
    }
}