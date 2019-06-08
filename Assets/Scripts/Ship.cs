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
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameManager.SetShip(this);
    }
    
    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForceAtPosition(_leftThruster.up * thrustForce, _leftThruster.position);
            _rigidbody.AddForceAtPosition(_rightThruster.up * thrustForce, _rightThruster.position);
        }
        
        // TODO: might want and Input controller of some sort?
        // TODO: at the least we need to use GetAxis or GetAction to use the arcade stick.
        if (Input.GetKeyDown(KeyCode.F))
        {
            _rigidbody.AddForceAtPosition(_leftThruster.up * thrustForce, _leftThruster.position);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            _rigidbody.AddForceAtPosition(_rightThruster.up * thrustForce, _rightThruster.position);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _rigidbody.AddForceAtPosition(thrustForce * 0.5f * _leftSide.right, _leftSide.position);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _rigidbody.AddForceAtPosition(thrustForce * -0.5f * _rightSide.right, _rightSide.position);
        }
    }

    private void OnDestroy()
    {
        GameManager.FireShipDeath();
    }
}