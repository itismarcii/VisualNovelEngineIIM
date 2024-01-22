using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneEventSystem
{
    public static class SceneEventHandler
    {
        public static readonly Dictionary<int, SceneEvent> SceneEvents = new Dictionary<int, SceneEvent>();
        public static Action<SceneEventInfoScriptable> OnSceneUnlocked;
        
        public static void GenerateSceneEvents()
        {
            SceneEvents.Clear();
            Gallery.TryCreateInstance();
            
            var sceneEvents = Resources.LoadAll<SceneEventInfoScriptable>("Scenes/SceneEvents");

            foreach (var sceneEvent in sceneEvents)
            {
                SceneEvents.Add(sceneEvent.ID,  new SceneEvent(sceneEvent));
            }
        }
        
        public static void GenerateSceneEvents(in SceneEvent[] sceneEvents)
        {
            SceneEvents.Clear();
            Gallery.TryCreateInstance();
            
            foreach (var sceneEvent in sceneEvents)
            {
                SceneEvents.Add(sceneEvent.SceneEventInfo.ID, sceneEvent);
            }
        }
        
        public static void UnlockScene(in SceneEventInfoScriptable sceneEvent)
        {
            SceneEvents[sceneEvent.ID].Unlock();
            OnSceneUnlocked?.Invoke(sceneEvent);
        }
        
        public static void UnlockScene(in SceneEventInfoScriptable[] sceneEvents)
        {
            foreach (var sceneEvent in sceneEvents)
            {
                SceneEvents[sceneEvent.ID].Unlock();
                OnSceneUnlocked?.Invoke(sceneEvent);
            }
        }
    }
}
