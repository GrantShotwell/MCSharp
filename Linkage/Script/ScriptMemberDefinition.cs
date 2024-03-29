﻿using MCSharp.Compilation;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Predefined;
using System;

namespace MCSharp.Linkage.Script;

/// <summary>
/// Represents a <see cref="IMemberDefinition"/> defined in script.
/// </summary>
public abstract class ScriptMemberDefinition : IMemberDefinition {

	/// <summary>
	/// 
	/// </summary>
	/// <param name="member"></param>
	/// <param name="settings"></param>
	/// <param name="virtualMachine"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	/// <exception cref="Exception"></exception>
	public static ScriptMemberDefinition CreateMemberDefinitionLink(ScriptMember member, Settings settings, VirtualMachine virtualMachine, ref Compiler.OnLoadDelegate onLoad) {

		switch(member.MemberType) {

			case MemberType.Field: {

				// Get the specific member definition.
				MCSharpParser.Field_definitionContext definition = member.FullContext.field_definition();

				// Get the 'initializer' definition. Null if undefined.
				ExpressionContext initialize;
				if(definition.ASSIGN() == null) initialize = null;
				else {
					initialize = definition.expression();
				}

				// Construct and return the field.
				return new Field(initialize);

			}

			case MemberType.Property: {

				// Get the specific member definition.
				MCSharpParser.Property_definitionContext definition = member.FullContext.property_definition();

				// Get the 'getter' definition. Null if undefined.
				MCSharpParser.Property_get_definitionContext getDefinition = definition.property_get_definition();
				StandaloneStatementFunction getFunction;
				if(getDefinition == null)
					getFunction = null;
				else {

					// Get function writer.
					var writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText() + "_get");

					// Get the statement list for the function.
					ScriptStatement[] statements;
					if(getDefinition.LAMBDA() != null) {
						// Return expression.
						throw new NotImplementedException("Lambda definitions for get property has not been implemented.");
					} else if(getDefinition.END() != null) {
						// Auto property.
						throw new NotImplementedException("Auto properties have not been implemented.");
					} else {
						// Code block.
						statements = ScriptStatement.CreateArrayFromArray(getDefinition.code_block().statement());
					}

					// Construct the 'get' function.
					getFunction = new StandaloneStatementFunction(
						writer, new Scope("get", member.Scope),
						Array.Empty<ScriptGenericParameter>(),
						Array.Empty<ScriptMethodParameter>(),
						statements,
						member.TypeIdentifier.GetText(),
						member.Modifiers.HasFlag(Modifier.Static) ? null : member.Declarer.Identifier.GetText(),
						ctor: false,
						ref onLoad
					);

				}

				// Get the 'setter' definition. Null if undefined.
				MCSharpParser.Property_set_definitionContext setDefinition = definition.property_set_definition();
				StandaloneStatementFunction setFunction;
				if(setDefinition == null)
					setFunction = null;
				else {

					// Get funciton writer.
					var writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText() + "_set");

					// Get the statement list for the function.
					ScriptStatement[] statements;
					if(setDefinition.LAMBDA() != null) {
						// Return expression.
						throw new NotImplementedException("Lambda definitions for set property has not been implemented.");
					} else if(setDefinition.END() != null) {
						// Auto property.
						throw new NotImplementedException("Auto properties have not been implemented.");
					} else {
						// Code block.
						statements = ScriptStatement.CreateArrayFromArray(setDefinition.code_block().statement());
					}

					// Construct the 'set' function.
					setFunction = new StandaloneStatementFunction(
						writer, new Scope("set", member.Scope),
						Array.Empty<ScriptGenericParameter>(),
						new IMethodParameter[] { new PredefinedMethodParameter(member.TypeIdentifier.GetText(), "value") },
						statements,
						member.TypeIdentifier.GetText(),
						member.Modifiers.HasFlag(Modifier.Static) ? null : member.Declarer.Identifier.GetText(),
						ctor: false,
						ref onLoad
					);

				}

				// Construct and return the property.
				return new Property(getFunction, setFunction);

			}

