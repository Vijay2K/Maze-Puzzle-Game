using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}

public class Maze : MonoBehaviour
{
    [SerializeField] private GameObject cam;

    public int width, depth;
    public float scaleVal = 1;
    public byte[,] map;
    public Color wallColor;

    private void Start()
    {
        width = PlayerPrefs.GetInt("width", width);
        depth = PlayerPrefs.GetInt("depth", depth);

        if(PlayerPrefs.GetInt("levelcount") % 3 == 0)
        {
            width += 2;
            depth += 2;

            PlayerPrefs.SetInt("width", width);
            PlayerPrefs.SetInt("depth", depth);
        }

        int x = width / 2;
        int z = depth / 2;

        cam.transform.position = new Vector3(x, 0, z);

        InitializeMaze();
        GenerateMaze();
        DrawMaze();
    }

    private void InitializeMaze()
    {
        map = new byte[width, depth];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                map[x, z] = 1;
            }
        }
    }

    public virtual void GenerateMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (Random.Range(0, 100) < 50)
                    map[x, z] = 0;
            }
        }
    }

    private void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if(map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * scaleVal, 0, z * scaleVal);
                    GameObject walls = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    walls.transform.position = pos;
                    walls.transform.localScale = Vector3.one * scaleVal;
                    walls.transform.GetComponent<Renderer>().material.color = wallColor;
                }
            }
        }
    }

    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;

        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
            return 5;

        if (map[x + 1, z] == 0) count++;
        if (map[x - 1, z] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        if (map[x, z - 1] == 0) count++;

        return count;
    }

    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;

        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
            return 5;

        if (map[x + 1, z + 1] == 0) count++;
        if (map[x - 1, z + 1] == 0) count++;
        if (map[x - 1, z - 1] == 0) count++;
        if (map[x + 1, z - 1] == 0) count++;

        return count;
    }

    public int CountAllNeighbours(int x, int z)
    {
        return CountSquareNeighbours(x, z) + CountDiagonalNeighbours(x, z);
    }
}
