using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int mapHalfWidth;
    public int mapHalfHeight;

    public Vector2 mapCenter;

    public Transform[] blockTypes;
    List<Vector2> critPath = new List<Vector2>();

    public Vector2[] playerStartPos;
    public int tempPlayerCount;
    
    [Range(0.1f, 0.9f)]
    public float blockDensity;

	// Use this for initialization
	void Start () {
        // Generate player locations
        playerStartPos = new Vector2[] {
            new Vector2(-mapHalfWidth, mapHalfHeight),
            new Vector2(mapHalfWidth, mapHalfHeight),
            new Vector2(-mapHalfWidth, -mapHalfHeight),
            new Vector2(mapHalfWidth, -mapHalfHeight),
        };
        for(int i = 0; i < tempPlayerCount; i++)
        {
            generateCritPath(playerStartPos[i]); // Create a critical path from player's spawn to center
        }
        for(int i = -mapHalfHeight; i < mapHalfHeight; i++)
        {
            for(int j = -mapHalfWidth; j < mapHalfWidth; j++)
            {
                Vector2 newLoc = new Vector2(j, i);
                if (!critPath.Contains(newLoc))
                {

                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void spawnNewTile()
    {
        
    }

    class Vertex
    {
        public Vector2 location;
        public Vertex parent;
    }

    void generateCritPath(Vector2 start)
    {
        // NOTE: THIS FUNCTION DOES NOT CLEAR CRIT PATH BEFORE HAND
        Stack<Vertex> toBeSearched = new Stack<Vertex>(); // Points to be searched
        List<Vector2> alreadySearched = new List<Vector2>();
        Vertex currentVertex = new Vertex();
        currentVertex.location = start;
        toBeSearched.Push(currentVertex);
        Vector2[] dirs =
        {
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, -1),
            new Vector2(-1, 0),
        };
        while(currentVertex != null)
        {
            currentVertex = toBeSearched.Pop();
            if(currentVertex.location == mapCenter) // If we've reached the center
            {
                while(currentVertex != null)
                {
                    critPath.Add(currentVertex.location); // Backtrack through the vertices to add to critical path
                    currentVertex = currentVertex.parent;
                }
                return;
            }
            shuffle(dirs); // Shuffle the directions for even more randomness!
            for(int i = 0; i < dirs.Length; i++)
            {
                Vector2 newLoc = currentVertex.location + dirs[i];
                if (isInMap(newLoc) && !alreadySearched.Contains(newLoc)
                    && !critPath.Contains(newLoc))
                {
                    Vertex newVertex = new Vertex();
                    newVertex.location = newLoc;
                    newVertex.parent = currentVertex;
                }
            }
        }
    }

    bool isInMap(Vector2 loc)
    {
        return (Mathf.Abs(loc.x) < mapHalfWidth && Mathf.Abs(loc.y) < mapHalfHeight);
    }

    void shuffle<T>(T[] leArray)
    {
        for(int i = 0; i < leArray.Length; i++)
        {
            T temp = leArray[i];
            int rand = Random.Range(0, leArray.Length);
            leArray[i] = leArray[rand];
            leArray[rand] = temp;
        }
    }
}
