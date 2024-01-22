using System;

namespace CharacterSystem
{
    [Flags]
    public enum CharacterHobby
    {
        READING = 1,
        WRITING = 2,
        DRAWING = 4,
        MUSIC = 8,
        SPORTS = 16,
        GAMING = 32,
        COOKING = 64,
        CODING = 128,
        THEATER = 256,
        PHOTOGRAPHY = 512,
        SCIENCE = 1024,
        GARDENING = 2048,
        ANIMAL_CARE = 4096,
        SHOPPING = 8192,
        DANCING = 16384,
        HANGING_OUT = 32768
    }
}
