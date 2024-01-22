using System;

namespace LocationSystem
{
    [Serializable]
    public struct LocationContainer
    {
        public CharacterLocationInfoScriptable Midnight;
        public CharacterLocationInfoScriptable EarlyMorning;
        public CharacterLocationInfoScriptable Morning;
        public CharacterLocationInfoScriptable LateMorning;
        public CharacterLocationInfoScriptable Noon;
        public CharacterLocationInfoScriptable Afternoon;
        public CharacterLocationInfoScriptable EarlyEvening;
        public CharacterLocationInfoScriptable Evening;
        public CharacterLocationInfoScriptable LateEvening;
        public CharacterLocationInfoScriptable Night;
        public CharacterLocationInfoScriptable LateNight;
    }
}
