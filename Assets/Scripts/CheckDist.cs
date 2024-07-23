using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckDist : MonoBehaviour
{
    [SerializeField] GameObject Car;
    [SerializeField] GameObject Wall;

    [SerializeField] Light[] _lights;
    [SerializeField] Text _distText;
    [SerializeField] float _holdTime = 0.3f;
    [SerializeField] float _distanceChecker = 10f;

    [HideInInspector]
    public float _distance;

    float _intensity;
    float _timeLeft;
    bool _lightUp;
    // Start is called before the first frame update
    void Start()
    {
        _lightUp = false;
        _intensity = 0f;
    }

    private void LateUpdate()
    {
        // _distance = Vector3.Distance(Car.transform.position, Wall.transform.position); 
        _distance = ConnectionArdunio.instance._pDistance;
        _distText.text = _distance.ToString();

        if (_distance < _distanceChecker)
        {
            _lightUp = true;
        }
        else
        {
            _lightUp = false;
        }

        if (_lightUp)
        {

            _intensity = Mathf.Lerp(0f, 10f, (Mathf.Sin(Time.time * _holdTime) + 1f) / 2f);
        }
        else
        {
            _intensity = Mathf.MoveTowards(_intensity, 0f, 0.8f);
        }

        foreach (var _light in _lights)
        {
            _light.intensity = _intensity;
        }
    }
}
