using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MCSharp.Linkage.Minecraft.Text {

	public class RawTextList : IList<RawText> {

		private IList<RawText> List { get; }
		public RawText this[int index] { get => List[index]; set => List[index] = value; }
		public int Count => List.Count;
		public bool IsReadOnly => List.IsReadOnly;


		public RawTextList() {
			List = new List<RawText>();
		}
		public RawTextList(int capacity) {
			List = new List<RawText>(capacity);
		}
		public RawTextList(IEnumerable<RawText> collection) {
			List = new List<RawText>(collection);
		}


		public static RawTextList FromJson(string json) {
			RawText[] array = JsonSerializer.Deserialize<RawText[]>(json, RawText.DeserializerOptions);
			RawTextList list = new RawTextList(array);
			return list;
		}
		public string GetJson() {
			RawText[] array = List.ToArray();
			string json = JsonSerializer.Serialize(array, RawText.SerializerOptions);
			return json;
		}

		public void Add(RawText item) => List.Add(item);
		public void Clear() => List.Clear();
		public bool Contains(RawText item) => List.Contains(item);
		public void CopyTo(RawText[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
		public IEnumerator<RawText> GetEnumerator() => List.GetEnumerator();
		public int IndexOf(RawText item) => List.IndexOf(item);
		public void Insert(int index, RawText item) => List.Insert(index, item);
		public bool Remove(RawText item) => List.Remove(item);
		public void RemoveAt(int index) => List.RemoveAt(index);
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)List).GetEnumerator();

	}

}
