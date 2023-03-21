using System.Collections;
using UnityEngine;

namespace Assembly_CSharp
{
    public class Attack : MonoBehaviour
    {
        float damage;
        GameObject target;
        Animator _Animator;
        // Use this for initialization
        void Start()
        {
            _Animator = GetComponent<Animator>();
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
    }
}