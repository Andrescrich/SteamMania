using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private Animator _anim;
    private Rigidbody2D _rb;
    private SpriteRenderer _sR;
    private PlayerStates _pS;
    public bool _canFlip = true;
    [SerializeField] private float velocity = 350f;
    [SerializeField] private float jumpForce = 500f;

    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sR = GetComponentInChildren<SpriteRenderer>();
        _pS = GetComponent<PlayerStates>();

    }

    // Update is called once per frame
    private void Update()
    {
        
        
        
        var dirMov = Input.GetAxisRaw("Horizontal");
        Move(dirMov, velocity);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _anim.SetTrigger(Shoot);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        
        _anim.SetFloat(Speed, Mathf.Abs(dirMov));
    }

    private void Move(float dirMov, float vel)
    {
        var vector = new Vector2(dirMov * vel * Time.deltaTime, _rb.velocity.y);
        _rb.velocity = vector;
        
        if (!_canFlip) return;
        if (dirMov > 0)
        {
            _sR.flipX = true;
        }
        else if (dirMov < 0)
        {
            _sR.flipX = false;
        }
    }

    private void Jump()
    {
        if (!_pS.isGrounded) return;
        var vector = new Vector2(_rb.velocity.x, jumpForce * Time.deltaTime);
        _rb.velocity = vector;
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            _pS.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            _pS.isGrounded = false;
        }
    }
}
