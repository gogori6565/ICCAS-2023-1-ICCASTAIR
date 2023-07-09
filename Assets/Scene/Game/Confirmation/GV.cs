using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GV : MonoBehaviour
{
    //on&off objects - value : 0(unclicked) or 1(clicked)
    public static int Light_LivingRoom, tv, PowerStrip_LivingRoom,
                        Light_Kitchen, GasRange, GasValve, faucet,
                        Light_Room, computer, PowerStrip_Room;

    // Start is called before the first frame update
    void Start()
    {
        //Variable reset
        Light_LivingRoom = 0; tv = 0; PowerStrip_LivingRoom = 0;
        Light_Kitchen = 0; GasRange = 0; GasValve=0; faucet = 0;
        Light_Room = 0; computer = 0; PowerStrip_Room = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
