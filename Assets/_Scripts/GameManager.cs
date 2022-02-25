using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

   public enum GameState { MENU, GAME, PAUSE, ENDGAME };

   public GameState gameState { get; private set; }
   // last gamestate
   public GameState lastState { get; private set; }
   public int vidas;
   public int pontos;

   public delegate void ChangeStateDelegate();
   public static ChangeStateDelegate changeStateDelegate;

   private static GameManager _instance;
   
   private GameManager()
   {
       vidas = 3;
       pontos = 0;
       gameState = GameState.MENU;
       // last gamestate
       lastState = GameState.MENU;
   }

   public static GameManager GetInstance()
   {
       if(_instance == null)
       {
           _instance = new GameManager();
       }

       return _instance;
   }

   public void ChangeState(GameState nextState)
   {
    if (gameState == GameState.ENDGAME && nextState == GameState.GAME) Reset();
    else if (gameState == GameState.MENU && nextState == GameState.GAME) Reset();
    //else if (gameState == GameState.PAUSE && nextState == GameState.GAME) return;
    lastState = gameState; // last gamestate
    gameState = nextState;
    changeStateDelegate();
   }

   private void Reset()
   {
      vidas = 3;
      pontos = 0;
   }

}
