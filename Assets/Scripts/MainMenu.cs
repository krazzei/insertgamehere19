using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour
{
    private AudioSource _source;

    [SerializeField]
    private AudioClip _ohYeah;
    
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        _source.PlayOneShot(_ohYeah);
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        yield return new WaitForSeconds(_ohYeah.length);
        SceneManager.LoadScene("Final_Scene");
    }

    public void DisplayLeaderboard()
    {
        SceneManager.LoadScene("Leaderboards");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
