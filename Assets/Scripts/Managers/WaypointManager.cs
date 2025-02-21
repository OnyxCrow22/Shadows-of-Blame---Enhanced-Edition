
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { ONEWAY, TWOWAY, TURNLEFT, TURNRIGHT, SLIP, NOTURNLEFT, NOTURNRIGHT }
    public GameObject lastNode, nextNode;
    public direction dirCheck;
}

public class WaypointManager : MonoBehaviour
{
    [Header("Waypoint References")]
    public GameObject[] waypoints;
    public GameObject[] pavementNodes;
    public List<Link> links = new List<Link>();
    public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        pavementNodes = GameObject.FindGameObjectsWithTag("PedestrianNodes");

        if (waypoints.Length > 0)
        {
            foreach (GameObject waypoints in waypoints)
            {
                graph.AddNode(waypoints);
            }
            foreach (Link l in links)
            {
                graph.AddEdge(l.lastNode, l.nextNode);
                if (l.dirCheck == Link.direction.TWOWAY)
                {
                    graph.AddEdge(l.lastNode, l.nextNode);
                }
            }
        }

        if (pavementNodes.Length > 0)
        {
            foreach (GameObject pavementN in pavementNodes)
            {
                graph.AddNode(pavementN);
            }
        }

        Linker();

        foreach (Link lk in links)
        {
            graph.AddEdge(lk.lastNode, lk.nextNode);
            if (lk.dirCheck == Link.direction.TWOWAY)
            {
                graph.AddEdge(lk.nextNode, lk.lastNode);
            }
        }
    }

    void Linker()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Link newLink = new Link();
            newLink.lastNode = waypoints[i];
            newLink.nextNode = waypoints[i + 1];
            newLink.dirCheck = Link.direction.TWOWAY;
            links.Add(newLink);
        }
    }
}
