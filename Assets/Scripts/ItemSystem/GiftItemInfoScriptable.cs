using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "Project/Item/Gift", fileName = "GiftItem")]
    public class GiftItemInfoScriptable : ItemInfoScriptable
    {
        [field: Header("Value")]
        [field: SerializeField] public int LikeValue { get; private set; }
        [field: SerializeField] public int DefaultValue { get; private set; }
        [field: SerializeField] public int DislikeValue { get; private set; }
    }
}
