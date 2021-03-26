using System.Collections.Generic;
using UnityEngine;

public class MovePathParams
{

}

public class SpawnPoint : MonoBehaviour
{
    public bool IsSpawnPointFree
    {
        get { return _insideObjectsCount == 0; }
    }

    private int _insideObjectsCount = 0;

    private WalkPath _walkPath;
    private bool _isForward;
    private int _pathIndex;

    private Queue<MovePathParams> _movePathQueue = new Queue<MovePathParams>();

    public static SpawnPoint PeopleCreate(
        string name, Vector3 spawnPoint, Vector3 nextPoint, 
        float lineSpacing, int pathIndex, bool isForward, WalkPath walkPath, 
        float boxHeight = 3f, float boxLength = 10f
    )
    {        
        var go = new GameObject(name);
        go.transform.position = spawnPoint;
                    
        var cl = go.AddComponent<BoxCollider>();
        var spComponent = go.AddComponent<SpawnPoint>();
        cl.isTrigger = true;               

        cl.transform.localScale = new Vector3(lineSpacing - 0.05f, boxHeight, boxLength);
        go.transform.LookAt(nextPoint);

        go.transform.localPosition += new Vector3(0f, boxHeight / 2f, 0f);

        go.transform.Translate(Vector3.forward * boxLength / 2f);

        spComponent._walkPath = walkPath;
        spComponent._isForward = isForward;
        spComponent._pathIndex = pathIndex;

        return spComponent;
    }

    public static SpawnPoint CarCreate(
    string name, Vector3 spawnPoint, Vector3 nextPoint,
    float lineSpacing, int pathIndex, bool isForward, WalkPath walkPath,
    float boxHeight = 3f, float boxLength = 10f
)
    {
        var go = new GameObject(name);
        go.transform.position = spawnPoint;

        var cl = go.AddComponent<BoxCollider>();
        var spComponent = go.AddComponent<SpawnPoint>();
        cl.isTrigger = true;

        cl.transform.localScale = new Vector3(lineSpacing - 0.05f, boxHeight, boxLength);
        go.transform.LookAt(nextPoint);

        go.transform.localPosition += new Vector3(0f, boxHeight / 2f, 0f);

        go.transform.Translate(Vector3.forward * boxLength / 2f);

        spComponent._walkPath = walkPath;
        spComponent._isForward = isForward;
        spComponent._pathIndex = pathIndex;

        return spComponent;
    }

    public void AddToSpawnQuery(MovePathParams movePathParams)
    {
        _movePathQueue.Enqueue(movePathParams);
    }

    private void FixedUpdate()
    {
        if (IsSpawnPointFree && _movePathQueue.Count > 0)
        {
            MovePathParams movePathParams = _movePathQueue.Dequeue();
            _walkPath.SpawnOnePeople(
                _pathIndex, _isForward
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _insideObjectsCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        _insideObjectsCount--;
    }
}
