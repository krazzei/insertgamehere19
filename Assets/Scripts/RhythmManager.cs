using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour
{
    private static RhythmManager _instance;

    public static RhythmManager Instance => _instance;
    private List<GameObject> beatSubs = new List<GameObject>();

    public AudioSource musicPlayer;
    //public AudioClip defaultMusic;
    //private AudioSource metronome;
    public AudioClip metronomeClip;

    private bool transitioning = true;

    private float bpm;
    public float spb; //seconds per beat

    private float beatTime = 0; //reset immediately after new beat
    private bool resetUsed = false; //keeps track of if we already used the nearest beat

    private PlayerPress[] playerPresses = new PlayerPress[1];
    public float arbitraryNumberToOffsetMusicByBecauseUnitysAudioIsAPieceOfShitAndDelayedForSomeReason = .125f;

    void Awake()
    {
        _instance = this;
        musicPlayer = GetComponent<AudioSource>(); //setup audiosource
    }

    void Start()
    {
        for (int i = 0; i < playerPresses.Length; i++) //initialize playerpresses
            playerPresses[i] = new PlayerPress();
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

        //DEBUG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.Q))
            print(evaluatePress(1));

        else if (Input.GetKeyDown(KeyCode.P))
            LevelManager.instance.nextLevel();
    }

    //sign up to receive messages on beat
    public void subToBeat(GameObject subscriber)
    {
        beatSubs.Add(subscriber);
    }

    private void beat()
    {
        //do something
        foreach (GameObject item in beatSubs)
        {
            item.SendMessage("onBeat");
        }
    }

    public float evaluatePress(int player)
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

    public void transitionLevels(Level newLevel)
    {
        StartCoroutine(countoff(newLevel, 2));
    }
    
    IEnumerator countoff(Level newLevel, float duration)
    {
        //prefade out
        transitioning = true;

        //fading out
        for (float time = duration; time > 0; time -= Time.deltaTime)
        {
            musicPlayer.volume = time / duration;
            yield return null;
        }

        //post fading out
        //musicPlayer.clip = metronomeClip;
        musicPlayer.Stop();
        musicPlayer.volume = 1;

        //counting off
        for (int j = 0; j < 1; j++) //measures
        {
            musicPlayer.pitch = 1;
            for (int i = 0; i < 3; i++)
            {
                musicPlayer.PlayOneShot(metronomeClip);
                yield return new WaitForSeconds(spb);
            }
            musicPlayer.pitch = 2f;
            musicPlayer.PlayOneShot(metronomeClip);
            yield return new WaitForSeconds(spb);
        }

        //play music
        musicPlayer.pitch = 1;
        musicPlayer.clip = newLevel.music;
        bpm = newLevel.bpm;
        spb = 60 / bpm;
        musicPlayer.Play();

        StopCoroutine(countoff(newLevel, duration));
    }
    
    //class that keeps track of players' presses between beats
    private class PlayerPress
    {
        public bool beatUsed = false;
        public void resetForNextBeat() { beatUsed = false; }
    }
}
