using System;
using UnityEngine;

namespace Player
{
    public abstract class PawnController : MonoBehaviour
    {
        public abstract void Unpossess();
        public abstract IPawn GetPawn();
    }

    public abstract class PawnController<T> : PawnController where T : IPawn
    {
        [field: SerializeField]
        public T Pawn { get; private set; }

        private void Start()
        {
            if (Pawn != null)
            {
                Possess(Pawn);
            }
        }

        public virtual void Possess(T pawn)
        {
            if (Pawn != null && !Equals(Pawn, pawn))
            {
                Pawn.OnUnpossess(this);
            }

            Pawn = pawn;
            Pawn.OnPossess(this);
        }

        public override void Unpossess()
        {
            if (Pawn == null) return;
            Pawn.OnUnpossess(this);
            Pawn = default(T);
        }

        public override IPawn GetPawn()
        {
            return Pawn;
        }
    }
}
