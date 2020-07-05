using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint StartWayPoint, EndWayPoint;

    Dictionary<Vector2Int, Waypoint> Grid = new Dictionary<Vector2Int, Waypoint>();

    Queue<Waypoint> queue = new Queue<Waypoint>();

    private bool IsRunning;

    Waypoint SearchCenter;

    private List<Waypoint> Path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {

        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
        return Path;
    }

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    private void CreatePath()
    {
        Path.Add(EndWayPoint);
        Waypoint previous = EndWayPoint.ExploredFrom;

        while (previous != StartWayPoint)
        {
            Path.Add(previous);
            previous = previous.ExploredFrom;
        }
        Path.Add(StartWayPoint);

        Path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        IsRunning = true;
        queue.Enqueue(StartWayPoint);
        while (queue.Count > 0)
        {
            SearchCenter = queue.Dequeue();
            ExploreNeighbour();
            HaltIfEndIsFound();
        }
    }

    private void HaltIfEndIsFound()
    {
        if (SearchCenter == EndWayPoint)
        {
            IsRunning = false;
            print("Found Endpoint ");
        }
    }

    private void ExploreNeighbour()
    {
        if (!IsRunning)
            return;

        foreach (var direction in directions)
        {
            var exploredCoords = SearchCenter.GetGridPos() + direction;
            if (Grid.ContainsKey(exploredCoords))
            {
                QueueNewNeighbour(exploredCoords);
            }
                
        }
        SearchCenter.IsExplored = true;
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoords)
    {
        Waypoint neighbour = Grid[neighbourCoords];

        if (neighbour.IsExplored || queue.Contains(neighbour) || neighbour.CanWalkOver == false)
            return;

        queue.Enqueue(neighbour);
        neighbour.ExploredFrom = SearchCenter;
    }

    private void ColorStartAndEnd()
    {
    //    StartWayPoint.SetTopColor(Color.green);
    //    EndWayPoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            var waypointGridPos = waypoint.GetGridPos();

            bool isOverlapping = Grid.ContainsKey(waypointGridPos);

            if (isOverlapping)
            {
                Debug.LogWarning("Overlapping Grid" + waypoint);
            }
            else
            {
                Grid.Add(waypointGridPos, waypoint);
                waypoint.SetTopColor(Color.grey);
            }

        }
    }
}
