using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light_Shake : MonoBehaviour
{
    private Light2D light;
    [SerializeField] private float radius;
    public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            radius = value;
        }
    }

    [SerializeField] private float fr = 20;
    [SerializeField] private float am = 0.1f;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    protected virtual void SetInitialReferences()
    {
        light = GetComponent<Light2D>();
        radius = light.pointLightOuterRadius;
    }

    private void Update()
    {
        float r = radius + Mathf.Sin(Time.time * fr) * am;
        light.pointLightOuterRadius = r;
        light.pointLightInnerRadius = r * 0.25F;
    }
}
