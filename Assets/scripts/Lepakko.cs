using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lepakko : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float nopeus = 4f;
    // Start is called before the first frame update
    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2( nopeus, _rb.velocity.y);
    }
}
