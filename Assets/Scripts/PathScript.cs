﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    /**
     * Waypoints absolute in world space
     */
    public List<Vector3> Waypoints = new List<Vector3>();

    public float Speed;
    public float TurningSpeed;

    // Index for the next waypoint
    private int _nextWaypointIndex;
    
    // Rotation
    private Quaternion _rotation;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Waypoints[_nextWaypointIndex]) < 0.001f)
        {
            _nextWaypointIndex = (_nextWaypointIndex + 1) % Waypoints.Count;
            _direction = (Waypoints[_nextWaypointIndex] - transform.position).normalized;
        }

        transform.position = Vector3.MoveTowards(transform.position, Waypoints[_nextWaypointIndex], Speed * Time.deltaTime);

        _rotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, TurningSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        foreach (var waypoint in Waypoints)
        {
            Gizmos.DrawSphere(waypoint, 0.1f);
        }
    }
}