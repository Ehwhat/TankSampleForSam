using Unity.Cinemachine;
using UnityEngine;
using Tank;

namespace Player.Tank
{
    [RequireComponent(typeof(LocalInput))]
    public class HumanTankController : PawnController<TankPawn>
    {
        [SerializeField]
        private CinemachineCamera _camera;

        private LocalInput _input;

        private void Awake()
        {
            _input = GetComponent<LocalInput>();
            if (_input == null)
            {
                Debug.LogError("LocalInput component not found on GameObject");
            }
        }

        public override void Possess(TankPawn pawn)
        {
            base.Possess(pawn);
            _camera.Follow = pawn.transform;
        }

        public override void Unpossess()
        {
            base.Unpossess();
            _camera.Follow = null;
        }

        private void Update()
        {
            if (Pawn == null) return;

            Vector3 steerDirection = TransformToCameraSpace(_input.SteerInput);
            Pawn.MoveInDirection(steerDirection);

            Vector3 aimDirection = TransformToCameraSpace(_input.AimInput);
            Pawn.AimInDirection(aimDirection);

        }

        private Vector3 TransformToCameraSpace(Vector2 inputDirection)
        {
            Vector3 worldDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
            if (!_camera) return worldDirection;

            Vector3 cameraForward = _camera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = _camera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            return cameraForward * worldDirection.z + cameraRight * worldDirection.x;
        }
    }
}
