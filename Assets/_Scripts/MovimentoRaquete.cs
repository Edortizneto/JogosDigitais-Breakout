using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoRaquete : MonoBehaviour
{
    [Range(1, 15)]
    public float velocidade = 5.0f;
    GameManager gm;
    Vector3 startPos = new Vector3( 0, -4, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
            if (gm.gameState == GameManager.GameState.MENU || gm.gameState == GameManager.GameState.ENDGAME ) {
                transform.position = startPos;
                return;
            }
            if (gm.gameState == GameManager.GameState.PAUSE) return;

            float inputX = Input.GetAxis("Horizontal");

            transform.position += new Vector3(inputX, 0, 0) * Time.deltaTime * velocidade;

            if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) 
            {
                gm.ChangeState(GameManager.GameState.PAUSE);
            }
    }
}
