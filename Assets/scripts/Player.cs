using Assets.scripts;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private float jumpingpower = 6f;
    private float horizontal;
    private Rigidbody2D _rb;
    private bool ilmassa;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Creature stats;
    static int lives = 3;
    public TextMeshProUGUI lifetext;
    public AudioSource kuolemisaani,kavelyaani;
    public CinemachineVirtualCamera cam;
    private void Start()
    {
        stats = GetComponent<Creature>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        lifetext.text = "Lives: " + lives.ToString();
        Debug.Log(_rb.isKinematic);

       
        if (_rb == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }
    }
    private void Update()
    {
        _animator.SetFloat(name: "speed", value: Mathf.Abs(_rb.velocity.x));
        horizontal = Input.GetAxis("Horizontal") ;
        if (!stats.dead)
        {
            if (Input.GetButtonDown("Jump") && ilmassa == false)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpingpower);
                ilmassa = true;
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _renderer.flipX = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _renderer.flipX = true;
            }
            if (Mathf.Abs(_rb.velocity.x) > 0.3 && !ilmassa && !kavelyaani.isPlaying)
                kavelyaani.Play();

            if (_rb.transform.position.y < -20)
            {
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, jumpingpower / 2);
                    stats.GetDamaged(stats.MaxHealth);
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                _animator.Play("Attack");
            }
        }
        else
            cam.Follow = null;
    }
    private void FixedUpdate()
    {
        if (!stats.dead)
        _rb.velocity = new Vector2(horizontal * stats.Speed, _rb.velocity.y);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ilmassa = false;
    }
    public void Death()
    {
        
        _animator.Play("Death");
        _ = StartCoroutine("DeathAnim");
    }
    public IEnumerator DeathAnim()
    {
        _rb.isKinematic = true;
        Debug.Log(_rb.isKinematic);
        _animator.SetBool("dead",true);
        kuolemisaani.Play();
        yield return new WaitForSeconds(3);
        
        if (lives > 0)
        {
            lives -= 1;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        
    }



}


    