using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// 2D 빛 깜빡임 이펙트
public class LightEffect : MonoBehaviour
{
    private Light2D light;
    private float originIntensity; // 기존 조명 밝기


    private void Start()
    {
        light = GetComponent<Light2D>();
        originIntensity = light.intensity;

        StartCoroutine(LightOff());
    }

    IEnumerator LightOff()
    {
        while (true)
        {
            float lightDuration = Random.Range(0.5f, 1.5f);

            light.intensity = originIntensity;
            yield return new WaitForSeconds(lightDuration);
            light.intensity = 0;
            yield return new WaitForSeconds(0.3f);
        }
    }
}