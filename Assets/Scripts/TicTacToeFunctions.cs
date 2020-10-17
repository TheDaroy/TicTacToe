using UnityEngine;


public enum TicTacState { None,Tie,Lose,Win}

static public class TicTacToeFunctions 
{
    static public TicTacState VictoryCheck(TicTacToeTile[,] tiles)
    {
        if (CheckHorizontal(tiles) || CheckVertical(tiles) || CheckDiagonal(tiles))
        {
            Debug.Log("Actual Win");
            return TicTacState.Win;
        }
        else if (TieCheck(tiles))
        {
            return TicTacState.Tie;
        }
        return TicTacState.None;
    }
    static public TicTacState VictoryCheck(int[,] tiles,int depth,bool printS,int playerNr)
    {

        TicTacState HState = CheckHorizontal(tiles, playerNr);
        if (HState != TicTacState.None)
        {
            
            return HState;
        }
        TicTacState VState = CheckVertical(tiles, playerNr);
        if (VState != TicTacState.None)
        {
           
            return VState;
        }
        TicTacState DState = CheckDiagonal(tiles, playerNr);
        if (DState != TicTacState.None)
        {
            
            return DState;
        }

        if (TieCheck(tiles))
        {
            
            return TicTacState.Tie;
        }
        return TicTacState.None;


    }

    static public TicTacState VictoryCheck(int[,] tiles,  int playerNr)
    {

        TicTacState HState = CheckHorizontal(tiles, playerNr);
        if (HState != TicTacState.None)
        {
            Debug.Log(HState.ToString() + " Player: " + playerNr);
            //PrintBoard(tiles);
            return HState;
        }
        TicTacState VState = CheckVertical(tiles, playerNr);
        if (VState != TicTacState.None)
        {
            Debug.Log(VState.ToString() + " Player: " + playerNr);
            // PrintBoard(tiles);
            return VState;
        }
        TicTacState DState = CheckDiagonal(tiles, playerNr);
        if (DState != TicTacState.None)
        {
            Debug.Log(DState.ToString() + " Player: " + playerNr);
            //PrintBoard(tiles);
            return DState;
        }

        if (TieCheck(tiles))
        {
            Debug.Log(TicTacState.Tie.ToString() + " Player: " + playerNr);
            return TicTacState.Tie;
        }
        return TicTacState.None;




    }


    static bool CheckVertical(TicTacToeTile[,] tiles)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1) - 1; y++)
            {
                if (tiles[x, y].currentState == 0 || tiles[x, y].currentState != tiles[x, y + 1].currentState)
                {
                    break;
                }
                else if (y == tiles.GetLength(1) - 2)
                {
                    
                    return true; // win      return true; // win
                }
            }
        }
        return false;
    }
    static TicTacState CheckVertical(int[,] tiles,int playerNR)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1) - 1; y++)
            {
                if (tiles[x, y] == 0 || tiles[x, y] != tiles[x, y + 1])
                {
                    break;
                }
                else if (y == tiles.GetLength(1) - 2)
                {
                    
                    if (tiles[x, y] == playerNR)
                    {
                        return TicTacState.Win;
                    }
                    else
                    {
                        return TicTacState.Lose;
                    }
                }
            }
        }
        return TicTacState.None;
    }
    

    static bool CheckHorizontal(TicTacToeTile[,] tiles)
    {
        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0) - 1; x++)
            {

                if (tiles[x, y].currentState == 0 || tiles[x, y].currentState != tiles[x + 1, y].currentState)
                {

                    break;
                }
                else if (x == tiles.GetLength(0) - 2)
                {

                   
                    return true; // win      return true; // win

                }

            }
        }
        return false;
    }
    static TicTacState CheckHorizontal(int[,] tiles,int playerNR)
    {
        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0) - 1; x++)
            {

                if (tiles[x, y] == 0 || tiles[x, y] != tiles[x + 1, y])
                {
                    break;
                }
                else if (x == tiles.GetLength(0) - 2)
                {
                   
                    

                    if (tiles[x, y] == playerNR)
                    {
                        return TicTacState.Win;
                    }
                    else
                    {
                        return TicTacState.Lose;
                    }

                }

            }
        }
        return TicTacState.None;
    }
    static bool CheckDiagonal(TicTacToeTile[,] tiles)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            if (tiles[x, x].currentState == 0 || tiles[x, x].currentState != tiles[x + 1, x + 1].currentState)
            {
                break;
            }
            else if (x == tiles.GetLength(0) - 2)
            {
                
                return true; // win

            }
        }
        for (int x = 0; x < tiles.GetLength(0) - 1; x++)
        {
            int y = tiles.GetLength(1) - 1 - x;

            if (tiles[x, y].currentState == 0 || tiles[x, y].currentState != tiles[x + 1, y - 1].currentState)
            {
                break;
            }
            else if (y == 1)
            {
                
                return true; // win
            }

        }
        return false;
    }
    static TicTacState CheckDiagonal(int[,] tiles,int playerNR)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            if (tiles[x, x] == 0 || tiles[x, x] != tiles[x + 1, x + 1])
            {
                break;
            }
            else if (x == tiles.GetLength(0) - 2)
            {
                
                if (tiles[x, x] == playerNR)
                {
                    return TicTacState.Win;
                }
                else
                {
                    return TicTacState.Lose;
                }

            }
        }
        for (int x = 0; x < tiles.GetLength(0) - 1; x++)
        {
            int y = tiles.GetLength(1) - 1 - x;

            if (tiles[x, y] == 0 || tiles[x, y] != tiles[x + 1, y - 1])
            {
                break;
            }
            else if (y == 1)
            {
               
                if (tiles[x, y] == playerNR)
                {
                    return TicTacState.Win;
                }
                else
                {
                    return TicTacState.Lose;
                }
            }

        }
        return TicTacState.None; // false
    }

    static bool TieCheck(TicTacToeTile[,] tiles)
    {
        foreach(TicTacToeTile tile in tiles)
        {
            if (tile.currentState == 0)
            {
                return false;
            }
        }
        return true;
    }
    static bool TieCheck(int[,] tiles)
    {
        foreach (int tile in tiles)
        {
            if (tile == 0)
            {
                return false;
            }
        }
        return true;
    }

    static void PrintBoard(int[,] tiles)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                Debug.Log("Tile: " + x + ": " + y + " Value:" + tiles[x, y]);
            }
        }
    }
    public static int[,] GetDummyBoard(TicTacToeTile[,] tiles)
    {
        int[,] dummyArray = new int[tiles.GetLength(0), tiles.GetLength(1)];
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                dummyArray[x, y] = tiles[x,y].currentState;
            }
        }
        return dummyArray;
    }




}
