using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurance.Api.Helpers
{
    public static class Extension
    {
        private static T MergeObjects<T>(IEnumerable<T> list) where T : new()
        {
            var resultObject = new T();
            var properties = typeof(T).GetProperties();

            foreach (var item in list)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(item);
                    var resultObjectPropertyValue = property.GetValue(resultObject);

                    if (value != DefaultValue(property.GetType()))
                    {
                        if (property.PropertyType.Name == "IEnumerable`1" && resultObjectPropertyValue != null)
                        {
                            var unionMethod = typeof(System.Linq.Enumerable).GetMethods()
                                                                       .Single(m => m.Name == "Union" &&
                                                                                    m.GetGenericArguments().Length == 1 &&
                                                                                    m.GetParameters().Length == 2)
                                                                       .MakeGenericMethod(property.PropertyType
                                                                                                  .GetGenericArguments()
                                                                                                  .First());

                            var union = unionMethod.Invoke(null, new object[] { resultObjectPropertyValue, value });

                            var toListMethod = typeof(System.Linq.Enumerable).GetMethod("ToList")
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