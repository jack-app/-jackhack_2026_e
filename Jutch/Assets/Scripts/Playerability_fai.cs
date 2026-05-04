using System.Collections;
using UnityEngine;

public class PlayerAbility_fai : MonoBehaviour
{
    public enum AbilityType {Blue, Red }
    
    [Header("能力設定")]
    public AbilityType currentAbility = AbilityType.Red;

    [Header("赤能力：ダッシュ設定")]
    public float dashPower = 24f;
    public float dashTime = 0.2f;
    public bool isDashing { get; private set; }

    // 【追加】空中ダッシュを1回に制限するためのフラグ
    private bool canDash = true;

    [Header("青能力：ハイジャンプ")]
    public float highJumpForce = 18f;

    private Rigidbody2D rb;
    private PlayerMovement_fai movement;
    private SpriteRenderer sr; 

    private Coroutine dashCoroutine;
    private float originalGravity = 1.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement_fai>();
        sr = GetComponent<SpriteRenderer>();
        UpdateAbilityVisuals();
    }

    void Update()
    {
        Debug.Log($"abinab:{currentAbility}");
        // 【修正点1】地面に着地している間、ダッシュ権限をリセット
        if (movement.isGrounded && !isDashing)
        {
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchAbility();
        }

        // 【修正点2】ダッシュ発動条件に canDash == true を追加
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentAbility == AbilityType.Red && !isDashing && canDash)
        {
            canDash = false; // ダッシュ開始時にフラグをオフにし、再着地まで使用不可にする
            dashCoroutine = StartCoroutine(PerformDash());
        }
    }

    private void SwitchAbility()
    {
        if (currentAbility == AbilityType.Red) currentAbility = AbilityType.Blue;
        else if (currentAbility == AbilityType.Blue) currentAbility = AbilityType.Red;

        UpdateAbilityVisuals();
    }

    private void UpdateAbilityVisuals()
    {
        /*switch (currentAbility)
        {
            case AbilityType.None: sr.color = Color.white; break;
            case AbilityType.Blue: sr.color = new Color(0.5f, 0.5f, 1f); break;
            case AbilityType.Red: sr.color = new Color(1f, 0.5f, 0.5f); break;
        }*/
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        rb.gravityScale = 0f;

        float direction = movement.isFacingRight ? -1f : 1f;
        rb.velocity = new Vector2(direction * dashPower, 0f);

        yield return new WaitForSeconds(dashTime);

        StopDash();
    }

    public void StopDash()
    {
        if (!isDashing) return;

        isDashing = false;
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero; // 残留速度を吸収（慣性によるガタつき防止）

        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
            dashCoroutine = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 壁に衝突した場合、即座にダッシュを停止して重力と速度をリセット
                if (Mathf.Abs(contact.normal.x) > 0.5f) 
                {
                    StopDash();
                    break;
                }
            }
        }
    }
}