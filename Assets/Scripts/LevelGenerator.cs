using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public LevelMaterials myLevelData;
    public Vector2[] playerStartPos;

    List<Vector2> critPath = new List<Vector2>();

    [Range(1, 4)]
    public int tempPlayerCount;
    
	// Use this for initialization
	void Start () {
        // Generate player locations
        playerStartPos = new Vector2[] {
            new Vector2(-myLevelData.mapHalfWidth, myLevelData.mapHalfHeight),
            new Vector2(myLevelData.mapHalfWidth, myLevelData.mapHalfHeight),
            new Vector2(-myLevelData.mapHalfWidth, -myLevelData.mapHalfHeight),
            new Vector2(myLevelData.mapHalfWidth, -myLevelData.mapHalfHeight),
        };
        shuffle(playerStartPos);
        for(int i = 0; i < playerStartPos.Length; i++)
        {
            Debug.Log("Generating Path for Player " + i + "...");
            generateCritPath(playerStartPos[i]); // Create a critical path from player's spawn to center
        }
        for(int i = 0; i < playerStartPos.Length; i++)
        {
            Transform newPlayHighlight = Instantiate(myLevelData.playerHighlight, playerStartPos[i], Quaternion.identity);
            newPlayHighlight.position -= Vector3.forward;
        }
        buildLevel();
        StartCoroutine(visualizeCritPath());
	}

    IEnumerator visualizeCritPath()
    {
        for (int i = 0; i < critPath.Count; i++)
        {
            Transform newCritHighlight = Instantiate(myLevelData.critPathHighlight, critPath[i], Quaternion.identity);
            newCritHighlight.position -= Vector3.forward;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("FINISHED!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void buildLevel()
    {
        for (int i = -myLevelData.mapHalfWidth; i < myLevelData.mapHalfWidth; i++)
        {
            for (int j = -myLevelData.mapHalfHeight; j < myLevelData.mapHalfHeight; j++)
            {
                Vector2 newLoc = new Vector2(i, j);
                if (!critPath.Contains(newLoc))
                {
                    spawnNewTile(newLoc);
                }
            }
        }
        for(int i = -myLevelData.mapHalfWidth - 1; i <= myLevelData.mapHalfWidth; i++)
        {
            Transform borderTileT = Instantiate(myLevelData.solidTiles[0], new Vector2(i, myLevelData.mapHalfHeight + 1), Quaternion.identity);
            Transform borderTileB = Instantiate(myLevelData.solidTiles[0], new Vector2(i, -myLevelData.mapHalfHeight - 1), Quaternion.identity);
        }
        for (int i = -myLevelData.mapHalfHeight - 1; i <= myLevelData.mapHalfHeight + 1; i++)
        {
            Transform borderTileL = Instantiate(myLevelData.solidTiles[0], new Vector2(-myLevelData.mapHalfWidth - 1, i), Quaternion.identity);
            Transform borderTileB = Instantiate(myLevelData.solidTiles[0], new Vector2(myLevelData.mapHalfWidth + 1, i), Quaternion.identity);
        }
    }

    void spawnNewTile(Vector2 loc)
    {
        float chance = myLevelData.blockDensity;
        bool closeToPlayer = false;
        Transform[] tilesList;
        for(int i = 0; i < playerStartPos.Length; i++)
        {
            if(Vector2.Distance(loc, playerStartPos[i]) < 4f)
            {
                closeToPlayer = true;
                break;
            }
        }
        tilesList = conCat(myLevelData.solidTiles, myLevelData.weakTiles);
        if (!closeToPlayer) { tilesList = conCat(tilesList, myLevelData.dangerTiles); }
        if(Random.value < chance)
        {
            Transform newTile = Instantiate(tilesList[Random.Range(0, tilesList.Length - 1)], loc, Quaternion.identity);
        }
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
        while(toBeSearched.Count > 0)
        {
            currentVertex = toBeSearched.Pop();
            if (currentVertex.location == myLevelData.mapCenter) // If we've reached the center
            {
                while(currentVertex != null)
                {
                    if (!critPath.Contains(currentVertex.location))
                    {
                        critPath.Add(currentVertex.location); // Backtrack through the vertices to add to critical path
                    }
                    currentVertex = currentVertex.parent;
                }
                return;
            }
            shuffle(dirs); // Shuffle the directions for even more randomness!
            for(int i = 0; i < dirs.Length; i++)
            {
                Vector2 newLoc = currentVertex.location + dirs[i];
                if (isInMap(newLoc) && !alreadySearched.Contains(newLoc))
                {
                    Vertex newVertex = new Vertex();
                    newVertex.location = newLoc;
                    newVertex.parent = currentVertex;
                    toBeSearched.Push(newVertex);
                }
            }
            alreadySearched.Add(currentVertex.location);
        }
    }

    bool isInMap(Vector2 loc)
    {
        return (Mathf.Abs(loc.x) <= myLevelData.mapHalfWidth && Mathf.Abs(loc.y) <= myLevelData.mapHalfHeight);
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

    T[] conCat<T>(T[] arr1, T[] arr2)
    {
        T[] newArr = new T[arr1.Length + arr2.Length];
        for(int i = 0; i < arr1.Length; i++)
        {
            newArr[i] = arr1[i];
        }
        for(int i = 0; i < arr2.Length; i++)
        {
            newArr[i + arr1.Length] = arr2[i];
        }
        return newArr;
    }
}
