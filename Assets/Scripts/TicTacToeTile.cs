using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TicTacToeTile : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    public event Action<TicTacToeTile> OnSelected;
    public int currentState = 0;
    
    
    void Turn()
    {

    }

    
    void EndTurn()
    {

    }
    void Select()
    {
        OnSelected?.Invoke(this);
    }
    public void ChangeState(int state, Color newColor)
    {
        currentState = state;
        renderer.color = newColor;
    }

}
