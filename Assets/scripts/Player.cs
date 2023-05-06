using Assembly_CSharp;
using Assets.scripts;
using Cinemachine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public TextMeshProUGUI lifetext, gemtext,pbTime,currentTime,gemmilkm;
    public AudioSource kavelyaani, lyomisaani,pickup,teleport;
    public CinemachineVirtualCamera cam;
    public float hitCoolDown = 0.55f;
    private float nextHit;
    private Attack _playerAttack;
    [SerializeField] private Flash _dmgFlash;
    [SerializeField] private Color _flashColor;
    [SerializeField] private GameObject _panel;
    static public int pisteet = 0;
    
    private void Start()
    {
        gemmilkm.text = Portaali.gemmien_lkm.ToString();
        _playerAttack = GetComponent<Attack>();
        stats = GetComponent<Creature>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        lifetext.text = "Lives: " + lives.ToString();
        _renderer = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.HasKey("pb"))
        {
            float pb = PlayerPrefs.GetFloat(key: "pb");
            pbTime.text = pb.ToString() + " s";
        }

    }
    private void Update()
    {
        _animator.SetFloat(name: "speed", value: Mathf.Abs(_rb.velocity.x));
        horizontal = Input.GetAxis("Horizontal");
        if (!stats.dead)
        {
            // hyppy
            if (Input.GetButtonDown("Jump") && ilmassa == false)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpingpower);
                ilmassa = true;

            }
            // spriten käännöt
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _renderer.flipX = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _renderer.flipX = true;
            }
            // kävelyäänen toisto
            if (Mathf.Abs(_rb.velocity.x) > 0.3 && !ilmassa && !kavelyaani.isPlaying)
                kavelyaani.Play();

            // putoamiskuolema
            if (_rb.transform.position.y < -20)
            {
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, jumpingpower / 2);
                    stats.GetDamaged(stats.MaxHealth);
                    _rb.isKinematic = true;
                }
            }
            // lyönti
            if (Input.GetButtonDown("Fire1") && Time.time > nextHit)
            {
                _playerAttack.PlayerAttack();
                nextHit = Time.time + hitCoolDown;
                _animator.Play("Attack");
                lyomisaani.Play();
            }

        }
        else
            cam.Follow = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gem"))
        {
            pickup.Play();
            pisteet += 1;
            gemtext.text = pisteet.ToString();
            gemmilkm.text = (Portaali.gemmien_lkm - pisteet).ToString();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("portaali"))
        {
            if(!Portaali.activity)
            {
                _panel.SetActive(true);
            }
            else if (Portaali.activity)
            {
                teleport.Play();
                _dmgFlash.StartFlash(.3f, 1f, Color.blue);
                if (!PlayerPrefs.HasKey("pb") || PlayerPrefs.GetFloat("pb") < Time.timeSinceLevelLoad)
                {
                    PlayerPrefs.SetFloat(key: "pb", value: Time.timeSinceLevelLoad);
                }
                SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
      
        if (collision.gameObject.layer != 6)
        {
            return;
        }
        stats.GetDamaged(30);

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("portaali"))
        {
            if (!Portaali.activity)
            {
                _panel.SetActive(false);
            }
        }
    }
    private void FixedUpdate()
    {
        currentTime.text = Time.timeSinceLevelLoad.ToString();
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
        _dmgFlash.DeathColor(3f, 1f, _flashColor);
        _ = StartCoroutine(nameof(DeathAnim));
    }
    public void DmgFlash(float duration)
    {
        _dmgFlash.StartFlash(duration, .6f, _flashColor);
    }
    public IEnumerator DeathAnim()
    {

        if (!stats.dyingSound.isPlaying)
        {
            stats.dyingSound.Play();
        }

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


