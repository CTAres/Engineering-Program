using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Mono.Cecil.Cil;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
public class Board : MonoBehaviour
{
    [Header("Input Settings: ")]
    [SerializeField] private LayerMask boxesLayerMask;
    [SerializeField] private float touchRadius;

    [Header("Mark Sprites: ")]
    [SerializeField] private Sprite spriteX;
    [SerializeField] private Sprite spriteO;
    [SerializeField] private Sprite spriteEmpty;

    [Header("Box Settings: ")]
    public Box[] allBoxes = new Box[9];

    public Mark[] marks;

    private Camera cam;
    public Mark currentMark;

    public int xWins;
    private int oWins;

    public int test = 0;

    public void Start()
    {
        cam = Camera.main;

        currentMark = Mark.X;

        marks = new Mark[9];
        
        GameObject board = GameObject.Find("Board");
        //this (line 44) is the line that fucks up the Unit Tests. I always get "NullReferenceException: Object reference not set to an instance of an object" as error.
        //Was not able to fix that in any way. Unit Tests still written out to show how they are supposed to work.
        Transform[] ts = board.GetComponentsInChildren<Transform>();
        
        for (int i = 1; i < ts.Length; i++)
        {
            allBoxes[i-1] = ts[i].GetComponent<Box>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) //Checks if a box is clicked
        {
            Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapCircle(touchPosition, touchRadius, boxesLayerMask);

            if(hit)
            {
                HitBox(hit.GetComponent <Box> ());
            }
        }
    }

    public void HitBox(Box box) //if the box isnt marked, it gets claimed for the current player. After that we check win conditions
    {
        if(!box.isMarked) 
        {
            marks[box.index] = currentMark;

            box.SetAsMarked(GetSprite(), currentMark);

            if(CheckIfWin())
            {
                EndRound(currentMark);
                //no return here even if win, so that after a win the other player starts
            }
            if(CheckIfDraw())
            {
                EndRound(Mark.None);
            }

            SwitchPlayer();
        }
    }

    public void EndRound(Mark winner) //increses win counter and prepares next game
    {
        if(winner != Mark.None)
        {
            Debug.Log(winner.ToString() + " Wins!");
            if(currentMark == Mark.X)
            {
                xWins = xWins+1;
            }
            else
            {
                oWins = oWins+1;
            }
            Debug.Log("Total wins: X = " + xWins + " | O = " + oWins);
            if(xWins >= 3)
            {
                //this test value is for the unit test "WinTest" to get some kind of feedback if X wins.
                test = 1;
                Debug.Log("X won!");
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else if(oWins >= 3)
            {
                Debug.Log("O won!");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        else
        {
            Debug.Log ("Draw!");
            Debug.Log("Total wins: X = " + xWins + " | O = " + oWins);
        }
        for(int i = 0; i < allBoxes.Length; i++) //reset all boxes in the box script and the marks array
        {
           allBoxes[i].ResetBox(spriteEmpty);
           marks[i] = Mark.None;
        }
    }

    private bool CheckIfWin() //checks all possible win combinations
    {
        return
        AreBoxesMatched(0,1,2) || AreBoxesMatched(3,4,5) || AreBoxesMatched(6,7,8) ||
        AreBoxesMatched(0,3,6) || AreBoxesMatched(1,4,7) || AreBoxesMatched(2,5,8) ||
        AreBoxesMatched(0,4,8) || AreBoxesMatched(2,4,6);
    }

    public bool CheckIfDraw()
    {
        for(int i = 0; i < allBoxes.Length; i++)
        {
            if(!allBoxes[i].isMarked)
            {
                return false;
            }
        }
        return !CheckIfWin();
    }

    private bool AreBoxesMatched(int a, int b, int c) //checks if three boxes have the same (current)mark
    {
        Mark m = currentMark;
        return (marks[a] == m && marks[b] == m && marks[c] == m);
    }

    public void SwitchPlayer() //switches to the next player
    {
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Sprite GetSprite() //gets the sprite of the current player
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }
}