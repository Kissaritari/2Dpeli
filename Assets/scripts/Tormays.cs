﻿using System.Collections;
using UnityEditor.UI;
using UnityEngine;

namespace Assets.scripts
{
    public class Tormays : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.tag == "Player")
            {
                int damage = GameObject.GetComponent<Creature>().Damage;
                collision.gameObject.GetComponent<Creature>().GetDamaged(damage);

            }
        }
    }
}
