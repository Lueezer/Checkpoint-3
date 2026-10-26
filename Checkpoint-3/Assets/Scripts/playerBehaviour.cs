using System.Collections;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    [Header("Move Settings")]
    [Space]
    [SerializeField] private float speed;

    [Header("Attack Settings")]
    [Space]
    [SerializeField] private GameObject attackObject;


    private Vector2 direction;
    private bool attack;

    private Rigidbody2D rigidbody2D;
    private Collider2D collider2D;

    private Collider2D attackCollider;

    void Awake()
    {
        attackCollider = attackObject.GetComponent<Collider2D>();
        attackCollider.enabled = false;

        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        Move();
        Attack();
    }

    void FixedUpdate()
    {
        OnMove();
    }

    void Move()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rigidbody2D.linearVelocity = direction.normalized * speed;
    }

    void Attack()
    {
        attack = Input.GetButtonDown("Jump");

        if (attack)
        {
            attackCollider.enabled = true;
        }
    }
}
