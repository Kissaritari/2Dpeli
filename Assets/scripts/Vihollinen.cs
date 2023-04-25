using Assets.scripts;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vihollinen : MonoBehaviour
{

    
    private Rigidbody2D _rb;
    private bool ilmassa;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Creature stats;
    public AudioSource kuolemisaani,kavelyaani,lyomisaani;
    public float hitCoolDown = 0.55f;
    private float nextHit;
    private void Start()
    {
        stats = GetComponent<Creature>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        _animator.SetFloat(name: "speed", value: Mathf.Abs(_rb.velocity.x));
        if (!stats.dead)
        {
            if (_rb.velocity.x > 0) { _renderer.flipX = true; }
            else { _renderer.flipX = false; }

            if (Mathf.Abs(_rb.velocity.x) > 0.3 && !ilmassa)
            {
                _animator.Play("run");
                if (!kavelyaani.isPlaying)
                {
                    kavelyaani.Play();
                }
                   
            }
            if (_rb.transform.position.y < -20)
            {
                {
                    stats.GetDamaged(stats.MaxHealth);
                }
            }
           /* if (Time.time > nextHit )
            {
                nextHit = Time.time + hitCoolDown;
                _animator.Play("Attack");
                lyomisaani.Play();
            }*/
        }
    }
 

    public void Death()
    {
        Debug.Log("kuoleminen");
        _animator.Play("die");
        _rb.isKinematic = true;
        kuolemisaani.Play();
        GetComponent<Collider2D>().enabled = false;
       this.enabled = false;
    }
    
    
       

        



}


    