using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    public float TotalSeconds;
    public float MaxIntensity;
    public float InitialDelay;
    private Light _light;
    private float _waitTime;
    private bool _flash;
    
    void Start()
    {
        _light = GetComponent<Light>();
        _waitTime = TotalSeconds / 2;

        Invoke(nameof(FlashDelayed), InitialDelay);
    }
    
    void Update()
    {
        if (_flash)
        {
            StartCoroutine(nameof(Flash));
            _flash = false;
        }
    }

    void FlashDelayed()
    {
        _flash = true;
    }

    public IEnumerator Flash()
    {
        while (_light.intensity < MaxIntensity)
        {
            _light.intensity += Time.deltaTime / _waitTime;
            yield return null;
        }

        while (_light.intensity > 0)
        {
            _light.intensity -= Time.deltaTime / _waitTime;
            yield return null;
        }
        
        Invoke(nameof(FlashDelayed), 3);

        yield return null;
    }
}
