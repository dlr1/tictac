using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe
{
    public class AppState
    {
        public BoardState State { get; private set; }

        private int _currentStateId;

        public GameHistory History { get;  private set; }
        public AppState()
        {
            State = new BoardState();
            History = new GameHistory();
        }
        public void ResetBoard()
        {            
            State = new BoardState();
            History = new GameHistory();            
        }

        public void GoToState(int i)
        {
            State = BoardState.DeepClone(History.States[i]);
            _currentStateId = i + 1;            
        }

        public void UpdateValue(int id)
        {
            State.ValueChanged(id);

            if (!State.IsGameOver)
                AddHistory(State);

            NotifyStateChanged();            
        }

        private void AddHistory(BoardState state)
        {
            if (_currentStateId > 0)
            {
                //Console.WriteLine(_currentStateId );

                while (History.States.Count > _currentStateId)
                    History.States.RemoveAt(History.States.Count - 1);

                //history = new GameHistory {States = new List<BoardState>(history.States.GetRange(0, _currentStateId))};
                _currentStateId = 0;
            }

            //history = GameHistory.DeepClone(history);
            History.States.Add(BoardState.DeepClone(state));            
        }


        public event Action OnStateChanged;
        private void NotifyStateChanged() => OnStateChanged?.Invoke();
    }
}
