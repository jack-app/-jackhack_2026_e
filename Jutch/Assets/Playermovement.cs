using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移動パラメータ")]
    public float moveSpeed = 8f;     // 移動速度
    public float jumpForce = 12f;    // ジャンプ力

    [Header("地面検知")]
    public Transform groundCheck;    // 地面チェック用のオブジェクト
    public float groundCheckRadius = 0.2f; // チェック範囲の半径
    public LayerMask groundLayer;    // 地面として認識するレイヤー

    private Rigidbody2D rb;
    private Animator anim;           // アニメーターコンポーネント
    private float moveInput;
    private bool isGrounded;
    private bool isFacingRight = true; // キャラクターが右を向いているか

    void Start()
    {
        // コンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. 移動入力の取得 (Aキーで左、Dキーで右)
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }

        // 2. ジャンプ入力の取得 (Wキーまたはスペースキー)
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            Jump();
        }

        // 3. アニメーションとキャラクターの向きを更新
        UpdateAnimationAndFacing();
    }

    void FixedUpdate()
    {
        // 4. 地面検知
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // 5. 水平移動の実行
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // アニメーションと向きを制御するメソッド
    private void UpdateAnimationAndFacing()
    {
        // 入力がある場合は「PlayerRun」、ない場合は「PlayerIdle」へ
        // Animator側のパラメーター "isRunning" を更新
        bool isRunning = Mathf.Abs(moveInput) > 0.1f;
        anim.SetBool("isRunning", isRunning);

        // 向きの反転処理
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}