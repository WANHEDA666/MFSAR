using Interfaces;
using UnityEngine;

namespace Views
{
    public class PlayerView : MonoBehaviour, IListenersSolver
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform playerTransform;
        
        public void AddListeners()
        {
            
        }

        public void SetDirection(Vector3 moveVector, Vector3 tempVector)
        {
            if (moveVector != new Vector3())
            {
                characterController.Move(tempVector);
                Animate(moveVector);
            }
            else
            {
                StopAnimation();
            }
        }

        private void Animate(Vector3 moveVector)
        {
            animator.SetInteger("State", 1);
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                var direction = Vector3.RotateTowards(Vector3.forward, moveVector, 3f, 0f);
                playerTransform.rotation = Quaternion.LookRotation(direction);
            }
        }
        
        private void StopAnimation()
        {
            animator.SetInteger("State", 0);
        }
        
        public void RemoveListeners()
        {
            Destroy(gameObject);
        }
    }
}