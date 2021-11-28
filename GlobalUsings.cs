
#pragma warning disable IDE0005
#pragma warning disable CS8019

// Script
global using ScriptContext = MCSharpParser.ScriptContext;

// Parameters
global using GenericParameterContext = MCSharpParser.Generic_parameterContext;
global using GenericParameterListContext = MCSharpParser.Generic_parameter_listContext;
global using GenericParametersContext = MCSharpParser.Generic_parametersContext;
global using MethodParameterContext = MCSharpParser.Method_parameterContext;
global using MethodParameterListContext = MCSharpParser.Method_parameter_listContext;
global using MethodParametersContext = MCSharpParser.Method_parametersContext;
global using IndexerParametersContext = MCSharpParser.Indexer_parametersContext;

// Arguments
global using ArgumentContext = MCSharpParser.ArgumentContext;
global using ArgumentListContext = MCSharpParser.Argument_listContext;
global using GenericArgumentsContext = MCSharpParser.Generic_argumentsContext;
global using MethodArgumentsContext = MCSharpParser.Method_argumentsContext;
global using IndexerArgumentsContext = MCSharpParser.Indexer_argumentsContext;

// Other Lists
global using MemberInitializerContext = MCSharpParser.Member_initializerContext;
global using ObjectInitializerContext = MCSharpParser.Object_initializerContext;
global using ElementInitializerContext = MCSharpParser.Element_initializerContext;
global using CollectionInitializerContext = MCSharpParser.Collection_initializerContext;
global using AnonymousElementInitializerContext = MCSharpParser.Anonymous_element_initializerContext;
global using AnonymousObjectInitializerContext = MCSharpParser.Anonymous_object_initializerContext;

// Definitions
global using ModifierContext = MCSharpParser.ModifierContext;
global using ParameterModifierContext = MCSharpParser.Parameter_modifierContext;
global using ClassTypeContext = MCSharpParser.Class_typeContext;
global using AttributeTagContext = MCSharpParser.Attribute_tagContext;
global using TypeDefinitionContext = MCSharpParser.Type_definitionContext;
global using MemberDefinitionContext = MCSharpParser.Member_definitionContext;
global using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;
global using FieldDefinitionContext = MCSharpParser.Field_definitionContext;
global using PropertyDefinitionContext = MCSharpParser.Property_definitionContext;
global using PropertyGetDefinitionContext = MCSharpParser.Property_get_definitionContext;
global using PropertySetDefinitionContext = MCSharpParser.Property_set_definitionContext;
global using MethodDefinitionContext = MCSharpParser.Method_definitionContext;

// Identifiers
global using LiteralContext = MCSharpParser.LiteralContext;
global using IdentifierContext = MCSharpParser.IdentifierContext;
global using ShortIdentifierContext = MCSharpParser.Short_identifierContext;

// Statements
global using StatementContext = MCSharpParser.StatementContext;
global using CodeBlockContext = MCSharpParser.Code_blockContext;

// Operators
global using AdditiveOperatorContext = MCSharpParser.Additive_operatorContext;
global using MultiplicativeOperatorContext = MCSharpParser.Multiplicative_operatorContext;
global using StepOperatorContext = MCSharpParser.Step_operatorContext;
global using BitwiseOperatorContext = MCSharpParser.Bitwise_operatorContext;
global using BooleanOperatorContext = MCSharpParser.Boolean_operatorContext;
global using ShiftOperatorContext = MCSharpParser.Shift_operatorContext;
global using EqualityOperatorContext = MCSharpParser.Equality_operatorContext;
global using RelationOperatorContext = MCSharpParser.Relation_operatorContext;
global using AssignmentOperatorContext = MCSharpParser.Assignment_operatorContext;
global using RangeOperatorContext = MCSharpParser.Range_operatorContext;

// Language Functions
global using LanguageFunctionContext = MCSharpParser.Language_functionContext;
global using IfStatementContext = MCSharpParser.If_statementContext;
global using ForStatementContext = MCSharpParser.For_statementContext;
global using ForeachStatementContext = MCSharpParser.Foreach_statementContext;
global using WhileStatementContext = MCSharpParser.While_statementContext;
global using DoStatementContext = MCSharpParser.Do_statementContext;
global using ReturnStatementContext = MCSharpParser.Return_statementContext;
global using ThrowStatementContext = MCSharpParser.Throw_statementContext;
global using TryStatementContext = MCSharpParser.Try_statementContext;

