# Opti_Struct

Le but de ce code est de fournir une solution pour créer automatiquement une structure d'optimisation. 
Selon le fichier txt, l'utilisateur peut décider de créer sa propre structure.
Le script lit dans les fichiers txt, les opérations souhaitées par l'utilisateur. 
Il existe plusieurs fichiers en fonction de la localisation, latéralité et/ou de la prescription 
(disponible dans le dossier \File) (ils seront amenés à évoluer ave le temps).


****************************************************************************************************************************
****************************************************************************************************************************
****************************************************************************************************************************

Pour le bon déroulé du code un vocabulaire spécifique doit être utilisé. Il respecte l'écriture suivante :

nom_structure_à_créer 		: 	nom_structure_1		 operateur	 nom_structure_2 	operateur 	nom_structure_3 	.....

Les ":" permettent de faire la différence entre le nom de la structre à créer (à gauche) des structures utilisée pour les opérations (à droite)

La partie operateur correspond aux opérations à effectuer. Il existe 6 opérateurs différents :


"|" pour la somme de deux structures

"&" pour récupérer uniquement l'intersection entre deux structures

"#" pour la soustraction entre deux structures

"^" pour récupérer uniquement l'union des structures

"!" pour créer une marge par rapport à une structure définie
    Pour l'utilisation de cet opérateur, la partie droite correspond à la marge en mm (exemple : PTV : ITV ! 5.00)

" " pour créer une structure vide


Une ligne contenant le mot verbose en premiere ligne de chaque fichier txt définit le niveau de verbosité; c-à-d le niveau de dialogue du code
Il existe deux niveaux dans la version V2.0.0.6 : 
	0 --> le code ne parle pas du tout (message de fin uniquement)
	1 --> le code parle et décrit chaque étape

Le code enregsitre ce qui se passe lors de son exécution dans un fichier "log.txt" peu importe le niveau de verbosité.


****************************************************************************************************************************
****************************************************************************************************************************
****************************************************************************************************************************

!!!! Attention, pour la V2.0.0.6, les noms après les ":" doivent être en minuscule !!
!!!! Le fichier txt ne doit pas être modifié durant l'exécution du script!!!


****************************************************************************************************************************
***************************************************** Exemple **************************************************************
****************************************************************************************************************************

// Marges


PTV : itv ! 5

ptv --> nom structure

itv --> structure 1

! --> opérateur pour les marges

5 --> 5.00 mm de marges isotropique [mm]

-------> Il créé la même structure avec 5 mm de marges




// Création structure vide

PTV : 

PTV --> nom structure qui sera créée vide

-------> Il créé une structure vide



// Avec deux structures


PTV - ITV : ptv # itv

PTV - ITV --> nom de la structure à créer

ptv --> structure 1 pour l'opération

"#" --> c'est l'opérateur (soustraction)

itv --> structure 2 pour l'opération

-------> Il créé ici une structure correspondante à la couronne entre le PTV et l'ITV



// Avec n structures

PTV - ITV : ptv # itv # gtv

PTV - ITV --> nom de la structure à créer

ptv --> structure 1 pour l'opération

"#" --> c'est l'opérateur (soustraction)

itv --> structure 2 pour l'opération

"#" --> c'est l'opérateur (soustraction)

gtv --> structure 3 pour l'opération

!!!!! Ici, les opérations sont réalisés dans l'ordre de lecture.

-------> Il créé une structure correspondant à l'enchaînement des opérations


****************************************************************************************************************************
****************************************************************************************************************************
****************************************************************************************************************************

##### Prochaine MAJ pour améliorer la casse des noms (plus de problèmes de majuscule ou minuscule; Expression régulière ; ...)  en cours ...
##### Amélioration de la détection des mauvais noms de structures et assignation à des structures existente de manière manuel et/ou automatique en cours ...
##### Marges asymétrique 
##### Création table et CE ?
##### problème sur les parotides en ORL


