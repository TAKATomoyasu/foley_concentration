using System;
using System.Linq;
using UnityEngine;

namespace Map
{
    public class CardMapController : MonoBehaviour
    {
        [SerializeField] private CardMap cardMap;


        public void Initialize(int num)
        {
            cardMap = new CardMap(num);
            Shuffle();
        }


        public void Shuffle()
        {
            for (int i = 0; i < cardMap.mapNum.Length; i++)
            {
                cardMap.mapNum[i] = i / 2;
            }

            cardMap.mapNum = cardMap.mapNum.OrderBy(i => Guid.NewGuid()).ToArray();
        }

        public bool IsExist(int num)
        {
            if (num >= cardMap.mapNum.Length) return false;
            return cardMap.mapNum[num] != -1;
        }

        public bool IsMatch(int num1, int num2)
        {
            var b = cardMap.mapNum[num1] == cardMap.mapNum[num2];
            if (b)
            {
                cardMap.mapNum[num1] = -1;
                cardMap.mapNum[num2] = -1;
            }

            return b;
        }

        public int GetCardNum(int num)
        {
            return cardMap.mapNum[num];
        }
    }
}