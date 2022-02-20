using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selected : MonoBehaviour
{

    private Renderer _renderer;
    private Color _origColor;
    private Material _material;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();   
    }

    public void Reset()
    {
        //_renderer.material.color = _origColor;
        _renderer.material.SetFloat("_ShowOutline", 0f);
        _renderer.material.SetFloat("_ShowHighlight", 0f);
    }

}
