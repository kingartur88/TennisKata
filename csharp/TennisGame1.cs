using System;

namespace Tennis
{    
    class TennisGame1 : TennisGame
    {
        private int scorePlayer1 = 0;
        private int scorePlayer2 = 0;

        private readonly string player1Name;
        private readonly string player2Name;     
  
        public TennisGame1(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        //Set point for player
        public void WonPoint(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
                throw new ArgumentNullException(playerName);

            if (playerName != player1Name && playerName != player2Name)
                throw new ArgumentException("Unknown player", playerName);

            if (playerName == player1Name)
                scorePlayer1 += 1;
            else
                scorePlayer2 += 1;
        }

        //Get score players
        public string GetScore()
        {
            if (scorePlayer1 <= Convert.ToInt32(IndividualScoreValues.Forty) &&
                scorePlayer2 <= Convert.ToInt32(IndividualScoreValues.Forty) &&
                !(scorePlayer1 == scorePlayer2 && scorePlayer1 == 3))
            {
                return StandardScore();
            }

            return AtLeastOneScoreOverFourPoints();
        }

        //Verify score
        private string StandardScore()
        {
            if (scorePlayer1 == scorePlayer2)
            {
                if (scorePlayer1 == Convert.ToInt32(IndividualScoreValues.Forty))
                    return EqualScoreFourtyAndOver();

                return EqualScoreUnderFortyFormat();
            }

            return string.Format("{0}-{1}", elementaryScoreNames[scorePlayer1], elementaryScoreNames[scorePlayer2]);
        }

        //More than forty score
        private string EqualScoreFourtyAndOver()
        {
            return advancedScoreNames[0];
        }

        //Score difference at least four points
        private string AtLeastOneScoreOverFourPoints()
        {
            int scoresDifference = scorePlayer1 - scorePlayer2;
            string scoreFormat = advancedScoreNames[Math.Min(Math.Abs(scoresDifference), 2)];

            // Scores are equal
            if (scoresDifference == 0)
            {
                return scoreFormat;
            }

            return string.Format(scoreFormat, scoresDifference > 0 ? player1Name : player2Name);
        }

        private readonly string[] elementaryScoreNames = new string[4]
        {
            "Love","Fifteen","Thirty","Forty"
        };

        private readonly string[] advancedScoreNames = new string[3]
        {
            "Deuce", "Advantage {0}", "Win for {0}"
        };

        private string EqualScoreUnderFortyFormat()
        {
            return string.Format("{0}-All", elementaryScoreNames[scorePlayer1]);
        }
    }

    internal enum IndividualScoreValues : int
    {
        Love = 0,
        Fifteen = 1,
        Thirty = 2,
        Forty = 3,
    }
}