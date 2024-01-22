using System;
using System.Linq;

namespace TimeSystem
{
    public static class TimeManager
    {
        public static Day Day { get; private set; } = Day.SATURDAY;
        public static TimeCycle Cycle { get; private set; } = TimeCycle.MIDNIGHT;

        public static int WeekCount { get; private set; } = 0;
        
        public static Action<TimeCycle> OnNextTimeCycle;
        public static Action<Day> OnNexDay;
        public static Action<int> OnNewWeek;
        
        private static readonly int TimeCycleMaxValue = Enum.GetValues(typeof(TimeCycle)).Cast<TimeCycle>().Select(d => Convert.ToInt32(d)).Max();
        private static readonly int DayMaxValue = Enum.GetValues(typeof(Day)).Cast<Day>().Select(d => Convert.ToInt32(d)).Max();
        
        internal static void UpdateTimeCycle()
        {
            Cycle = (TimeCycle)(((int)Cycle << 1) % (TimeCycleMaxValue << 1));
            Cycle = Cycle == 0 ? TimeCycle.MIDNIGHT : Cycle;
            OnNextTimeCycle?.Invoke(Cycle);
            if (Cycle == TimeCycle.MIDNIGHT) NewDay();
        }

        internal static void NewDay()
        {
            Cycle = TimeCycle.MIDNIGHT;
            Day = (Day)(((int)Day << 1) % (DayMaxValue << 1));
            Day = Day == 0 ? Day.MONDAY : Day;
            OnNexDay?.Invoke(Day);
            
            if (Day == Day.MONDAY)
            {
                WeekCount++;
                OnNewWeek?.Invoke(WeekCount);
            }
        }

        internal static void NewMorning()
        {
            Cycle = (TimeCycle) 1;
            OnNextTimeCycle?.Invoke(Cycle);
            NewDay();
        }
    }
}
