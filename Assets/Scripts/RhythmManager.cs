using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Thrusters
{
    MainLeft = 0,
    MainRight,
    SidLeft,
    SideRight
}

[RequireComponent(typeof(AudioSource))]
public class RhythmManager : MonoBehaviour
{
    private static RhythmManager _instance;

    public static RhythmManager Instance => _instance;

    private AudioSource _musicPlayer;
    public AudioClip metronomeClip;

    private bool _transitioning = true;

    private float _bpm;
    private float _spb; //seconds per beat

    private readonly PlayerPress[] _playerPresses = new PlayerPress[4];
    private float _lastBeatTime;
    private float _timeBetweenLastBeat;

    private void Awake()
    {
        _instance = this;
        _musicPlayer = GetComponent<AudioSource>(); //setup audiosource
    }

    private void Start()
    {
        for (int i = 0; i < _playerPresses.Length; i++) //initialize playerpresses
            _playerPresses[i] = new PlayerPress();
    }

    void Update()
    {
        _timeBetweenLastBeat = _musicPlayer.time - _lastBeatTime;
        if (_timeBetweenLastBeat >= _spb && !_transitioning)
        {
            _lastBeatTime = _musicPlayer.time;
            StartCoroutine(ResetBeatPress(Time.deltaTime));
        }
    }

    public float EvaluatePress(Thrusters player)
    {
        var playerPress = _playerPresses[(int)player];
        float effectiveness = 0; //closer to last or coming beat = closer to 1
        
        if (playerPress.BeatUsed) return effectiveness;
        
        var beatPercent = _timeBetweenLastBeat / _spb;
        effectiveness = beatPercent >= 0.5
            ? Mathf.Lerp(1, 0, (beatPercent - 0.5f) * 2)
            : Mathf.Lerp(1, 0, beatPercent * 2);
            
        playerPress.BeatUsed = true;
        return effectiveness;
    }

    public void TransitionLevels(Level newLevel)
    {
        StartCoroutine(CountOff(newLevel, 1));
    }

    private IEnumerator ResetBeatPress(float frameTime)
    {
        // some overhead with WaitForSeconds so only wait 49% of the SPB. 
        yield return new WaitForSeconds(_spb * 0.49f);
        
        foreach (var playerPress in _playerPresses)
        {
            playerPress.BeatUsed = false;
        }
    }

    private IEnumerator CountOff(Level newLevel, float duration)
    {
        //prefade out
        _transitioning = true;

        //fading out
        for (float time = duration; time > 0; time -= Time.deltaTime)
        {
            _musicPlayer.volume = time / duration;
            yield return null;
        }

        //post fading out
        //musicPlayer.clip = metronomeClip;
        _musicPlayer.Stop();
        _musicPlayer.volume = 1;

        //counting off
        for (int j = 0; j < 2; j++)
        {
            _musicPlayer.pitch = 1;
            for (int i = 0; i < 3; i++)
            {
                _musicPlayer.PlayOneShot(metronomeClip);
                yield return new WaitForSeconds(_spb);
            }
            _musicPlayer.pitch = 2f;
            _musicPlayer.PlayOneShot(metronomeClip);
            yield return new WaitForSeconds(_spb);
        }

        //play music
        _musicPlayer.pitch = 1;
        _musicPlayer.clip = newLevel.music;
        _bpm = newLevel.bpm;
        _spb = 60 / _bpm;
        _musicPlayer.Play();
        _transitioning = false;
    }

    //class that keeps track of players' presses between beats
    private class PlayerPress
    {
        public bool BeatUsed = false;
    }
}
