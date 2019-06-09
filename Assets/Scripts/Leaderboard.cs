using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    private ScrollRect _scroll;

    [SerializeField]
    private HighScoreDisplayItem _displayItem;
    
    private void Awake()
    {
        _scroll = GetComponentInChildren<ScrollRect>();
    }
    
    private void Start()
    {
        var scores = GameManager.GetSortedHighScore();
        for (var i = 0; i < scores.Count; i++)
        {
            var item = Instantiate(_displayItem, Vector3.zero, Quaternion.identity, _scroll.content);
            item.SetHighScore(scores[i]);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
