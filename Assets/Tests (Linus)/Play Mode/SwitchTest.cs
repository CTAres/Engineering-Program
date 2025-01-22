using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SwitchTest
{
    [UnityTest]
    public IEnumerator IsSwitched()
    {
        var gameObject = new GameObject();
        var board = gameObject.AddComponent<Board>();

        board.currentMark = Mark.X;
        board.SwitchPlayer();

        Assert.AreEqual(Mark.O, board.currentMark);
        
        yield return null;
    }
}
