using Assets.scripts;
using UnityEngine;

public class Lepakko : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private Creature stats;
    private float y = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Creature>();
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Lepakko is missing a Rigidbody2D component");
        }
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Lepakko-Sprite is missing a renderer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(stats.Speed * Time.deltaTime, y * Time.deltaTime, 0f);


        if ((GetComponent<Transform>().position.x > 8f) ||
            (GetComponent<Transform>().position.x < -8f))
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
