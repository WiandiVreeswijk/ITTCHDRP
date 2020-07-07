using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PoliceMovement : MonoBehaviour
{
    public ThirdPersonCharacter character;
    private NavMeshAgent agent;
    private GameObject player;
    private CivilianMovement civilian;

    private speakerInteraction speakerint;
    public enum State
    {
        PATROL,
        CHASE
    }

    public List<GameObject> closePatrolPoints;
    private int patrolPointInd = 0;

    public State state;
    private bool alive;

    [Range(1.0f, 150.0f)]
    public float radius = 1.0f;
    public float patrolSpeed = 0.5f;

    public float chaseSpeed = 1f;

    public bool arrested = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        character = this.GetComponent<ThirdPersonCharacter>();
        civilian = FindObjectOfType<CivilianMovement>();
        speakerint = FindObjectOfType<speakerInteraction>();

        GameObject[] patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        closePatrolPoints = new List<GameObject>();

        agent.updatePosition = true;
        agent.updateRotation = false;
        foreach (var patrolpoint in patrolPoints)
        {
            if (Vector3.Distance(transform.position, patrolpoint.transform.position) < radius)
            {
                closePatrolPoints.Add(patrolpoint);
            }
        }
        state = PoliceMovement.State.PATROL;

        alive = true;

        StartCoroutine("FSM");
    }
    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;
            }
            yield return null;
        }
    }

    void Patrol()
    {
        agent.stoppingDistance = Random.Range(0.5f, 1.5f);
        agent.speed = patrolSpeed;

        if (Vector3.Distance(this.transform.position, closePatrolPoints[patrolPointInd].transform.position) >= 2)
        {
            agent.SetDestination(closePatrolPoints[patrolPointInd].transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        else if (Vector3.Distance(this.transform.position, closePatrolPoints[patrolPointInd].transform.position) <= 2)
        {
            patrolPointInd += 1;
            if (patrolPointInd >= closePatrolPoints.Count)
            {
                patrolPointInd = 0;
            }
        }
        else
        {
            character.Move(Vector3.zero, false, false);
            agent.velocity = Vector3.zero;
        }

    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
        character.Move(agent.desiredVelocity, false, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MusicDome")
        {
            arrested = true;
            //speakerint.isPickedUp = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MusicDome")
        {
            arrested = false;
        }
    }

    void Inpursuit()
    {

    }

    private void OnDrawGizmos() {
        Handles.CircleHandleCap(0, transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f), radius, EventType.Repaint);
    }
}
