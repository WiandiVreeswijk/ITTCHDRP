using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMovement : MonoBehaviour
{

    public ThirdPersonCharacter character;

    public float speed = 10f;


    [NonSerialized]
    public bool interaction;


    void Start()
    {


        character = this.GetComponent<ThirdPersonCharacter>();

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * vertical * speed * Time.deltaTime;
        Vector3 right = transform.right * horizontal * speed * Time.deltaTime;

        character.Move(forward + right,false,false);
    }
}


/*
if(Input.GetMouseButtonDown(1))
{
    interaction = true;
}
else if(Input.GetMouseButtonUp(1))
{
    interaction = false;
}

if (Input.GetMouseButtonDown(0))
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100, whatCanBeClickedOn))
    {
        agent.SetDestination(hit.point);
        mouseTarget.SetActive(true);
        Vector3 point = hit.point;
        point.y = 0.2f;
        mouseTarget.transform.position = point;


    }
}
*/
