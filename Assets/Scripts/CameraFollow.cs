using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    [SerializeField]public Transform Target;

    private void Update()
    {
        Vector3 newPosition = Target.position+new Vector3(5,0,0);
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}