user('ricardo',1,1,2).
user('nuno',0,2,2).
user('tiago',0,3,2).
user('mauro',2,1,1).
user('gisela',2,2,2).
user('rui',1,2,1).
user('paula',1,2,3).

amigo('ricardo','nuno').
amigo('nuno','ricardo').
amigo('ricardo','tiago').
amigo('tiago','ricardo').
amigo('tiago','mauro').
amigo('mauro','tiago').
amigo('tiago','gisela').
amigo('gisela','tiago').
amigo('gisela','nuno').
amigo('nuno','gisela').
amigo('rui','gisela').
amigo('gisela','rui').
amigo('rui','paula').
amigo('paula','rui').


determinar_tamanho_rede(U,R):-findall(X,amigo(U,X),L1), length(L1,R1), 
		determina_amigos_amigos(L1,[],R2), R is R1+R2.

determina_amigos_amigos([],L,R):-EliminaRepetidos(L,L1), length(L1,R).
determina_amigos_amigos([X|L],LA,R):- not member(X,LA)
		,!,findall(E,amigo(X,E),L1), write(L1),append(L1,[X|LA],LR),
		determina_amigos_amigos(L,LR,R1), R is R1+1.
determina_amigos_amigos([_|L],LA,R):-determina_amigos_amigos(L,LA,R).

EliminaRepetidos(L,L1):-EliminaRep(L,[],L1).
EliminaRep([],L,L).
EliminaRep([X|L],LA,L1):-member(X,LA),!,EliminaRep(L,LA,L1).
EliminaRep([X|L],LA,L1):-EliminaRep(L,[X|LA],L1)-