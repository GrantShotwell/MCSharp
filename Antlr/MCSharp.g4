
grammar MCSharp;

/*
 *   Grammar Rules
 */

/*
 *   Parser Rules
 */

// Script
script
	: type_definition* EOF
	;

// Parameters
generic_parameter
	: NAME
	;
generic_parameter_list
	: generic_parameter ( COMMA generic_parameter )*
	;
generic_parameters
	: LESS_THAN generic_parameter_list GREATER_THAN
	;
method_parameter
	: parameter_modifier? NAME NAME
	;
method_parameter_list
	: method_parameter ( COMMA method_parameter )*
	;
method_parameters
	: OP method_parameter_list? CP
	;
indexer_parameters
	: OB method_parameter_list? CB
	;

// Arguments
argument
	: expression
	| parameter_modifier NAME NAME
	;
argument_list
	: argument ( COMMA argument )*
	;
generic_arguments
	: LESS_THAN generic_parameter_list? GREATER_THAN
	;
method_arguments
	: OP argument_list? CP
	;
indexer_arguments
	: OB argument_list? CB
	;

// Other Lists
member_initializer
	: NAME generic_arguments? ASSIGN ( expression | object_or_collection_initializer )
	;
object_initializer
	: OC ( member_initializer ( COMMA member_initializer )? COMMA? )? CC 
	;
element_initializer
	: non_assignment_expression
	| OC expression CC
	;
collection_initializer
	: OC ( element_initializer ( COMMA element_initializer )? COMMA? )? CC
	;
anonymous_element_initializer
	: NAME generic_arguments?
	| member_access
	| identifier ASSIGN expression
	;
anonymous_object_initializer
	: OC ( anonymous_element_initializer ( COMMA anonymous_element_initializer )? COMMA? )? CC
	;

// Definitions
modifier: PUBLIC | PRIVATE | PROTECTED | STATIC | ABSTRACT | VIRTUAL;
parameter_modifier: IN | OUT | REF;
class_type: CLASS | STRUCT;
type_definition
	: modifier* class_type NAME OC member_definition* CC
	;
member_definition
	: modifier* NAME NAME ( field_definition | property_definition | method_definition )
	;
field_definition
	: ( ASSIGN expression )? END
	;
property_definition
	: LAMBDA expression
	| OC ( ( modifier* property_get_definition ) ( modifier* property_set_definition )? ) | ( ( modifier* property_set_definition ) ( modifier* property_get_definition )? )  CC
	;
property_get_definition
	: GET END
	| GET LAMBDA expression END
	| GET code_block
	;
property_set_definition
	: SET END
	| SET LAMBDA expression END
	| SET code_block
	;
method_definition
	: generic_parameters? method_parameters ( LAMBDA expression END | code_block)
	;

// Identifiers
literal
	: INTEGER
	| DECIMAL
	| STRING
	;
identifier
	: '@'? ( NAME ) ( DOT NAME )* generic_arguments?
	;

// Statements
statement
	: code_block
	| language_function
	| initialization_expression END
	| expression END
	;
code_block
	: OC statement* CC
	;

// Operators
additive_operator: PLUS | MINUS;
multiplicative_operator: MULTIPLY | DIVIDE | MODULUS;
step_operator: INCREMENT | DECREMENT;
bitwise_operator: BITWISE_AND | BITWISE_OR | BITWISE_XOR;
boolean_operator: BOOLEAN_AND | BOOLEAN_OR;
shift_operator: SHIFT_LEFT | SHIFT_RIGHT;
equality_operator: EQUIVALENT | NOT_EQUIVALENT;
relation_operator: LESS_THAN | GREATER_THAN | LESS_THAN_EQUAL | GREATER_THAN_EQUAL;
assignment_operator: ASSIGN | ASSIGN_PLUS | ASSIGN_MINUS | ASSIGN_MULTIPLY | ASSIGN_DIVIDE | ASSIGN_MODULUS | ASSIGN_ACCESS | ASSIGN_AND | ASSIGN_OR | ASSIGN_XOR | ASSIGN_LEFT | ASSIGN_RIGHT;
range_operator: RANGE_INCLUSIVE | RANGE_EXCLUSIVE;

