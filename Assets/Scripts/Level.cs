using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public AudioClip music;
    public float bpm;
    public Enemy[] spawnableEnemies;
    public Sprite bg;

    public Level(AudioClip music, float bpm)
    {
        this.music = music;
        this.bpm = bpm;
    }
}
