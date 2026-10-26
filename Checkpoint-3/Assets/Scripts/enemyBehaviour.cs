using System.Collections;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float distanceWakeUp;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float distanceReturn;
    [SerializeField] private int life;
    [SerializeField] private Vector2 startPosition;

    private bool wakeUp;
    private bool attacking;
    private bool damage;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null)
        {
            Debug.Log("Player Null.");
            return;
        }

        if (wakeUp == false)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceWakeUp)
            {
                wakeUp = true;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceAttack)
            {
                Attack();
            }
            else
            {
                Move();
                /*if (Vector2.Distance(transform.position, player.transform.position) > distanceReturn)
                {
                    transform.Translate(startPosition.x * Time.deltaTime, startPosition.y * Time.deltaTime, 0.0f);
                }
                else
                {
                    Move();
                }*/
            }
        }

        AnimatorManager();
    }

    void Move()
    {
        if (attacking == true) return;
        if (damage == true) return;
        if(life == 0) return;


        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (transform.position.x < player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

    }

    void Attack()
    {
        if (damage == true) return;
        if (life == 0) return;
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        attacking = true;
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }


    void AnimatorManager()
    {
        animator.SetBool("pWakeUp", wakeUp);
        animator.SetBool("pAttack", attacking);
        animator.SetBool("pHit", damage);
        animator.SetBool("pDead", life == 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Attack")
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        damage = true;

        life--;

        if (life <= 0)
        {
            yield return new WaitForSeconds(3.0f);
            Destroy(gameObject, 2.0f);
        }
    }
}
