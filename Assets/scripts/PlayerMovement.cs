using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float jumpingpower = 6f;
    private float horizontal;
    public float nopeus = 8f;
    private Rigidbody2D _rb;
    private bool ilmassa;
    private SpriteRenderer _renderer;
    private Animator _animator;

    private void Start()
    {

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

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
        horizontal = Input.GetAxisRaw("Horizontal") ;
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
        
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(horizontal * nopeus, _rb.velocity.y);
        if (_rb.velocity.x != 0) { _animator.SetBool("movement", true); }
        else { _animator.SetBool("movement",false); }
        Debug.Log(_animator.GetFloat("movement"));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ilmassa = false;
    }


}


    