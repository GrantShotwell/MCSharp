using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

	/// <summary>
	/// Represents the information about the success/failure of some operation.
	/// </summary>
	public struct ResultInfo {

		/// <summary>
		/// A successful <see cref="ResultInfo"/> with some generic success message.
		/// </summary>
		public static ResultInfo DefaultSuccess { get; } = new ResultInfo(true, "The operation was successful.");

		/// <summary>
		/// Whether or not the operation was successful.
		/// </summary>
		public bool Success { get; }

		/// <summary>
		/// Whether or not the operation was a failure.
		/// </summary>
		public bool Failure => !Success;

		/// <summary>
		/// The message attached to the result of the operation.
		/// </summary>
		public string Message { get; }


		/// <summary>
		/// Creates a new <see cref="ResultInfo"/>.
		/// </summary>
		/// <param name="success">Whether or not the operation was successful.</param>
		/// <param name="message">The message to attach to the result of the operation.</param>
		public ResultInfo(bool success, string message) {
			Success = success;
			Message = message ?? throw new ArgumentNullException(nameof(message));
		}


		/// <summary>
		/// Compares the <see cref="Success"/> and <see cref="Message"/> values of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">One of the <see cref="ResultInfo"/>s to compare.</param>
		/// <param name="right">One of the <see cref="ResultInfo"/>s to compare.</param>
		/// <returns>Returns <see langword="true"/> if both of the compared values are equivalent.</returns>
		public static bool operator ==(ResultInfo left, ResultInfo right) => (left.Success == right.Success) && (left.Message == right.Message);

		/// <summary>
		/// Compares the <see cref="Success"/> and <see cref="Message"/> values of <paramref name="left"/> and <paramref name="right"/>.
		/// </summary>
		/// <param name="left">One of the <see cref="ResultInfo"/>s to compare.</param>
		/// <param name="right">One of the <see cref="ResultInfo"/>s values to compare.</param>
		/// <returns>Returns <see langword="true"/> if either of the compared values are not equivalent.</returns>
		public static bool operator !=(ResultInfo left, ResultInfo right) => (left.Success != right.Success) || (left.Message != right.Message);

		/// <summary>
		/// Creates a new <see cref="ResultInfo"/> by appending <paramref name="right"/> to the end of <paramref name="left"/>'s <see cref="Message"/>.
		/// </summary>
		/// <param name="left">The original <see cref="ResultInfo"/>.</param>
		/// <param name="right">The <see cref="string"/> to append.</param>
		/// <returns>Returns the <see cref="ResultInfo"/> with <paramref name="left"/>'s <see cref="Success"/> value.</returns>
		public static ResultInfo operator +(ResultInfo left, string right) => new ResultInfo(left.Success, left.Message + right);

		/// <summary>
		/// Creates a new <see cref="ResultInfo"/> by appending <paramref name="left"/> to the front of <paramref name="right"/>'s <see cref="Message"/>.
		/// </summary>
		/// <param name="left">The <see cref="string"/> to append.</param>
		/// <param name="right">The original <see cref="ResultInfo"/>.</param>
		/// <returns>Returns the <see cref="ResultInfo"/> with <paramref name="right"/>'s <see cref="Success"/> value.</returns>
		public static ResultInfo operator +(string left, ResultInfo right) => new ResultInfo(right.Success, left + right.Message);

		/// <inheritdoc/>
		public override string ToString() => $"{Success}: {Message}";

	}

}
