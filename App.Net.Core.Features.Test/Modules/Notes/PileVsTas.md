Différence clé entre Tas et Pile
La pile (ou Stack) est utilisé pour l’allocation de mémoire statique et le tas(ou Heap) pour l’allocation de mémoire dynamique, 
les deux se trouvent dans la RAM de l’ordinateur.
 
 
Les variables allouées sur la pile sont stockées directement dans la mémoire. 
L’accès à cette mémoire est très rapide. Cette allocation est traitée lors de la compilation du programme. 
Lorsqu’une fonction ou une méthode appelle une autre fonction qui à son tour appelle une autre fonction, etc., 
l’exécution de toutes ces fonctions reste suspendue jusqu’à ce que la dernière fonction renvoie sa valeur. 
La pile est toujours réservée dans un ordre LIFO, le dernier bloc réservé est toujours le prochain bloc à libérer. 
Cela rend très simple le suivi de la pile, libérer un bloc de la pile n’est rien d’autre que l’ajustement d’un pointeur.

La mémoire est allouée aux variables affectées au segment de mémoire au moment de l’exécution et l’accès à cette mémoire est un peu plus lent, 
mais la taille de segment de mémoire n’est limitée que par la taille de la mémoire virtuelle. 
Les éléments du tas n’ont aucune dépendance les uns avec les autres et peuvent toujours être consultés de manière aléatoire à tout moment. 
Vous pouvez allouer un bloc à tout moment et le libérer à tout moment. 
Cela rend beaucoup plus complexe le suivi des parties du tas allouées ou libres à un moment donné.

Dans une situation multi-thread, chaque thread aura sa propre pile complètement indépendante mais partagera le tas. 
La pile est spécifique au thread et le tas est spécifique à l’application. 
La pile est importante à prendre en compte dans la gestion des exceptions et les exécutions de threads.

La pile (STACK)

Accès très rapide
Ne devez pas explicitement désallouer des variables
L’espace est géré efficacement par le processeur, la mémoire ne sera pas fragmentée
Variables locales uniquement.
Limite sur la taille de la pile (dépend du système d’exploitation).
Les variables ne peuvent pas être redimensionnées.
 
Le tas (HEAP)

Les variables sont accessibles globalement
Pas de limite sur la taille de la mémoire
Accès (relativement) plus lent
L’utilisation efficace de l’espace n’est pas garantie, la mémoire peut se fragmenter dans le temps lorsque des blocs de mémoire sont alloués, puis libérés
Vous devez gérer la mémoire (vous êtes responsable de l’allocation et de la libération des variables)
Les variables peuvent être redimensionnées en utilisant realloc()