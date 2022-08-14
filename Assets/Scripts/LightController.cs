using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public enum Distance { Off, Low, Medium, High, VeryHigh }
    public enum Intensity { Off, Low, High }

    [SerializeField]
    private float tweenDuration = 1.0f;

    private const float LOW_RADIUS_VALUE = 1f;
    private const float MEDIUM_RADIUS_VALUE = 2.5f;
    private const float HIGH_RADIUS_VALUE = 3.5f;
    private const float VERY_HIGH_RADIUS_VALUE = 5f;

    private const float LOW_INTENSITY_VALUE = 0.6f;
    private const float HIGH_INTENSITY_VALUE = 1f;

    private float fromValue = 0f;
    private float toValue = 1f;
    private Light2D _light;

    private void Awake()
    {
        _light = gameObject.GetComponent<Light2D>();
    }

    public void SetOuterRadius(Distance distance)
    {
        toValue = GetRadius(distance);
        _light.pointLightOuterRadius = toValue;
    }

    public void SetIntensity(Intensity intensity)
    {
        toValue = GetIntensity(intensity);
        _light.intensity = toValue;
    }

    public void TweenOuterRadius(Distance distance)
    {
        fromValue = _light.pointLightOuterRadius;
        toValue = GetRadius(distance);

        StartCoroutine(LerpValue(toValue, result => _light.pointLightOuterRadius = result));
    }

    public void TweenIntensity(Intensity intensity)
    {
        fromValue = _light.intensity;
        toValue = GetIntensity(intensity);

        StartCoroutine(LerpValue(toValue, result => _light.intensity = result));
    }

    private float GetRadius(Distance distance)
    {
        float radius = 1f;
        if (distance == Distance.Off)
            radius = 0f;
        else if (distance == Distance.Low)
            radius = LOW_RADIUS_VALUE;
        else if (distance == Distance.Medium)
            radius = MEDIUM_RADIUS_VALUE;
        else if (distance == Distance.High)
            radius = HIGH_RADIUS_VALUE;
        else if (distance == Distance.VeryHigh)
            radius = VERY_HIGH_RADIUS_VALUE;
        return radius;
    }

    private float GetIntensity(Intensity intensity)
    {
        float value = 1f;
        if (intensity == Intensity.Off)
            value = 0f;
        else if (intensity == Intensity.Low)
            value = LOW_INTENSITY_VALUE;
        return value;
    }

    private IEnumerator LerpValue(float toValue, Action<float> result)
    {
        float timeElapsed = 0;

        while (timeElapsed < tweenDuration)
        {
            float value = Mathf.Lerp(fromValue, toValue, timeElapsed / tweenDuration);
            result(value);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _light.pointLightOuterRadius = toValue;
    }
}
