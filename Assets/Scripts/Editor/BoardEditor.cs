using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{
    Board myboard;
    int _rows;
    int _columns;
    private void OnEnable()
    {
        Setup();
    }

    void Setup()
    {
        myboard = (Board)target;

    }

    
    public override void OnInspectorGUI()
    {


        myboard.Rows = EditorGUILayout.IntField("Hello", myboard.Rows);
        myboard.Columns = EditorGUILayout.IntField("Hello", myboard.Columns);
        myboard.TileType = (GameObject)EditorGUILayout.ObjectField(myboard.TileType, typeof(GameObject), true);

        if (GUILayout.Button("Build Board"))
        {
            myboard.Create2DBoard();
        }
    }
    private void OnSceneGUI()
    {
        DrawThingy();
    }

    void DrawThingy()
    {
        for (int x = 0; x < myboard.Rows; x++)
        {
            for (int y = 0; y < myboard.Columns; y++)
            {
                Handles.DrawWireCube(new Vector3(x + myboard.tileSize, y + myboard.tileSize, 0f), new Vector3(myboard.tileSize, myboard.tileSize, 1f));

            }
        }
    }

}
