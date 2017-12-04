using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurance.Api.Helpers
{
    public static class Extension
    {
        public static T MergeObjects<T>(this IEnumerable<T> list) where T : new()
        {
            var resultObject = new T();

            if (typeof(T).IsValueType)
            {
                return resultObject;
            }

            var properties = typeof(T).GetProperties();

            foreach (var item in list)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(item);
                    if (value != DefaultValue(property.GetType()))
                    {
                        //if (property.GetType().GetInterfaces().Contains(typeof(IEnumerable<>)))
                        //{
                        //    value
                        //}
                        property.SetValue(resultObject, value);
                    }
                }
            }

            return resultObject;
        }

        private static object DefaultValue(Type T)
        {
            if (T.IsValueType)
            {
                return Activator.CreateInstance(T);
            }
            return null;
        }

    }
}