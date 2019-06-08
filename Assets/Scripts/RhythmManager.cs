using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    private static AudioSource musicPlayer;
    public AudioClip defaultMusic;
    private static float bpm;
    private static float spb; //seconds per beat
    private static bool[] beatUsed; //set to false when player presses button; reset to true after each beat; p1 at [0] p2 and [1]

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        Level tempLvl = new Level(musicPlayer.clip, 90);
        transitionLevels(tempLvl);
    }

    private float beatTime = 0; //reset upon new beat
    void Update()
    {
        //evaluatePress(1);
        float musicTime = (float)musicPlayer.timeSamples / musicPlayer.clip.frequency;
        float newBeatTime = musicTime % spb;
        if (newBeatTime < beatTime)
        {
            beat();
            beatTime = 0;
        }
        else
            beatTime = newBeatTime;
    }

    private static void beat()
    {
        print("BEAT");
    }

    public static void transitionLevels(Level newLevel)
    {
        musicPlayer.clip = newLevel.music;
        bpm = newLevel.bpm;
        spb = 60 / bpm;
        musicPlayer.Play();
    }

    public static float evaluatePress(int player)
    {
        return 0;
    }
}
