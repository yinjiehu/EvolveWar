using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Network
{
    public static class Validator
    {
        public static bool ValidateObject(object o, List<string> results)
        {
            if (o == null)
                return true;
            Type type = o.GetType();
            if(type.IsPrimitive || type.IsEnum)
            {
                //do nothing
            }
            else if (type.GetInterface(typeof(IEnumerable).FullName) != null)
            {
                var e = ((IEnumerable)o).GetEnumerator();
                while (e.MoveNext())
                {
                    if (e is IDictionaryEnumerator)
                        ValidateObject(((IDictionaryEnumerator)e).Value, results);
                    else
                        ValidateObject(e.Current, results);
                }
            }
            else
            {
                var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var propertyInfo in props)
                {

                    var attrs = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);
                    foreach (ValidationAttribute validation in attrs)
                    {
                        object obj = propertyInfo.GetValue(o, null);
                        if (!validation.IsValid(obj))
                        { 
                            results.Add(validation.ErrorMessage);
                        }

                        ValidateObject(obj, results);
                    }

                }
            }
            return true;
        }

        public static bool ValidateObject(object o)
        {
            List<string> results = new List<string>();
            ValidateObject(o, results);

			if(results.Count != 0)
			{
				string outlog = "Validator check error!";
				foreach (string content in results)
				{
					outlog += "\n\t" + content;
				}
				UnityEngine.Debug.LogError(outlog);
			}

            return results.Count == 0;
        }
    }
}
