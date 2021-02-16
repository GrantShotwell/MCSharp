using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Script {

	public class TokenList : IEnumerable<ScriptToken> {

		private Node Head { get; set; }
		private Node Tail {
			get {
				if(Head == null) return null;
				else {
					Node current = Head;
					while(current.Next != null) {
						current = current.Next;
						if(current == Head) throw new CircularListException(this);
					}
					return current;
				}
			}
		}
		public int Count {
			get {
				if(Head == null) return 0;
				int count = 1;
				Node current = Head;
				while(current.Next != null) {
					count++;
					current = current.Next;
					if(current == Head) throw new CircularListException(this);
				}
				return count;
			}
		}

		public TokenList() { }

		public void AddLast(ScriptToken token) {
			if(Head == null) Head = new Node(token);
			else Tail.Next = new Node(token);
		}

		public void AddFirst(ScriptToken token) {
			if(Head == null) Head = new Node(token);
			else Head = new Node(token, Head);
		}

		public void AddLast(TokenList list) {
			if(Head == null) Head = list.Head;
			else Tail.Next = list.Head;
		}

		public ScriptToken[] ToArray() {
			ScriptToken[] array = new ScriptToken[Count];
			if(array.Length == 0) return array;
			Node current = Head;
			for(int i = 0; i < array.Length; i++) {
				array[i] = current.Value;
				current = current.Next;
				if(current == Head) throw new CircularListException(this);
			}
			return array;
		}

		public override string ToString() {
			string[] array = new string[Count];
			int i = 0;
			foreach(ScriptToken token in this) array[i++] = (string)token;
			return string.Join(' ', array);
		}

		public IEnumerator<ScriptToken> GetEnumerator() => new Enumerator(this);
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private class Node {
			public ScriptToken Value { get; }
			public Node Next { get; set; }
			public Node(ScriptToken value) {
				Value = value;
				Next = null;
			}
			public Node(ScriptToken value, Node next) {
				Value = value;
				Next = next;
			}
		}

		private class Enumerator : IEnumerator<ScriptToken> {

			TokenList List { get; }
			public Node Current { get; private set; }
			ScriptToken IEnumerator<ScriptToken>.Current => Current.Value;
			object IEnumerator.Current => Current.Value;

			public Enumerator(TokenList list) {
				List = list;
				Reset();
			}

			public bool MoveNext() {

				if(Current == null) {

					if(List.Head == null) return false;
					else Current = List.Head;
					return true;

				} else {

					if(Current.Next == null) return false;
					else Current = Current.Next;
					return true;

				}

			}
			public void Reset() => Current = null;

			public void Dispose() { /* Nothing to dispose of. */ }

		}

		public class CircularListException : Exception {
			public CircularListException(TokenList list) : base($"{nameof(TokenList)} was detected to be circular.") { }
		}

	}

}
