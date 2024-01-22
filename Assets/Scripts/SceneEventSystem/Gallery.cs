using System;
using System.Collections.Generic;

namespace SceneEventSystem
{
    [Serializable]
    public class Gallery
    {
        private readonly List<SceneEventInfoScriptable> GalleryScenes = new List<SceneEventInfoScriptable>();
        
        public static Gallery Instance { get; private set; }
        
        public static Action<SceneEventInfoScriptable> OnNewGalleryEvent;
        
        private Gallery()
        {
            Instance = this;
            SceneEventHandler.OnSceneUnlocked += AddSceneEvent;
        }
        
        private Gallery(in Gallery gallery)
        {
            Instance = this;
            SceneEventHandler.OnSceneUnlocked += AddSceneEvent;
            GalleryScenes = gallery.GalleryScenes;
        }

        ~Gallery()
        {
            SceneEventHandler.OnSceneUnlocked -= AddSceneEvent;
            Instance = null;
        }

        public static Gallery TryCreateInstance() => Instance ??= new Gallery();
        public static Gallery LoadGallery(in Gallery gallery) => new (gallery);

        private void AddSceneEvent(SceneEventInfoScriptable sceneEvent)
        {
            if(!sceneEvent.InGallery) return;
            GalleryScenes.Add(sceneEvent);
            OnNewGalleryEvent?.Invoke(sceneEvent);
        }

        public static SceneEventInfoScriptable[] GetGalleryScenes() => Instance.GalleryScenes.ToArray();
    }
}
