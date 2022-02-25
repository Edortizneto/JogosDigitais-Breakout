using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoSpawner : MonoBehaviour
{
    public GameObject Bloco;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += Construir;
        Construir();

    }
    void Construir()
    {
     
       //Debug.Log($"lastState: {gm.lastState}");
       if (gm.gameState == GameManager.GameState.GAME && gm.lastState != GameManager.GameState.PAUSE )
       {
          foreach (Transform child in transform) {
              GameObject.Destroy(child.gameObject);
          }
          for(int i = 0; i < 12; i++)
          {
              for(int j = 0; j < 4; j++){
                  Vector3 posicao = new Vector3(-8.6f + 1.55f * i, 4 - 0.55f * j);

                  Instantiate(Bloco, posicao, Quaternion.identity, transform);
              }
          }
       }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0 && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
    }
}
