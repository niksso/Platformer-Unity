using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public enum ShakeMode {OnlyX, OnlyY, OnlyZ, XY, XZ, XYZ};

    private static Transform _transform;
    private static float _elapsed, _duration, _power, _percentComplete;
    private static ShakeMode _mode;
    private static Vector3 _originalPos;

    void Start()
    {
        _percentComplete = 1;
        _transform = GetComponent<Transform>();
    }

    public static void Shake(float duration, float power)
    {
        if(_percentComplete == 1) _originalPos = _transform.localPosition;
        _mode = ShakeMode.XYZ;
        _elapsed = 0;
        _duration = duration;
        _power = power;
    }

    public static void Shake(float duration, float power, ShakeMode mode)
    {
        if(_percentComplete == 1) _originalPos = _transform.localPosition;
        _mode = mode;
        _elapsed = 0;
        _duration = duration;
        _power = power;
    }

    void Update()
    {
        if(_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            _percentComplete = _elapsed / _duration;
            _percentComplete = Mathf.Clamp01(_percentComplete);
            Vector3 randomVector = Random.insideUnitSphere * _power * (1f - _percentComplete);

            switch(_mode)
            {
                case ShakeMode.XYZ:
                    _transform.localPosition = _originalPos + randomVector;
                    break;
                case ShakeMode.OnlyX:
                    _transform.localPosition = _originalPos + new Vector3(randomVector.x, 0, 0);
                    break;
                case ShakeMode.OnlyY:
                    _transform.localPosition = _originalPos + new Vector3(0, randomVector.y, 0);
                    break;
                case ShakeMode.OnlyZ:
                    _transform.localPosition = _originalPos + new Vector3(0, 0, randomVector.z);
                    break;
                case ShakeMode.XY:
                    _transform.localPosition = _originalPos + new Vector3(randomVector.x, randomVector.y, 0);
                    break;
                case ShakeMode.XZ:
                    _transform.localPosition = _originalPos + new Vector3(randomVector.x, 0, randomVector.z);
                    break;
            }
        }
    }
}
