using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private LayoutElement _layoutElement;
    private Hashtable _enlargeArgs = new Hashtable
    {
        { "from", 1f },
        { "to", 2f },
        { "time", 1f  },
        { "onupdate", "ChangeFlexibleHeight" }
    };
    private Hashtable _shrinkArgs = new Hashtable
    {
        { "from", 2f },
        { "to", 1f },
        { "time", 0.5f  },
        { "onupdate", "ChangeFlexibleHeight" }
    };


    private void Awake()
    {
        _layoutElement = GetComponent<LayoutElement>();
    }


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        iTween.ValueTo(gameObject, _enlargeArgs);
    }


    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //iTween.ValueTo(gameObject, _shrinkArgs);
        iTween.Stop();
        _layoutElement.flexibleHeight = 1f;
    }


    private void ChangeFlexibleHeight(float height)
    {
        _layoutElement.flexibleHeight = height;
    }
}
