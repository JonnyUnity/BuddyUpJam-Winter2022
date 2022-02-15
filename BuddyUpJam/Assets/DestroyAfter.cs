using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 5f);
        Invoke("FadeOut", 4f);
    }


    private void FadeOut()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        var args = new Hashtable
        {
            { "from", 1f },
            { "to", 0f },
            { "time", 1f  },
            { "onupdate", "ChangeGroupAlpha" },
            { "oncomplete", "DoDestroy" }
        };

        iTween.ValueTo(gameObject, args);
    }

    private void ChangeGroupAlpha(float newValue)
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = newValue;
    }

    private void DoDestroy()
    {
        Destroy(gameObject);
    }

}
