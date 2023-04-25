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
        public bool dead;
        void Start () 
        {
            Health = MaxHealth;
            dead = false;
        }
        // Update is called once per frame
        void Update()
        {
            
           // Debug.Log(Health);
            if (Health <= 0)
            {
                dead = true;
                
                if (gameObject.CompareTag("Vihollinen"))
                {
                    Debug.Log("kuoleminen");
                    GetComponent<Vihollinen>().Death();
                }
                else if (gameObject.CompareTag("Player"))
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