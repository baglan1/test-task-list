using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace List.View
{
    /// <summary>
    /// Custom component for making toggle out of button.
    /// </summary>
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] Image image;
        [SerializeField] Sprite trueSprite;
        [SerializeField] Sprite falseSprite;

        // Toggle value change events
        [SerializeField] UnityEvent OnValueTrueEvent = new UnityEvent();
        [SerializeField] UnityEvent OnValueFalseEvent = new UnityEvent();

        bool currentValue = true;

        // Start is called before the first frame update
        void Start()
        {
            image.sprite = currentValue ? trueSprite : falseSprite;

            button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            // invoke the current value event first
            if (currentValue)
                OnValueTrueEvent.Invoke();
            else
                OnValueFalseEvent.Invoke();

            currentValue = !currentValue;
            image.sprite = currentValue ? trueSprite : falseSprite;
        }
    }
}