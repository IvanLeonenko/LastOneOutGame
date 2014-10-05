using System;
using Game.Common.Helpers;

namespace Game.Lastoneout.GameInfrastructure.AiPLayer
{
    public class BenderPlayer : IAiPlayer
    {
        public string Name { get { return "Bender"; } }
        public string ImageSource { get { return "/Game.Lastoneout;component/Images/bender.png"; } }
        public string GetMessage()
        {
            return "Hey, wanna kill all humans?";
        }

        public TimeSpan GetDelay()
        {
            return TimeSpan.FromMilliseconds(RandomHelper.RandomNumber(2000, 5001));
        }

        public int GetMove(int state)
        {
            return RandomHelper.RandomNumber(1, Math.Min(3, state) + 1);
        }
    }
}
