using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplayItem : MonoBehaviour
{
    [SerializeField]
    private Text _initials;
    [SerializeField]
    private Text _score;

    public void SetHighScore(HighScore score)
    {
        _initials.text = score.Initials;
        _score.text = TimeToString(score.Time);
    }

    private string TimeToString(float time)
    {
        var minutes = (int) time / 60;
        return $"{minutes}:{time - minutes * 60:0.0000}";
    }
}
