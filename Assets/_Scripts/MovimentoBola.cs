using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour
{
    [Range(1, 15)]
    public float velocidade = 5.0f;

    private Vector3 direcao;
    Vector3 startBolaPos = new Vector3( 0, -3.6f, 0);
    public bool emJogo;
    public Transform explosion;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = startBolaPos;

        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);

        direcao = new Vector3(dirX, dirY).normalized;

        emJogo = false;

        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState == GameManager.GameState.MENU || gm.gameState == GameManager.GameState.ENDGAME ) {
            transform.position = startBolaPos;
            emJogo = false;
            return;
        }
        if (gm.gameState == GameManager.GameState.PAUSE) return;

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (!emJogo) transform.position = playerPosition + new Vector3(0, 0.5f, 0);; 

        if (Input.GetButtonDown ("Jump") && !emJogo) emJogo = true;

        if (emJogo) {
            transform.position += direcao * Time.deltaTime * velocidade;
        }
        Vector2 posicaoViewport = Camera.main.WorldToViewportPoint(transform.position);

        if( posicaoViewport.x < 0 || posicaoViewport.x > 1 )
        {
            direcao = new Vector3(-direcao.x, direcao.y);
        }
        if( posicaoViewport.y < 0 || posicaoViewport.y > 1 )
        {
            direcao = new Vector3(direcao.x, -direcao.y);
        }
        if(posicaoViewport.y < 0)
        {
           Reset();
        }
        
    }

    private void Reset()
    {
       Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
       transform.position = playerPosition + new Vector3(0, 0.5f, 0);

       float dirX = Random.Range(-5.0f, 5.0f);
       float dirY = Random.Range(2.0f, 5.0f);

       direcao = new Vector3(dirX, dirY).normalized;
       gm.vidas--;
       emJogo = false;

       if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
       {
        gm.ChangeState(GameManager.GameState.ENDGAME);
       }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            
            float dirX = 0.0f; //= Random.Range(-5.0f, 5.0f);
            float dirY = Random.Range(1.0f, 5.0f);
            if(playerPosition[0] < transform.position.x) dirX = Random.Range(0.0f, 5.0f);
            else if(playerPosition[0] > transform.position.x) dirX = Random.Range(-5.0f, 0.0f);

            direcao = new Vector3(dirX, dirY).normalized;
        }
        else if(col.gameObject.CompareTag("Tijolo"))
        {
            direcao = new Vector3(direcao.x, -direcao.y);
            gm.pontos++;
            //Vector3 brickPosition = GameObject.FindGameObjectWithTag("Tijolo").transform.position;
            Transform newExplosion = Instantiate(explosion,col.transform.position, col.transform.rotation);
            Destroy(newExplosion.gameObject,2.5f);
        }
    }
}
