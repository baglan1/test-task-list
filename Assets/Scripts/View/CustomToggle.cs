using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] Sprite trueSprite;
    [SerializeField] Sprite falseSprite;

    [SerializeField] UnityEvent OnValueTrueEvent = new UnityEvent();
    [SerializeField] UnityEvent OnValueFalseEvent = new UnityEvent();
    bool currentValue = true;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = currentValue ? trueSprite : falseSprite;

        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        if (currentValue) 
            OnValueTrueEvent.Invoke();
        else 
            OnValueFalseEvent.Invoke();

        currentValue = !currentValue;
        image.sprite = currentValue ? trueSprite : falseSprite;
    }
}