			case MemberType.Method: {

				// Get the specific member definition.
				MCSharpParser.Method_definitionContext definition = member.FullContext.method_definition();

				// Get function writer, generic parameters, and method parameters.
				var writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText());
				ScriptGenericParameter[] generics = ScriptGenericParameter.CreateArrayFromArray(definition.generic_parameters()?.generic_parameter_list()?.generic_parameter());
				ScriptMethodParameter[] parameters = ScriptMethodParameter.CreateArrayFromArray(definition.method_parameters().method_parameter_list()?.method_parameter());

				// Get the statement list for the function.
				ScriptStatement[] statements;
				if(definition.LAMBDA() != null) {
					// Return expression.
					throw new NotImplementedException("Lambda definitions for methods has not been implemented.");
				} else {
					// Code block.
					statements = ScriptStatement.CreateArrayFromArray(definition.code_block().statement());
				}

				// Construct the function.
				var function = new StandaloneStatementFunction(
					writer, new Scope(null, member.Scope),
					generics,
					parameters,
					statements,
					member.TypeIdentifier.GetText(),
					member.Modifiers.HasFlag(Modifier.Static) ? null : member.Declarer.Identifier.GetText(),
					ctor: false,
					ref onLoad
				);

				// Construct and return the method.
				return new Method(function);

			}

			default: throw new Exception($"Unrecognized '{nameof(MemberType)}'.");

		}

	}

	/// <inheritdoc/>
	public abstract void Dispose();

	/// <summary>
	/// Represents a <see cref="IField"/> defined by script.
	/// </summary>
	public class Field : ScriptMemberDefinition, IField {

		/// <inheritdoc cref="IField.Initializer"/>
		public ScriptExpression Initializer { get; }
		IExpression IField.Initializer => Initializer;

		/// <summary>
		/// Creates a new <see cref="Field"/>.
		/// </summary>
		/// <param name="initialize">The value of <see cref="Initializer"/>.</param>
		public Field(ExpressionContext initialize) {
			if(initialize == null) Initializer = null;
			else Initializer = new ScriptExpression(initialize);
		}

		/// <inheritdoc/>
		public override void Dispose() {
			// Nothing to dispose of.
			GC.SuppressFinalize(this);
		}

	}

	/// <summary>
	/// Represents a <see cref="IProperty"/> defined by script.
	/// </summary>
	public class Property : ScriptMemberDefinition, IProperty {

		/// <inheritdoc cref="IProperty.Getter"/>
		public StandaloneStatementFunction Getter { get; }
		IFunction IProperty.Getter => Getter;

		/// <inheritdoc cref="IProperty.Setter"/>
		public StandaloneStatementFunction Setter { get; }
		IFunction IProperty.Setter => Setter;

		/// <summary>
		/// Creates a new <see cref="Property"/>.
		/// </summary>
		/// <param name="get">The value of <see cref="Getter"/>.</param>
		/// <param name="set">The value of <see cref="Setter"/>.</param>
		public Property(StandaloneStatementFunction get, StandaloneStatementFunction set) {
			Getter = get;
			Setter = set;
		}

		/// <inheritdoc/>
		public override void Dispose() {
			Getter?.Dispose();
			Setter?.Dispose();
			GC.SuppressFinalize(this);
		}

	}

	/// <summary>
	/// Represents a <see cref="IMethod"/> defined by script.
	/// </summary>
	public class Method : ScriptMemberDefinition, IMethod {

		/// <inheritdoc cref="IMethod.Invoker"/>
		public StandaloneStatementFunction Invoker { get; }
		IFunction IMethod.Invoker => Invoker;

		/// <summary>
		/// Creates a new <see cref="Method"/>.
		/// </summary>
		/// <param name="invoke">The value of <see cref="Invoker"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="invoke"/> is <see langword="null"/>.</exception>
		public Method(StandaloneStatementFunction invoke) {
			Invoker = invoke ?? throw new ArgumentNullException(nameof(invoke));
		}

		/// <inheritdoc/>
		public override void Dispose() {
			Invoker.Dispose();
			GC.SuppressFinalize(this);
		}

	}

}
