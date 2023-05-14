--Restricciones en SQL Server: SQL NOT NULL, UNIQUE y SQL PRIMARY KEY
--sqlshack.com/es/restricciones-en-sql-server-sql-not-null-unique-y-sql-primary-key/
--https://www.youtube.com/watch?v=5hQuxKAF7fQ
Create table TUsuario(
	cNif CHAR(9) NOT NULL PRIMARY KEY,
	cUserlogin VARCHAR(30),
	cPassword VARCHAR(30),
	cNombre_Usuario VARCHAR(30),
	cApellidos_Usuario VARCHAR(30),
	cEmail_usuario VARCHAR(30),
	cTelefono_usuario VARCHAR(30),
	cDireccion_Usuario VARCHAR(30),
	dNacimiento_Usuario DATE,
	dAlta_Usuario DATE
);

Create table TLista(
	cSignatura VARCHAR(15) NOT NULL PRIMARY KEY,
	cNombre_Lista VARCHAR(30),
	dLista DATE ,
	cNif CHAR(9),
	FOREIGN KEY (cNif) REFERENCES TUsuario(cNif)
);

Create table TCancion(
	nCancionID INT NOT NULL PRIMARY KEY,
	cTitulo VARCHAR(100),
	nAnyoPublicacion INT,
	cGeneroMusical VARCHAR(30),
	nValoracion INT ,
	cComentario VARCHAR(50),
	lEscuchado BIT  --no existe boolean en sqlserver
);

Create table TAutor(
	nAutorID INT NOT NULL PRIMARY KEY,
	cNombre_Autor CHAR(30),
	cApellidos_Autor CHAR(30)
);

Create table TCancionAutor(
	nCancionID INT,
	nAutorID INT,
	PRIMARY KEY (nCancionID, nAutorID),
	FOREIGN KEY (nCancionID) REFERENCES TCancion(nCancionID),
	FOREIGN KEY (nAutorID) REFERENCES TAutor(nAutorID)
);

Create table TListaCancion(
	cSignatura VARCHAR(15),
	nCancionID INT,
	PRIMARY KEY (cSignatura, nCancionID),
	FOREIGN KEY (cSignatura) REFERENCES TLista(cSignatura),
	FOREIGN KEY (nCancionID) REFERENCES TCancion(nCancionID)
);

INSERT INTO TUsuario
	(cNif,cUserlogin,cPassword,cNombre_Usuario,cApellidos_Usuario,cEmail_usuario,cTelefono_usuario,cDireccion_Usuario,dNacimiento_Usuario,dAlta_Usuario)
VALUES
	('11771243X','eli1980','guapa99','Elisa','Garcia','elisa@spotify.com','657541214','C/Brunete','1980-01-12','2022-01-01'),
	('11230149P','mercenario2021','armas2021','Manuel','Guardiola','guardi@spotify.com','665541278','C/Gol','1985-12-23','2022-02-02'),
	('11114521S','superpaco','carapan2022','Francisco','Montelivo','francis@spotify.com','657541214','C/Italia','1995-10-06','2022-03-03');

INSERT INTO TAutor
	(nAutorID,cNombre_Autor,cApellidos_Autor)
VALUES
	(1,'Wig Wan',NULL),
	(2,'Foxy','Shazam'),
	(3,'Nashville','Pussy'),
	(4,'Y&T',NULL),
	(5,'The Poodles',NULL),
	(6,'The Quireboys',NULL),
	(7,'The Cruel Intentions',NULL),
	(8,'Firehouse',NULL),
	(9,'Santa Cruz',NULL),
	(10,'Dust Bowl','Jokies'),
	(11,'John','Murphy'),
	(12,'Ralph','Saenz'),
	(13,'Enemies Swe',NULL),
	(14,'Sister',NULL),
	(15,'Kissin','Dynamite'),
	(16,'Bang','Camaro'),
	(17,'Vain',NULL),
	(18,'Pretty','Boy Floyd'),
	(19,'The Cruel Intentions',NULL),
	(20,'Faster','Pussycat'),
	(21,'Sister','Sin'),
	(22,'Hanoi Rocks',NULL),
	(23,'House Of Lords',NULL),
	(24,'Dynazty',NULL),
	(25,'Enuff','ZNuff'),
	(26,'Reckless Love',NULL),
	(27,'Tigertaliz',NULL),
	(28,'Steel Panther',NULL),
	(29,'Lita','Ford'),
	(30,'Kix',NULL),
	(31,'John','Cena'),
	(32,'Hellacopters',NULL),
	(33,'Hardline',NULL),
	(34,'Hardcore Superstar',NULL),
	(35,'Bang','Camaro'),
	(36,'Dogs D´Amour',NULL),
	(37,'Desconocido',NULL),
	(38,'50 Cents',NULL),
	(39,'Justin','Timberlake'),
	(40,'Bon Jovi',NULL),
	(41,'M-Clan',NULL),
	(42,'Leiva',NULL),
	(43,'David','Bisbal'),
	(44,'Aitana',NULL),
	(45,'Melendi',NULL),
	(46,'Vanesa','Martin');

INSERT INTO TLista
	(cSignatura,cNombre_Lista,dLista,cNif)
