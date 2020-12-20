using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDetector : MonoBehaviour
{
    public PlayerCarScript player;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("RESET DETECTION");
        player.resetPosition();
    }
}
