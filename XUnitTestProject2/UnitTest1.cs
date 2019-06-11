using System;
using Xunit;

namespace XUnitTestProject2
{
    class Game
    {
        private int[] rolls = new int[21];
        private int rolled = 0;
        public void Roll(int pins)
        {
            if (pins > 10 || pins < 0)
                throw new ArgumentException();
            if (rolled > 20)
                throw new IndexOutOfRangeException();
            //if (pins <= 10 && pins >= 0 && rolled < 21)
            //{
                rolls[rolled++] = pins;
            //}
        }

        public int Score()
        {
            int index = 0;
            int score = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (rolls[index] == 10)
                {
                    score += rolls[index] + rolls[index + 1] + rolls[index + 2];
                    index++;
                }
                else if (rolls[index] + rolls[index + 1] == 10)
                {
                    score += rolls[index] + rolls[index + 1] + rolls[index + 2];
                    index += 2;
                }
                else
                {
                    score += rolls[index] + rolls[index + 1];
                    index += 2;
                }
            }
            return score;
        }
    }
    public class UnitTest1
    {
        [Fact]
        public void ArgumentException_lessThenZero()
        {
            Game game = new Game();
            Action act = () => game.Roll(-1);
            Assert.Throws<ArgumentException>(act);
            /*for (int i = 0; i < 20; i++)
                game.Roll(-1);
            Assert.Equal(0, game.Score());*/
        }

        [Fact]
        public void ArgumentException_moreThan10()
        {
            Game game = new Game();
            Action act = () => game.Roll(11);
            Assert.Throws<ArgumentException>(act);
            /*for (int i = 0; i < 20; i++)
                game.Roll(11);
            Assert.Equal(0, game.Score());*/
        }

        [Fact]
        public void IndexOutOfRangeException_MoreThan10Frames()
        {
            Game game = new Game();
            for (int i = 0; i < 21; i++)
                game.Roll(1);
            Action act = () => game.Roll(1);
            Assert.Throws<IndexOutOfRangeException>(act);
        }
        [Fact]
        public void TenthFrame()
        {
            Game game = new Game();
            for (int i = 0; i < 21; i++)
                game.Roll(1);
            //Assert.Equal(20, game.Score());
            Assert.NotEqual(21, game.Score());
        }

        [Fact]
        public void AlwaysZero()
        {
            Game game = new Game();
            for (int i = 0; i < 20; i++)
                game.Roll(0);
            Assert.Equal(0, game.Score());
        }

        [Fact]
        public void AlwaysOne()
        {
            Game game = new Game();
            for (int i = 0; i < 20; i++)
                game.Roll(1);
            Assert.Equal(20, game.Score());
        }

        [Fact]
        public void Spare()
        {
            Game game = new Game();
            game.Roll(3);
            game.Roll(7);
            game.Roll(2);
            game.Roll(5);
            for (int i = 0; i < 16; i++)
                game.Roll(0);
            Assert.Equal(19, game.Score());
        }

        [Fact]
        public void Strike()
        {
            Game game = new Game();
            game.Roll(10);
            game.Roll(2);
            game.Roll(4);
            game.Roll(1);
            for (int i = 0; i < 16; i++)
                game.Roll(0);
            Assert.Equal(23, game.Score());
        }

        [Fact]
        public void AlwaysTen()
        {
            Game game = new Game();
            for (int i = 0; i < 12; i++)
                game.Roll(10);
            Assert.Equal(300, game.Score());
        }

        [Fact]
        public void AlwaysFive()
        {
            Game game = new Game();
            for (int i = 0; i < 21; i++)
                game.Roll(5);
            Assert.Equal(150, game.Score());
        }

        [Fact]
        public void TenthFrame_Strike()
        {
            Game game = new Game();
           
            for (int i = 0; i < 18; i++)
                game.Roll(0);
            game.Roll(10);
            game.Roll(2);
            game.Roll(4);
            Assert.Equal(16, game.Score());
        }


        [Fact]
        public void TenthFrame_Spare()
        {
            Game game = new Game();
            
            for (int i = 0; i < 18; i++)
                game.Roll(0);
            game.Roll(2);
            game.Roll(8);
            game.Roll(6);
            Assert.Equal(16, game.Score());
        }
    }
}
