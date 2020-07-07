using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerState : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject speaker, aura;
    public Material activeMaterial, nonActiveMaterial;


    PlayerMovement playerMove;
    speakerInteraction speakerInteraction;


    void Start()
    {
        playerMove = FindObjectOfType<PlayerMovement>();
        speakerInteraction = FindObjectOfType<speakerInteraction>();
        SpeakerNotActive();
    }

    void FixedUpdate()
    {

        if (speakerInteraction.isPickedUp)
        {
            if (playerMove.activateSpeaker)
            {
                SpeakerActive();
            }
            else if (!playerMove.activateSpeaker)
            {
                print("test");
                SpeakerNotActive();
            }
        }
    }
    private void SpeakerNotActive()
    {

        audioSource.enabled = false;
        speaker.GetComponent<Renderer>().material = nonActiveMaterial;
        aura.SetActive(false);
    }
    private void SpeakerActive()
    {
        audioSource.enabled = true;
        speaker.GetComponent<Renderer>().material = activeMaterial;
        aura.SetActive(true);
    }
}
