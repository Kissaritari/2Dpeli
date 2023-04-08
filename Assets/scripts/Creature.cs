using System.Collections;
using UnityEngine;

namespace Assets.scripts
{
    public class Creature : MonoBehaviour
    {
        public int Damage;
        private int Health;
        public int MaxHealth;
        public float Speed;
      

        // Update is called once per frame
        void Update()
        {
            if (Health <= 0)
            {
                if (gameObject.tag != "Player")
                {
                    Destroy(gameObject);
                }
                else if (gameObject.tag == "Player")
                {
                    GetComponent<Player>().Death();
                }
            }
        }
        public void GetDamaged(int Damage)
        {
            Health -= Damage;
        }


    }
}