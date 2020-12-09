using System;

namespace Map
{
    [Serializable]
    public struct CardMap
    {
        public int[] mapNum;

        public CardMap(int num)
        {
            mapNum = new int[num];
        }
    }
}