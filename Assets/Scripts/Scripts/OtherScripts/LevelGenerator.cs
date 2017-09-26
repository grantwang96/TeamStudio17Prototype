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
	}
	
	// Update is called once per frame
	void Update () {
		
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
        Vertex currentVertex = new Vertex();
        currentVertex.location = start;
        toBeSearched.Push(currentVertex);
        while(currentVertex != null)
        {
            currentVertex = toBeSearched.Pop();
            if(currentVertex.location == mapCenter) // If we've reached the center
            {
                while(currentVertex != null)
                {
                    critPath.Add(currentVertex.location);
                    currentVertex = currentVertex.parent;
                }
                return;
            }
        }
    }

<<<<<<< Updated upstream:Assets/Scripts/LevelGenerator.cs
   // bool isInMap(Vector2 loc)
   // {
		//if(loc.x < );
    //}
=======
//    bool isInMap(Vector2 loc)
//    {
//        if(loc.x < )
//    }
>>>>>>> Stashed changes:Assets/Scripts/Scripts/OtherScripts/LevelGenerator.cs
}
