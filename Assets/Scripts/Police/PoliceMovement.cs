using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PoliceMovement : MonoBehaviour
{
    public ThirdPersonCharacter character;
    private NavMeshAgent agent;
    private GameObject player;
    private CivilianMovement civilian;
    public enum State
    {
        PATROL,
        CHASE
    }

    public GameObject[] patrolPoints;
    private int patrolPointInd = 0;

    public State state;
    private bool alive;

    public float patrolSpeed = 0.5f;

    public float chaseSpeed = 1f;

    public bool arrested = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        character = this.GetComponent<ThirdPersonCharacter>();
        civilian = FindObjectOfType<CivilianMovement>();

        agent.updatePosition = true;
        agent.updateRotation = false;

        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
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
        if (Vector3.Distance(this.transform.position, patrolPoints[patrolPointInd].transform.position) >= 2)
        {
            agent.SetDestination(patrolPoints[patrolPointInd].transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        else if (Vector3.Distance(this.transform.position, patrolPoints[patrolPointInd].transform.position) <= 2)
        {
            patrolPointInd += 1;
            if (patrolPointInd >= patrolPoints.Length)
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
}
