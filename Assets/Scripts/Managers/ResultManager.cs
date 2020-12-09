using System;
using Sound;
using UniRx;
using UnityEngine;

namespace Managers
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] StateManager stateManager;
        [SerializeField] ResultPlayer resultPlayer = new ResultPlayer();
        private ScoreCounter _scoreCounter;

        private void Start()
        {
            stateManager = BaseGameManager.Instance.stateManager;
            _scoreCounter = BaseGameManager.Instance.scoreCounter;

            stateManager.GameState
                .Where(s => s == GameState.Finished)
                .Delay(TimeSpan.FromSeconds(5))
                .Subscribe(_ => { resultPlayer.PlayResult(_scoreCounter.Player1Steps); })
                .AddTo(gameObject);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            resultPlayer.PlayResult(_scoreCounter.Player1Steps);
        }
    }
}