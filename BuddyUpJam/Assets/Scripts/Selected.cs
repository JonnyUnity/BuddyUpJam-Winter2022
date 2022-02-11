using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selected : MonoBehaviour
{

    private Renderer _renderer;
    private Color _origColor;
    private Material _material;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _origColor = _renderer.material.color;
        _material = _renderer.material;

        _propertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_propertyBlock);    
    }

    public void Reset()
    {
        _renderer.material.color = _origColor;
    }

}
