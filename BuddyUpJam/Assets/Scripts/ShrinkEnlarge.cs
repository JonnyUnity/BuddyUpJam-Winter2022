using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkEnlarge : MonoBehaviour
{

    private bool _beenShrunk;
    private bool _beenEnlarged;

    private Transform _transform;
    private Vector3 _currentScale;

    private float _sizeFactor = 1f;

    [SerializeField] private float _maxSizeFactor = 2f;
    [SerializeField] private float _minSizeFactor = 0.5f;

    [SerializeField] private float _wiggleDuration = 0.25f;

    private PickUp _pickUp;

    private void Awake()
    {
        _transform = transform;
        _currentScale = _transform.localScale;
        _pickUp = GetComponent<PickUp>();

    }


    public void Shrink()
    {
        if (!_beenShrunk)
        {

            _currentScale = new Vector3(_currentScale.x * 0.5f, _currentScale.y * 0.5f, _currentScale.z * 0.5f);
            _transform.localScale = _currentScale;
            _beenEnlarged = false;
            _beenShrunk = true;
        }
    }

    public bool CanTransform(float changeInSize)
    {
        float newSizeFactor = _sizeFactor * changeInSize;

        return (_minSizeFactor <= newSizeFactor && newSizeFactor <= _maxSizeFactor);
    }

    public float GetSizeFactor()
    {
        return _sizeFactor;
    }

    public void SetNewSizeFactor(float sizeFactor)
    {
        _sizeFactor *= sizeFactor;
        if (_pickUp != null)
        {
            _pickUp.UpdatePickUpStatus(_sizeFactor);
        }
    }

    public IEnumerator TransformCoroutine(float transformFactor, float timeToTransform)
    {
        float timeElapsed = 0;

        _currentScale = _transform.localScale;

        while (timeElapsed < timeToTransform)
        {
            _transform.localScale = Vector3.Lerp(_currentScale, new Vector3(_currentScale.x * transformFactor, _currentScale.y * transformFactor, _currentScale.z * transformFactor), timeElapsed);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _currentScale = new Vector3(_currentScale.x * transformFactor, _currentScale.y * transformFactor, _currentScale.z * transformFactor);
        _transform.localScale = _currentScale;
        _sizeFactor = transformFactor;

    }

    public IEnumerator CantTransformWiggleCoroutine()
    {
        float timeElapsed = 0;
        Vector3 startPosition = _transform.position;

        while (timeElapsed < _wiggleDuration)
        {

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _transform.position = Vector3.Lerp(_transform.position, startPosition, 0.3f);
        

    }

    public void ShakeTest()
    {
        var args = new Hashtable();
        args.Add("x", 0.05f);
        args.Add("time", _wiggleDuration);

        iTween.ShakePosition(gameObject, args);


    }

    public void Enlarge(float timeToTransform, GameObject objectToDeselect)
    {

        Vector3 newScale = _transform.localScale;
        newScale *= 2f;

        var shrinkArgs = new Hashtable();
        shrinkArgs.Add("time", timeToTransform);
        shrinkArgs.Add("scale", newScale);
        shrinkArgs.Add("easetype", iTween.EaseType.easeOutElastic);
        shrinkArgs.Add("oncompletetarget", GameManager.Instance);
        shrinkArgs.Add("oncomplete", "ResetSelection");
        shrinkArgs.Add("oncompleteparams", objectToDeselect);


        iTween.ScaleTo(gameObject, shrinkArgs);
        _sizeFactor = 2f;

        if (!_beenEnlarged)
        {

            _currentScale = new Vector3(_currentScale.x * 2f, _currentScale.y * 2f, _currentScale.z * 2f);
            _transform.localScale = _currentScale;

            _beenShrunk = false;
            _beenEnlarged = true;
        }
    }

}
