using Assets.scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Assembly_CSharp
{
    public class Attack : MonoBehaviour
    {
 
        private Animator _Animator;
        private Creature _Creature;
        public Transform attackPoint;
        public float _attackRange = 0.5f;
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

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayers);

            foreach (var hit in hitEnemies)
            {
                Debug.Log(hit.name);
                hit.GetComponent<Creature>().GetDamaged(_Creature.Damage);
            }
        }
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
        }
    }
}