using Assembly_CSharp;
using Assets.scripts;
using System.Collections;
using UnityEngine;

public class Vihollinen : MonoBehaviour
{
    private Patrol _patrolScript;
    private Attack attackScript;
    private Rigidbody2D _rb;
    private bool ilmassa;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Creature stats;
    public AudioSource kuolemisaani, kavelyaani, lyomisaani;
    [SerializeField] private BoxCollider2D playerDetector; 
    public float hitCoolDown = 0.55f;
    private float xCoord;
    public bool ruumis = false;
    private bool pelaajanVieres = false;

    private void Start()
    {
        _patrolScript = GetComponent<Patrol>();
        attackScript = GetComponent<Attack>();
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
            if (xCoord - transform.position.x > 0 & !pelaajanVieres) 
            {
                gameObject.transform.localScale = new Vector3(8.430253f, 8.430253f, 8.430253f);
              
            }
            else if (xCoord - transform.position.x < 0 & !pelaajanVieres) 
            {
                gameObject.transform.localScale = new Vector3(-8.430253f, 8.430253f, 8.430253f);

            }
   
            if (Mathf.Abs(xCoord - transform.position.x) > 0.03 && !ilmassa && !_patrolScript.attacking)
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
        Debug.Log("laitetaan attacking trueksi");
       
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

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !ruumis)
        {
            pelaajanVieres = true;

            if (collision.gameObject.transform.position.x - gameObject.transform.position.x < 0)
            {
                gameObject.transform.localScale = new Vector3(8.430253f, 8.430253f, 8.430253f);
            }
            else if (collision.gameObject.transform.position.x - gameObject.transform.position.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-8.430253f, 8.430253f, 8.430253f);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pelaajanVieres = false;
        }
    }
    





}


