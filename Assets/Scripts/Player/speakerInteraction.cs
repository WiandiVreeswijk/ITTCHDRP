using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakerInteraction : MonoBehaviour
{
    private float distance;

    [NonSerialized]
    public bool isPickedUp;

    PlayerMovement playerMove;
    public GameObject target, speakerHolder;

    private Vector3 offsetPlayer = new Vector3(0, 2.5f, 0);
    private Vector3 offsetSpeakerHolder = new Vector3(0, 1, 0);
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, speakerHolder.transform.position);
        if(distance < 5 && playerMove.interaction)
        {
            if (!isPickedUp)
            {
                target.transform.parent = transform;
                target.transform.position = transform.position + offsetPlayer;
                playerMove.interaction = false;
                isPickedUp = true;
            }
            else
            {
                target.transform.parent = speakerHolder.transform;
                target.transform.position = speakerHolder.transform.position + offsetSpeakerHolder;
                playerMove.interaction = false;
                isPickedUp = false;
            }
        }
    }
}
