using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorRooms : MonoBehaviour {

    #region Important Data
    public LevelMaterials myLevelData;
    public Vector2[] playerStartPos;

    class roomData
    {
        public Vector2 location;
        public bool isSpawnRoom;
        public bool partOfCritPath;
        public Vector2 pathDir;
    }

    public List<Vector2> critPath = new List<Vector2>();
    List<Vector2> roomLocs = new List<Vector2>();

    List<roomData> rooms = new List<roomData>();
    List<roomData> spawnRooms = new List<roomData>();
    
    int roomCount;
    int roomHeight;
    int roomWidth;

    int mapLimitX;
    int mapLimitY;

    [Range(1, 4)]
    public int tempPlayerCount;
    #endregion

    // Use this for initialization
    void Start () {

        roomCount = myLevelData.roomCount; // Each row/column will have this number + 1 rooms. Room count is actually (roomCount + 1)^2
        roomHeight = (myLevelData.mapHalfHeight) / roomCount;
        roomWidth = (myLevelData.mapHalfWidth) / roomCount;

        Debug.Log("Room Width: " + roomWidth);
        Debug.Log("Room Height: " + roomHeight);

        for(int i = -(roomCount / 2); i <= roomCount / 2; i++)
        {
            for(int j = -(roomCount/2); j <= roomCount / 2; j++)
            {
                int newLocX = i * (roomWidth);
                int newLocY = j * (roomHeight);
                roomLocs.Add(new Vector2(newLocX, newLocY));
                roomData newRoom = new roomData();
                newRoom.location = new Vector2(newLocX, newLocY);
                rooms.Add(newRoom);
            }
        }

        mapLimitX = (int)((roomCount / 2) * roomWidth) + roomWidth / 2;
        mapLimitY = (int)((roomCount / 2) * roomHeight) + roomHeight / 2;

        Debug.Log("Map Limit is: " + mapLimitX + ", " + mapLimitY);
        cornerRoomCheck();
        // initiateRoomColor();
        initiateCritPathBuild();
        buildLevel();
        // shuffle(playerStartPos);
        Debug.Log("FINISHED!");
        StartCoroutine(visualizeCritPath());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Map Generation Methods

    class Vertex
    {
        public Vector2 loc;
        public Vertex parent;
        public int moveCost;
        public Vector2 pathDir;
    }

    void initiateCritPathBuild()
    {
        Debug.Log("Starting critical path build...");
        for(int i = 0; i < spawnRooms.Count; i++)
        {
            List<roomData> roomPath = roomPathfinding(spawnRooms[i], myLevelData.mapCenter);
            if(roomPath != null)
            {
                Debug.Log("Initiating Room To Room pathfinding");
                roomToRoomPathFinding(roomPath);
            }
        }
    }

    void buildLevel()
    {
        for (int i = -mapLimitX; i < mapLimitX; i++)
        {
            for (int j = -mapLimitY; j < mapLimitY; j++)
            {
                Vector2 newLoc = new Vector2(i, j);
                if (!critPath.Contains(newLoc))
                {
                    spawnNewTile(newLoc);
                }
            }
        }
        for (int i = -mapLimitX - 1; i <= mapLimitX; i++)
        {
            Transform borderTileT = Instantiate(myLevelData.solidTiles[0], new Vector2(i, mapLimitY + 1), Quaternion.identity);
            Transform borderTileB = Instantiate(myLevelData.solidTiles[0], new Vector2(i, -mapLimitY - 1), Quaternion.identity);
        }
        for (int i = -mapLimitY - 1; i <= mapLimitY + 1; i++)
        {
            Transform borderTileL = Instantiate(myLevelData.solidTiles[0], new Vector2(-mapLimitX - 1, i), Quaternion.identity);
            Transform borderTileB = Instantiate(myLevelData.solidTiles[0], new Vector2(mapLimitX + 1, i), Quaternion.identity);
        }
    }

    void roomToRoomPathFinding(List<roomData> roomPath)
    {
        Debug.Log("Starting room to room pathfinding!");
        roomPath.Reverse();
        for(int i = 0; i < roomPath.Count - 1; i++)
        {
            tilePathfinding(roomPath[i], roomPath[i + 1]);
        }
    }

    List<roomData> roomPathfinding(roomData currRoom, Vector2 target)
    {
        Debug.Log("Starting room pathfinding from " + currRoom.location);
        Queue<Vertex> toBeSearched = new Queue<Vertex>();
        List<Vector2> beenThere = new List<Vector2>();
        Vector2[] dirs =
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1),
        };
        Vertex current = new Vertex();
        current.loc = currRoom.location;
        current.moveCost = manhattanDistance(current.loc, target);
        toBeSearched.Enqueue(current);
        while(toBeSearched.Count > 0)
        {
            current = toBeSearched.Dequeue();
            if(current.loc == target) // You've found it!
            {
                Debug.Log("Found path from room!");
                List<roomData> path = new List<roomData>();
                while(current != null)
                {
                    Debug.Log("Adding room at " + current.loc);
                    roomData newRoomData = new roomData();
                    newRoomData.location = current.loc;
                    newRoomData.pathDir = current.pathDir;
                    if(newRoomData.location == currRoom.location) { newRoomData.isSpawnRoom = true; }
                    path.Add(newRoomData);
                    current = current.parent;
                }
                return path;
            }
            List<Vertex> potentialSearches = new List<Vertex>();
            for(int i = 0; i < dirs.Length; i++)
            {
                Vector2 newLoc = current.loc + new Vector2(dirs[i].x * roomWidth, dirs[i].y * roomHeight);
                int dist = manhattanDistance(newLoc, target);
                if (isInMap(newLoc) && !beenThere.Contains(newLoc) && dist <= current.moveCost)
                {
                    Vertex newVertex = new Vertex();
                    newVertex.loc = newLoc;
                    newVertex.parent = current;
                    newVertex.moveCost = dist;
                    newVertex.pathDir = dirs[i];
                    potentialSearches.Add(newVertex);
                }
            }
            if(potentialSearches.Count > 0)
            {
                toBeSearched.Enqueue(potentialSearches[Random.Range(0, potentialSearches.Count)]);
            }
            beenThere.Add(current.loc);
        }
        return null;
    }

    void tilePathfinding(roomData currRoom, roomData nextRoom) // YO DAWG, I HEARD YOU LIKE A*!
    {
        Debug.Log("Started tilePathfinding from room" + currRoom.location);
        Debug.Log("Going to " + nextRoom.location);

        Queue<Vertex> toBeSearched = new Queue<Vertex>();
        List<Vector2> beenThere = new List<Vector2>();
        Vector2[] dirs =
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1),
        };
        Vertex current = new Vertex();
        current.loc = currRoom.location;
        current.moveCost = manhattanDistance(current.loc, nextRoom.location);
        toBeSearched.Enqueue(current);
        while(toBeSearched.Count > 0)
        {
            current = toBeSearched.Dequeue();
            if(current.loc == nextRoom.location ||
               critPath.Contains(current.loc))
            {
                while(current != null)
                {
                    critPath.Add(current.loc);
                    current = current.parent;
                }
            }
            List<Vertex> potentialSearches = new List<Vertex>();
            for(int i = 0; i < dirs.Length; i++)
            {
                Vector2 newLoc = current.loc + dirs[i];
                int dist = manhattanDistance(newLoc, nextRoom.location);
                if (isInMap(newLoc) && dist <= current.moveCost + 1 && !beenThere.Contains(newLoc))
                {
                    Vertex nextVertex = new Vertex();
                    nextVertex.loc = newLoc;
                    nextVertex.parent = current;
                    nextVertex.moveCost = dist;
                    nextVertex.pathDir = dirs[i];
                    potentialSearches.Add(nextVertex);
                }
            }
            if(toBeSearched.Count > 0) {
                Debug.Log("Adding a thing!");
                toBeSearched.Enqueue(potentialSearches[Random.Range(0, potentialSearches.Count)]);
            }
            beenThere.Add(current.loc);
        }
    }

    int manhattanDistance(Vector2 current, Vector2 target)
    {
        return (int)Mathf.Abs(current.x - target.x) + (int)Mathf.Abs(current.y - target.y);
    }

    void spawnNewTile(Vector2 loc)
    {
        float chance = myLevelData.blockDensity;
        bool closeToPlayer = false;
        Transform[] tilesList;
        for (int i = 0; i < playerStartPos.Length; i++)
        {
            if (Vector2.Distance(loc, playerStartPos[i]) < 4f)
            {
                closeToPlayer = true;
                break;
            }
        }
        tilesList = conCat(myLevelData.solidTiles, myLevelData.weakTiles);
        if (!closeToPlayer) { tilesList = conCat(tilesList, myLevelData.dangerTiles); }
        if (Random.value < chance)
        {
            Transform newTile = Instantiate(tilesList[Random.Range(0, tilesList.Length - 1)], loc, Quaternion.identity);
        }
    }

    bool isInMap(Vector2 loc)
    {
        return (Mathf.Abs(loc.x) <= mapLimitX && Mathf.Abs(loc.y) <= mapLimitY);
    }

    void cornerRoomCheck()
    {
        Debug.Log("Room Location Limit X: " + (mapLimitX - (roomWidth / 2)));
        Debug.Log("Room Location Limit Y: " + (mapLimitY - (roomHeight / 2)));

        for(int i = 0; i < rooms.Count; i++)
        {
            if(Mathf.Abs(rooms[i].location.x) == mapLimitX - (roomWidth / 2) &&
               Mathf.Abs(rooms[i].location.y) == mapLimitY - (roomHeight / 2))
            {
                rooms[i].isSpawnRoom = true;
                spawnRooms.Add(rooms[i]);
                Debug.Log("Added Room at " + rooms[i].location + " to spawn rooms.");
            }
        }
    }

    #endregion

    #region General Utility Methods

    void shuffle<T>(T[] leArray)
    {
        for (int i = 0; i < leArray.Length; i++)
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
        for (int i = 0; i < arr1.Length; i++)
        {
            newArr[i] = arr1[i];
        }
        for (int i = 0; i < arr2.Length; i++)
        {
            newArr[i + arr1.Length] = arr2[i];
        }
        return newArr;
    }

    void colorRoom(Vector2 center)
    {
        Transform newCrit = Instantiate(myLevelData.critPathHighlight, center, Quaternion.identity);
        newCrit.position -= Vector3.forward;
        for (int i = (int)center.x - (roomWidth / 2); i <= center.x + (roomWidth / 2); i++)
        {
            for (int j = (int)center.y - (roomHeight / 2); j <= center.y + (roomHeight / 2); j++)
            {
                Vector2 newLoc = new Vector2(i, j);
                if (center == newLoc) { continue; }
                Instantiate(myLevelData.playerHighlight, newLoc, Quaternion.identity);
            }
        }
    }

    void initiateRoomColor()
    {
        for(int i = 0; i < roomLocs.Count; i++)
        {
            colorRoom(roomLocs[i]);
        }
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
    #endregion
}
