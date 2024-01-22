using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaveSystem
{
   public static class SavableSerializer
   {
      private const string FIELD_NAME_ID = "<ID>k__BackingField";
      private const string IDENTIFIER = "ID";
      
      /// <summary>
      /// Serializes objects with the attribute SavableAttribute and their fields with the attribute SavableFieldAttribute which are located in the SavableObjects dictionary in SavableAttribute.
      /// </summary>
      private static object Serialize()
      {
         var bundledData = new Dictionary<Type, Dictionary<int, Dictionary<string, object>>>();

         foreach (var (key, value) in SavableAttribute.SavableObjects)
         {
            var typeDataDictionary = new Dictionary<int, Dictionary<string, object>>();

            foreach (var (identifier, saveDictionary) in value)
            {
               var typeData = SerializeDictionary(saveDictionary, key);
               typeDataDictionary.Add(identifier, typeData);
            }

            bundledData.Add(key, typeDataDictionary);
         }

         return bundledData;
      }

      public static void SerializeToBinary(in string filePath)
      {
         using var stream = new FileStream(filePath, FileMode.Create);
         var formatter = new BinaryFormatter();
         formatter.Serialize(stream, Serialize());
      }
      
      /// <summary>
      /// Serializes dictionary of an specific object type with the attribute SavableAttribute.
      /// </summary>
      /// <param name="saveDictionary">Deserialize object dictionary.</param>
      /// <param name="objectType">Type of the object.</param>
      /// <param name="objectType">Type of the object.</param>
      private static Dictionary<string, object> SerializeDictionary(object saveDictionary, Type objectType)
      {
         if (!objectType.IsDefined(typeof(SavableAttribute), false))
         {
            throw new InvalidOperationException($"The type '{objectType.FullName}' does not have the required SavableAttribute attached.");
         }

         var savableFields = GetSavableFields(objectType);

         // Create a dictionary to store the field names and their values
         var fieldValues = savableFields.ToDictionary(field => field.Name, field => field.GetValue(saveDictionary));

         return fieldValues;
      }
      
      /// <summary>
      /// Deserializes objects with the attribute SavableAttribute and their fields with the attribute SavableFieldAttribute which are located in the SavableObjects dictionary in SavableAttribute.
      /// </summary>
      /// <param name="stream">Deserialize file stream.</param>
      public static void Deserialize(Stream stream)
      {
         var formatter = new BinaryFormatter();
         var bundledData = (Dictionary<Type, Dictionary<int, Dictionary<string, object>>>)formatter.Deserialize(stream);
         
         foreach (var (key, value) in bundledData)
         {
            foreach (var data in value)
            {
               DeserializeDictionary(data.Value, key);
            }
         }
      }
      
      /// <summary>
      /// Deserializes dictionary of an specific object type with the attribute SavableAttribute.
      /// </summary>
      /// <param name="saveDictionary">Deserialize object dictionary.</param>
      /// <param name="objectType">Type of the object.</param>
      private static void DeserializeDictionary(Dictionary<string, object> saveDictionary, Type objectType)
      {
         if (!objectType.IsDefined(typeof(SavableAttribute), false))
         {
            throw new InvalidOperationException($"The type '{objectType.FullName}' does not have the required SavableAttribute attached.");
         }
         
         var identifierProperty = objectType.GetProperty(IDENTIFIER);
         
         if (identifierProperty == null)
         {
            throw new InvalidOperationException($"The type '{objectType.FullName}' does not have a property named '{IDENTIFIER}' as an identifier.");
         }
         
         if (!saveDictionary.TryGetValue(FIELD_NAME_ID, out var identifierValue)) return;
         
         if (!SavableAttribute.SavableObjects.TryGetValue(objectType, out var objectList) ||
             !objectList.TryGetValue((int) identifierValue, out var saveObject)) return;
         
         var savableFields = GetSavableFields(objectType);
         foreach (var field in savableFields)
         {
            var fieldName = field.Name;

            if(fieldName == IDENTIFIER) continue;
                  
            if (saveDictionary.TryGetValue(fieldName, out var fieldValue))
            {
               field.SetValue(saveObject, fieldValue);
            }
         }
      }
      
      /// <summary>
      /// Get all fields with the tag SavableAttribute of an type with the binding flags [Instance, Public, NonPublic]
      /// </summary>
      /// <param name="type">Type of object.</param>
      private static IEnumerable<FieldInfo> GetSavableFields(IReflect type)
      {
         return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => field.IsDefined(typeof(SavableFieldAttribute), false));
      }
   }
}
