using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Board board;
     public TicTacToeTile[,] tiles;

    public Player[] players;
    int currentPlayer;

    private void Awake()
    {
        players = new Player[2];
        AddPlayer<AI>(0);
        AddPlayer<Human>(1);
    }
    void AddPlayer<T> (int i) where T : Player 
    {
        GameObject temp = new GameObject("Player: " + (i + 1).ToString());
        temp.transform.parent = transform;
        players[i] = temp.AddComponent<T>();
        players[i].Setup(GetColor(i + 1), i + 1, this);
    }
    
    void Start()
    {
        board.Create2DBoard();
        tiles = board.GetArrayOfTileComponents<TicTacToeTile>();
        foreach (TicTacToeTile item in tiles)
        {
            Debug.Log(item.currentState);
        }
        currentPlayer = Random.Range(1,players.Length);
        NextPlayer(currentPlayer);
    }

    public void NextTurn()
    {
        if (VictoryCheck() == TicTacState.None)
        {
           
            if (currentPlayer == 1)
            {
                currentPlayer = 2;
            }
            else
            {

                currentPlayer = 1;
            }
            Debug.Log("CurrentPlayer: "+ currentPlayer);
            NextPlayer(currentPlayer);
        }
    }
    void NextPlayer(int i)
    {
        
        players[i-1].ActivateTurn();
    }

    TicTacState VictoryCheck()
    {
        switch (TicTacToeFunctions.VictoryCheck(TicTacToeFunctions.GetDummyBoard(tiles),currentPlayer))
        {
            case TicTacState.Win:
                Victory();
                return TicTacState.Win;
            case TicTacState.Lose:
                Victory();
                return TicTacState.Win;
            case TicTacState.Tie:
                Tie();
                return TicTacState.Tie;
            default:
                return TicTacState.None;
        }
    }
   
    void Victory()
    {
        Debug.Log("Player: " + currentPlayer.ToString() + " Win");
    }
    void Tie()
    {
        Debug.Log("Its a Tie");
    }

    Color GetColor(int i)
    {
        switch (i)
        {
            case 0:
                
                return Color.green;
            case 1:
                return Color.red;
                
            case 2:
                return Color.blue;
                
            case 3:
                return Color.yellow;
               
            case 4:
                return Color.magenta;
               
            
                
            default:
                return Color.black;
               
        }
    }
}
