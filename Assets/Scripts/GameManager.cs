using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static Ship _ship;
    private static float _startTime;
    private static float _endTime;

    private static HighScoreManager _highScoreManager;

    static GameManager()
    {
        Debug.Log(Application.persistentDataPath);
        _highScoreManager = new HighScoreManager(Application.persistentDataPath + "/IGH19_highScore.json");
    }

    public static void SetShip(Ship ship)
    {
        _ship = ship;
    }

    public static event Action OnShipDeath;

    public static void FireShipDeath()
    {
        OnShipDeath?.Invoke();
    }

    public static event Action OnWinTriggerHit;

    public static void FireWinTriggerHit()
    {
        Time.timeScale = 0; // Might have issues with this...
        _endTime = Time.time;
        OnWinTriggerHit?.Invoke();
    }

    public static void Restart()
    {
        SceneManager.LoadScene("TwigTestScene");
        Time.timeScale = 1;
    }

    public static void Start()
    {
        _startTime = Time.time;
        LevelManager.nextLevel();
    }
    
    public static void RecordScore(string initials)
    {
        var time = _endTime - _startTime;
        _highScoreManager.UpdateOrInsertHighScore(initials, time);
    }

    public static List<HighScore> GetSortedHighScore()
    {
        return _highScoreManager.GetSortedHighScore();
    }
}
