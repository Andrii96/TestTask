using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurance.Api.Helpers
{
    public static class Extension
    {
        public static T MergeObjectsDynamicly<T>(this IEnumerable<T> list) where T : new()
        {
            var resultObject = new T();
            var properties = typeof(T).GetProperties();

            foreach (var item in list)
            {
                if (item == null) continue;

                foreach (var property in properties)
                {
                    var value = property.GetValue(item);
                    var resultObjectPropertyValue = property.GetValue(resultObject);

                    if (value != null && !value.Equals(DefaultValue(property.PropertyType)))
                    {
                        if (property.PropertyType.DoesTypeSupportInterface(typeof(IEnumerable)) && resultObjectPropertyValue != null)
                        {
                            var unionMethod = typeof(Enumerable).GetMethods()
                                                                .Single(m => m.Name == "Union" &&
                                                                            m.GetGenericArguments().Length == 1 &&
                                                                            m.GetParameters().Length == 2)
                                                                .MakeGenericMethod(property.PropertyType
                                                                                            .GetGenericArguments()
                                                                                            .First());

                            var union = unionMethod.Invoke(null, new object[] { resultObjectPropertyValue, value });

                            var toListMethod = typeof(Enumerable).GetMethod("ToList")
                                                                             .MakeGenericMethod(property.PropertyType
                                                                                                        .GetGenericArguments()
                                                                                                        .First());
                            value = toListMethod.Invoke(null, new object[] { union });

                        }

                        property.SetValue(resultObject, value);
                    }
                }
            }
            return resultObject;
        }

        public static bool DoesTypeSupportInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
        }

        private static object DefaultValue(Type T)
        {
            if (T.IsValueType)
            {
                var instance = Activator.CreateInstance(T);
                return instance;
            }
            return null;
        }
    }
}