using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header(" Movement ")]
    public float moveSpeed = 8f;
    public float jumpForce = 15f;


    [Header(" Ground Collision ")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 groundCheckBoxSize;
    [SerializeField] private float groundCastDistance;


    [Header(" Wall Collision ")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Vector2 wallBoxSize;
    [SerializeField] private float wallCastDistance;

    [Header(" State ")]
    private bool _isGrounded;

    #region [--- Components ---]
    public Animator animator {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region [--- States ---]
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this,stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        Debug.Log("Ground " + isGrounded());
        Debug.Log("Wall " + isWallDetected());
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public bool isGrounded() => Physics2D.BoxCast(transform.position, groundCheckBoxSize, 0, -transform.up, groundCastDistance, whatIsGround);

    public bool isWallDetected() => Physics2D.BoxCast(transform.position, groundCheckBoxSize, 0, -transform.right, wallCastDistance, whatIsWall);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * groundCastDistance, groundCheckBoxSize);
        Gizmos.DrawWireCube(transform.position - transform.right * wallCastDistance, wallBoxSize);

    }
}
