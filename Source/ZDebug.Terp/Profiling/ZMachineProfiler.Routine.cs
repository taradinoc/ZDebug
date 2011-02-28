﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ZDebug.Terp.Profiling
{
    public partial class ZMachineProfiler
    {
        private sealed class Routine : IRoutine
        {
            private readonly ZMachineProfiler profiler;
            private readonly int address;

            private List<int> callIndexes;
            private ReadOnlyCollection<ICall> calls;
            private TimeSpan inclusiveTime;
            private TimeSpan exclusiveTime;

            public Routine(ZMachineProfiler profiler, int address)
            {
                this.profiler = profiler;
                this.address = address;
                this.callIndexes = new List<int>();
            }

            public void AddCall(int index)
            {
                callIndexes.Add(index);
            }

            public void Done()
            {
                var callList = callIndexes.ConvertAll(i => (ICall)profiler.GetCallByIndex(i));
                callList.TrimExcess();
                calls = new ReadOnlyCollection<ICall>(callList);
                callIndexes = null;

                inclusiveTime = calls.Aggregate(TimeSpan.Zero, (r, c) => r + c.InclusiveTime);
                exclusiveTime = calls.Aggregate(TimeSpan.Zero, (r, c) => r + c.ExclusiveTime);
            }

            public int Address
            {
                get
                {
                    return address;
                }
            }

            public ReadOnlyCollection<ICall> Calls
            {
                get
                {
                    return calls;
                }
            }

            public TimeSpan InclusiveTime
            {
                get
                {
                    return inclusiveTime;
                }
            }

            public TimeSpan ExclusiveTime
            {
                get
                {
                    return exclusiveTime;
                }
            }

            public double InclusivePercentage
            {
                get
                {
                    return ((double)inclusiveTime.Ticks / (double)profiler.RunningTime.Ticks) * 100;
                }
            }

            public double ExclusivePercentage
            {
                get
                {
                    return ((double)exclusiveTime.Ticks / (double)profiler.RunningTime.Ticks) * 100;
                }
            }
        }
    }
}
