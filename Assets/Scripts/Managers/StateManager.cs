using UniRx;
using UnityEngine;

namespace Managers
{
    public enum GameState
    {
        Ready,
        Playing,
        Finished
    }

    
    public class StateManager
    {
        private ReactiveProperty<GameState> gameState = new ReactiveProperty<GameState>();
        public ReactiveProperty<GameState> GameState => gameState;

        public void SetGameState(GameState state)
        {
            gameState.Value = state;
        }
    }
}