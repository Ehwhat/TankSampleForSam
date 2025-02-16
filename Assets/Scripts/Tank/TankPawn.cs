using Player;
using UnityEngine;

namespace Tank
{
    public class TankPawn : MonoBehaviour, IPawn
    {
        [SerializeField]
        private TankMovement _tankMovement;

        [SerializeField]
        private TankTurret _tankTurret;

        public void MoveInDirection(Vector3 direction) => _tankMovement.MoveInDirection(direction);
        public void AimInDirection(Vector3 direction) => _tankTurret.RotateTurretToFaceDirection(direction);

        public void OnPossess(PawnController pawnController)
        {
            Debug.Log("Tank possessed by " + pawnController.name);
        }

        public void OnUnpossess(PawnController pawnController)
        {
            Debug.Log("Tank unpossessed by " + pawnController.name);
        }
    }
}
