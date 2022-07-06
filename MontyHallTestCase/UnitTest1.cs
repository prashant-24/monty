using MontyHall;
using MontyHall.Model;
using Moq;
using Xunit;

namespace MontyHallTestCase
{
    public class UnitTest1
    {

        [Fact]
        public void PlayGameIfYouWantToSwitch()
        {
            Mock<MontyService> _mockMontyService = new Mock<MontyService>();
            _mockMontyService.SetupSequence(x => x.PlayGame("A", 1000))
                .Returns(new montyModel { switchedDoorCount = 1000, totalWinCount = 689, winPercentage = (decimal)0.0689, validModel = 0 })
                .Returns(new montyModel { switchedDoorCount = 1000, totalWinCount = 671, winPercentage = (decimal)0.0671, validModel = 0 });

            var resultOne = _mockMontyService.Object.PlayGame("A", 1000);
            var resultTwo = _mockMontyService.Object.PlayGame("A", 1000);
            Assert.Equal(1000, resultOne.switchedDoorCount);
            Assert.Equal(689, resultOne.totalWinCount);
            Assert.Equal((decimal)0.0689, resultOne.winPercentage);
            Assert.Equal(0, resultOne.validModel);

            Assert.Equal(1000, resultTwo.switchedDoorCount);
            Assert.Equal(671, resultTwo.totalWinCount);
            Assert.Equal((decimal)0.0671, resultTwo.winPercentage);
            Assert.Equal(0, resultOne.validModel);

        }
        [Fact]
        public void PlayGameIfYouWantToKeep()
        {
            Mock<MontyService> _mockMontyService = new Mock<MontyService>();
            _mockMontyService.SetupSequence(x => x.PlayGame("B", 1000))
                .Returns(new montyModel { switchedDoorCount = 0, totalWinCount = 323, winPercentage = (decimal)0.0323, validModel = 0 })
                .Returns(new montyModel { switchedDoorCount = 0, totalWinCount = 333, winPercentage = (decimal)0.0333, validModel = 0 });

            var resultOne = _mockMontyService.Object.PlayGame("B", 1000);
            var resultTwo = _mockMontyService.Object.PlayGame("B", 1000);
            Assert.Equal(0, resultOne.switchedDoorCount);
            Assert.Equal(323, resultOne.totalWinCount);
            Assert.Equal((decimal)0.0323, resultOne.winPercentage);
            Assert.Equal(0, resultOne.validModel);

            Assert.Equal(0, resultTwo.switchedDoorCount);
            Assert.Equal(333, resultTwo.totalWinCount);
            Assert.Equal((decimal)0.0333, resultTwo.winPercentage);
            Assert.Equal(0, resultOne.validModel);
        }
        [Fact]
        public void PlayGameIfYouSelectNone()
        {
            Mock<MontyService> _mockMontyService = new Mock<MontyService>();
            _mockMontyService.SetupSequence(x => x.PlayGame("C", 1000))
                .Returns(new montyModel { switchedDoorCount = 488, totalWinCount = 505, winPercentage = (decimal)0.0505, validModel = 0})
                .Returns(new montyModel { switchedDoorCount = 499, totalWinCount = 515, winPercentage = (decimal)0.0515, validModel = 0 });

            var resultOne = _mockMontyService.Object.PlayGame("C", 1000);
            var resultTwo = _mockMontyService.Object.PlayGame("C", 1000);
            Assert.Equal(488, resultOne.switchedDoorCount);
            Assert.Equal(505, resultOne.totalWinCount);
            Assert.Equal((decimal)0.0505, resultOne.winPercentage);
            Assert.Equal(0, resultOne.validModel);

            Assert.Equal(499, resultTwo.switchedDoorCount);
            Assert.Equal(515, resultTwo.totalWinCount);
            Assert.Equal((decimal)0.0515, resultTwo.winPercentage);
            Assert.Equal(0, resultOne.validModel);
        }

        [Fact]
        public void PlayGameIfYouEnterInvalidOption()
        {
            Mock<MontyService> _mockMontyService = new Mock<MontyService>();
            _mockMontyService.SetupSequence(x => x.PlayGame("D", 1000))
                .Returns(new montyModel { switchedDoorCount = 0, totalWinCount = 0, winPercentage = 0, validModel = 1 });               

            var resultOne = _mockMontyService.Object.PlayGame("D", 1000);            
            Assert.Equal(1, resultOne.validModel);
           
        }

        [Fact]
        public void PlayIfYouWantToSwitch()
        {
            Mock<MontyHallGame> _mockMontyHallgame = new Mock<MontyHallGame>();
            _mockMontyHallgame.SetupSequence(x => x.Play(MontyHallGame.Strategy.Switch))
                .Returns(new Result { ContestantSwitchedDoor = true, ContestantWins = true })
                .Returns(new Result { ContestantSwitchedDoor = true, ContestantWins = false });

            var resultOne = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);
            var resultTwo = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);

            Assert.True(resultOne.ContestantSwitchedDoor, "User want to switch door");
            Assert.True(resultOne.ContestantWins, "User win in this door");

            Assert.True(resultTwo.ContestantSwitchedDoor, "User want to switch door");
            Assert.False(resultTwo.ContestantWins, "User cannot win in this door");
        }

        [Fact]
        public void PlayIfYouWantToKeep()
        {
            Mock<MontyHallGame> _mockMontyHallgame = new Mock<MontyHallGame>();
            _mockMontyHallgame.SetupSequence(x => x.Play(MontyHallGame.Strategy.Switch))
                .Returns(new Result { ContestantSwitchedDoor = false, ContestantWins = true })
                .Returns(new Result { ContestantSwitchedDoor = false, ContestantWins = false });

            var resultOne = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);
            var resultTwo = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);

            Assert.False(resultOne.ContestantSwitchedDoor, "User don't want switch door");
            Assert.True(resultOne.ContestantWins, "User win in this door");

            Assert.False(resultTwo.ContestantSwitchedDoor, "User don't want switch door");
            Assert.False(resultTwo.ContestantWins, "User cannot win in this door");
        }

        [Fact]
        public void PlayIfYouWantToSelectNone()
        {
            Mock<MontyHallGame> _mockMontyHallgame = new Mock<MontyHallGame>();
            _mockMontyHallgame.SetupSequence(x => x.Play(MontyHallGame.Strategy.Switch))
                .Returns(new Result { ContestantSwitchedDoor = true, ContestantWins = true })
                .Returns(new Result { ContestantSwitchedDoor = false, ContestantWins = false })
             .Returns(new Result { ContestantSwitchedDoor = true, ContestantWins = false })
              .Returns(new Result { ContestantSwitchedDoor = false, ContestantWins = true });

            var resultOne = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);
            var resultTwo = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);
            var resultThree = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);
            var resultFour = _mockMontyHallgame.Object.Play(MontyHallGame.Strategy.Switch);

            Assert.True(resultOne.ContestantSwitchedDoor, "User want to switch door");
            Assert.True(resultOne.ContestantWins, "User win in this door");

            Assert.False(resultTwo.ContestantSwitchedDoor, "User don't want switch door");
            Assert.False(resultTwo.ContestantWins, "User cannot win in this door");

            Assert.True(resultThree.ContestantSwitchedDoor, "User want to switch door");
            Assert.False(resultThree.ContestantWins, "User cannot win in this door");

            Assert.False(resultFour.ContestantSwitchedDoor, "User don't want switch door");
            Assert.True(resultFour.ContestantWins, "User win in this door");
        }
    }

}