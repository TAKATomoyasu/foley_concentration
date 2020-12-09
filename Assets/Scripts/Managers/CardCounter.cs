using System;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class CardCounter
    {
        [SerializeField, NonEditable] int _cardCount;

        public void Initialize(int num)
        {
            _cardCount = num;
        }

        public void RemoveCards()
        {
            _cardCount -= 2;
        }

        public bool IsOver()
        {
            return _cardCount <= 0;
        }
    }
}