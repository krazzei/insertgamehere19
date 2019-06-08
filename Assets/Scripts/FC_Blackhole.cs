using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Blackhole : MonoBehaviour
{
    [SerializeField]
    AudioClip blackHole;
    [SerializeField]
    AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = blackHole;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<AudioSource>().Play();
        music.mute = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<AudioSource>().Stop();
        music.mute = false;
    }
}