VALUES
	('9780679405139','Lista1_Canciones_Favoritas','2022-01-01','11771243X'),
	('9780679405140','Lista2_Musica_variada','2022-01-01','11771243X'),
	('9780679405141','Mi_Lista1_music','2022-02-02','11230149P'),
	('9780679405142','ListaTop2022','2022-03-05','11114521S');


INSERT INTO TCancion
	(nCancionID,cTitulo,nAnyoPublicacion,cGeneroMusical,nValoracion,cComentario,lEscuchado)
VALUES
	(1,'Do Ya Wanna Taste It',2022,'Rock',10,'Grabado en estudio',0),
	(2,'Welcome To The Church Of Rock And Roll',2022,'Rock',9,'Grabado en estudio',0),
	(3,'Come On Come On',2022,'Rock',10,'Grabado en estudio',0),
	(4,'Summertime Girls',2022,'Rock',8,'Grabado en estudio',0),
	(5,'Night Of Passion',2022,'Rock',10,'Grabado en estudio',0),
	(6,'I Dont Love You Anymore',2022,'Rock',7,'Grabado en estudio',0),
	(7,'Love Bomb Baby',2022,'Rock',10,'Grabado en estudio',0),
	(8,'7 OClock',2022,'Rock',10,'Grabado en estudio',0),
	(9,'Borderline Crazy',2022,'Rock',10,'Grabado en estudio',0),
	(10,'Dont Treat Me Bad',2022,'Rock',10,'Grabado en estudio',0),
	(11,'Drag Me Down',2022,'Rock',10,'Grabado en estudio',0),
	(12,'Boots on Rocks Off',2022,'Rock',10,'Grabado en estudio',0),
	(13,'Pumped Up Kicks',2022,'Rock',10,'Grabado en estudio',0),
	(14,'Powertrain',2022,'Rock',10,'Grabado en estudio',0),
	(15,'Would You Love a Creature',2022,'Rock',10,'Grabado en estudio',0),
	(16,'Six Feet Under',2022,'Rock',10,'Grabado en estudio',0),
	(17,'Push Push (Lady Lightning)',2022,'Rock',10,'Grabado en estudio',0),
	(18,'Beat the Bullet',2022,'Rock',10,'Grabado en estudio',0),
	(19,'I Wanna Be With You',2022,'Rock',10,'Grabado en estudio',0),
	(20,'Jawbreaker',2022,'Rock',10,'Grabado en estudio',0),
	(21,'House of Pain',2022,'Rock',10,'Grabado en estudio',0),
	(22,'Ayo technology',2008,'Hip Hop',10,'RadioEdit',0),
	(23,'In Da Club',2008,'Hip Hop',10,'RadioEdit',0),
	(24,'Bounce',2002,'Rock',8,'Grabado en estudio',0),
	(25,'Have a Nice Day',2005,'Rock',8,'Grabado en estudio',0),
	(26,'Its My Life',2005,'Rock',10,'Grabado en estudio',0),
	(27,'Concierto Salvaje',2016,'Rock',10,'Grabado en estudio',0),
	(28,'La Lluvia en los Zapatos',2016,'Rock',10,'Grabado en estudio',0),
	(29,'Si tu la quieres',2020,'Pop',8,'Grabado en estudio',0),
	(30,'Un violinista en tu tejado',2008,'Pop',9,'Grabado en estudio',0),
	(31,'Arrancame',2012,'Pop',10,'Grabado en estudio',0);

INSERT INTO TCancionAutor
	(nCancionID,nAutorID)
VALUES
	(1,1),
	(2,2),
	(3,3),
	(4,4),
	(5,5),
	(6,6),
	(7,27),
	(8,6),
	(9,7),
	(10,8),
	(11,9),
	(12,10),
	(13,11),
	(13,12),
	(14,13),
	(15,14),
	(16,15),
	(17,16),
	(18,17),
	(19,18),
	(20,19),
	(21,20),
	(22,38),
	(22,39),
	(23,38),
	(24,40),
	(25,40),
	(26,40),
	(27,41),
	(28,42),
	(29,43),
	(29,44),
	(30,45),
	(31,46);

	INSERT INTO TListaCancion
	(cSignatura,nCancionID)
	VALUES
	('9780679405139',1),
	('9780679405139',13),
	('9780679405139',16),
	('9780679405139',24),
	('9780679405139',25),
	('9780679405139',26),
	('9780679405139',17),
	('9780679405140',2),
	('9780679405140',3),
	('9780679405140',5),
	('9780679405140',31),
	('9780679405140',9),
	('9780679405140',10),
	('9780679405141',1),
	('9780679405141',20),
	('9780679405141',19),
	('9780679405141',21),
	('9780679405141',23),
	('9780679405141',3),
	('9780679405141',9),
	('9780679405141',28),
	('9780679405142',3),
	('9780679405142',5),
	('9780679405142',7),
	('9780679405142',24),
	('9780679405142',27),
	('9780679405142',9),
	('9780679405142',13),
	('9780679405142',14),
	('9780679405142',15),
	('9780679405142',19),
	('9780679405142',20),
	('9780679405142',29),
	('9780679405142',22);