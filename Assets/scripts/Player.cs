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
    public AudioSource kavelyaani, lyomisaani,pickup,teleport,viestiAani;
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
        pisteet = 0;
        gemtext.text = pisteet.ToString();
        gemmilkm.text = Portaali.gemmien_lkm.ToString();
        _playerAttack = GetComponent<Attack>();
        stats = GetComponent<Creature>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        lifetext.text = "Lives: " + lives.ToString();
        _renderer = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.HasKey("pb" + SceneManager.GetActiveScene().buildIndex.ToString()))
        {
            float pb = PlayerPrefs.GetFloat(key: "pb" + SceneManager.GetActiveScene().buildIndex.ToString());
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
                gameObject.transform.localScale = new Vector3(5.191104f, 4.700242f, 2.1049f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                gameObject.transform.localScale = new Vector3(-5.191104f, 4.700242f, 2.1049f);
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
                viestiAani.Play();
                _panel.SetActive(true);
            }
            else if (Portaali.activity)
            {
                if (!teleport.isPlaying) 
                {
                    teleport.Play(); 
                }
                
                _dmgFlash.DeathColor(1.6f, 1f, Color.blue);
                
                if (!PlayerPrefs.HasKey("pb" + SceneManager.GetActiveScene().buildIndex.ToString()) ||
                    PlayerPrefs.GetFloat("pb" + SceneManager.GetActiveScene().buildIndex.ToString()) > Time.timeSinceLevelLoad &&
                    PlayerPrefs.GetFloat("pb" + SceneManager.GetActiveScene().buildIndex.ToString()) > 0f)
                {
                    PlayerPrefs.SetFloat(key: "pb" + SceneManager.GetActiveScene().buildIndex.ToString(), value: Time.timeSinceLevelLoad);
                }
                _ = StartCoroutine(nameof(portalEffects));
                
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
    public IEnumerator portalEffects()
    {
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
    }



}


