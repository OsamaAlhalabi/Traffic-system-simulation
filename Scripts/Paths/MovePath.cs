using UnityEngine;

public class MovePath : MonoBehaviour
{
    [HideInInspector] public float _walkPointThreshold = 0.5f;
    [HideInInspector] public int w;
    [HideInInspector] public bool forward;
    [HideInInspector] public WalkPath walkPath;
     public Vector3 finishPos;
     public Vector3 nextFinishPos = Vector3.zero;
    [HideInInspector] public int targetPoint;
    [HideInInspector] public int targetPointsTotal;
    [HideInInspector] public float randXFinish;
    [HideInInspector] public float randZFinish;
    [HideInInspector] public bool loop;

    public void InitStartPosition(int _w, int _i, bool _loop, bool _forward)
    {
        forward = _forward;

        var _WalkPath = walkPath;
        w = _w;
        targetPointsTotal = _WalkPath.getPointsTotal(0) - 2;

        loop = _loop;

        if (loop)
        {
            if (_i < targetPointsTotal && _i > 0)
            {
                if (forward)
                {
                    targetPoint = _i + 1;
                    finishPos = _WalkPath.getNextPoint(w, _i + 1);
                }
                else
                {
                    targetPoint = _i;
                    finishPos = _WalkPath.getNextPoint(w, _i);
                }
            }
            else
            {
                if (forward)
                {
                    targetPoint = 1;
                    finishPos = _WalkPath.getNextPoint(w, 1);
                }
                else
                {
                    targetPoint = targetPointsTotal;
                    finishPos = _WalkPath.getNextPoint(w, targetPointsTotal);
                }
            }

        }
        else
        {
            if (forward)
            {
                targetPoint = _i + 1;
                finishPos = _WalkPath.getNextPoint(w, _i + 1);
            }
            else
            {
                targetPoint = _i;
                finishPos = _WalkPath.getNextPoint(w, _i);
            }
        }

    }

    public void SetLookPosition()
    {
        Vector3 targetPos = new Vector3(finishPos.x, transform.position.y, finishPos.z);
        transform.LookAt(targetPos);
    }
}