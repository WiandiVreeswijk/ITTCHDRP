using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class CivilianMovement : MonoBehaviour
{

    public ThirdPersonCharacter character;
    private GameObject player;
    private NavMeshAgent agent;

    public enum State
    {
        WANDER,
        FOLLOW
    }

    public State state;
    private bool alive;

    public GameObject[] waypoints;
    private int waypointInd;
    public float wanderSpeed = 0.5f;

    public float followSpeed = 1f;

    private Vector3 pointOnUnitCircle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        character = this.GetComponent<ThirdPersonCharacter>();

        agent.updatePosition = true;
        agent.updateRotation = false;

        waypoints = GameObject.FindGameObjectsWithTag("WayPoint");
        waypointInd = Random.Range(0, waypoints.Length);
        state = CivilianMovement.State.WANDER;

        alive = true;

        StartCoroutine("FSM");

        agent.stoppingDistance = Random.Range(1.5f, 3f);

        Vector2 pOUC = Random.insideUnitCircle.normalized * 3;
        pointOnUnitCircle = new Vector3(pOUC.x, 0, pOUC.y);
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.WANDER:
                    Wander();
                    break;
                case State.FOLLOW:
                    Follow();
                    break;
            }
            yield return null/*new WaitForSeconds(1.0f)*/;
        }
    }

    void FixedUpdate()
    {
        /*
        agent.SetDestination(GetTargetDest());

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
            agent.velocity = Vector3.zero;
        }
        */
    }

    void Wander()
    {
        agent.speed = wanderSpeed;
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 3.5)
        {
            agent.SetDestination(waypoints[waypointInd].transform.position);
            character.Move(agent.desiredVelocity, false, false);
        } else if(Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 3.5)
        {
            waypointInd = Random.Range(0, waypoints.Length);

        } else
        {
            character.Move(Vector3.zero, false, false);
            agent.velocity = Vector3.zero;
        }
    }
    void Follow()
    {
        agent.speed = followSpeed;
        agent.SetDestination(GetTargetDest());
        character.Move(agent.desiredVelocity, false, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MusicDome")
        {
            state = CivilianMovement.State.FOLLOW;
        }
    }

    private Vector3 GetTargetDest()
    {
        if (player)
        {
            Vector3 playerView = player.transform.forward;
            Vector3 playerPositionTemp = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            Vector3 positionTemp = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 playerToThis = positionTemp - playerPositionTemp;
            Vector3 playerToThisNormalized = playerToThis.normalized;
            float distance = ((Vector3.Dot(playerView, playerToThisNormalized) + 1) * 2) + 2;
            Vector3 playerToThisScaled = playerToThisNormalized;
            playerToThisScaled.Scale(new Vector3(distance, distance, distance));
            //return playerPositionTemp + playerToThisScaled;
            return new Vector3(playerPositionTemp.x, 0.14f, playerPositionTemp.z) + new Vector3(playerToThisScaled.x, 0, playerToThisScaled.z);
        }
        return new Vector3(0, 0, 0);
    }


}
