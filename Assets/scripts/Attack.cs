using Assets.scripts;
using System.Collections;
using UnityEngine;

namespace Assembly_CSharp
{
    public class Attack : MonoBehaviour
    {
 
        private Animator _Animator;
        private Creature _Creature;
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
                _Animator.SetTrigger("Attack");

            }
            if (Input.GetButtonUp("Fire1"))
            {
                _Animator.ResetTrigger("Attack");
            }

      
        }

        void PlayerAttack(GameObject target)
        {
            target.GetComponent<Creature>().GetDamaged(_Creature.Damage);
        }
    }
}