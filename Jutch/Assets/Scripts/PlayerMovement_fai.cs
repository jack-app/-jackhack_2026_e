using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement_fai : MonoBehaviour
{
    [Header("基本移動パラメータ")]
    public float moveSpeed = 8f;
    public float normalJumpForce = 12f;

    [Header("物理・地面検知")]
    public float fallMultiplier = 4.5f;   // 落下時の重力倍率
    public float lowJumpMultiplier = 3f;  // 短くジャンプした時の重力倍率
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // アニメーション状態の定義
    public enum MovementState { Idle, Running, Jumping, Falling, Dashing }
    
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerAbility_fai ability;

    [HideInInspector] public bool isGrounded;
    private float moveInput;
    private float lockedAirSpeed;
    
    // 他のスクリプトからアクセスできるように public に設定
    [HideInInspector] public bool isFacingRight = true;

    [SerializeField] GroundChecker groundChecker;

    public float beltspeed = 0; //ベルトコンベアの速度管理

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ability = GetComponent<PlayerAbility_fai>();
        rb.gravityScale = 1.5f;
        beltspeed = 0;
    }

    void Update()
    {
        // 地面接地判定
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = groundChecker.IsGrounded;
        Debug.Log($"isGrounded:{isGrounded}");

        // ダッシュ中は入力を受け付けない
        if (ability != null && ability.isDashing) 
        {
            UpdateAnimationAndFacing();
            return;
        }

        // 入力の取得
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        else if (Input.GetKey(KeyCode.D)) moveInput = 1f;

        // ジャンプ処理
        float jumpForce = (ability.currentAbility == PlayerAbility_fai.AbilityType.Blue) ? ability.highJumpForce : normalJumpForce;
        
        if (isGrounded)
        {
            lockedAirSpeed = moveInput * moveSpeed; 
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        UpdateAnimationAndFacing();
    }

    void FixedUpdate()
    {
        if (ability != null && ability.isDashing) return;

        // 水平移動：地上では入力(moveInput)を、空中では離陸時の速度(lockedAirSpeed)を使用
        rb.velocity = new Vector2(isGrounded ? moveInput * moveSpeed - beltspeed : lockedAirSpeed, rb.velocity.y);

        ApplyCustomGravity();
    }

    private void UpdateAnimationAndFacing()
    {
        if (moveInput > 0 && isFacingRight) Flip();
        else if (moveInput < 0 && !isFacingRight) Flip();

        MovementState state;

        if (ability != null && ability.isDashing)
        {
            state = MovementState.Dashing;
        }
        else if (isGrounded)
        {
            // 【重要】rb.velocity.x ではなく moveInput で判定することで、壁に当たっても走りアニメを継続
            state = (Mathf.Abs(moveInput) > 0.1f) ? MovementState.Running : MovementState.Idle;
        }
        else
        {
            state = (rb.velocity.y > 0.1f) ? MovementState.Jumping : MovementState.Falling;
        }

        if (anim != null)
        {
            //anim.SetInteger("state", (int)state);
            if(state == MovementState.Idle)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isJumping", false);
            }else if(state == MovementState.Running || state == MovementState.Dashing)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isJumping", false);
            }else if(state == MovementState.Jumping || state == MovementState.Falling)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isJumping", true);
            }

            Debug.Log($"currentAbility:{ability.currentAbility}");

            if(ability.currentAbility == PlayerAbility_fai.AbilityType.Red)
            {
                anim.SetBool("isBlue", true);
            }
            else
            {
                anim.SetBool("isBlue", false);
            }
        }
    }

    private void ApplyCustomGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnCollisionStay2D(Collision2D collision) //!ベルトコンベア接触検知
    //タグをつけたオブジェクトに触れると作動します。
    //"leftbelt"は左向きのベルトコンベア
    //"rightbelt"は右向きのベルトコンベアにつけてください。
    // 箱オブジェクトのコードにもコピペしておきます。箱もベルトコンベアに乗ると動くようにします。
    {
        if (collision.gameObject.CompareTag("leftbelt"))
        {
            Debug.Log("leftbelt");
            beltspeed=4.2f;
        }
        
        if(collision.gameObject.CompareTag("rightbelt"))
        {
            Debug.Log("rightbelt");
            beltspeed=-4.2f;
        }
        
    }
    private void OnCollisionExit2D (Collision2D collision)
    {
        beltspeed=0.0f;
    }
} // クラスの終わり