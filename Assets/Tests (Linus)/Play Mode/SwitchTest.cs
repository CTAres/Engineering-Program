using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//This Unit Test is not doing much as there is not much that can not work with SwitchPLayer()
//but I included it as it was the only function that didnt need the allBoxes[] Array and I wanted to have at least 1 working Unit Test


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
