using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameEngine ge;
    private const int REFRESH_RATE = 60;
    private int count = 0;

    System.Random rand = new System.Random();

    //float X_OFF, Y_OFF;

    // Use this for initialization
    void Start()
    {
        ge = new GameEngine();

        ge.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (count % REFRESH_RATE == 0)
        {
            ge.playGame();
            //ge.PlaceNewUnit();
        }


        count++;
    }
}
