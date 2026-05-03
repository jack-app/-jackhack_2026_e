using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ryyuzaki_test : MonoBehaviour
{
    // 0が青、１が緑
    public int player_state;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }      
     
        
            
    }
        
}
