using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private int switchNum; //スイッチの番号
    //[SerializeField] private DoorManager doorManager; //ドアマネージャー
    private DoorManager[] doorManagers; //ドアマネージャーの配列
    // Start is called before the first frame update
    void Start()
    {
        doorManagers = Object.FindObjectsOfType<DoorManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(var target in doorManagers)
            {
                target.DoorOpen(switchNum);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(var target in doorManagers)
            {
                target.DoorClose(switchNum);
            }
        }
    }
}
