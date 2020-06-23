using NUnit.Framework;
using UnityEngine;

public class ScoreTester : MonoBehaviour
{
    ScoreManager scoreManager;
    Pickup pickup;

    [SetUp]
    public void SetUpTests()
    {
        scoreManager = new ScoreManager();
        pickup = new Pickup();
    }

    [Test]
    public void TestCorrectPickup()
    {
        
        pickup.scoreValue = 10;
        try
        {
            scoreManager.IncreaseScore(pickup.scoreValue);
        }
        catch (System.Exception)
        {
            Debug.Log("Expected Error because ScoreText");
        }
        
        Assert.AreEqual(scoreManager.Score, 10);
    }

    [Test]
    public void TestWrongPickup()
    {
        pickup.scoreValue = 50;

        try
        {
            scoreManager.IncreaseScore(pickup.scoreValue);
        }
        catch (System.Exception)
        {
            Debug.Log("Expected Error because ScoreText");
        }

        Assert.AreNotEqual(scoreManager.Score, 10);
    }
}