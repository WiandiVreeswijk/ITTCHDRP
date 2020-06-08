using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerAura : MonoBehaviour
{
    public GameObject aura;
    public AudioSource source;
    public float BPM = 60;

    private float bpmInterval;
    private float previousTime;

    [SerializeField]
    private float goalSize = 165.0f;

    [SerializeField]
    private float sizeIncrement = 25.0f;

    [SerializeField]
    private float sizeDecrement = 0.001f;

    void Start()
    {
        bpmInterval = 60.0f / BPM;
    }


    void OnBeat()
    {
        aura.transform.localScale += new Vector3(sizeIncrement, sizeIncrement, sizeIncrement);
    }


    void Update()
    {
        float x = aura.transform.localScale.x;
        if (x > goalSize)
        {
            float a = (Mathf.Pow(x - goalSize, 2) * sizeDecrement);

            aura.transform.localScale = aura.transform.localScale - new Vector3(a, a, a);

        }

        if (source.time < previousTime)
        {
            if (source.time > bpmInterval)
            {
                OnBeat();
            }
            previousTime = source.time;

        }

        float difference = source.time - previousTime;
        if (difference > bpmInterval)
        {
            OnBeat();
            previousTime += bpmInterval;
        }
    }
}
