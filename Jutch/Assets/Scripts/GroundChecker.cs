using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded = true;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("温トリガーエンター");
        if(collision.transform.CompareTag("Ground"))
            IsGrounded = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("温トリガーエンター");
        if(collision.transform.CompareTag("Ground"))
            IsGrounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("温トリガーイグジット");
        if(collision.transform.CompareTag("Ground"))
            IsGrounded = false;
    }
}
