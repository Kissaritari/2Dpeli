using Assets.scripts;
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
    public float nopeus = 8f;
    private Rigidbody2D _rb;
    private bool ilmassa;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Creature stats;
    static int lives = 3;
    public TextMeshProUGUI lifetext;
    
    private void Start()
    {
        
        stats = GetComponent<Creature>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        lifetext.text = "Lives: " + lives.ToString();

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
        if (_rb.transform.position.y < -20) {
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpingpower/2);
                stats.GetDamaged(stats.MaxHealth);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.Play("Attack");

        }

    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(horizontal * nopeus, _rb.velocity.y);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ilmassa = false;
    }
    public void Death()
    {
        _animator.Play("Death");
        StartCoroutine("DeathAnim");
    }
    public IEnumerator DeathAnim()
    {
        _animator.SetBool("dead",true);
        yield return new WaitForSeconds(3);
        _rb.isKinematic = true;
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


    