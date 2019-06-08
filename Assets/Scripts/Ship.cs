using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private GameObject _leftThruster;

    [SerializeField]
    private GameObject _rightThruster;

    [SerializeField]
    private float thrustForce;
    
    private Transform _leftTransform;
    
    private Transform _rightTransform;

    [SerializeField]
    private Transform _leftSide;

    [SerializeField]
    private Transform _rightSide;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _leftTransform = _leftThruster.transform;
        _rightTransform = _rightThruster.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForceAtPosition(_leftTransform.up * thrustForce, _leftTransform.position);
            _rigidbody.AddForceAtPosition(_rightTransform.up * thrustForce, _rightTransform.position);
        }
        
        // TODO: might want and Input controller of some sort?
        // TODO: at the least we need to use GetAxis or GetAction to use the arcade stick.
        if (Input.GetKeyDown(KeyCode.F))
        {
            _rigidbody.AddForceAtPosition(_leftTransform.up * thrustForce, _leftTransform.position);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            _rigidbody.AddForceAtPosition(_rightTransform.up * thrustForce, _rightTransform.position);
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
}