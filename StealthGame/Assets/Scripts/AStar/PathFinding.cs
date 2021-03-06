﻿using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    private List<Node> openList;
    private List<Node> closeList;
    private Grid grid;
    private int distance = 10;
    private int distanceDiag = 14;

    public List<Node> FindingPath(Node startNode, Node endNode)
    {
        // Checking if the destination is reachable
        if (endNode.IsWalkable == false)
        {
            return null;
        }

        List<Node> path = new List<Node>();
        openList = new List<Node>();
        closeList = new List<Node>();

        Node currentNode = null;

        startNode.GCost = 0;
        startNode.HCost = manhattanDistance(startNode, endNode);
        startNode.Parent = null;
        openList.Add(startNode);

        int iterations = 0;

        while (openList.Count > 0)
        {
            iterations++;
            // Get next current node.
            currentNode = getNextNode();

            // Checking if target is reached and terminating loop
            if (currentNode == endNode)
            {
                //pathSuccess = true;
                break;
            }

            closeList.Add(currentNode);

            List<Node> neigbours = grid.GetNeighbours(currentNode);
            for (int i = 0; i < neigbours.Count; i++)
            {
                if (closeList.Contains(neigbours[i]))
                    continue;

                if (!neigbours[i].IsWalkable)
                {
                    closeList.Add(neigbours[i]);
                }
                else
                {
                    // Calculating g-cost for current neighbour.
                    int gCost = getDistance(currentNode, neigbours[i]) + currentNode.GCost;

                    // If h-cost is zero (g-cost = f-cost), h-cost is calculated.
                    if (neigbours[i].GCost == neigbours[i].FCost)
                    {
                        neigbours[i].HCost = manhattanDistance(neigbours[i], endNode);
                    }


                    if (openList.Contains(neigbours[i]))
                    {
                        if (gCost < neigbours[i].GCost)
                        {
                            neigbours[i].GCost = gCost;
                            neigbours[i].Parent = currentNode;
                        }
                    }
                    else
                    {
                        neigbours[i].GCost = gCost;
                        neigbours[i].Parent = currentNode;
                        openList.Add(neigbours[i]);
                    }
                }
            }
        }

        Node n = endNode;
        while (n != null)
        {
            path.Add(n);
            n = path[path.Count - 1].Parent;
        }

        return path;
    }


    // Returns Node with the lowest f-cost of the openlist and deletes it.
    private Node getNextNode()
    {
        int lowestFCost = openList[0].FCost;
        int index = 0;
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].FCost < lowestFCost)
            {
                lowestFCost = openList[i].FCost;
                index = i;
            }
        }
        Node next = openList[index];
        openList.RemoveAt(index);
        return next;
    }

    // Returns estimated distance between 'start' and 'end'.
    private int manhattanDistance(Node start, Node end)
    {
        return Mathf.Abs((int)start.GridPosition.x - (int)end.GridPosition.x) + Mathf.Abs((int)start.GridPosition.y - (int)end.GridPosition.y);
    }

    // Returnes distances between node 'a' and 'b'.
    private int getDistance(Node a, Node b)
    {
        float distanceX = a.GridPosition.x - b.GridPosition.x;
        float distanceY = a.GridPosition.y - b.GridPosition.y;

        if (distanceX == 0.0f || distanceY == 0.0f)
            return distance;
        else
            return distanceDiag;
    }

    // Use this for initialization
    void Start()
    {
        grid = GameObject.Find("A*").GetComponent<Grid>();
        openList = new List<Node>();
        closeList = new List<Node>();
    }
}
