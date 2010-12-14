﻿namespace ZDebug.Core.Execution
{
    public sealed partial class Processor
    {
        private class NullMessageLog : IMessageLog
        {
            private NullMessageLog()
            {
            }

            public void SendWarning(string message)
            {
            }

            public void SendError(string message)
            {
            }

            public static readonly IMessageLog Instance = new NullMessageLog();
        }
    }
}
