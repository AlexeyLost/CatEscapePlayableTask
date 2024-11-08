using UnityEngine;

namespace GAME.Scripts.Guards
{
    public class GuardView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        [field: SerializeField] public float TurnSpeed  { get; private set; }
        [field: SerializeField] public float AttackDistance  { get; private set; }
        [field: SerializeField] public GuardPlayerDetectorView PlayerDetectorView { get; private set; }
        
        
        private readonly int _attackAnimationKey = Animator.StringToHash("Attack");


        public void SetAttackAnimation()
        {
            _animator.SetTrigger(_attackAnimationKey);
        }
    }
}
