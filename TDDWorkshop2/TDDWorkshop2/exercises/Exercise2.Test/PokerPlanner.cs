using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Exercise2.Test
{
    public class PokerPlanner_Should
    {
        [Test]
        public void Start_new_game_with_one_player_and_play_a_game_that_results_in_the_card_that_was_played()
        {
            const int NumberOfPlayers = 1;

            // arrange
            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(NumberOfPlayers);

            // act
            game.Play("5");

            // assert
            Assert.That(game.WaitForResult(0), Is.EqualTo(new[] { "5" }));
        }

        [Test]
        public void Start_new_game_with_two_players_and_play_a_game_that_results_in_both_cards_that_were_played()
        {
            const int NumberOfPlayers = 2;

            // arrange
            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(NumberOfPlayers);

            // act
            game.Play("5");
            game.Play("8");

            // assert
            Assert.That(game.WaitForResult(0), Is.EqualTo(new[] { "5", "8" }));
        }

        [Test]
        public void Not_give_result_until_all_players_have_played_their_cards()
        {
            const int NumberOfPlayers = 3;

            // arrange
            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(NumberOfPlayers);

            // act
            game.Play("5");
            game.Play("8");
            // game.Play(3); the missing play

            // assert - wait for 1 ms, if result has not arrived throw exception
            TestDelegate getResult = () => game.WaitForResult(1);
            Assert.That(getResult, Throws.Exception);
        }

        [Test]
        public void Wait_for_all_players_to_play_their_cards_before_returning_the_result()
        {
            const int NumberOfPlayers = 3;

            // arrange
            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(NumberOfPlayers);

            // get the result
            var task = Task.Run(() => game.WaitForResult(100));
            System.Threading.Thread.Sleep(50);

            // act
            game.Play("5");
            game.Play("8");
            game.Play("3");

            // assert
            Assert.That(task.Result, Is.EqualTo(new[] { "5", "8", "3" }));
        }

        [Test]  
        public void Get_exiting_game_and_play_it()
        {
            const int NumberOfPlayers = 2;
            const string GameName = "My Game";

            // arrange
            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(GameName, NumberOfPlayers);

            // act
            pokerPlanner.GetGame(GameName).Play("5");
            pokerPlanner.GetGame(GameName).Play("8");

            // assert
            Assert.That(game.WaitForResult(0), Is.EqualTo(new[] { "5", "8" }));
        }

        ///
        /// Exercise 2:1
        ///

        [Test]
        public void Ensure_That_Play_Method_Accepts_Only_Valid_Cards()
        {
            const int NumberOfPlayers = 2;
            const string GameName = "My Game";

            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(GameName, NumberOfPlayers);

            // act
            pokerPlanner.GetGame(GameName).Play("5");
            

            TestDelegate getResult = () => pokerPlanner.GetGame(GameName).Play("9");
            Assert.That(getResult, Throws.ArgumentException);
        }
        ///
        /// Exercise 2:2
        ///
        [Test]
        public void Ensure_Game_Can_Not_be_Replayed_After_Is_Completed()
        {
            const int NumberOfPlayers = 2;
            const string GameName = "My Game";

            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(GameName, NumberOfPlayers);

            // act
            pokerPlanner.GetGame(GameName).Play("5");
            pokerPlanner.GetGame(GameName).Play("8");

            TestDelegate getResult = () => pokerPlanner.GetGame(GameName).Play("5");
            Assert.That(getResult, Throws.Exception);
        }
        ///
        /// Exercise 2:3
        ///

        [Test]
        public void Enable_The_Game_To_Replay()
        {
            const int NumberOfPlayers = 2;
            const string GameName = "My Game";

            var pokerPlanner = new PokerPlanner();
            var game = pokerPlanner.NewGame(GameName, NumberOfPlayers);

            // act
            pokerPlanner.GetGame(GameName).Play("5");
            pokerPlanner.GetGame(GameName).Play("8");
            game.ResetGame();
            pokerPlanner.GetGame(GameName).Play("3");
            pokerPlanner.GetGame(GameName).Play("100");
            
            Assert.That(game.WaitForResult(10), Is.EqualTo(new[] { "3", "100" }));
        }
    }
}
