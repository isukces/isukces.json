using System;
using System.Threading;

namespace isukces.json
{
    static class Sync
    {
        public static T Calc<T>(IJsonSemaphore mutex, Func<T> func)
        {
            mutex.WaitOne();
            try
            {
                return func();
            }
            finally
            {
                mutex.ReleaseOne();
            }
        }

        public static void Exec(IJsonSemaphore mutex, Action action)
        {
            mutex.WaitOne();
            try
            {
                action();
            }
            finally
            {
                mutex.ReleaseOne();
            }
        }
    }
}