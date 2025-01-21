using System.Collections;
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
    [SerializeField] private Box[] allBoxes;

    public Mark[] marks;

    private Camera cam;
    private Mark currentMark;

    public bool roundOver;

    private int xWins;
    private int oWins;

    private void Start()
    {
        cam = Camera.main;

        currentMark = Mark.X;

        marks = new Mark[9];
    }

    private void Update()
    {
        if (roundOver)
        {
            EndRound();
        }
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

    private void HitBox(Box box) //if the box isnt marked, it gets claimed for the current player. After that we check win conditions
    {
        if(!box.isMarked) 
        {
            marks[box.index] = currentMark;

            box.SetAsMarked(GetSprite(), currentMark);

            if(CheckIfWin())
            {
                roundOver = true;
                //no return here even if win, so that after a win the other player starts
            };

            SwitchPlayer();
        }
    }

    private void EndRound() //increses win counter and prepares next game
    {
        Debug.Log (currentMark.ToString() + " Wins!");
        if(currentMark == Mark.X)
        {
            xWins = xWins+1;
        }
        else
        {
            oWins = oWins+1;
        }
         Debug.Log("Total wins: X = " + xWins + " | O = " + oWins);

         for(int i = 0; i < allBoxes.Length; i++) //reset all boxes in the box script and the marks array
         {
            allBoxes[i].ResetBox(spriteEmpty);
            marks[i] = Mark.None;
         }
         roundOver = false;
    }

    private bool CheckIfWin() //checks all possible win combinations
    {
        return
        AreBoxesMatched(0,1,2) || AreBoxesMatched(3,4,5) || AreBoxesMatched(6,7,8) ||
        AreBoxesMatched(0,3,6) || AreBoxesMatched(1,4,7) || AreBoxesMatched(2,5,8) ||
        AreBoxesMatched(0,4,8) || AreBoxesMatched(2,4,6);
    }

    private bool AreBoxesMatched(int a, int b, int c) //checks if three boxes have the same (current)mark
    {
        Mark m = currentMark;
        return (marks[a] == m && marks[b] == m && marks[c] == m);
    }

    private void SwitchPlayer() //switches to the next player
    {
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Sprite GetSprite() //gets the sprite of the current player
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }
}