using Assets.scripts;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Header("patrol points")]
    [SerializeField] private BoxCollider2D leftEdge;
    [SerializeField] private BoxCollider2D rightEdge;
    private float speed;
    private int direction = 1;
    public bool attacking = false;
    private Vihollinen _vihollinen;

    // Start is called before the first frame update
    void Start()
    {
        _vihollinen = GetComponent<Vihollinen>();
        speed = GetComponent<Creature>().Speed;
    }

    private void FixedUpdate()
    {
        if (!attacking && !_vihollinen.ruumis)
        {
            transform.Translate(direction * speed * Time.deltaTime * Vector2.left);

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "left")
        {
            direction = 1;
        }
        else if (other.name == "right")
        {
            direction = -1;
        }
    }
    public void SetAttacking(bool arvo)
    {
        attacking = arvo;
    }
}
