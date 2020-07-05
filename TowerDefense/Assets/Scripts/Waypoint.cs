using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int _gridSize = 10;

    public bool IsExplored = false;
    [SerializeField] public Waypoint ExploredFrom;
    [SerializeField] public bool CanWalkOver = true;


    public int GetGridSize()
    {
        return _gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / _gridSize),
            Mathf.RoundToInt(transform.position.z / _gridSize));
    }

    public void SetTopColor(Color color)
    {
        //var topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();

        //topMeshRenderer.material.color = color;
    }
    private void Update()
    {
        if(IsExplored)
        {
            SetTopColor(Color.black);
        }
    }
}
