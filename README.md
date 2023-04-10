# Copia_AngryBirds_PruebaTecnica
Se esta realizando una copia del juego Angry Birds y se desea agregar algunas funcionalidades extras al proyecto.

1- Se generó una escena nueva para la seleccion de las aves, se editó una imagen de Background para el fondo, se agregó un botón de Play para pasar al game y 3 zonas de boton para la seleccion de las 3 aves.
	Refactorizacion de Codigo (RC): para comenzar con la correccion y modificacion de código primero se ordenaron los componentes de los scripts porque estaban todos mal ubicados. se subieron todas las variables al principio y se ordenaron las funciones principales  como Awake, Start, Update y demas.

2- Se generó el script "PlayerSelection" para gestionar los personajes elegidos en la escena de eleccion inicial y poder enviar los datos a la escena de Game. Tambien se generó el script de "SceneManager" para el boton de Play de la misma escena.
En el script "GameManager" se agregó en el método Start la instanciación de los 3 aves seleccionadas anteriormente y se quitó del Inspector los 3 gameobjects que habian fijos.
Se revisaron los script nuevamente y además de implemantar el correspondiente responsive en la escena generada para la seleccion de aves, tambien se refactorizaron los scripts "SlingShot", "ParallaxScrolling"  y "GameManager" que contenian instrucciones obsoletas y tambien lineas inecesarias. Se marcaron los cambios en script con comentario //.