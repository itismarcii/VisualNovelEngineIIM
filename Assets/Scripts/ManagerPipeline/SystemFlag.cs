using System;

[Flags]
public enum SystemFlags : byte
{
    Character = 1,
    Dialogue = 2,
    Location = 4,
    Quest = 8,
    Save = 16,
    SceneEvent = 32,
    Time = 64
}