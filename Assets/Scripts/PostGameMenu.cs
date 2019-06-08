using UnityEngine;
using UnityEngine.UI;

public class PostGameMenu : MonoBehaviour
{
    private string _playerInitials;

    [SerializeField]
    private InputField _initialsField;

    private void Start()
    {
        if (_initialsField != null)
        {
            _initialsField.onEndEdit.AddListener(InitialsChanged);
            _initialsField.onValueChanged.AddListener(InitialsChanged);
        }
        else
        {
            Debug.LogError("Initials Field was not set");
        }
    }

    public void Restart()
    {
        GameManager.RecordScore(_playerInitials);
        GameManager.Restart();
    }

    public void Continue()
    {
        GameManager.RecordScore(_playerInitials);
        Physics.gravity = Vector3.zero;
        Destroy(gameObject);
    }

    private void InitialsChanged(string input)
    {
        Debug.Log(input);
        _playerInitials = input;
    }
}
