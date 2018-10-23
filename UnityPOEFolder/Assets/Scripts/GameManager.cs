using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    const float PADDING = 5.12f;
    private const int REFRESH_RATE = 60;
    float X_OFF, Y_OFF;

    GameEngine ge = new GameEngine();

    // Use this for initialization
    void Start ()
    {
        FillGrass();
        Debug.Log("Hello");
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void FillGrass()
    {
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                Instantiate(Resources.Load("Grass"), new Vector3(X_OFF + (x * PADDING), Y_OFF + (-y * PADDING)), Quaternion.identity);
            }
        }
    }
}
