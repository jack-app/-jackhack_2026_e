using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class box : MonoBehaviour
{
        public float beltspeed;
        public float beltspeedoption;
        private Rigidbody2D rb;
        void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision) //!ベルトコンベア接触検知
    //タグをつけたオブジェクトに触れると作動します。
    //"leftbelt"は左向きのベルトコンベア
    //"rightbelt"は右向きのベルトコンベアにつけてください。
    // 箱オブジェクトのコードにもコピペしておきます。箱もベルトコンベアに乗ると動くようにします。
    {
        if(collision.gameObject.CompareTag("leftbelt"))
        beltspeed=beltspeedoption;
        if(collision.gameObject.CompareTag("rightbelt"))
        beltspeed=-beltspeedoption;
    }
    void FixedUpdate()
    {
        // 5. 水平移動の実行
        rb.velocity = new Vector2(rb.velocity.x-beltspeed, rb.velocity.y);　//ベルトコンベアの処理を追加しました。
    }
    private void OnCollisionExit2D (Collision2D collision)
    {
        beltspeed=0.0f;
    }
}