﻿using System;

namespace Game.Lastoneout.Helpers
{
    public class RandomHelper
    {
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();
        
        public static int RandomNumber(int min, int max)
        {
            lock (SyncLock)
            {
                return Random.Next(min, max);
            }
        }

        public static bool FlipACoin()
        {
            return RandomNumber(0, 1) == 1;
        }
    }
}
