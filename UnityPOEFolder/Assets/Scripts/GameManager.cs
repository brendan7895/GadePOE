using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameEngine ge;
    const float PADDING = 5.12f;

    private const int REFRESH_RATE = 60;
    private int count = 0;


    System.Random rand = new System.Random();

    float X_OFF, Y_OFF;

    // Use this for initialization
    void Start()
    {
        ge = new GameEngine();
        X_OFF = -Camera.main.orthographicSize;
        Y_OFF = Camera.main.orthographicSize;
        FillGrass();
        //FillUnits(ge.Units);

    }

    // Update is called once per frame
    void Update()
    {
        //if (count % REFRESH_RATE == 0)
        //{
        //    //x = ge.Units[0].X +=1;
        //    move();
        //    Redraw();


        //}
        //count++;

    }

    void FillGrass()
    {
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                Debug.Log("Cehck");
                Instantiate(Resources.Load("Grass"), new Vector3(X_OFF + (x * PADDING), Y_OFF + (-y * PADDING)), Quaternion.identity);
            }
        }
    }
}
