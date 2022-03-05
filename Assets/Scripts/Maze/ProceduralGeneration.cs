using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : Maze
{
    public override void GenerateMaze()
    {
        int x = 2;
        int z = 2;

        int end_x = 0, end_z = 0;

        map[x, z] = 0;

        Color[] colors =
        {
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.grey,
            Color.blue
        };

        int randomIndex = Random.Range(0, colors.Length);

        GameObjectCreator.Create(x, z, scaleVal / 2, PrimitiveType.Cube, true, colors[randomIndex]);
        List<MapLocation> wallLocationLists = new List<MapLocation>();
        wallLocationLists.Add(new MapLocation(x + 1, z));
        wallLocationLists.Add(new MapLocation(x - 1, z));
        wallLocationLists.Add(new MapLocation(x, z + 1));
        wallLocationLists.Add(new MapLocation(x, z - 1));

        while (wallLocationLists.Count > 0)
        {
            int random_wall = Random.Range(0, wallLocationLists.Count);
            x = wallLocationLists[random_wall].x;
            z = wallLocationLists[random_wall].z;
            wallLocationLists.RemoveAt(random_wall);

            if (CountSquareNeighbours(x, z) == 1)
            {
                map[x, z] = 0;
                end_x = x;
                end_z = z;
                wallLocationLists.Add(new MapLocation(x + 1, z));
                wallLocationLists.Add(new MapLocation(x - 1, z));
                wallLocationLists.Add(new MapLocation(x, z + 1));
                wallLocationLists.Add(new MapLocation(x, z - 1));
            }
        }

        map[end_x, end_z] = 2;

        GameObjectCreator.Create(end_x, end_z, scaleVal / 2, PrimitiveType.Cube, false, Color.red);        
    }
}
