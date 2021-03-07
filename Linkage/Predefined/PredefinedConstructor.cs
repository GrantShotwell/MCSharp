using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	/// <summary>
	/// Represents a predefined constructor.
	/// </summary>
	public class PredefinedConstructor : IConstructor {

		/// <summary>
		/// The predefined type that has defined this constructor.
		/// </summary>
		public PredefinedType Declarer { get; set; }
		/// <inheritdoc/>
		IType IConstructor.Declarer => Declarer;

		/// <inheritdoc/>
		public Modifier Modifiers { get; }

		/// <inheritdoc/>
		public IFunction Invoker { get; }

		/// <summary>
		/// Creates a new predefined constructor.
		/// </summary>
		/// <param name="declarer">The predefined type that has defined this constructor.</param>
		/// <param name="modifiers">The modifiers that affect this constructor.</param>
		/// <param name="invoker">The mcfunction file that will contain the final commands to execute this constructor.</param>
		public PredefinedConstructor(PredefinedType declarer, Modifier modifiers, StandaloneStatementFunction invoker) {
			Declarer = declarer ?? throw new ArgumentNullException(nameof(declarer));
			Modifiers = modifiers;
			Invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
		}

		public void Dispose() {
			Invoker.Dispose();
		}

	}

}
