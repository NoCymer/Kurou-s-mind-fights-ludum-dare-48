using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class LightRandom : MonoBehaviour
{
    Light2D light2d;
    float desiredIntensity;
    void Start()
    {
        light2d = GetComponent<Light2D>();
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {
        light2d.intensity = Mathf.Lerp(light2d.intensity, desiredIntensity, 0.1f);
    }
    IEnumerator Flicker()
	{
        yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
        desiredIntensity = Random.Range(0.9f, 1.1f);
        StartCoroutine(Flicker());
	}
}
