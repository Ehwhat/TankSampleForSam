using UnityEngine;

namespace Tank
{
    public class TankTurret : MonoBehaviour
    {
        public float rotationSpeedDegreesPerSecond = 180f;

        private Vector3 _targetDirection;

        private void Start()
        {
            _targetDirection = transform.forward;
        }

        private void Update()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                rotationSpeedDegreesPerSecond * Time.deltaTime);
        }

        public void RotateTurretToFaceDirection(Vector3 targetDirection)
        {
            if (targetDirection.sqrMagnitude < 0.01f) return;

            _targetDirection = targetDirection.normalized;
        }
    }
}
