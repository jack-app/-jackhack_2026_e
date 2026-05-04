using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_off_block : MonoBehaviour
{
    public Collider2D col ;
    SpriteRenderer sprite;
    Color color;


    // Start is called before the first frame update
    void Awake()
    {
        col = transform.GetComponent<Collider2D>(); 
        sprite = gameObject.GetComponent<SpriteRenderer>();
        color = sprite.color;
        OnOffBlock(0);  
    }

    public int block_color = 0;
    public void OnOffBlock(int player_state)
    {
        if(block_color != player_state)
        {
            col.enabled = false;
            color.a = 0.3f;
        }
        else
        {
            col.enabled = true;
            color.a = 1.0f;
        }
        sprite.color = color;
    }
}
