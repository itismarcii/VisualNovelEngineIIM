using System;
using UnityEngine;

namespace TimeSystem
{
    public enum TimeCycle : int
    {
        MIDNIGHT = 1,
        EARLY_MORNING = 2,
        MORNING = 4,
        LATE_MORNING = 8,
        NOON = 16,
        AFTERNOON = 32,
        EARLY_EVENING = 64,
        EVENING = 128,
        LATE_EVENING = 256,
        NIGHT = 512,
        LATE_NIGHT = 1024
    }
}
