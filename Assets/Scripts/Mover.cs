using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public float snapDistance;
    public float rayLength = 0.5f;

    private Vector3 targetPos;
    private Vector3 startPos;
    private bool isMoving;
    private Maze maze;
    private DisplayGameCompletionPanel display;

    private List<MapLocation> directions = new List<MapLocation>()
    {
        new MapLocation(1, 0),
        new MapLocation(-1, 0),
        new MapLocation(0, 1),
        new MapLocation(0, -1)
    };

    private void Start()
    {
        display = GameObject.FindObjectOfType<DisplayGameCompletionPanel>();
        maze = GameObject.FindObjectOfType<Maze>();
    }

    private void Update()
    {
        if(FindEnd((int)transform.position.x, (int)transform.position.z) == 1)
        {
            display.DisplayPanel();
        }

        if(isMoving)
        {
            if(Vector3.Distance(startPos, transform.position) > 1f)
            {
                transform.position = targetPos;
                isMoving = false;
                return;
            }

            transform.position += (targetPos - startPos) * speed * Time.deltaTime;
            return;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            if (!Physics.Raycast(transform.position, Vector3.forward, rayLength))
            {
                targetPos = transform.position + Vector3.forward;
                startPos = transform.position;
                isMoving = true;
            }
        } else if(Input.GetKeyDown(KeyCode.S))
        {
            if(!Physics.Raycast(transform.position, Vector3.back, rayLength))
            {
                targetPos = transform.position + Vector3.back;
                startPos = transform.position;
                isMoving = true;
            }
        } else if(Input.GetKeyDown(KeyCode.D))
        {
            if(!Physics.Raycast(transform.position, Vector3.right, rayLength))
            {
                targetPos = transform.position + Vector3.right;
                startPos = transform.position;
                isMoving = true;
            }
        } else if(Input.GetKeyDown(KeyCode.A))
        {
            if(!Physics.Raycast(transform.position, Vector3.left, rayLength))
            {
                targetPos = transform.position + Vector3.left;
                startPos = transform.position;
                isMoving = true;
            }

        }
    }
    
    private int FindEnd(int x, int z)
    {
        int count = 0;

        for(int i = 0; i < directions.Count; i++)
        {
            int next_x = x + directions[i].x;
            int next_z = z + directions[i].z;

            if(next_x > 0 && next_x < maze.width - 1 && next_z > 0 && next_z < maze.depth - 1)
            {
                if (maze.map[next_x, next_z] == 2)
                    count++;
            }

        }

        return count;
    }

}
