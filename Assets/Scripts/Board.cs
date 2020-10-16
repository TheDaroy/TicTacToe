using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Board : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] public float tileSize = 1;
    [SerializeField] public int Rows = 3;
    [SerializeField] public int Columns = 3;
    [SerializeField] public GameObject[,] TileMap;
    [SerializeField] public GameObject TileType;
    [SerializeField] public TileData tileData;

    
  
     public void Create2DBoard()
    {
        TileMap = new GameObject[Rows, Columns];
       
        for (int x = 0; x < Rows; x++)
        {
            for (int y = 0; y < Columns; y++)
            {
                TileSetup(x,y);
            }
        }
    }

    void TileSetup(int x, int y)
    {

       GameObject temp = Instantiate(TileType, new Vector3(x + tileSize, y + tileSize, 0f), Quaternion.identity);
       temp.name = (x.ToString() + " : " + y.ToString());
       temp.transform.parent = transform;
       TileMap[x, y] = temp;
    }
    


    public GameObject[,] GetBoardData()
    {
        return TileMap;
    }

    public T[,] GetArrayOfTileComponents<T>() where T : Component 
    {
        
        T[,] convertedArray = new T[TileMap.GetLength(0), TileMap.GetLength(1)];
        for (int x = 0; x < TileMap.GetLength(0); x++)
        {
            for (int y = 0; y < TileMap.GetLength(1); y++)
            {
                convertedArray[x, y] = TileMap[x, y].GetComponent<T>();
            }
        }
        return convertedArray;
    }
}


