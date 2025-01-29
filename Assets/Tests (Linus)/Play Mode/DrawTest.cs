using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DrawTest
{
    [UnityTest]
    public IEnumerator IsDraw()
    {
        var gameObject = new GameObject();
        var board = gameObject.AddComponent<Board>();

        //sets all boxes to marked + creates a board situation that is a draw.
        for (int i = 0; i < 8; i++)
        {
            board.allBoxes[i].isMarked = true;
        }
        board.allBoxes[0].mark = Mark.O;
        board.allBoxes[1].mark = Mark.X;
        board.allBoxes[2].mark = Mark.O;
        board.allBoxes[3].mark = Mark.O;
        board.allBoxes[4].mark = Mark.X;
        board.allBoxes[5].mark = Mark.X;
        board.allBoxes[6].mark = Mark.X;
        board.allBoxes[7].mark = Mark.O;
        board.allBoxes[8].mark = Mark.X;

        Assert.AreEqual(true, board.CheckIfDraw());
        
        yield return null;
    }
}
