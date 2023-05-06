using Assets.scripts;
using UnityEngine;

namespace Assembly_CSharp
{
    public class Attack : MonoBehaviour
    {

        private Animator _Animator;
        private Creature _Creature;
        public Transform attackPoint;
        public float AttackRange = 0.5f;
        public LayerMask enemyLayers;


        // Use this for initialization
        void Start()
        {
            _Animator = GetComponent<Animator>();
            _Creature = GetComponent<Creature>();
        }

        // Update is called once per frame


        public void PlayerAttack()
        {
            _Animator.Play("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, enemyLayers);

            foreach (var hit in hitEnemies)
            {

                hit.GetComponent<Creature>().GetDamaged(_Creature.Damage);
            }
        }
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
        }
    }
}