using System.Collections.Generic;
using System.Threading;

namespace TTCtestFramework.Utility
{
    /// <summary>
    /// This class is used to track the list of items for a scenario that were stored globally and can be accessed form different step definition classes. 
    /// </summary>

    public class Globals
    {

        private ThreadLocal<Dictionary<string, object>> globalValues = new ThreadLocal<Dictionary<string, object>>();

        /// <summary>
        /// Resets storage.
        /// </summary>
        public void Clear()
        {
            globalValues.Value = null;
        }

        /// <summary>
        /// Adds an objects to the dictionary. If the key already exists, overwrite the value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            Dictionary<string, object> dict = GetDictionary();
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
                return;
            }
            dict.Add(key, value);
        }

        /// <summary>
        /// Adds an object to a list of objects associated with this key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddToList(string key, object value)
        {
            Dictionary<string, object> dict = GetDictionary();

            if (!dict.ContainsKey(key))
                dict.Add(key, new BaseList<object>());

            ((BaseList<object>)dict[key]).Add(value);
        }

        /// <summary>
        /// Removes the object from a list of objects associated with this key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void RemoveFromList(string key, object value)
        {
            Dictionary<string, object> dict = GetDictionary();

            if (dict.ContainsKey(key))
            {
                ((BaseList<object>)dict[key]).Remove(value);
            }
        }

        /// <summary>
        /// Returns the object mathing this key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (GetDictionary().ContainsKey(key))
            {
                return (T)GetDictionary()[key];
            }
            return default(T);
        }

        /// <summary>
        /// Determines whether the dictionary contains the cpecified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return GetDictionary().ContainsKey(key);
        }

        /// <summary>
        /// Internal method used to return a thred-local dictionary and initilize it if not yet initilized.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetDictionary()
        {
            Dictionary<string, object> dictionary = globalValues.Value;
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, object>();
                globalValues.Value = dictionary;
            }

            return dictionary;
        }
    }
}
