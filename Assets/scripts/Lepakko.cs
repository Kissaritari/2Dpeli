using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lepakko : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float nopeus = 4f;
    private SpriteRenderer _renderer;
    private Creature stats;
    
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Creature>();
        _rb= GetComponent<Rigidbody2D>();
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
        _rb.velocity = new Vector2( nopeus, _rb.velocity.y);

    }
}
