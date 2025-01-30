using System;
using System.Collections;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AllCubesArrayTest
{
    [UnityTest]
    public IEnumerator IsFilled()
    {
        
        var gameObject = new GameObject();
        var board = gameObject.AddComponent<Board>();

        board.Start();
        bool IsCorrect()
        {
            for(int i = 0; i < board.allBoxes.Length; i++)
            {
                if (board.allBoxes[i].name != $"Box {i}")
                {
                    return false;
                }
                else if (i == board.allBoxes.Length)
                {
                    return true;
                }
            }
            Debug.Log("this should not happen");
            return false;

        }

        Assert.AreEqual(true, IsCorrect());
        
        yield return null;
    }
}
