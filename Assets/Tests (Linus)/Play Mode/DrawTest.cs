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

        //hier benötige ich das box array um alle Boxen auf marks zu stellen, sodass ein draw entsteht.

        Assert.AreEqual(true, board.CheckIfDraw());
        
        yield return null;
    }
}
