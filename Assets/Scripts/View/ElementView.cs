using List.Model;
using TMPro;
using UnityEngine;

namespace List.View
{
    /// <summary>
    /// View component of the element.
    /// </summary>
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
        
        /// <summary>
        /// Sets the model of the view and updates the view.
        /// </summary>
        public void SetElement(MyElement element)
        {
            var text = element.Text + " : " + element.Number.ToString();

            textComponent.text = text;

            this.element = element;
        }
    }
}