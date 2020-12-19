using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [HideInInspector]
    public Goal nextGoal;

    public bool isEndGoal()
    {
        return nextGoal == null;
    }
}
