using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    public TextMeshProUGUI guiText;
    public float time;
    
    public void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void StopWatchStart()
    {
        StartCoroutine("StopWatch");
    }
    public void StopWatchStop()
    {
        StopCoroutine("StopWatch");
    }
    public void StopWatchReset()
    {
        time = 0;
        guiText.text = "00:00:00";
    }

    IEnumerator StopWatch()
    {
        while(true)
        {
            time += Time.deltaTime;
            guiText.text = formatTime(time);

            yield return null;
        }
    }

    public static string formatTime(float time)
    {
        var msec = (int)((time - (int)time) * 100);
        var sec = (int)(time % 60);
        var min = (int)(time / 60 % 60);

        return string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
    }
}
