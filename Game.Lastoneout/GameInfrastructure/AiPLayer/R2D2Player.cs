using System;
using Game.Common.Helpers;

namespace Game.Lastoneout.GameInfrastructure.AiPLayer
{
    class R2D2Player : IAiPlayer
    {
        public string Name { get { return "R2D2"; } }
        public string ImageSource { get { return ""; } }
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
            //When state is 5 doesn't matter what is returned (loose case)
            // [2 - 4] win case 
            if (state <= 5)
                return state <= 4 ? Math.Max(state - 1, 1) : RandomHelper.RandomNumber(1, 4);

            var tmp = 0;
            var i = 0;
            while (tmp < state)
            {
                tmp = 5 + (4 * i);
                i++;
            }

            if (tmp == state) // like 5
            {
                return 1;
            }

            if (RandomHelper.FlipACoin()) //maintains optimal position in 50% os cases
            {
                switch (tmp - state) // maintain position winning position
                {
                    case 3:
                        return 1;
                    case 2:
                        return 2;
                    default:
                        return 3;
                }
            }
            return RandomHelper.RandomNumber(1, 4);
        }
    }
}
