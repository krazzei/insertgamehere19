using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    private static AudioSource musicPlayer;
    public AudioClip defaultMusic;
    private static float bpm;
    private static float spb; //seconds per beat

    private static float beatTime = 0; //reset immediately after new beat
    private bool resetUsed = false; //keeps track of if we already used the nearest beat

    private static PlayerPress[] playerPresses = new PlayerPress[1];
    public float arbitraryNumberToOffsetMusicByBecauseUnitysAudioIsAPieceOfShitAndDelayedForSomeReason = .125f;

    void Start()
    {
        for (int i = 0; i < playerPresses.Length; i++) //initialize playerpresses
            playerPresses[i] = new PlayerPress();
        
        musicPlayer = GetComponent<AudioSource>(); //setup audiosource
        Level tempLvl = new Level(musicPlayer.clip, 90);
        transitionLevels(tempLvl);
    }

    void Update()
    {
        float musicTime = musicPlayer.time - arbitraryNumberToOffsetMusicByBecauseUnitysAudioIsAPieceOfShitAndDelayedForSomeReason;
        float checkIfBeatTimeResetToZero = musicTime % spb;
        float betweenBeatPercentage = checkIfBeatTimeResetToZero / spb; //0 to 1 how far from next beat; if < .5, group with previous beat

        if (!resetUsed && betweenBeatPercentage >= .5) //make the next coming beat in the future the next beat to absorb the keypress
        {
            for (int i = 0; i < playerPresses.Length; i++)
                playerPresses[i].resetForNextBeat();
            resetUsed = true;
        }

        if (checkIfBeatTimeResetToZero < beatTime) //we know the we're in the next beat if 
        {
            beat();
            beatTime = 0;
            resetUsed = false;
        }
        else
        {
            beatTime = checkIfBeatTimeResetToZero;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            print(evaluatePress(1));
    }

    private static void beat()
    {
        //do something
    }

    //give player score based on how on-time they are
    public static float evaluatePress(int player)
    {
        PlayerPress playerPress = playerPresses[player - 1];
        float beatCompletePercentage = 0; //closer to coming beat = closer to 1
        float effectiveness = 0; //closer to last or coming beat = closer to 1
        if (!playerPress.beatUsed) //can only be evaluated once per beat
        {
            //some stupid math to map 0..1 to 1..0..1
            beatCompletePercentage = beatTime / spb;
            if (beatCompletePercentage <= .5f)
                effectiveness = 1 - beatCompletePercentage;
            else
                effectiveness = beatCompletePercentage;
            effectiveness = (effectiveness - .5f ) *2;

            playerPress.beatUsed = true;
        }
        return effectiveness;
    }

    public static void transitionLevels(Level newLevel)
    {
        musicPlayer.clip = newLevel.music;
        bpm = newLevel.bpm;
        spb = 60 / bpm;
        musicPlayer.Play();
    }

    //class that keeps track of players' presses between beats
    private class PlayerPress
    {
        public bool beatUsed = false;
        //public float beatEffect = 0.0f;
        //public void resetForNextBeat() { beatUsed = false; beatEffect = 0; }
        public void resetForNextBeat() { beatUsed = false; }

    }

}


