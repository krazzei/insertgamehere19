using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    private static AudioSource musicPlayer;
    private static float bpm;
    private static bool[] beatUsed; //set to false when player presses button; reset to true after each beat; p1 at [0] p2 and [1]

    void Start()
    {
        
    }

    void Update()
    {
        evaluatePress(1);
    }

    public static void transitionLevels(Level newLevel)
    {
        musicPlayer.clip = newLevel.music;
        bpm = newLevel.bpm;
        musicPlayer.Play();
    }

    public static float evaluatePress(int player)
    {
        print(musicPlayer.timeSamples);
        return 0;
    }

    IEnumerator onBeat()
    {
        float i = 0;
        for (i; f >= 0; f -= 0.1f)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return null;
        }
    }

}
