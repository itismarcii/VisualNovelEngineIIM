using System.Runtime.InteropServices;
using CharacterSystem;
using SaveSystem;
using UnityEditor;
using UnityEngine;

public class SaveTestScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // SaveHandler.SelectSaveFolder();
        //
        // SaveMethod();
    }

    private void SaveMethod() {
        
        CharacterHandler.GenerateCharacters();
        
        foreach (var character in CharacterHandler.Characters)
        {
        }
        
        SaveHandler.Save("Save0");
        
        
        foreach (var character in CharacterHandler.Characters)
        {
        }
        
        SaveHandler.Load("Save0");
        
        foreach (var savable in SavableAttribute.SavableObjects)       
        {
            foreach (var data in savable.Value)
            {
                if (data.Value is Character character)
                {
                }
            }
        }
    }
}
