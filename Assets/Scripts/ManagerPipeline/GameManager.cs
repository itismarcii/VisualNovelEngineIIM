using CharacterSystem;
using DialogueSystem;
using QuestSystem;
using SceneEventSystem;
using UnityEngine;

namespace ManagerPipeline
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private SystemFlags _System_BIT = 0;
        
        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            Generate();
            LoadGallery();
            DialogueManager.GenerateCharacterTags();
        }

        private void Generate()
        {
            CharacterHandler.GenerateCharacters();
            SceneEventHandler.GenerateSceneEvents();
            QuestHandler.GenerateQuests();
            
        }

        private void Load()
        {
            
        }

        private void Save()
        {
            
        }

        private void LoadGallery()
        {
            
        }
    }
}
