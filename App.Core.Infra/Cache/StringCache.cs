using KGySoft.Collections;
using KGySoft.CoreLibraries;
using System;

namespace App.Core.Infra.Cache
{
    public class StringCache
    {
        private readonly StringKeyedDictionary<string> dicy;

        public StringCache()
        {
            dicy = new StringKeyedDictionary<string>();
        }

        public StringCache(StringSegmentComparer comparer)
        {
            dicy = new StringKeyedDictionary<string>(comparer);
        }

        public void SetValue(ReadOnlySpan<char> value)
        {
            if (!dicy.ContainsKey(value))
            {
                string val = new string(value);
                dicy.Add(val, val);
            }
        }

        public string GetValue(ReadOnlySpan<char> key)
        {
            return !dicy.TryGetValue(key, out string value) ? value : null;
        }
    }
}
