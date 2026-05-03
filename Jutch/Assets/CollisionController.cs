using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("OOB"))
        {
            Debug.Log("stay");

            //transformを取得
        Transform myTransform = this.transform;

        //座標の取得
        Vector2 pos = myTransform.position;
        pos.x += 0.01f;  //x座標へ0.01加算
        pos.y += 0.01f;  //y座標へ0.01加算

        myTransform.position = pos; //座標を設定

        }
    }
    
}