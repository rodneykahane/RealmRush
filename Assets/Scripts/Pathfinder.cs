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


    Vector2Int[] directions = {

       Vector2Int.up,
       Vector2Int.right,
       Vector2Int.down,
       Vector2Int.left
       
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();
        //ExploreNeighors();
    }

    private void Pathfind()
    {

        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            print("Searching from: " + searchCenter); // todo remove later
            HaltIfEndFound(searchCenter);
            ExploreNeighors(searchCenter);
        }

        print("finished pathfinding?");
        
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == endWaypoint)
        {
            Debug.Log("Searching from end node, quitting");
            isRunning = false;            
        }
        else
        {

            //print("doing stuff");
        }
    }

    private void ExploreNeighors(Waypoint from)
    {
        if (!isRunning)
        {
            return;  //exit if isRunning is false
        }
        foreach(Vector2Int direction in directions)
        {           
            Vector2Int neighborCoordinates = startWaypoint.GetGridPos() + direction;
            
            try
            {
                Waypoint neighbor = grid[neighborCoordinates];
                neighbor.SetTopColor(Color.blue);
                queue.Enqueue(neighbor);
                print("Queuing " + neighbor);
            }
            catch
            {
                // do nothing
                //Debug.Log("non existent waypoint");
            }

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
