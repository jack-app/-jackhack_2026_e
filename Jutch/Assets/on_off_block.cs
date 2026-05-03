using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_off_block : MonoBehaviour
{
    public Collider2D col ;


    // Start is called before the first frame update
    void Start()
    {
        col = transform.GetComponent<Collider2D>(); 
        Color color = gameObject.GetComponent<Image>().color;
        OnOffBlock(0);  
    }

       public int block_color = 0;
    public void OnOffBlock(int player_state)
    {
        if(block_color == player_state)
        {
            col.enabled = true;
            color.a = 0.3f;
        }
        else
        {
            col.enabled = false;
            color.a = 1.0f;
        }
        gameObject.GetComponent<Image>().color = block_color;
    }
}
