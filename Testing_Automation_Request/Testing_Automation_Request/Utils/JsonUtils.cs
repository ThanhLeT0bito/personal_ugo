using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CloudBanking.Utilities
{
    public static class JsonUtil
    {
        public static string SerializeObject(this object data)
        {
#if !DEBUG
            var setting = new JsonSerializerSettings()
            {
                ContractResolver = new CustomContractResolver()
            };

            return JsonConvert.SerializeObject(data, setting);
#else
            return JsonConvert.SerializeObject(data);
#endif
        }

        public static T DeserializeObject<T>(this string resource) where T : class
        {

#if !DEBUG
            var setting = new JsonSerializerSettings()
            {
                ContractResolver = new CustomContractResolver()
            };

            return JsonConvert.DeserializeObject<T>(resource, setting);
#else
            return JsonConvert.DeserializeObject<T>(resource);
#endif
        }

        public static T DeserializeStruct<T>(this string resource) where T : struct
        {

#if !DEBUG
            var setting = new JsonSerializerSettings()
            {
                ContractResolver = new CustomContractResolver()
            };

            return JsonConvert.DeserializeObject<T>(resource, setting);
#else
            return JsonConvert.DeserializeObject<T>(resource);
#endif
        }
    }

    public class CustomContractResolver : DefaultContractResolver
    {
        int _index;

        public CustomContractResolver()
        {
            _index = 0;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.PropertyName = string.Format("{0}", _index);

            _index++;

            return property;
        }
    }
}
