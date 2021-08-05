using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public enum Operation {

		// Assign
		Assign,
		AssignAddition,
		AssignSubtraction,
		AssignMultiplication,
		AssignDivision,
		AssignModulo,
		AssignAccess,
		AssignBitwiseAND,
		AssignBitwiseOR,
		AssignBitwiseXOR,
		AssignShiftLeft,
		AssignShiftRight,

		// Conditional Expression
		Conditional,

		// Null-Coalescing Expression
		NullCoalescing,

		// Boolean OR Expression
		BooleanOR,

		// Boolean AND Expression
		BooleanAND,

		// Bitwise OR Expression
		BitwiseOR,

		// Bitwise XOR Expression
		BitwiseXOR,

		// Bitwise AND Expression
		BitwiseAND,

		// Equality Expression
		Equality,
		Inequality,

		// Relational Expression
		LessThan,
		LessThanOrEqual,
		GreaterThan,
		GreaterThanOrEqual,

		// Type Check Expression
		TypeCheck,

		// Shift Expression
		ShiftLeft,
		ShiftRight,

		// Additive Expression
		Addition,
		Subtraction,

		// Multiplicative Expression
		Multiplication,
		Division,
		Modulo,

		// Range Expression
		Range,

		// Unary Expression
		Positive,
		Negative,
		BooleanNOT,
		BitwiseNOT,

		// Pre-Step Expression
		PreStep,

		// Cast Expression
		Cast,

		// Pointer Indirection Expression
		PointerIndirection,

		// Addressof Expression
		Addressof,

		// Post-Step Expression
		PostStep,

	}

}
