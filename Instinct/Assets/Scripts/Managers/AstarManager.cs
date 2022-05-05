using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarManager : MonoBehaviour
{

    public static AstarManager Instance { get; private set; }

    private GridGraph pathfindingGraph;

    #region Singleton
    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    private void Awake()
    {
        CreateInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        pathfindingGraph = AstarPath.active.data.gridGraph;
        pathfindingGraph.Scan();
    }

    public void ScanDoorArea(Bounds areaBounds)
    {
        areaBounds.Expand(2f);
        GraphUpdateObject guo = new GraphUpdateObject(areaBounds);
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
    }
}
