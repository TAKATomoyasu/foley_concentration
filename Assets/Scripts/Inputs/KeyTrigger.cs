using System;
using UniRx;
using UnityEngine;

namespace Inputs
{
    public class KeyTrigger : MonoBehaviour, ITrigger
    {
        private Subject<int> triggerNum = new Subject<int>();


        [SerializeField] private string[] keys =
        {
            "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x",
            "c",
            "v", "b", "n", "m"
        };


        public Subject<int> TriggerNum()
        {
            return triggerNum;
        }

        public int MaxTrigger()
        {
            return keys.Length;
        }

        private void Update()
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    // Debug.Log("key: " + keys[i]);
                    triggerNum.OnNext(i);
                }
            }
        }
    }
}