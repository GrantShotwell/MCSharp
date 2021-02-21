using MCSharp.Compilation;
using MCSharp.Linkage.Minecraft;
using System;

namespace MCSharp.Linkage {

	public abstract class ScriptMemberDefinition {

		public static ScriptMemberDefinition CreateMemberDefinitionLink(ScriptMember member, Settings settings, VirtualMachine virtualMachine) {

			switch(member.MemberType) {

				case MemberType.Field: {

					// Get the specific member definition.
					MCSharpParser.Field_definitionContext definition = member.FullContext.field_definition();

					// Get the 'initializer' definition. Null if undefined.
					MCSharpParser.ExpressionContext initialize;
					if(definition.ASSIGN() == null) {
						initialize = null;
					} else {
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
					Function getFunction;
					if(getDefinition == null) {

						getFunction = null;

					} else {

						// Get function writer.
						FunctionWriter writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText());

						// Get the statement list for the function.
						MCSharpParser.StatementContext[] statements;
						if(getDefinition.LAMBDA() != null) {
							// Return expression.
							throw new NotImplementedException("Lambda definitions for get property has not been implemented.");
						} else if(getDefinition.END() != null) {
							// Auto property.
							throw new NotImplementedException("Auto properties have not been implemented.");
						} else {
							// Code block.
							statements = getDefinition.code_block().statement();
						}

						// Construct the 'get' function.
						getFunction = new Function(writer, new GenericParameter[] { }, new MethodParameter[] { }, statements, member.ReturnTypeIdentifier);

					}

					// Get the 'setter' definition. Null if undefined.
					MCSharpParser.Property_set_definitionContext setDefinition = definition.property_set_definition();
					Function setFunction;
					if(setDefinition == null) {

						setFunction = null;

					} else {

						// Get funciton writer.
						FunctionWriter writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText());

						// Get the statement list for the function.
						MCSharpParser.StatementContext[] statements;
						if(setDefinition.LAMBDA() != null) {
							// Return expression.
							throw new NotImplementedException("Lambda definitions for set property has not been implemented.");
						} else if(setDefinition.END() != null) {
							// Auto property.
							throw new NotImplementedException("Auto properties have not been implemented.");
						} else {
							// Code block.
							statements = setDefinition.code_block().statement();
						}

						// Construct the 'set' function.
						setFunction = new Function(writer, new GenericParameter[] { }, new MethodParameter[] { }, statements, member.ReturnTypeIdentifier);

					}

					// Construct and return the property.
					return new Property(getFunction, setFunction);

				}

				case MemberType.Method: {

					// Get the specific member definition.
					MCSharpParser.Method_definitionContext definition = member.FullContext.method_definition();

					// Get function writer, generic parameters, and method parameters.
					FunctionWriter writer = new FunctionWriter(virtualMachine, settings, member.Declarer.Identifier.GetText(), member.Identifier.GetText());
					GenericParameter[] generics = GenericParameter.CreateArrayFromArray(definition.generic_parameters()?.generic_parameter_list()?.generic_parameter());
					MethodParameter[] parameters = MethodParameter.CreateArrayFromArray(definition.method_parameters().method_parameter_list()?.method_parameter());

					// Get the statement list for the function.
					MCSharpParser.StatementContext[] statements;
					if(definition.LAMBDA() != null) {
						// Return expression.
						throw new NotImplementedException("Lambda definitions for methods has not been implemented.");
					} else {
						// Code block.
						statements = definition.code_block().statement();
					}

					// Construct the function.
					Function function = new Function(writer, generics, parameters, statements, member.ReturnTypeIdentifier);

					// Construct and return the method.
					return new Method(function);

				}

				default: throw new Exception($"Unrecognized '{nameof(MemberType)}'.");

			}

		}

		public class Field : ScriptMemberDefinition {

			public MCSharpParser.ExpressionContext Initializer { get; }

			public Field(MCSharpParser.ExpressionContext initialize) {
				Initializer = initialize;
			}

		}

		public class Property : ScriptMemberDefinition {

			public Function Setter { get; }
			public Function Getter { get; }

			public Property(Function set, Function get) {
				Setter = set;
				Getter = get;
			}

		}

		public class Method : ScriptMemberDefinition {

			public Function Invoker { get; }

			public Method(Function invoke) {
				Invoker = invoke ?? throw new ArgumentNullException(nameof(invoke));
			}

		}

	}

}
