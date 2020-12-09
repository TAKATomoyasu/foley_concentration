using Inputs;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Managers
{
    public class SoloGameManager : BaseGameManager
    {
        private ITrigger _trigger;

        void Start()
        {
            _trigger = GetComponent<ITrigger>();
            Initialize(_trigger.MaxTrigger());


            _trigger.TriggerNum()
                .Do(num => Debug.Log("Triggered: " + num))
                .Where(n => FirstCard != n) //同じカードでない
                // ソロはターン性ではない
                .Subscribe(num =>
                {
                    if (cardMapController.IsExist(num))
                    {
                        CheckCard(num);
                    }
                    else
                    {
                        uiSoundPlayer.PlayNoCard();
                        Debug.Log("ここにカードはないよ。");
                    }
                })
                .AddTo(gameObject);
        }

        private void Initialize(int maxTrigger)
        {
            cardCounter.Initialize(maxTrigger);
            cardMapController.Initialize(maxTrigger);
        }

        private void CheckCard(int num)
        {
            soundPlayer.PlayCardSound(num, cardMapController.GetCardNum(num));
            if (FirstCard == -1) // 1枚めならば
            {
                FirstCard = num;
            }
            else
            {
                if (cardMapController.IsMatch(FirstCard, num)) //合ってる場合
                {
                    CorrectPair();
                }
                else //外れた場合
                {
                    uiSoundPlayer.PlayWrong();
                }

                scoreCounter.AddSteps(currentPlayer);
                FirstCard = -1;
            }
        }

        private void CorrectPair()
        {
            scoreCounter.AddScore(currentPlayer);
            uiSoundPlayer.PlayCorrect();
            cardCounter.RemoveCards();

            if (cardCounter.IsOver())
            {
                stateManager.SetGameState(GameState.Finished);
                uiSoundPlayer.PlayClear();
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Z) && Input.GetKeyDown(KeyCode.P))
            {
                CorrectPair();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (stateManager.GameState.Value == GameState.Ready)
            {
                if (Input.GetKeyDown(KeyCode.Alpha8)) Initialize(10);
                if (Input.GetKeyDown(KeyCode.Alpha9)) Initialize(19);
                if (Input.GetKeyDown(KeyCode.Alpha0)) Initialize(26);
            }
        }
    }
}