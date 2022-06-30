using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject tile;
    public GameObject city;
    public GameObject turret;

    private Dictionary<string, GameObject> gameObjectDictionary;
    private Dictionary<Vector3, List<Module>> wfcMap;
    private WaveFunctionCollapse wfc;
    //private Dictionary<Vector3, GameObject> gameMap = new Dictionary<Vector3, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialise(Vector3 mapSize)
    {
        gameObjectDictionary = new Dictionary<string, GameObject>();
        wfc = new WaveFunctionCollapse();
        LoadGameObjects();
        wfc.Initialise(mapSize);
        wfcMap = wfc.waveFunction;
        BuildMap();

    }

    private void LoadGameObjects()
    {
        gameObjectDictionary.Add("Ground_Tile", Resources.Load("Objects\\Ground_Tile") as GameObject);
        gameObjectDictionary.Add("Dirt_Tile", Resources.Load("Objects\\Dirt_Tile") as GameObject);

    }

    public bool TryPlaceGameObject(string gameObjectString, Vector3 pos)
    {
        bool placed = false;

        if (gameObjectDictionary.TryGetValue(gameObjectString, out GameObject gameObject))
        {
            gameObject = Instantiate(gameObject);
            gameObject.transform.position = pos;
            placed = true;
        }

        return placed;
    }

    private void BuildMap()
    {
        foreach (KeyValuePair<Vector3, List<Module>> entry in wfcMap)
        {
            if(!TryPlaceGameObject(entry.Value[0].objectName, entry.Key))
            {
                Debug.LogError("Error while building wfc map!");
            }
        }
    }
    //private void CreateMap(Dictionary<Vector3, List<Module>> wfcMap)
    //{

    //    //foreach (KeyValuePair<Vector3, List<Module>> entry in wfcMap)
    //    //{
    //    //    GameObject go = 
    //    //}

    //        Vector3 map = new Vector3(11, 2, 11);

    //    GameObject go;
    //    //Instantiate a tile at each of the points in the 3d grid.
    //    for (int x = 0; x < map.x; x++)
    //    {
    //        for (int y = 0; y < map.y; y++)
    //        {
    //            for (int z = 0; z < map.z; z++)
    //            {
    //                //Instantiate City in the middle of the map
    //                if (x==5 && y==1 && z==5)
    //                {
    //                    go = Instantiate(city);
    //                    Vector3 v = new(x, y, z);
    //                    go.transform.position = v;
    //                    gameMap.Add(v, go);
    //                }
    //                //For now game is only a square of 1 height
    //                if (y > 0) { }
    //                else
    //                {
    //                    Vector3 objectPos = new Vector3(x, y, z);
    //                    go = Instantiate(tile);
    //                    go.transform.position = objectPos;

    //                    gameMap.Add(objectPos, go);
    //                }
    //            }
    //        }
    //    }
    //}

    //public void Build(Vector3 pos, string objectName)
    //{
    //    Vector3 buildPos = new Vector3(pos.x, pos.y + 1, pos.z);
    //    //First check to see if an object already exists in that position
    //    if (gameMap.TryGetValue(buildPos, out GameObject _))
    //    {
    //        Debug.LogError("Error: Object exsts at location");
    //    } else
    //    {
    //        GameObject gObject = Instantiate(turret);
    //        gObject.transform.position = buildPos;
    //        gameMap.Add(buildPos, gObject);
    //    }
    //}

    private Vector3[] GetVertices(GameObject go)
    {
        MeshFilter[] mfs = go.GetComponentsInChildren<MeshFilter>();
        List<Vector3> vList = new List<Vector3>();
        foreach (MeshFilter mf in mfs)
        {
            vList.AddRange(mf.sharedMesh.vertices);
        }
        return vList.ToArray();
    }
}
