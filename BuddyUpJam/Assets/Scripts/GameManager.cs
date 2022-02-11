using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private Color _shrinkColor;
    [SerializeField] private Color _enlargeColor;
    [SerializeField] private float _timeToTransform;

    private GameObject _objectToShrink;
    private GameObject _shrinkAnchor;

    private GameObject _objectToEnlarge;
    private GameObject _enlargeAnchor;

    private bool enlargeRayFired;
    private bool shrinkRayFired;

    private PlayerController playerController;

    private void Awake()
    {
        var playerObj = GameObject.Find("Player");

        if (playerObj != null)
        {
            playerController = playerObj.GetComponent<PlayerController>();
        }       

    }

    public Color GetEnlargeColour()
    {
        return _enlargeColor;
    }

    public Color GetShrinkColour()
    {
        return _shrinkColor;
    }

    public Color GetCursorColour()
    {
        return enlargeRayFired ? _shrinkColor : _enlargeColor;
    }


    public Color SetSelection(GameObject selectedObject, GameObject objectAnchor)
    {
        Color colourToReturn;

        if (selectedObject == _objectToEnlarge)
        {
            colourToReturn = _enlargeColor;
            return colourToReturn;
        }

        if (_objectToEnlarge == null)
        {
            _objectToEnlarge = selectedObject;
            _enlargeAnchor = objectAnchor;

            colourToReturn = _enlargeColor;
            enlargeRayFired = true;
        }
        else
        {
            _objectToShrink = selectedObject;
            _shrinkAnchor = objectAnchor;

            colourToReturn = _shrinkColor;
            shrinkRayFired = true;

        }

        return colourToReturn;

    }


    private void DrawBeam()
    {
        Vector3[] linePositions = new Vector3[]
        {
            _objectToShrink.transform.position,
            _objectToEnlarge.transform.position
        };

    }

    public void DoSwapSize()
    {
        
        if (_objectToEnlarge != null && _objectToShrink != null)
        {
            ShrinkEnlarge se1 = _shrinkAnchor.GetComponent<ShrinkEnlarge>();
            ShrinkEnlarge se2 = _enlargeAnchor.GetComponent<ShrinkEnlarge>();

            // if either object has already been shrunk/enlarged before then it cannot be shrunk/enlarged again?
            
            if (se1.CanTransform(0.5f) && se2.CanTransform(2f))
            {
                Vector3 newScale = _shrinkAnchor.transform.localScale;
                newScale *= 0.5f;

                var shrinkArgs = new Hashtable();
                shrinkArgs.Add("time", _timeToTransform);
                shrinkArgs.Add("scale", newScale);
                shrinkArgs.Add("easetype", iTween.EaseType.easeOutElastic);
                shrinkArgs.Add("oncompletetarget", gameObject);
                shrinkArgs.Add("oncomplete", "ResetSelection");
                shrinkArgs.Add("oncompleteparams", _objectToShrink);


                iTween.ScaleTo(_shrinkAnchor, shrinkArgs);
                se1.SetNewSizeFactor(0.5f);

                newScale = _enlargeAnchor.transform.localScale;
                newScale *= 2f;

                shrinkArgs = new Hashtable
                {
                    { "time", _timeToTransform },
                    { "scale", newScale },
                    { "easetype", iTween.EaseType.easeOutElastic },
                    { "oncompletetarget", gameObject },
                    { "oncomplete", "ResetSelection" },
                    { "oncompleteparams", _objectToEnlarge }
                };


                iTween.ScaleTo(_enlargeAnchor, shrinkArgs);
                se2.SetNewSizeFactor(2f);


            }
            else
            {
                var args = new Hashtable
                {
                    { "x", 0.05f },
                    { "time", 0.5f },
                    { "oncompletetarget", gameObject },
                    { "oncomplete", "ResetSelection" },
                    { "oncompleteparams", _objectToShrink }
                };

                iTween.ShakePosition(_shrinkAnchor, args);

                args = new Hashtable
                {
                    { "x", 0.05f },
                    { "time", 0.5f },
                    { "oncompletetarget", gameObject },
                    { "oncomplete", "ResetSelection" },
                    { "oncompleteparams", _objectToEnlarge }
                };

                iTween.ShakePosition(_enlargeAnchor, args);
            }

            _objectToShrink = null;
            _objectToEnlarge = null;
            enlargeRayFired = false;
            shrinkRayFired = false;
        }


    }

    public void ResetSelection(GameObject obj)
    {
        Debug.Log("Reset selection!");
        Selected s = obj.GetComponent<Selected>();
        s.Reset();

    }

    public void ClearSelections()
    {
        Selected s = _objectToShrink.GetComponent<Selected>();
        s.Reset();
        s = _objectToEnlarge.GetComponent<Selected>();
        s.Reset();


        _objectToShrink = null;
        _objectToEnlarge = null;
        _shrinkAnchor = null;
        _enlargeAnchor = null;
        enlargeRayFired = false;
        shrinkRayFired = false;
        
    }


}
