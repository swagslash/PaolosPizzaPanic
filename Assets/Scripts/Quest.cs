using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private List<Transform> goalLocations;
    
    // not used in GUI, ignore for now
    public List<string> goalNames;

    public TextMeshProUGUI goalText;
    
    public TextMeshProUGUI bestScoreText;

    private float bestTime; 
    
    public Goal goalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        bestTime = Single.PositiveInfinity;
        goalLocations = new List<Transform>();
        foreach (var componentInChild in GetComponentsInChildren<Transform>())
        {
            if(componentInChild.position == this.transform.position) continue;
            goalLocations.Add(componentInChild);
            goalNames.Add(componentInChild.name);
        }
    }

    public List<Goal> ActivateQuest(Stopwatch stopwatchComponent)
    {
        List<Goal> instantiatedGoals = new List<Goal>();
        stopwatchComponent.StopWatchReset();
        stopwatchComponent.StopWatchStart();
        for (var index = 0; index < goalLocations.Count; index++)
        {
            var t = goalLocations[index];
            var goal = Instantiate(goalPrefab, t.position, Quaternion.identity);
            goal.name = goalNames[index];
            instantiatedGoals.Add(goal);
        }

        DisableQuest(); // so we can only do it once
        return new List<Goal>(instantiatedGoals);
    }

    // Update is called once per frame
    void DisableQuest()
    {
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    
    void EnableQuest()
    {
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    public void Complete(float time)
    {
        Debug.Log("Completed quest in " + time);
        EnableQuest();
        if (time < bestTime)
        {
            bestTime = time;
            bestScoreText.text = this.name + " - Best time: " + Stopwatch.formatTime(bestTime);
        }
    }
}
