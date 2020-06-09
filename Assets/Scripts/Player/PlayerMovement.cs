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
    public CinemachineFreeLook camera;
    public float speed = 10f;

    private float targetValueX = 0;
    private float targetValueY = 0;

    [NonSerialized]
    public bool interaction;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        character = this.GetComponent<ThirdPersonCharacter>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //camera.m_XAxis.Value = transform.rotation.y;
        //camera.m_XAxis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * vertical * speed * Time.deltaTime;
        Vector3 right = transform.right * horizontal * speed * Time.deltaTime;

        //rb.AddForce(forward + right);
        character.Move(forward + right, false, false);

        if (Input.GetKeyDown(KeyCode.E))
        {
            interaction = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            interaction = false;
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
        camera.m_XAxis.m_InputAxisValue = Mathf.Lerp(camera.m_XAxis.m_InputAxisValue, targetValueX, Time.deltaTime * 5);
        camera.m_YAxis.m_InputAxisValue = Mathf.Lerp(camera.m_YAxis.m_InputAxisValue, targetValueY, Time.deltaTime * 5);
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
