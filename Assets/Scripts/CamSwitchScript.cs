using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamSwitchScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<CinemachineVirtualCamera> cameras;
    public int activeCamIndex;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            activeCamIndex = (activeCamIndex + 1) % cameras.Count;
            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].gameObject.SetActive(i == activeCamIndex);
            }
        }
    }
}
