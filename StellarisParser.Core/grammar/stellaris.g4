grammar stellaris;

content: 
   expr+
   ;

expr: 
   keyval+
   ;

keyval:
   key ('=' | '>' | '<')+ val
   ;

key: 
   id | attrib
   ;

val: 
   id | attrib | group
   ;

attrib: 
   accessor (attrib| id)
   ;

accessor: 
   '.'|'@'|':'
   ;

group: 
   '{' (expr* | id*) '}'
   ;

id: 
   IDENTIFIER | STRING | INTEGER | DOUBLE
   ;

IDENTIFIER: 
   IDENITIFIERHEAD IDENITIFIERBODY*
   ;

DOUBLE:
   [+-]? INTEGERFRAG PT INTEGERFRAG
   ;

INTEGER: 
   [+-]? INTEGERFRAG
   ;

PT : '.';

fragment INTEGERFRAG: 
   [0-9]+
   ;

fragment IDENITIFIERHEAD: 
   [a-zA-Z]
   ;

fragment IDENITIFIERBODY
   : IDENITIFIERHEAD | [0-9_]
   ;

STRING: 
   '"' ~["\r\n]* '"'
   ;

COMMENT: 
   '#' ~[\r\n]* -> channel(HIDDEN)
   ;

SPACE: 
   [ \t\f] -> channel(HIDDEN)
   ;

NL: 
   [\r\n] -> channel(HIDDEN)
   ;


