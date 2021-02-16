
grammar MCSharp;

/*
 *   Grammar Rules
 */



/*
 *   Parser Rules
 */

// Parameters
generic_parameter
	: TYPE_NAME
	;
generic_parameter_list
	: generic_parameter ( ',' generic_parameter )*
	;
generic_parameters
	: '<' generic_parameter_list '>'
	;
method_parameter
	: PARAMETER_MODIFIER? TYPE_NAME VARIABLE_NAME
	;
method_parameter_list
	: method_parameter ( ',' method_parameter )*
	;
method_parameters
	: '(' method_parameter_list ')'
	;
indexer_parameters
	: '[' method_parameter_list ']'
	;

// Arguments
argument
	: expression
	| PARAMETER_MODIFIER TYPE_NAME VARIABLE_NAME
	;
argument_list
	: argument ( ',' argument )*
	;
generic_arguments
	: '<' generic_parameter_list '>'
	;
method_arguments
	: '(' argument_list ')'
	;
indexer_arguments
	: '[' argument_list ']'
	;

// Other Lists
member_initializer
	: MEMBER_NAME generic_arguments? '=' ( expression | object_or_collection_initializer )
	;
object_initializer
	: '{' ( member_initializer ( ',' member_initializer )? ','? )? '}' 
	;
element_initializer
	: non_assignment_expression
	| '{' expression '}'
	;
collection_initializer
	: '{' ( element_initializer ( ',' element_initializer )? ','? )? '}'
	;
anonymous_element_initializer
	: MEMBER_NAME generic_arguments?
	| member_access
	| identifier '=' expression
	;
anonymous_object_initializer
	: '{' ( anonymous_element_initializer ( ',' anonymous_element_initializer )? ','? )? '}'
	;

// Definitions
type_definition
	: MODIFIER* TYPE_NAME '{' member_definition* '}'
	;
member_definition
	: field_definition
	| property_definition
	| method_definition
	;
field_definition
	: MODIFIER* MEMBER_NAME field_definition_end
	;
field_definition_end
	: ( '=' expression )? ';'
	;
property_definition
	: MODIFIER* MEMBER_NAME property_definition_end
	;
property_definition_end
	: '=>' expression
	| '{' ( ( MODIFIER* property_get_definition ) ( MODIFIER* property_set_definition )? ) | ( ( MODIFIER* property_set_definition ) ( MODIFIER* property_get_definition )? )  '}'
	;
property_get_definition
	: 'get' ';'
	| 'get' '=>' expression ';'
	| 'get' code_block
	;
property_set_definition
	: 'set' ';'
	| 'set' '=>' expression ';'
	| 'set' code_block
	;
method_definition
	: MODIFIER* MEMBER_NAME method_parameters method_definition_end
	;
method_definition_end
	: '=>' expression ';'
	| code_block
	;

// Identifiers
literal
	: INTEGER
	| DECIMAL
	| STRING
	;
identifier
	: '@'? ( TYPE_NAME|MEMBER_NAME|VARIABLE_NAME ) ( ('.') TYPE_NAME|MEMBER_NAME|VARIABLE_NAME )* generic_arguments?
	;

// Statements
statement
	: code_block
	| language_function
	| initialization_expression ';'
	| expression ';'
	;
code_block
	: '{' statement* '}'
	;

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
	: 'if' '(' expression ')' statement
	;
for_statement
	: 'for' '(' expression ';' expression ';' expression ')' statement
	;
foreach_statement
	: 'foreach' '(' TYPE_NAME VARIABLE_NAME 'in' expression ')' statement
	;
while_statement
	: 'while' '(' expression ')' statement
	;
do_statement
	: 'do' statement 'while' '(' expression ')' ';'
	;
return_statement
	: 'return' expression ';'
	;
throw_statement
	: 'throw' expression ';'
	;
try_statement
	: 'try' statement ( 'catch' '(' TYPE_NAME VARIABLE_NAME ')' statement )* ( 'finally' statement )?
	;

// Expressions
expression
	: non_assignment_expression
	| assignment_expression
	;
initialization_expression
	: TYPE_NAME VARIABLE_NAME ( '=' expression )
	;
non_assignment_expression
	: conditional_expression
	| lambda_expression
	//| query_expression
	;
lambda_expression
	: method_arguments '=>' ( code_block )
	;
// query_expression
// 	: 'from' TYPE_NAME? identifier 'in' expression ...
// 	;
expression_list
	: expression ( ',' expression )*
	;
conditional_expression
	: null_coalescing_expression ( ('?') expression (':') expression )?
	;
