using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public float height = 21f;

    void LateUpdate()
    {
        if (target != null)
        {
            //transform.position = new Vector3(target.position.x, height, target.position.z -20);
            //transform.rotation = Quaternion.Euler(45, 0, 0);

            transform.position = new Vector3(target.position.x, height, target.position.z);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}
