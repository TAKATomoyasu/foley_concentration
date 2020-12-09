using System;
using UniRx;

namespace Inputs
{
    
    public interface ITrigger
    {
        Subject<int> TriggerNum();
        int MaxTrigger();
    }
}