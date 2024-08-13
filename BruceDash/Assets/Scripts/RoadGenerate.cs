using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerate : MonoBehaviour
{
    public GameObject[] RoadPieces = new GameObject[2];
    public float RoadLength = 100f; //length of roads

    public float RoadSpeed = 5f; //speed to scroll roads at

    void Update()
    {
        foreach (GameObject road in RoadPieces)
        {
            Vector3 newRoadPos = road.transform.position;
            newRoadPos.x -= RoadSpeed * Time.deltaTime;
            if (newRoadPos.x < -RoadLength / 2)
            {
                newRoadPos.x += RoadLength;
            }
            road.transform.position = newRoadPos;
        }
    }
}
