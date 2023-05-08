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
        public AudioSource dyingSound, osumisaani
            ;
        void Start()
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
            if (!dead)
            {
                osumisaani.PlayDelayed(0.2f);
                Health -= Damage;
                if (gameObject.CompareTag("Player") && Health > 0)
                {
                    gameObject.GetComponent<Player>().DmgFlash(.6f);
                }
            }
        }
        public int GetHealth()
        {
            return Health;
        }

    }
}