using System;
using TimeSystem;
using UnityEngine;

namespace LocationSystem
{
    [Serializable]
    public struct LocationWeekIndexContainer
    {
        [Serializable]
        public struct TimeCycleContainer
        {
            public int MidnightID;
            public int EarlyMorningID;
            public int MorningID;
            public int LateMorningID;
            public int NoonID;
            public int AfternoonID;
            public int EarlyEveningID;
            public int EveningID;
            public int LateEveningID;
            public int NightID;
            public int LateNightID;

            public TimeCycleContainer(in LocationContainer container)
            {
                MidnightID = container.Midnight == null ? -1: container.Midnight.ID;
                EarlyMorningID =  container.EarlyMorning == null ? -1: container.EarlyMorning.ID;
                MorningID = container.Morning == null ? -1: container.Morning.ID;
                LateMorningID = container.LateMorning == null ? -1: container.LateMorning.ID;
                NoonID = container.Noon == null ? -1: container.Noon.ID;
                AfternoonID = container.Afternoon == null ? -1: container.Afternoon.ID;
                EarlyEveningID = container.EarlyEvening == null ? -1: container.EarlyEvening.ID;
                EveningID = container.Evening == null ? -1: container.Evening.ID;
                LateEveningID = container.LateEvening == null ? -1: container.LateEvening.ID;
                NightID = container.Night == null ? -1: container.Night.ID;
                LateNightID = container.LateNight == null ? -1: container.LateNight.ID;
            }
        }
        
        public TimeCycleContainer Monday;
        public TimeCycleContainer Tuesday;
        public TimeCycleContainer Wednesday;
        public TimeCycleContainer Thursday;
        public TimeCycleContainer Friday;
        public TimeCycleContainer Saturday;
        public TimeCycleContainer Sunday;

        public LocationWeekIndexContainer(in LocationWeekObjectContainer objectContainer)
        {
            Monday = new TimeCycleContainer(objectContainer.Monday);
            Tuesday = new TimeCycleContainer(objectContainer.Tuesday);
            Wednesday = new TimeCycleContainer(objectContainer.Wednesday);
            Thursday = new TimeCycleContainer(objectContainer.Thursday);
            Friday = new TimeCycleContainer(objectContainer.Friday);
            Saturday = new TimeCycleContainer(objectContainer.Saturday);
            Sunday = new TimeCycleContainer(objectContainer.Sunday);
        }

        public void ApplyNewSchedule(in LocationWeekObjectContainer container)
        {
            
        }
    }
}