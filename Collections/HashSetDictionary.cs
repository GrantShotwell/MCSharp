using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MCSharp.Collections {

	public class HashSetDictionary<TKey, TValue> : IHashSetDictionary<TKey, TValue> {

		/// <inheritdoc/>
		private IDictionary<TKey, HashSet<TValue>> Dictionary { get; }

		/// <inheritdoc/>
		public ICollection<TKey> Keys => Dictionary.Keys;

		/// <inheritdoc/>
		public ICollection<HashSet<TValue>> Values => Dictionary.Values;

		/// <inheritdoc/>
		public int Count => Dictionary.Count;

		/// <inheritdoc/>
		public bool IsReadOnly => Dictionary.IsReadOnly;

		/// <inheritdoc/>
		public HashSet<TValue> this[TKey key] { get => Dictionary[key]; set => Dictionary[key] = value; }


		private HashSetDictionary(IDictionary<TKey, HashSet<TValue>> dictionary) {
			Dictionary = dictionary;
		}

		public HashSetDictionary() {
			Dictionary = new Dictionary<TKey, HashSet<TValue>>();
		}


		/// <inheritdoc/>
		public void Add(TKey key, HashSet<TValue> value) => Dictionary.Add(key, value);

		/// <inheritdoc/>
		public void Add(KeyValuePair<TKey, HashSet<TValue>> item) => Dictionary.Add(item);

		/// <inheritdoc/>
		public void Add(TKey key, TValue item) {

			IDictionary<TKey, HashSet<TValue>> dictionary = Dictionary;
			HashSet<TValue> hashSet;

			if(dictionary.ContainsKey(key)) {
				hashSet = dictionary[key];
				hashSet.Add(item);
			} else {
				hashSet = new HashSet<TValue> { item };
				dictionary.Add(key, hashSet);
			}

		}

		/// <inheritdoc/>
		public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

		/// <inheritdoc/>
		public bool Remove(TKey key) => Dictionary.Remove(key);

		/// <inheritdoc/>
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out HashSet<TValue> value) => Dictionary.TryGetValue(key, out value);

		/// <inheritdoc/>
		public void Clear() => Dictionary.Clear();

		/// <inheritdoc/>
		public bool Contains(KeyValuePair<TKey, HashSet<TValue>> item) => Dictionary.Contains(item);

		/// <inheritdoc/>
		public void CopyTo(KeyValuePair<TKey, HashSet<TValue>>[] array, int arrayIndex) => Dictionary.CopyTo(array, arrayIndex);

		/// <inheritdoc/>
		public bool Remove(KeyValuePair<TKey, HashSet<TValue>> item) => Dictionary.Remove(item);

		/// <inheritdoc/>
		public IEnumerator<KeyValuePair<TKey, HashSet<TValue>>> GetEnumerator() => Dictionary.GetEnumerator();

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Dictionary).GetEnumerator();

	}

}
