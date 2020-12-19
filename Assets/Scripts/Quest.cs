using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<Transform> goalLocations;
    public List<string> goalNames;
    
    private List<Goal> instantiatedGoals;
    
    public Goal goalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.goalLocations.Add(this.transform);
        instantiatedGoals = new List<Goal>();
    }

    public List<Goal> ActivateQuest(Stopwatch stopwatchComponent)
    {
        stopwatchComponent.StopWatchStart();
        foreach (var t in goalLocations)
        {
            var goal = Instantiate(goalPrefab, t.position, Quaternion.identity);
            instantiatedGoals.Add(goal);
        }
        for (var i = 0; i < instantiatedGoals.Count - 1; i++)
        {
            instantiatedGoals[i].nextGoal = instantiatedGoals[i + 1];
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

    public void Complete()
    {
        Debug.Log("Completed quest");
    }
}
