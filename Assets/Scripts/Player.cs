using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Color playerColor;
    public int playerNummber;
    public GameManager manager;
    public bool currentTurn = false;
    
   
    public void Setup(Color newColor, int newPlayerNr, GameManager newManager)
    {
        playerColor = newColor;
        playerNummber = newPlayerNr;
        manager = newManager;
        currentTurn = false;
        
    }
    public virtual void ActivateTurn()
    {
        currentTurn = true;
        Debug.Log(playerNummber+ " Turn");
    }
    protected virtual void TurnComplete(TicTacToeTile tile)
    {
        tile.ChangeState(playerNummber, playerColor);
        currentTurn = false;
        Debug.Log(playerNummber + " Complete :"  );
        manager.NextTurn();
    }

}
public class Human : Player
{
    
    private void Update()
    {
        if (currentTurn)
        {
            Thingy();
        }
        
    }
    void Thingy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.transform.gameObject.layer == 8)
            {
                TicTacToeTile temp = hit.transform.gameObject.GetComponent<TicTacToeTile>();
                if (temp.currentState == 0)
                {
                    TurnComplete(temp);
                }
                
               
            }
        }
    }
}
public class AI : Player
{
    int NodesTaken = 0;
    public override void ActivateTurn()
    {
        base.ActivateTurn();
        int[,] dummyArray = TicTacToeFunctions.GetDummyBoard(manager.tiles);

        NodesTaken = 0;
        BestMove(dummyArray);
        Debug.Log("Nodes Taken: " + NodesTaken);
    }

    void BestMove(int[,] dummyArray)
    {

        int bestScore;
        int bestX = 0;
        int bestY = 0;
        int alpha = int.MinValue;
        int beta = int.MaxValue;
       
        for (int x = 0; x < dummyArray.GetLength(0); x++)
        {
            for (int y = 0; y < dummyArray.GetLength(1); y++)
            {
                if (dummyArray[x, y] == 0)
                {
                    dummyArray[x, y] = playerNummber;
                    bestScore = MiniMax(dummyArray, 0, false, alpha, beta);
                    dummyArray[x, y] = 0;
                    if (bestScore > alpha)
                    {
                        alpha = bestScore;
                        bestX = x;
                        bestY = y;
                        
                    }
                    //alpha = Math.Max(alpha, bestScore);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                
            }
        }
       // Debug.Log("Base Turns: " + i);
       // Debug.Log(playerNummber + " Selected :" + bestX + ":" + bestY);
        TurnComplete(manager.tiles[bestX, bestY]);      
    }

    int MiniMax(int[,] dummyArray, int depth, bool maximizing, int alpha, int beta)
    {
        NodesTaken++;
        TicTacState currentState = TicTacToeFunctions.VictoryCheck(dummyArray, depth, false, playerNummber);   
        if ( currentState != TicTacState.None )
        {
            return ScoreCheck(currentState, depth);
        }

        int score;     
        if (maximizing)
        {
            //score = int.MinValue;
            for (int x = 0; x < dummyArray.GetLength(0); x++)
            {
                for (int y = 0; y < dummyArray.GetLength(1); y++)
                {
                    if (dummyArray[x, y] == 0)
                    {
                        dummyArray[x, y] = playerNummber;
                        score = MiniMax(dummyArray, depth++, false, alpha, beta);
                        dummyArray[x, y] = 0;
                       // score = Math.Max(newScore, score);
                        alpha = Math.Max(alpha, score);
                        if (beta <= alpha) {break;}                      
                    }
                    if (beta <= alpha) { break; }
                }
            }
            return alpha;
        }
        else
        {
           // score = int.MaxValue;
            for (int x = 0; x < dummyArray.GetLength(0); x++)
            {
                for (int y = 0; y < dummyArray.GetLength(1); y++)
                {
                    if (dummyArray[x, y] == 0)
                    {
                        dummyArray[x, y] = 1;
                        score = MiniMax(dummyArray, depth++, true,alpha,beta);
                        dummyArray[x, y] = 0;
                        //score = Math.Min(score, newScore);
                        beta = Math.Min(beta, score);
                        if (beta <= alpha){break;}
                    }
                    if (beta <= alpha) { break; }
                }
            }
            return beta;
        }
        
    }

    int ScoreCheck(TicTacState state, int depth)
    {
        switch (state)
        {
            case TicTacState.Tie:
                return 0;

            case TicTacState.Lose:
                return -1 ;

            case TicTacState.Win:
                return 1;

            default:
                return 0;
        }
    }


    

    int ResultScore(TicTacState state)
    {
        switch (state)
        {
            case TicTacState.Win:
                return 1;
             
            case TicTacState.Tie:
                return 0;
                
            case TicTacState.None:
                return 0;
               
            default:
                Debug.Log("Bad stuff happened");
                return 0;
               
        }

    }
   
    int GetNextPlayer(int currentPlayer)
    {
        if (currentPlayer < manager.players.Length)
        {
            return currentPlayer++;
        }
        else
        {

            return  1;
        }
    }
   
    
        
}
