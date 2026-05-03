using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] public int doorNum; //ドアの番号
    float scaleY; //ドアのスケールX
    float posX; //ドアの位置X
    float posY; //ドアの位置Y
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        scaleY = transform.localScale.y;
        posX = transform.position.x;
        posY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void DoorOpen(int num)
    {
        //ドアを開く処理
        if (num == doorNum)
        {
            StartCoroutine(DoorOpenCoroutine());
        }
    }

    public void DoorClose(int num)
    {
        //ドアを閉じる処理
        if (num == doorNum)
        {
            StartCoroutine(DoorCloseCoroutine());
        }
    }

    IEnumerator DoorOpenCoroutine()
    {
        for (int i = 1; i < 11; i++)
        {
            transform.localScale = new Vector3(1, scaleY - 0.1f * i*scaleY, 1);
            transform.position = new Vector3(posX, posY + 0.05f * i*scaleY, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator DoorCloseCoroutine()
    {
        for (int i = 1; i < 11; i++)
        {
            transform.localScale = new Vector3(1, 0.1f * i*scaleY, 1);
            transform.position = new Vector3(posX, posY + 0.05f * (10 - i)*scaleY, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
