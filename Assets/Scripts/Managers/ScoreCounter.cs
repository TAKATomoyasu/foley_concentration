using System;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class ScoreCounter
    {
        [SerializeField] private int player1Score;
        public int Player1Score => player1Score;
        [SerializeField] private int player2Score;
        public int Player2Score => player2Score;

        [SerializeField] private int player1Steps;
        public int Player1Steps => player1Steps;
        [SerializeField] private int player2Steps;
        public int Player2Steps => player2Steps;


        public void AddScore(int playerNum)
        {
            if (playerNum == 0) player1Score++;
            if (playerNum == 1) player2Score++;
        }

        public void AddSteps(int playerNum)
        {
            if (playerNum == 0) player1Steps++;
            if (playerNum == 1) player2Steps++;
        }
    }
}


