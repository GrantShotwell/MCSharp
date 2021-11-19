using System.Collections.Generic;

namespace MCSharp.Collections;

/// <summary>
/// Represents a dictionary with multiple values per key.
/// </summary>
/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
public interface IHashSetDictionary<TKey, TValue> : IDictionary<TKey, HashSet<TValue>> where TKey : notnull {

	public void Add(TKey key, TValue item);

}
