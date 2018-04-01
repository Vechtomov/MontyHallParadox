using System;

namespace MontyHallLibrary
{
    /// <summary>
    /// Глобальный потокобезопасный рандомайзер
    /// </summary>
    public static class GlobalRandom
    {
        private static object locker = new object();
        private static Random rand = new Random();

        public static int Next(int minValue, int maxValue)
        {
            lock (locker) {
                return rand.Next(minValue, maxValue);
            }
        }

        public static double NextDouble()
        {
            lock (locker) {
                return rand.NextDouble();
            }
        }

        public static void NextBytes(byte[] buffer)
        {
            lock (locker) {
                rand.NextBytes(buffer);
            }
        }
    }
}
