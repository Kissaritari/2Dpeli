using System.Collections;
using UnityEngine;

namespace Assets.scripts
{
    public class Creature : MonoBehaviour
    {
        public int Damage;
        private int Health;
        public int MaxHealth;
        // Use this for initialization
        void Start()
        {
            Health = MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (Health <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
        public void GetDamaged(int Damage)
        {
            Health -= Damage;
        }
    }
}