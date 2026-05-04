using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ryyuzaki_test : MonoBehaviour
{
    // 0が青、１が緑
    public int player_state = 0;
    on_off_block[] blocks;

    // Start is called before the first frame update
    void Start()
    {
        blocks = GameObject.FindGameObjectsWithTag("Finish").GetComponent<on_off_block>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("E")) {
            if(player_state == 0)
            {
                player_state = 1;
            }
            
            else {
                player_state = 0;
            }

            foreach(on_off_block block in blocks)
            {
                block.OnOffBlock(player_state);
            }
        }      
     
        
            
    }
        
}
