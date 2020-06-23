using AntiCheatService.Models;
using NUnit.Framework;

namespace AntiCheatServiceTest
{
    public class Tests
    {
        private LevelMinimum newLevelResult;
        private LevelMinimum levelMinimum;

        [SetUp]
        public void Setup()
        {
            levelMinimum = new LevelMinimum
            {
                LevelMinimumId = 1,
                MaxScore = 75,
                MinimumTime = 3000
            };
        }

        [Test]
        public void TestValidationNewResultGood()
        {
            newLevelResult = new LevelMinimum
            {
                LevelMinimumId = 1,
                MaxScore = 35,
                MinimumTime = 3500
            };

            bool result = LevelMinimumContext.TestForCheat(levelMinimum, newLevelResult.LevelMinimumId, newLevelResult.MaxScore, newLevelResult.MinimumTime);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestValidationNewResultBadLevel()
        {
            newLevelResult = new LevelMinimum
            {
                LevelMinimumId = 2,
                MaxScore = 35,
                MinimumTime = 3500
            };

            bool result = LevelMinimumContext.TestForCheat(levelMinimum, newLevelResult.LevelMinimumId, newLevelResult.MaxScore, newLevelResult.MinimumTime);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestValidationNewResultBadScore()
        {
            newLevelResult = new LevelMinimum
            {
                LevelMinimumId = 1,
                MaxScore = 1337,
                MinimumTime = 3500
            };

            bool result = LevelMinimumContext.TestForCheat(levelMinimum, newLevelResult.LevelMinimumId, newLevelResult.MaxScore, newLevelResult.MinimumTime);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestValidationNewResultBadTime()
        {
            newLevelResult = new LevelMinimum
            {
                LevelMinimumId = 1,
                MaxScore = 35,
                MinimumTime = 1
            };

            bool result = LevelMinimumContext.TestForCheat(levelMinimum, newLevelResult.LevelMinimumId, newLevelResult.MaxScore, newLevelResult.MinimumTime);
            Assert.IsTrue(result);
        }
    }
}