﻿using System.Collections.Generic;
using System.Threading;

namespace PCLabs.Helpers
{
    public static class ThreadExtension
    {
        public static void WaitAll(this IEnumerable<Thread> threads)
        {
            if (threads != null)
            {
                foreach (Thread thread in threads)
                { thread.Join(); }
            }
        }
    }
}
