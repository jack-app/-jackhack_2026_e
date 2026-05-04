using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ryyuzaki_test : MonoBehaviour
{
    // 0が青、１が緑
    public int player_state = 0;
    on_off_block[] oobs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("OOB");
        Debug.Log($"blockCount:{blocks.Length}");
        oobs = new on_off_block[blocks.Length];
        for(int i = 0; i < blocks.Length; i++)
        {
            oobs[i] = blocks[i].GetComponent<on_off_block>();
            oobs[i].OnOffBlock(player_state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("key:e");
            if(player_state == 0)
            {
                player_state = 1;
            }
            
            else {
                player_state = 0;
            }

            foreach(on_off_block oob in oobs)
            {
                oob.OnOffBlock(player_state);
            }
        }      
     
        
            
    }
        
}
