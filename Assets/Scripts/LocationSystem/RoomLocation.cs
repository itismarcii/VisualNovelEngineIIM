using TimeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LocationSystem
{
    [RequireComponent(typeof(Image))]
    public class RoomLocation : Location
    {
        [field: SerializeField] public RoomLocationInfoScriptable RoomLocationInfo { get; private set; }
        private RoomLocationInfoScriptable.DayCycle _ActiveDayCycle;
        private int _ActiveCycleIndex = 0;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            ID = GetHasNumber(this.name + transform.parent);
            if(RoomLocationInfo != null) RoomLocationInfo.ApplyLocationID(ID);
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
        
        private void Start()
        {
            TimeManager.OnNextTimeCycle += CycleChange;
        }

        private void CycleChange(TimeCycle cycle)
        {
            if (RoomLocationInfo.DayCycles == null || RoomLocationInfo.DayCycles.Length == 0) return;

            var startIndex = _ActiveCycleIndex;
            var cycleLength = RoomLocationInfo.DayCycles.Length;
            var isNewActiveCycle = false;
            
            do
            {
                var dayCycle = RoomLocationInfo.DayCycles[_ActiveCycleIndex];

                if (dayCycle.TimeCycle == cycle)
                {
                    _ActiveDayCycle = dayCycle;
                    isNewActiveCycle = true;
                    break;
                }

                _ActiveCycleIndex = (_ActiveCycleIndex + 1) % cycleLength;

            } while (_ActiveCycleIndex != startIndex);

            if (isNewActiveCycle) SetSprite(_ActiveDayCycle);
        }

        private void SetSprite(RoomLocationInfoScriptable.DayCycle activeDayCycle)
        {
            Image.sprite = activeDayCycle.Image;
        }
    }
}
