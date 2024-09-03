using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ePizzaHub.UI.Helper
{
    public static class TempDataExtension
    {
        public static void  Set<T>(this ITempDataDictionary tempData,string Key, T value) where T : class
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            tempData[Key]= JsonSerializer.Serialize(value,options);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string Key) where T : class
        {
            tempData.TryGetValue(Key, out object value);
            return value == null ? null : JsonSerializer.Deserialize<T>((string)value);
        }

        public static T Peek<T>(this  ITempDataDictionary tempData, string Key) where T: class
        {
            object o= tempData.Peek(Key);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }
    }
}
