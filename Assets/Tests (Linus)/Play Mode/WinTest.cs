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

        board.xWins = 3;
        board.EndRound(Mark.X);

        Assert.AreEqual(1, board.test);
        
        yield return null;
    }
}