// Language Functions
language_function	
	: if_statement
	| for_statement
	| foreach_statement
	| while_statement
	| do_statement
	| return_statement
	| throw_statement
	| try_statement
	;
if_statement
	: IF OP expression CP statement ( ELSE statement )?
	;
for_statement
	: FOR OP initialization_expression END expression END expression CP statement
	;
foreach_statement
	: FOREACH OP NAME NAME IN expression CP statement
	;
while_statement
	: WHILE OP expression CP statement
	;
do_statement
	: DO statement WHILE OP expression CP END
	;
return_statement
	: RETURN expression END
	;
throw_statement
	: THROW expression END
	;
try_statement
	: TRY statement ( CATCH OP NAME NAME CP statement )* ( FINALLY statement )?
	;

// Expressions
expression
	: non_assignment_expression
	| assignment_expression
	;
initialization_expression
	: NAME NAME ( ASSIGN expression )
	;
non_assignment_expression
	: conditional_expression
	| lambda_expression
	;
lambda_expression
	: method_arguments LAMBDA ( code_block )
	;
expression_list
	: expression ( COMMA expression )*
	;
conditional_expression
	: null_coalescing_expression ( CONDITION_IF expression CONDITION_ELSE expression )?
	;
null_coalescing_expression
	: conditional_or_expression ( NULL_COALESCING null_coalescing_expression )?
	;
conditional_or_expression
	: conditional_and_expression ( BOOLEAN_OR conditional_and_expression )*
	;
conditional_and_expression
	: inclusive_or_expression ( BOOLEAN_AND inclusive_or_expression )*
	;
inclusive_or_expression
	: exclusive_or_expression ( BITWISE_OR exclusive_or_expression )*
	;
exclusive_or_expression
	: and_expression ( BITWISE_XOR and_expression )*
	;
and_expression
	: equality_expression ( BITWISE_AND equality_expression )*
	;
equality_expression
	: relational_expression ( equality_operator relational_expression )*
	;
relational_expression
	: shift_expression ( relation_or_type_check )*
	;
relation_or_type_check
	: relation_operator shift_expression
	| ( IS | AS ) NAME
	;
shift_expression
	: additive_expression ( shift_operator additive_expression )*
	;
additive_expression
	: multiplicative_expression ( additive_operator multiplicative_expression )*
	;
multiplicative_expression
	: with_expression ( multiplicative_operator with_expression )*
	;
with_expression
	: range_expression ( WITH anonymous_element_initializer )?
	;
range_expression
	: unary_expression ( range_operator unary_expression )?
	;
pre_step_expression
	: step_operator ( unary_expression )
	;
post_step_expression
	: ( literal | identifier ) step_operator
	;
unary_expression
	: primary_expression
	| (PLUS|MINUS|BOOLEAN_NOT|BITWISE_NOT) unary_expression
	| step_operator unary_expression
	| cast_expression
	| pointer_indirection_expression
	| addressof_expression
	;
cast_expression
	: OP NAME CP unary_expression
	;
pointer_indirection_expression
	: MULTIPLY unary_expression
	;
addressof_expression
	: BITWISE_AND unary_expression
	;
assignment_expression
	: unary_expression assignment_operator expression
	;
primary_expression
	: array_creation_expression
	| primary_no_array_creation_expression
	;
array_creation_expression
	: NEW indexer_arguments array_rank_specifier? array_initializer?
	;
array_rank_specifier
	: OB COMMA* CB
	;
array_initializer
	: OC ( variable_initializer ( COMMA variable_initializer )* COMMA? )? CC
	;
variable_initializer
	: expression
	| array_initializer
	;
primary_no_array_creation_expression
	: literal
	| identifier
	| OP expression CP
	| member_access
	| post_step_expression
	| keyword_expression
	;
member_access
	: ( OP primary_expression CP DOT )? identifier generic_arguments? ( method_arguments | indexer_arguments )?
	;
keyword_expression
	: new_keyword_expression
	| typeof_keyword_expression
	| checked_expression
	| unchecked_expression
	| default_keyword_expression
	| sizeof_keyword_expression
	;
object_or_collection_initializer
	: object_initializer
	| collection_initializer
	;
