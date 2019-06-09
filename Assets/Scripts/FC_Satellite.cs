using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Satellite : MonoBehaviour
{
    [SerializeField]
    AudioClip whiteNoise;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = whiteNoise;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject, 2);
        GetComponent<AudioSource>().Stop();
    }
}
