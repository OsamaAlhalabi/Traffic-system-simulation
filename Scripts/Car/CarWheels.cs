using UnityEngine;

public class CarWheels : MonoBehaviour
{
    public WheelCollider[] WheelColliders;
    public Transform[] tireMeshes;
    public bool useCustomCenterOfMass = false;
    public Vector3 centerOfMassOffset;

    void Start()
    {
        CheckCenterOfMass();
    }
    void Update()
    {
        UpdateMeshesPositions();
    }

    private void CheckCenterOfMass()
    {
        if (useCustomCenterOfMass)
        {
            GetComponent<Rigidbody>().centerOfMass = centerOfMassOffset;
        }
    }

    private void UpdateMeshesPositions()
    {
        for (int i = 0; i < WheelColliders.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;
            WheelColliders[i].GetWorldPose(out pos, out quat);

            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        CheckCenterOfMass();
    }
#endif
}