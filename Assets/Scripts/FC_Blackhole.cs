using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Blackhole : MonoBehaviour
{
    [SerializeField]
    GameObject musicPlayer;
    [SerializeField]
    AudioClip blackHole;
    [SerializeField]
    AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = blackHole;
        musicPlayer = GameObject.Find("MusicPlayer");
        music = musicPlayer.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<AudioSource>().Play();
        music.mute = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject, 2);
        GetComponent<AudioSource>().Stop();
        music.mute = false;
    }
}
