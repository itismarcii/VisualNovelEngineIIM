using System;
using LocationSystem;

namespace CharacterSystem
{
    [Serializable]
    public class DayRoutineContainer
    {
        [Serializable]
        public class Container
        {
            public RoomLocation morningRoomLocation;
            public RoomLocation noonRoomLocation;
            public RoomLocation eveningRoomLocation;
            public RoomLocation nightRoomLocation;
            public RoomLocation midnightRoomLocation;
        }

        public Container Monday;
        public Container Tuesday;
        public Container Wednessday;
        public Container Thursday;
        public Container Friday;
        public Container Saturday;
        public Container Sunday;
    }
}
