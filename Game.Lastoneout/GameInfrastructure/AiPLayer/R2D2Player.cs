using System;
using Game.Common.Helpers;

namespace Game.Lastoneout.GameInfrastructure.AiPLayer
{
    class R2D2Player : IAiPlayer
    {
        public string Name { get { return "R2D2"; } }
        public string ImageSource { get { return "/Game.Lastoneout;component/Images/r2d2.png"; } }
        public string GetMessage()
        {
            return "Beep beep... beep beep beep";
        }

        public TimeSpan GetDelay()
        {
            return TimeSpan.FromMilliseconds(RandomHelper.RandomNumber(1500, 3001));
        }

        public int GetMove(int state)
        {
            // maintain winning position in 50% of cases

            if (!RandomHelper.FlipACoin())
                return RandomHelper.RandomNumber(1, Math.Min(3, state) + 1);
            
            switch (state % 4) 
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