// Expressions
global using ExpressionContext = MCSharpParser.ExpressionContext;
global using InitializationExpressionContext = MCSharpParser.Initialization_expressionContext;
global using NonAssignmentExpressionContext = MCSharpParser.Non_assignment_expressionContext;
global using LambdaExpressionContext = MCSharpParser.Lambda_expressionContext;
global using ExpressionListContext = MCSharpParser.Expression_listContext;
global using ConditionalExpressionContext = MCSharpParser.Conditional_expressionContext;
global using NullCoalescingExpressionContext = MCSharpParser.Null_coalescing_expressionContext;
global using ConditionalOrExpressionContext = MCSharpParser.Conditional_or_expressionContext;
global using ConditionalAndExpressionContext = MCSharpParser.Conditional_and_expressionContext;
global using InclusiveOrExpressionContext = MCSharpParser.Inclusive_or_expressionContext;
global using ExclusiveOrExpressionContext = MCSharpParser.Exclusive_or_expressionContext;
global using AndExpressionContext = MCSharpParser.And_expressionContext;
global using EqualityExpressionContext = MCSharpParser.Equality_expressionContext;
global using RelationalExpressionContext = MCSharpParser.Relational_expressionContext;
global using RelationOrTypeCheckContext = MCSharpParser.Relation_or_type_checkContext;
global using ShiftExpressionContext = MCSharpParser.Shift_expressionContext;
global using AdditiveExpressionContext = MCSharpParser.Additive_expressionContext;
global using MultiplicativeExpressionContext = MCSharpParser.Multiplicative_expressionContext;
global using WithExpressionContext = MCSharpParser.With_expressionContext;
global using RangeExpressionContext = MCSharpParser.Range_expressionContext;
global using PreStepExpressionContext = MCSharpParser.Pre_step_expressionContext;
global using PostStepExpressionContext = MCSharpParser.Post_step_expressionContext;
global using UnaryExpressionContext = MCSharpParser.Unary_expressionContext;
global using CastExpressionContext = MCSharpParser.Cast_expressionContext;
global using PointerIndirectionExpressionContext = MCSharpParser.Pointer_indirection_expressionContext;
global using AddressofExpressionContext = MCSharpParser.Addressof_expressionContext;
global using AssignmentExpressionContext = MCSharpParser.Assignment_expressionContext;
global using PrimaryExpressionContext = MCSharpParser.Primary_expressionContext;
global using ArrayCreationExpressionContext = MCSharpParser.Array_creation_expressionContext;
global using ArrayRankSpecifierContext = MCSharpParser.Array_rank_specifierContext;
global using ArrayInitializerContext = MCSharpParser.Array_initializerContext;
global using VariableInitializerContext = MCSharpParser.Variable_initializerContext;
global using PrimaryNoArrayCreationExpressionContext = MCSharpParser.Primary_no_array_creation_expressionContext;
global using MemberAccessPrefixContext = MCSharpParser.Member_access_prefixContext;
global using MemberAccessContext = MCSharpParser.Member_accessContext;
global using KeywordExpressionContext = MCSharpParser.Keyword_expressionContext;
global using ObjectOrCollectionInitializerContext = MCSharpParser.Object_or_collection_initializerContext;
global using NewKeywordExpressionContext = MCSharpParser.New_keyword_expressionContext;
global using TypeofKeywordExpressionContext = MCSharpParser.Typeof_keyword_expressionContext;
global using CheckedExpressionContext = MCSharpParser.Checked_expressionContext;
global using UncheckedExpressionContext = MCSharpParser.Unchecked_expressionContext;
global using DefaultKeywordExpressionContext = MCSharpParser.Default_keyword_expressionContext;
global using DelegateKeywordExpressionContext = MCSharpParser.Delegate_keyword_expressionContext;
global using SizeofKeywordExpressionContext = MCSharpParser.Sizeof_keyword_expressionContext;
