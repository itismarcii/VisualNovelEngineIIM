using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class SavableFieldAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class SavableAttribute : Attribute
    {
        public static readonly Dictionary<Type, Dictionary<int, object>> SavableObjects = new();

        public static void RegisterObject(in int identifier, object obj)
        {
            if (SavableObjects.TryGetValue(obj.GetType(), out var dictionary))
            {
                dictionary.TryAdd(identifier, obj);
            }
            else
            {
                SavableObjects.Add(obj.GetType(), new Dictionary<int, object>() { {identifier, obj} });
            }
        }

        public static void RemoveFromRegister(int identifier, object obj)
        {
            if (SavableObjects.TryGetValue(obj.GetType(), out var list))
            {
                list.Remove(identifier);
            }
        }
    }
}
