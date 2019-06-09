using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private Transform _leftThruster;

    [SerializeField]
    private Transform _rightThruster;

    [SerializeField]
    private float thrustForce;

    [SerializeField]
    private Transform _leftSide;

    [SerializeField]
    private Transform _rightSide;

    [SerializeField]
    private float sideForce;

    private float _evaluation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameManager.SetShip(this);
    }

    private void Start()
    {
        // TODO: move this, I think we want a count down.
        GameManager.Start();
    }

    private void Update()
    {
        // TODO: might want and Input controller of some sort?
        // TODO: at the least we need to use GetAxis or GetAction to use the arcade stick.
        if (Input.GetKeyDown(KeyCode.F))
        {
            _evaluation = RhythmManager.Instance.EvaluatePress(Thrusters.MainLeft);
            //Debug.Log($"left: {_evaluation}");
            _rigidbody.AddForceAtPosition(thrustForce * _evaluation * _leftThruster.up, _leftThruster.position);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            _evaluation = RhythmManager.Instance.EvaluatePress(Thrusters.MainRight);
            //Debug.Log($"right: {_evaluation}");
            _rigidbody.AddForceAtPosition(thrustForce * _evaluation * _rightThruster.up, _rightThruster.position);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _evaluation = RhythmManager.Instance.EvaluatePress(Thrusters.SidLeft);
//            Debug.Log($"side left: {_evaluation}");
            _rigidbody.AddForceAtPosition(sideForce * _evaluation * _leftSide.right, _leftSide.position);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _evaluation = RhythmManager.Instance.EvaluatePress(Thrusters.SideRight);
//            Debug.Log($"side right: {_evaluation}");
            _rigidbody.AddForceAtPosition(-sideForce * _evaluation * _rightSide.right, _rightSide.position);
        }
    }

    private void OnDestroy()
    {
        GameManager.FireShipDeath();
    }
}