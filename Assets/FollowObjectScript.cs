using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectScript : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = target.transform.position.x;
        pos.z = target.transform.position.z;
        transform.position = pos;
    }
}