new_keyword_expression
	: NEW NAME ( ( method_arguments object_or_collection_initializer? ) | ( object_or_collection_initializer ) )
	| NEW NAME ( OP expression CP )
	| NEW anonymous_object_initializer
	;
typeof_keyword_expression
	: TYPEOF OP ( NAME ) CP
	;
checked_expression
	: CHECKED OP expression CP
	;
unchecked_expression
	: UNCHECKED OP expression CP
	;
default_keyword_expression
	: DEFAULT ( OP NAME CP )?
	;
delegate_keyword_expression
	: DELEGATE method_parameters code_block
	;
sizeof_keyword_expression
	: SIZEOF OP NAME CP
	;

/*
 *   Lexer Rules
 */

// Common Keywords
END: ';';
COMMA: ',';
OP: '(';
CP: ')';
OB: '[';
CB: ']';
OC: '{';
CC: '}';

// Operators
PLUS: '+';
MINUS: '-';
MULTIPLY: '*';
DIVIDE: '/';
MODULUS: '%';
INCREMENT: '++';
DECREMENT: '--';
BITWISE_AND: '&';
BITWISE_OR: '|';
BITWISE_XOR: '^';
BITWISE_NOT: '~';
BOOLEAN_AND: '&&';
BOOLEAN_OR: '||';
BOOLEAN_NOT: '!';
SHIFT_LEFT: '<<';
SHIFT_RIGHT: '>>';
EQUIVALENT: '==';
NOT_EQUIVALENT: '!=';
LESS_THAN: '>';
GREATER_THAN: '<';
LESS_THAN_EQUAL: '>=';
GREATER_THAN_EQUAL: '<=';
DOT: '.';
ASSIGN: '=';
ASSIGN_PLUS: '+=';
ASSIGN_MINUS: '-=';
ASSIGN_MULTIPLY: '*=';
ASSIGN_DIVIDE: '/=';
ASSIGN_MODULUS: '%=';
ASSIGN_ACCESS: '.=';
ASSIGN_AND: '&=';
ASSIGN_OR: '|=';
ASSIGN_XOR: '^=';
ASSIGN_LEFT: '<<=';
ASSIGN_RIGHT: '>>=';
CONDITION_IF: '?';
CONDITION_ELSE: ':';
RANGE_INCLUSIVE: '..';
RANGE_EXCLUSIVE: '..^';
IS: 'is';
AS: 'as';
IN: 'in';
OUT: 'out';
LAMBDA: '=>';
NULL_COALESCING: '??';

// Statement Keywords
IF: 'if';
ELSE: 'else';
FOR: 'for';
FOREACH: 'foreach';
DO: 'do';
WHILE: 'while';
RETURN: 'return';
THROW: 'throw';
TRY: 'try';
CATCH: 'catch';
FINALLY: 'finally';

// Expression Keywords
NEW: 'new';
TYPEOF: 'typeof';
CHECKED: 'checked';
UNCHECKED: 'unchecked';
DEFAULT: 'default';
DELEGATE: 'delegate';
SIZEOF: 'sizeof';
WITH: 'with';

// Property Keywords
GET: 'get';
SET: 'set';

// Modifier Keywords
PUBLIC: 'public';
PRIVATE: 'private';
PROTECTED: 'protected';
STATIC: 'static';
ABSTRACT: 'abstract';
VIRTUAL: 'virtual';
OVERRIDE: 'override';
REF: 'ref';
CLASS: 'class';
STRUCT: 'struct';

// Literals
STRING: '"' ( '\\\\' | '\\"' | ~'"' ) '"';
DECIMAL: [0-9]+ '.' [0-9]+;
INTEGER: [0-9]+;

// Simple Identifiers
fragment SIMPLE_NAME_CHARACTER
	: [a-zA-Z]
	| '_'
	| '-'
	;
fragment COMPLEX_NAME_CHARACTER
	: SIMPLE_NAME_CHARACTER
	| [0-9]
	;
NAME
	: SIMPLE_NAME_CHARACTER COMPLEX_NAME_CHARACTER*
	;

// Whitespace
WHITESPACE: [ \t\r]+ -> skip;
NEWLINE: [\n]+ -> skip;
