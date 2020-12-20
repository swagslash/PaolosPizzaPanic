using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    public float TotalSeconds;
    public float MaxIntensity;
    public float InitialDelay;
    public float CycleTime = 3;
    private Light _light;
    private float _waitTime;
    private bool _flash;
    
    void Start()
    {
        _light = GetComponent<Light>();

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
        _waitTime = TotalSeconds / 2;
        
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
        
        Invoke(nameof(FlashDelayed), CycleTime);

        yield return null;
    }
}
