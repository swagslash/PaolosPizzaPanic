using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerCarScript : MonoBehaviour
{

    public GameObject arrow;
    private List<Vector3> targets;
    //The interval you want your player to be able to fire.
    public float resetRate  = 5f;
 
    //The actual time the player will be able to fire.
    private float nextReset = 0;

    public Stopwatch stopwatchComponent;
    
    private Quest activeQuest = null;
    // Start is called before the first frame update
    void Start()
    {
        targets = new List<Vector3>();
    }

    private void OnEnable()
    {
        // convert list to queue?
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Quest>() != null && activeQuest == null)
        {
            // Collided with a questmarker!
            Debug.Log("Activating quest!");
            var quest = other.gameObject.GetComponent<Quest>();
            
            activeQuest = quest;
            var newGoals = quest.ActivateQuest(stopwatchComponent);
            foreach (var goal in newGoals)
            {
                targets.Add(goal.transform.position);
            }
        } else if (other.gameObject.GetComponent<Goal>() != null && activeQuest)
        {
            Debug.Log("Found goal");
            var goal = other.gameObject.GetComponent<Goal>();

            var remove = targets.Remove(goal.transform.position);
            Debug.Log(remove);

            if (targets.Count == 0)
            {
                // Reset active quest
                activeQuest.Complete(stopwatchComponent.time);
                activeQuest = null;
                stopwatchComponent.StopWatchStop();
            }
            GameObject.Destroy(goal.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Time.time > nextReset)
        {
            Debug.Log("Reset pressed");
            nextReset = Time.time + resetRate;
            resetPosition();
        }
    }

    public void resetPosition()
    {
        var rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        var transform1 = transform;
        transform1.rotation = Quaternion.identity;
        transform1.position += new Vector3(0,1,0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targets.Count > 0)
        {
            arrow.SetActive(true);
            arrow.transform.LookAt(findClosestPosition(targets));
        }
        else
        {
            arrow.SetActive(false);
        }
    }

    private Vector3 findClosestPosition(List<Vector3> transforms)
    {
        var minDist = Double.PositiveInfinity;
        Vector3 closestTransform = Vector3.zero;
        foreach (var target in transforms)
        {
            var dist = Vector3.Distance(transform.position, target);
            if (dist < minDist)
            {
                minDist = dist;
                closestTransform = target;
            }
        }

        return closestTransform;
    }
}
