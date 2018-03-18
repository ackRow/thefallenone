# Player

* Script
```
Assets/Target.cs
Assets/Player/Scripts
```

* Animation

J'ai rigger le modèle 3d du personnage que Romain m'a fournis sur Blender.
Ensuite j'ai utilisé ce squelette pour enregistrer des animations pour le personnage (Marcher, Courir, Tirer, Mourir)

Pour le futur, il faudra ajouter encore quelques animations, par exemple pour s'accroupir.
* Mecanim

J'ai importé les différentes animations du fichier perso.blend dans unity puis j'ai utilisé leur gestionnaire d'animation
Mecanim afin de déclencher les animations au bon moment à l'aide de variable `float`, `bool` ou `trigger`
Ces variables sont modifiés dans les scripts gérant les mouvements du personnages notamment dans `PlayerMovement.cs`

Pour le futur, il faudra mieux faire la distinction entre certaine animation qui se joueront que en solo et d'autre en multi.

* Déplacement

Les déplacements du joueur sont gérés dans le fichier `PlayerMovement.cs`, dans la fonction `FixedUpdate()`
Je suis plutôt satisfait du résultat, en effet, j'ai modifié directement le vector velocity du rigidbody au lieu d'utiliser
la fonction AddForce (sauf pour le saut car plus réaliste), ce qui permet de se déplacer plus précisement.

Pour le futur, il faudra ajouter le mode de déplacement accroupit, ce qui demande de diminuer la taille de la hitbox directement
dans le script.

* Mise en réseau

Les principales fonctionnalités du joueur fonctionnent en multi joueur. Il y a parfois des comportements inattendus qu'il faudra
résoudre ( ex : l'animation de tir qui se joue 2 fois pour le host ).

* Héritage

Pour l'instant il n'y a pas d'héritage, mais je pense que la classe `Player.cs` comportant les variables principales du joueur
pourrait hérité de la classe `Target.cs` qui représente tous les objets du jeu qui ont de la vie et peuvent se faire tirer dessus.

# PNJ (player non joueur)

* Script
```
Assets/Target.cs
```

* Commencement

Globalement, tout ce que j'ai fait pour le joueur me servira pour le PNJ.
Pour l'instant, j'ai fait un PNJ "punching ball", qui ne peut que recevoir des coups et lancer des animations.

* A faire

Je compte lui faire une IA rudimentaire qui lui permettra idéalement d'aller vers le joueur et de lui tirer dessus.
