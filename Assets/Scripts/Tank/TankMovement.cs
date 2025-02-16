using System;
using UnityEngine;
using Utility;

namespace Tank
{
    [RequireComponent(typeof(Rigidbody))]
    public class TankMovement : MonoBehaviour
    {
        public float accelerationForce = 10f;
        public float frontDragFactor = 0.1f;
        public float sideDragFactor = 0.1f;

        public float forwardFactor = 1f;
        public float reverseFactor = 0.7f;

        public float rotationTorqueForce = 10f;
        public float rotationDamper = 0.1f;

        private Rigidbody _rigidbody;
        private Vector3 _targetVector;
        private Vector3 _lastDirection;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody == null)
            {
                Debug.LogError("Rigidbody component not found on GameObject");
            }

            _lastDirection = transform.forward;
        }

        public void MoveInDirection(Vector3 direction)
        {
            _targetVector = direction;
        }

        private void FixedUpdate()
        {
            bool isTargetVectorZero = _targetVector.sqrMagnitude < 0.01f;
            if (!isTargetVectorZero)
            {
                ApplyAcceleration(_targetVector);
                _lastDirection = _targetVector.normalized;
            }

            _rigidbody.ApplyTorqueToReachRotation(
                Quaternion.LookRotation(_lastDirection, transform.up),
                rotationTorqueForce,
                rotationDamper);

            ApplyDrag();
        }

        private void ApplyAcceleration(Vector3 vector)
        {
            var xzForward = new Vector3(transform.forward.x, 0f, transform.forward.z);
            float accelerationFactor =
                Mathf.Lerp(reverseFactor, forwardFactor, (Vector3.Dot(vector.normalized, xzForward) + 1) * 0.5f);
            float speed = vector.magnitude * accelerationForce * accelerationFactor;
            _rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }

        private void ApplyDrag()
        {
            ApplyForwardDrag();
            ApplySideDrag();
        }

        private void ApplyForwardDrag()
        {
            Vector3 forwardVelocity = Vector3.Project(_rigidbody.linearVelocity, transform.forward);
            _rigidbody.AddForce(-forwardVelocity * frontDragFactor, ForceMode.Acceleration);
        }

        private void ApplySideDrag()
        {
            Vector3 sideVelocity = Vector3.Project(_rigidbody.linearVelocity, transform.right);
            _rigidbody.AddForce(-sideVelocity * sideDragFactor, ForceMode.Acceleration);
        }
    }
}
