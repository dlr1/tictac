using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    [Serializable]
    public class GameHistory
    {
        public List<BoardState> States { get; set; }
        public GameHistory()
        {
            States = new List<BoardState>();
        }
        public static GameHistory DeepClone(object obj)
        {
            GameHistory objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms) as GameHistory;
            }
            return objResult;
        }
    }

    [Serializable]
    public class BoardState
    {
        public List<string> Values { get; set; }
        public bool XIsNext { get; set; }

        public string Status { get; set; }

        public bool IsGameOver { get; set; }
        public BoardState()
        {
            Values = Enumerable.Range(0, 9).Select(x => "").ToList();
        }

        public string CalculateWinner()
        {
            int[,] lines = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            
            for (var i = 0; i <= lines.GetUpperBound(0); i++)
            {
                var a = lines[i, 0];
                var b = lines[i, 1];
                var c = lines[i, 2];

                if (Values[a] != "" && (Values[a] == Values[b] && Values[a] == Values[c]))
                {
                    return Values[a];
                }

            }

            return null;
        }

        public void ValueChanged(int id)
        {
            if (IsGameOver || Values[id] != "")
                return;

            Values[id] = XIsNext ? "X" : "O";
            XIsNext = !XIsNext;

            var winner = CalculateWinner();

            IsGameOver = winner != null;
            Status = IsGameOver ? $"Winner : {winner}" : "Next player: " + (XIsNext ? "X" : "O");
            
        }
        public static BoardState DeepClone(object obj)
        {
            BoardState objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms) as BoardState;
            }
            return objResult;
        }
    }

}
