using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Roasted : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer roast;
    [SerializeField]
    AudioClip ohYeah;
    [SerializeField]
    AudioSource roastSong;
    [SerializeField]
    AudioSource currentSong;

    [SerializeField]
    float roastDuration = 15.0f;
    float timer = 0.0f;
    bool startRoast = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = ohYeah;
        roast = GetComponent<SpriteRenderer>();
        roast.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Roasting();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<AudioSource>().Play();
        roast.enabled = false;
        currentSong.Pause();
        roastSong.Play();
        startRoast = true;
 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    void Roasting()
    {
        if (startRoast == true)
        {
            Destroy(gameObject, roastDuration);
            print("does this get printed");
            timer += Time.deltaTime;
            if (timer > roastDuration)
            {
                currentSong.Play();
                roastSong.Pause();
                timer = 0.0f;
                startRoast = false;
            }
        }
    }
}
