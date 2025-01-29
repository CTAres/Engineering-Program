using System;
using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WinTest
{
    [UnityTest]
    public IEnumerator HasWon()
    {
        

        var gameObject = new GameObject();
        var board = gameObject.AddComponent<Board>();

        //sets X wins to 2 and then triggers end function with X as current mark, which adds 1 point to X and therefore lets them win.
        board.xWins = 2;
        board.currentMark = Mark.X;
        board.EndRound(Mark.X);

        Assert.AreEqual(1, board.test);
        
        yield return null;
    }
}
