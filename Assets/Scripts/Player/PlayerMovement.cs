using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public LayerMask whatCanBeClickedOn;
    public GameObject mouseTarget;

    public ThirdPersonCharacter character;

    public float moveSpeed, acceleration;

    [NonSerialized]
    public bool interaction;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.acceleration = acceleration;

        agent.updatePosition = true;
        agent.updateRotation = false;

        character = this.GetComponent<ThirdPersonCharacter>();

    }

    void Update()
    {
        //removes mouse target after reaching destination
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            mouseTarget.SetActive(false);
            character.Move(Vector3.zero, false, false);
        }
        else
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        

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
    }
}
