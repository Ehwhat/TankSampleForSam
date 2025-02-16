using UnityEngine;

namespace Player
{
    public interface IPawn
    {
        virtual void OnPossess(PawnController pawnController)
        {
        }

        virtual void OnUnpossess(PawnController pawnController)
        {
        }
    }
}
