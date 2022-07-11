using UnityEngine;

namespace Assets.Code.Tick
{
    public sealed class WaitForTick : CustomYieldInstruction
    {
        private int _ticksToAwait;
        private int _ticks;
        public override bool keepWaiting => _ticks < 0;


        public WaitForTick(int tickAmount = 1)
        {
            _ticksToAwait = tickAmount;
            Reset();
        }

        private void IncrementTick()
        {
            _ticks++;
            if (_ticks >= _ticksToAwait)
                TickManager.Instance.OnTick -= IncrementTick;
        }

        public override void Reset()
        {
            _ticks = 0;
            TickManager.Instance.OnTick += IncrementTick;
        }
    }
}