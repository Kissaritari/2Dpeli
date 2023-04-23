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
        private float _attackRange = 0.5f;
        public LayerMask enemyLayers;

        // Use this for initialization
        void Start()
        {
            _Animator = GetComponent<Animator>();
            _Creature = GetComponent<Creature>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerAttack();

            }

      
        }

        void PlayerAttack()
        {
            _Animator.Play("Attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayers);

            foreach (var hit in hitEnemies)
            {
                hit.GetComponent<Creature>().GetDamaged(_Creature.Damage);
            }
        }
    }
}