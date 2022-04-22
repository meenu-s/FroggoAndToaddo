using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetTransformFrog;
    public Transform targetTransformToad;
    Vector3 tempVec3 = new Vector3();

    void LateUpdate()
    {
        tempVec3.x = 0.5f*(targetTransformFrog.position.x+targetTransformToad.position.x);
        tempVec3.y = this.transform.position.y;
        tempVec3.z = this.transform.position.z;
        this.transform.position = tempVec3;
    }
}
