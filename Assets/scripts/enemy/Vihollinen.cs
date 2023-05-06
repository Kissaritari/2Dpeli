using Assets.scripts;
using System.Collections;
using UnityEngine;

public class Vihollinen : MonoBehaviour
{


    private Rigidbody2D _rb;
    private bool ilmassa;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Creature stats;
    public AudioSource kuolemisaani, kavelyaani, lyomisaani;
    public float hitCoolDown = 0.55f;
    private float xCoord;
    public bool ruumis = false;


    private void Start()
    {
        stats = GetComponent<Creature>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        xCoord = transform.position.x;
    }
    private void Update()
    {
        _animator.SetFloat(name: "speed", value: Mathf.Abs(_rb.velocity.x));
        if (!stats.dead)
        {
            if (xCoord - transform.position.x < 0) { _renderer.flipX = true; }
            else if (xCoord - transform.position.x > 0) { _renderer.flipX = false; }

            if (Mathf.Abs(xCoord - transform.position.x) > 0.3 && !ilmassa)
            {
                _animator.Play("run");
                if (!kavelyaani.isPlaying)
                {
                    kavelyaani.Play();
                }

            }
            else
            {
                kavelyaani.Stop();
            }

            if (_rb.transform.position.y < -20)
            {
                {
                    stats.GetDamaged(stats.MaxHealth);
                }
            }

            xCoord = transform.position.x;
        }
    }


    public void Death()
    {
        if (!ruumis)
        {
            kavelyaani.enabled = false;
            _animator.Play("die");
            _rb.isKinematic = true;
            _ = StartCoroutine(nameof(DeathAnim));
            GetComponent<Collider2D>().enabled = false;
            _rb.velocity = Vector3.zero;
            enabled = false;
            ruumis = true;
        }

    }
    public void Hit()
    {
        GetComponent<Patrol>().attacking = true;
        lyomisaani.Play();
        _animator.Play("attack");

    }
    public IEnumerator DeathAnim()
    {
        if (!stats.dyingSound.isPlaying)
        {
            stats.dyingSound.Play();
        }
        yield return new WaitUntil(() => !stats.dyingSound.isPlaying);
        stats.dyingSound.Stop();
    }








}


