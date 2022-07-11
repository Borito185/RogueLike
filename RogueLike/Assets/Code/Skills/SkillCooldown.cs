using Mirror;
using Steamworks;
using UnityEngine.PlayerLoop;

namespace Assets.Code.Skills
{
    public struct SkillCooldown
    {
        public double StartTime;
        public double Duration;
        public SkillCooldown(double duration = 0)
        {
            StartTime = NetworkTime.time;
            Duration = duration;
        }

        public bool CooldownIsOver()
        {
            double currentTime = NetworkTime.time;
            return currentTime >= StartTime + Duration;
        }
    }
}