null_coalescing_expression
	: conditional_or_expression ( ('??') null_coalescing_expression )?
	;
conditional_or_expression
	: conditional_and_expression ( ('||') conditional_and_expression )*
	;
conditional_and_expression
	: inclusive_or_expression ( ('&&') inclusive_or_expression )*
	;
inclusive_or_expression
	: exclusive_or_expression ( ('|') exclusive_or_expression )*
	;
exclusive_or_expression
	: and_expression ( ('^') and_expression )*
	;
and_expression
	: equality_expression ( ('&') equality_expression )*
	;
equality_expression
	: relational_expression ( ('=='|'!=') relational_expression )*
	;
relational_expression
	: shift_expression ( relation_or_type_check )*
	;
relation_or_type_check
	: ('<'|'>'|'<='|'>=') shift_expression
	| ('is'|'as') TYPE_NAME
	;
shift_expression
	: additive_expression ( ('<<'|'>>') additive_expression )*
	;
additive_expression
	: multiplicative_expression ( ('+'|'-') multiplicative_expression )*
	;
multiplicative_expression
	: with_expression ( ('*'|'/'|'%') with_expression )*
	;
with_expression
	: range_expression ( ('with') anonymous_element_initializer )?
	;
range_expression
	: unary_expression ( ('..'|'..^') unary_expression )?
	;
unary_expression
	: primary_expression
	| ('+'|'-'|'!'|'~') unary_expression
	| ('++'|'--') unary_expression
	| cast_expression
	| pointer_indirection_expression
	| addressof_expression
	;
cast_expression
	: '(' TYPE_NAME ')' unary_expression
	;
pointer_indirection_expression
	: '*' unary_expression
	;
addressof_expression
	: '&' unary_expression
	;
assignment_expression
	: unary_expression ('='|'+='|'-='|'*='|'/='|'%='|'&='|'|='|'^='|'<<='|'>>='|'.=') expression
	;
primary_expression
	: array_creation_expression
	| primary_no_array_creation_expression
	;
array_creation_expression
	: 'new' indexer_arguments array_rank_specifier? array_initializer?
	;
array_rank_specifier
	: '[' ','* ']'
	;
array_initializer
	: '{' ( variable_initializer ( ',' variable_initializer )* ','? )? '}'
	;
variable_initializer
	: expression
	| array_initializer
	;
primary_no_array_creation_expression
	: literal
	| identifier
	| '(' expression ')'
	| member_access
	| invocation_expression
	| indexer_expression
	| post_step_expression
	| keyword_expression
	;
member_access
	: ( '(' primary_expression ')' | TYPE_NAME ) '.' identifier generic_arguments?
	;
invocation_expression
	: member_access method_arguments
	;
indexer_expression
	: member_access indexer_arguments
	;
post_step_expression
	: ( literal | identifier ) ('++'|'--')
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
	: 'new' TYPE_NAME ( ( method_arguments object_or_collection_initializer? ) | ( object_or_collection_initializer ) )
	| 'new' TYPE_NAME ( '(' expression ')' )
	| 'new' anonymous_object_initializer
	;
typeof_keyword_expression
	: 'typeof' '(' ( TYPE_NAME ) ')'
	;
checked_expression
	: 'checked' '(' expression ')'
	;
unchecked_expression
	: 'unchecked' '(' expression ')'
	;
default_keyword_expression
	: 'default' ( '(' TYPE_NAME ')' )?
	;
delegate_keyword_expression
	: 'delegate' method_parameters code_block
	;
sizeof_keyword_expression
	: 'sizeof' '(' TYPE_NAME ')'
	;

/*
 *   Lexer Rules
 */

// Whitespace
WHITESPACE: ' '|'\t'|'\r';
NEWLINE: '\n';

// Literals
INTEGER: [0-9]+;
DECIMAL: [0-9]+ '.' [0-9]+;
STRING: '"' ( '\\\\' | '\\"' | ~'"' ) '"';

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
TYPE_NAME
	: SIMPLE_NAME_CHARACTER COMPLEX_NAME_CHARACTER*
	;
MEMBER_NAME
	: SIMPLE_NAME_CHARACTER COMPLEX_NAME_CHARACTER*
	;
VARIABLE_NAME
	: SIMPLE_NAME_CHARACTER COMPLEX_NAME_CHARACTER*
	;

// Modifiers
ACCESS
	: 'public'
	| 'private'
	| 'protected'
	;
USAGE
	: 'static'
	| 'abstract'
	| 'virtual'
	| 'override'
	;
MODIFIER
	: ACCESS | USAGE
	;
PARAMETER_MODIFIER
	: 'in'|'out'|'ref'
	;
