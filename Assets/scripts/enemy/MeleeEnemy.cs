using Assembly_CSharp;
using Assets.scripts;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform attackPoint;
    private float cooldownTimer = 0;
    private float sightTimer = 0;
    public BoxCollider2D _boxCollider;
    public Attack _attackScript;
    private Creature _creature;
    private Vihollinen _vihollinen;
    private Patrol _patrolScript;


    private void Start()
    {
        _patrolScript = GetComponent<Patrol>();
        _creature = GetComponent<Creature>();
        _vihollinen = GetComponent<Vihollinen>();

    }
    private void Update()
    {
        if (!_creature.dead)
        {
            cooldownTimer += Time.deltaTime;
          
            if (PlayerInSight())
            {
                sightTimer = cooldownTimer;
                _patrolScript.SetAttacking(true);
                if (cooldownTimer >= attackCooldown)
                {
                    print("cooldown ohi, hit käynnistyy");
                    
                    _vihollinen.Hit();
                }
            }
            if (cooldownTimer - sightTimer > 1.7 & !PlayerInSight())
            {
                print("poistetaan attacking true");
                _patrolScript.SetAttacking(false);
            }
          
                
        }


    }
    public void Swing()
    {
        cooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackScript.AttackRange, _attackScript.enemyLayers);
        foreach (var hit in hitEnemies)
        {
            if (!hit.CompareTag("Player"))
            {
                return;
            }
            hit.GetComponent<Creature>().GetDamaged(_creature.Damage);
        }

    }
    private bool PlayerInSight()
    {
        bool sight = false;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackScript.AttackRange, _attackScript.enemyLayers);
        foreach (var hit in hitEnemies)
        {
            if (hit.CompareTag("Player"))
            {
                sight = true;
                Debug.Log("sight true palautetaan");
            }
        }
        return sight;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, _attackScript.AttackRange);
    }
}

