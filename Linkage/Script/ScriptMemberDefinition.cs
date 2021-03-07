using MCSharp.Compilation;
using MCSharp.Linkage.Minecraft;
using System;

namespace MCSharp.Linkage.Script {

	public abstract class ScriptMemberDefinition : IMemberDefinition {

		public static ScriptMemberDefinition CreateMemberDefinitionLink(ScriptMember member, Settings settings, VirtualMachine virtualMachine) {

			switch(member.MemberType) {

				case MemberType.Field: {

					// Get the specific member definition.
					MCSharpParser.Field_definitionContext definition = member.FullContext.field_definition();

					// Get the 'initializer' definition. Null if undefined.
					MCSharpParser.ExpressionContext initialize;
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
						var writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText());

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
						getFunction = new StandaloneStatementFunction(writer, new ScriptGenericParameter[] { }, new ScriptMethodParameter[] { }, statements, member.ReturnTypeIdentifier.GetText());

					}

					// Get the 'setter' definition. Null if undefined.
					MCSharpParser.Property_set_definitionContext setDefinition = definition.property_set_definition();
					StandaloneStatementFunction setFunction;
					if(setDefinition == null)
						setFunction = null;
					else {

						// Get funciton writer.
						var writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText());

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
						setFunction = new StandaloneStatementFunction(writer, new ScriptGenericParameter[] { }, new ScriptMethodParameter[] { }, statements, member.ReturnTypeIdentifier.GetText());

					}

					// Construct and return the property.
					return new Property(setFunction, getFunction);

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
					var function = new StandaloneStatementFunction(writer, generics, parameters, statements, member.ReturnTypeIdentifier.GetText());

					// Construct and return the method.
					return new Method(function);

				}

				default: throw new Exception($"Unrecognized '{nameof(MemberType)}'.");

			}

		}

		public abstract void Dispose();

		public class Field : ScriptMemberDefinition, IField {

			public ScriptExpression Initializer { get; }
			IExpression IField.Initializer => Initializer;

			public Field(MCSharpParser.ExpressionContext initialize) {
				Initializer = new ScriptExpression(initialize);
			}

			public override void Dispose() {
				// Nothing to dispose of.
			}

		}

		public class Property : ScriptMemberDefinition, IProperty {

			public StandaloneStatementFunction Getter { get; }
			IFunction IProperty.Getter => Getter;

			public StandaloneStatementFunction Setter { get; }
			IFunction IProperty.Setter => Setter;

			public Property(StandaloneStatementFunction get, StandaloneStatementFunction set) {
				Getter = get;
				Setter = set;
			}

			public override void Dispose() {
				Getter?.Dispose();
				Setter?.Dispose();
			}

		}

		public class Method : ScriptMemberDefinition, IMethod {

			public StandaloneStatementFunction Invoker { get; }
			IFunction IMethod.Invoker => Invoker;

			public Method(StandaloneStatementFunction invoke) {
				Invoker = invoke ?? throw new ArgumentNullException(nameof(invoke));
			}

			public override void Dispose() {
				Invoker.Dispose();
			}

		}

	}

}
