using UnityEngine;

namespace Utility
{
    public static class RigidbodyEx
    {
        public static void ApplyTorqueToReachRotation(this Rigidbody rigidbody, 
            Quaternion targetRotation, 
            float force, 
            float damping)
        {
            Quaternion deltaRotation = targetRotation * Quaternion.Inverse(rigidbody.rotation);
            deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
            if (angle > 180f) angle -= 360f;
            Vector3 torque = axis * (angle * Mathf.Deg2Rad * force) - rigidbody.angularVelocity * damping;
            rigidbody.AddTorque(torque, ForceMode.Acceleration);
        }
    }
}