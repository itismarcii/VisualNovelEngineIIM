using System.Collections.Generic;
using LocationSystem;
using UnityEngine;

namespace CharacterSystem
{
    public static class CharacterHandler
    {
        public static readonly Dictionary<int, Character> Characters = new Dictionary<int, Character>();
        public static readonly Dictionary<int, CharacterInfoScriptable> CharacterInfos = new Dictionary<int, CharacterInfoScriptable>();

        public static void GenerateCharacters()
        {
            Characters.Clear();

            var characterInfos = Resources.LoadAll<CharacterInfoScriptable>("Character");

            foreach (var character in characterInfos)
            {
                Characters.Add(character.ID, new Character(character));
            }
        }

        public static void GenerateCharacters(in Character[] characters)
        {
            Characters.Clear();

            foreach (var character in characters)
            {
                Characters.Add(character.CharacterInfo.ID, character);
            }
        }

        // public static void LoadCharacter(in CharacterSave[] saves)
        // {
        //     
        //     Characters.Clear();
        //     
        //     if (CharacterInfos.Count <= 0)
        //     {
        //         var characterInfos = Resources.LoadAll<CharacterInfoScriptable>("Character");
        //         foreach (var character in characterInfos)
        //         {
        //             CharacterInfos.Add(character.ID, character);
        //         }
        //     }
        //
        //     foreach (var save in saves)
        //     {
        //         Characters.Add(save.ID, new Character(save, CharacterInfos[save.ID]));
        //     }
        //
        //     foreach (var characterInfo in CharacterInfos)
        //     {
        //         if(Characters.ContainsKey(characterInfo.Key)) continue;
        //         Characters.Add(characterInfo.Key, new Character(characterInfo.Value));
        //     }
        // }

        public static void ApplyLocationOverride(in LocationOverrideContainer locationOverride)
        {
            Characters[locationOverride.Character.ID].LocationWeekSchedule.ApplyNewSchedule(locationOverride.Location);
        }
        
        public static void ApplyLocationOverride(in LocationOverrideContainer[] locationOverrides)
        {
            foreach (var locationOverride in locationOverrides)
            {
                Characters[locationOverride.Character.ID].LocationWeekSchedule.ApplyNewSchedule(locationOverride.Location);
            }
        }
    }
}
