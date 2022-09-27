using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public Collider2D Collider;

    public Transform GroundSensor;
    public float SensorRadius = 0.1f;
    public LayerMask GroundLayer;

    public float MovementVelocity = 10f;
    public float JumpForce = 10f;

    private float _horAxis;
    private float _vertAxis;
    private bool _jumpButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horAxis = Input.GetAxis("Horizontal");
        _vertAxis = Input.GetAxis("Vertical");
        _jumpButton = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        var velocity = new Vector2(0, RigidBody.velocity.y);
        var bottom = new Vector2(Collider.bounds.center.x, Collider.bounds.center.y - Collider.bounds.size.y/2);
        velocity.x = _horAxis * MovementVelocity * Time.fixedDeltaTime;
        //Debug.LogWarning(bottom);
        if (_jumpButton && Mathf.Abs(RigidBody.velocity.y) < .1 &&Physics2D.OverlapCircle(bottom, SensorRadius, GroundLayer))
        {
            //Debug.LogWarning("Jump Block...");
            velocity.Set(0, JumpForce * Time.fixedDeltaTime);
        }

        RigidBody.velocity = velocity;
    }
}
