using System;
using Map;
using Sound;
using UniRx;
using UnityEngine;

namespace Managers
{
    public abstract class BaseGameManager : MonoBehaviour
    {
        public static BaseGameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        protected int currentPlayer;
        protected int FirstCard = -1;
        [SerializeField] protected CardMapController cardMapController;
        [SerializeField] public ScoreCounter scoreCounter = new ScoreCounter();
        [SerializeField] protected CardCounter cardCounter = new CardCounter();
        [SerializeField] public StateManager stateManager = new StateManager();
        [SerializeField] protected UISoundPlayer uiSoundPlayer ;
        [SerializeField] protected SoundPlayer soundPlayer;
    }
}