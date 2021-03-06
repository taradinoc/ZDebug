﻿using System;
using System.Collections.ObjectModel;

namespace ZDebug.Terp.Profiling
{
    public interface IRoutine
    {
        int Address { get; }

        ReadOnlyCollection<ICall> Calls { get; }

        TimeSpan InclusiveTime { get; }
        TimeSpan ExclusiveTime { get; }

        double InclusivePercentage { get; }
        double ExclusivePercentage { get; }

        int LocalCount { get; }

        int ZCodeInstructionCount { get; }
        int ILInstructionCount { get; }
        int ILByteSize { get; }
    }
}
