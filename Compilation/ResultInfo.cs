using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

	public struct ResultInfo {

		public bool Success { get; }

		public string Message { get; }

		public ResultInfo(bool success, string message) {
			Success = success;
			Message = message ?? throw new ArgumentNullException(nameof(message));
		}

		public static ResultInfo DefaultSuccess() {

			return new ResultInfo(true, "Success.");

		}

	}

}
