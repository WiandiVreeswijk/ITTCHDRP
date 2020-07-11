using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using Cinemachine;
public class PlayerMovement : MonoBehaviour
{


    public ThirdPersonCharacter character;
    private Rigidbody rb;
    public CinemachineFreeLook outsideCamera, clubCamera;
    public GameObject mouseTarget;
    private NavMeshAgent agent;
    public LayerMask whatCanBeClickedOn;
    private float speed = 30f;

    private float targetValueX = 0;
    private float targetValueY = 0;

    [NonSerialized]
    public bool interaction;

    [NonSerialized]
    public bool activateSpeaker = false;

    private speakerInteraction speakerinteraction;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        character = this.GetComponent<ThirdPersonCharacter>();
        agent = this.GetComponent<NavMeshAgent>();
        //Cursor.lockState = CursorLockMode.Locked;

        speakerinteraction = FindObjectOfType<speakerInteraction>();

    }

    void Update()
    {
        MoveWithMouse();
        KeyboardInteraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (clubCamera.Priority == 0)
        {
            if (other.gameObject.tag == "CameraTransitionPoint")
            {
                clubCamera.Priority = 2;
            }
        }
        else if(other.gameObject.tag == "CameraTransitionPoint")
        {
            clubCamera.Priority = 0;
        }
    }


    //WSAD movement
    private void MoveWithKeyboard()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * vertical * speed * Time.deltaTime;
        Vector3 right = transform.right * horizontal * speed * Time.deltaTime;

        character.Move(forward + right, false, false);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1000f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 60f;
        }

        if (Input.GetMouseButton(1))
        {

            targetValueX = Input.GetAxis("Mouse X");
            targetValueY = Input.GetAxis("Mouse Y");

        }

        if (Input.GetMouseButtonUp(1))
        {
            targetValueX = 0;
            targetValueY = 0;
        }
        outsideCamera.m_XAxis.m_InputAxisValue = Mathf.Lerp(outsideCamera.m_XAxis.m_InputAxisValue, targetValueX, Time.deltaTime * 5);
        outsideCamera.m_YAxis.m_InputAxisValue = Mathf.Lerp(outsideCamera.m_YAxis.m_InputAxisValue, targetValueY, Time.deltaTime * 5);
    }

    //Mouse Movement
    private void MoveWithMouse()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            mouseTarget.SetActive(false);
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
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

        if (Input.GetKey(KeyCode.W))
        {
            outsideCamera.m_YAxis.Value -= 0.02f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            outsideCamera.m_YAxis.Value += 0.02f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            outsideCamera.m_XAxis.Value += 1.5f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            outsideCamera.m_XAxis.Value -= 1.5f;
        }

    }

    private void KeyboardInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interaction = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            interaction = false;
        }
        if (speakerinteraction.isPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (activateSpeaker)
                {
                    activateSpeaker = false;
                }
                else
                {
                    activateSpeaker = true;
                }
            }
        }

    }
}
