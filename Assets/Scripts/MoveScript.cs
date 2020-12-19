using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveScript : MonoBehaviour
{
    
    public float Acceleration;
    public float TopSpeed;
    public float TurningSpeed;
    
    // Moving
    private float _acceleration;
    private bool _isAirborn;
    
    // Rotation
    private Quaternion _rotation;
    private Vector3 _direction;

    private Rigidbody _rigidbody3D;
    private static readonly Vector3 MoveThreshold = new Vector3(0.01f, 0, 0.01f);
    
    // Start is called before the first frame update
    void Start()
    {
        _rotation = transform.rotation;
        _rigidbody3D = GetComponent<Rigidbody>();
        _acceleration = Acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Debug.Log(direction + " " + Vector3.Distance(direction, MoveThreshold));
        
        if (Vector3.Distance(direction, MoveThreshold) > 0.05f)
        {
            // Rotate to position pressed
            _rotation = Quaternion.LookRotation(direction.normalized);
            // Accelerate player

            var movingDirection = _isAirborn ? _rigidbody3D.velocity.normalized : direction.normalized;
            _rigidbody3D.AddForce(movingDirection * (_acceleration * Time.deltaTime));
        }
        else if (Vector3.Distance(Vector3.zero, _rigidbody3D.velocity) > 0.001f)
        {
            // Rotate to moving position if no input is pressed
            _rotation = Quaternion.LookRotation(_rigidbody3D.velocity.normalized);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, TurningSpeed * Time.deltaTime);

        // Cap speed
        if (_rigidbody3D.velocity.magnitude > TopSpeed)
        {
            _rigidbody3D.velocity = _rigidbody3D.velocity.normalized * TopSpeed;
        }
        
        CheckFloorPhysics();
    }

    private void CheckFloorPhysics()
    {
        // Detect floor
        var result = new RaycastHit[1];
        var size = Physics.RaycastNonAlloc(new Ray(transform.position, Vector3.down), result, 0.55f);

        _isAirborn = size == 0;
        
        if (size <= 0)
        {
            return;
        }
        
        if (result[0].transform.CompareTag("Gras"))
        {
            _rigidbody3D.drag = 1;
            _acceleration = Acceleration - 10;
            Debug.Log("gras");
        } else if (result[0].transform.CompareTag("Street"))
        {
            _acceleration = Acceleration;
            _rigidbody3D.drag = 2;
            Debug.Log("street");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.55f);
    }
}
