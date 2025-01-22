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
        GameObject board = GameObject.Find("Board");
        Board script = board.GetComponent<Board>();

        Assert.AreEqual(false, script.CheckIfDraw());

        yield return null;
    }
}
