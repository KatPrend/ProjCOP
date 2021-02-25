using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestGameFlow
    {
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Chess");
        }
        
        [UnityTest]
        public IEnumerator TestGetSquareCenter()
        {
            BoardManager board = BoardManager.Instance;

            yield return null;

            Vector3 leftWhiteRookPosition = board.Pieces[0, 0].transform.position;
            Assert.AreEqual(leftWhiteRookPosition, new Vector3(0.5f, 0, 0.5f));
        }
    }
}
