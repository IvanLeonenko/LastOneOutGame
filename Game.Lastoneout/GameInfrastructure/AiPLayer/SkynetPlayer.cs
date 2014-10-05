using System;
using Game.Common.Helpers;

namespace Game.Lastoneout.GameInfrastructure.AiPLayer
{
    public class SkynetPlayer : IAiPlayer
    {
        public string Name { get { return "Skynet"; } }
        public string ImageSource { get { return "/Game.Lastoneout;component/Images/skynet.png"; } }
        public string GetMessage()
        {
            return "You won't be back, human..";
        }

        public TimeSpan GetDelay()
        {
            return TimeSpan.FromMilliseconds(RandomHelper.RandomNumber(750, 1250));
        }

        public int GetMove(int state)
        {
            switch (state % 4) // maintain winning position
            {
                case 0:
                    return 3;
                case 3:
                    return 2;
                default:
                    return 1;
            }
        }
    }
}
