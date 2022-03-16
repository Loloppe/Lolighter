using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using static Lolighter.Info.Enumerator;

namespace Lolighter.Info
{
    internal static class Utils
    {
        /// <summary>
        /// Method to randomly generate a number between the minimum and maximum (excluded)
        /// If Maximum is Minimum, return Minimum
        /// </summary>
        /// <param name="Low">Minimum</param>
        /// <param name="High">Maximum - 1</param>
        /// <returns></returns>
        internal static int RandNumber(int Low, int High)
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            int rnd = rndNum.Next(Low, High);

            return rnd;
        }

        /// <summary>
        /// Method to swap between Fade and On for EventLightValue
        /// </summary>
        /// <param name="value">Current EventLightValue</param>
        /// <returns>Swapped EventLightValue</returns>
        internal static int Swap(int value)
        {
            switch (value)
            {
                case EventLightValue.BLUE_FADE: return EventLightValue.BLUE_ON;
                case EventLightValue.RED_FADE: return EventLightValue.RED_ON;
                case EventLightValue.BLUE_ON: return EventLightValue.BLUE_FADE;
                case EventLightValue.RED_ON: return EventLightValue.RED_FADE;
                default: return 0;
            }
        }

        /// <summary>
        /// Method to inverse the current EventLightValue between Red and Blue
        /// </summary>
        /// <param name="value">Current EventLightValue</param>
        /// <returns>Inversed EventLightValue</returns>
        internal static int Inverse(int value)
        {
            if (value > EventLightValue.BLUE_FADE)
                return value - 4; //Turn to blue
            else
                return value + 4; //Turn to red
        }

        /// <summary>
        /// Method to randomise the element of a List
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="list">List</param>
        internal static void Shuffle<T>(this IList<T> list)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do rng.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
