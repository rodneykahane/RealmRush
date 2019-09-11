using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();  


    Vector2Int[] directions = {

       Vector2Int.up,
       Vector2Int.right,
       Vector2Int.down,
       Vector2Int.left
       
    };

    public List<Waypoint> GetPath()
    {
        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatPath();
        return path;
    }

    private void CreatPath()
    {
        path.Add(endWaypoint);
        
        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);        
        path.Reverse();        
    }

    /*
    //added this to add back colors for start/end cubes
    private void Update()
    {
        ColorStartAndEnd();
    }
    */

    private void BreadthFirstSearch()
    {

        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)        
        {
            searchCenter = queue.Dequeue();
            Debug.Log("Searching from: " + searchCenter);
            HaltIfEndFound();
            ExploreNeighors();
            searchCenter.isExplored = true;
        }

        print("finished pathfinding?");        
        
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            Debug.Log("Searching from end node, quitting");
            isRunning = false;            
        }
        
    }

    private void ExploreNeighors()
    {
        if (!isRunning)
        {
            return;  //exit if isRunning is false
        }
        foreach(Vector2Int direction in directions)
        {           
            Vector2Int neighborCoordinates = searchCenter.GetGridPos() + direction;
            
            try
            {
                QueueNewNeighbors(neighborCoordinates);
            }
            catch
            {
                // do nothing
                //Debug.Log("non existent waypoint");
            }

        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoordinates)
    {
        Waypoint neighbor = grid[neighborCoordinates];
        if (neighbor.isExplored || queue.Contains(neighbor))
        {


        }
        else
        { 
            //neighbor.SetTopColor(Color.blue);  //moving this to Waypoint.cs
            queue.Enqueue(neighbor);
            Debug.Log("Queueing " + neighbor);
            neighbor.exploredFrom = searchCenter;
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.cyan);
    }

    private void LoadBlocks()
    {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {            
            var gridPos = waypoint.GetGridPos();
            
            if (grid.ContainsKey(gridPos))
            {
                Debug.Log("Skipping overlapping block + " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
                
            }
            
        }
        
    }

}
