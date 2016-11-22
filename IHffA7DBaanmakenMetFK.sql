﻿/* ---------------------------------------------------- */
/*  Generated by Enterprise Architect Version 12.0 		*/
/*  Created On : 18-nov-2016 07:17:47 				*/
/*  DBMS       : MySql 						*/
/* ---------------------------------------------------- */


/* Drop Tables */



/* Create Tables */

CREATE TABLE Wishlist
(
	id INT NOT NULL IDENTITY (1,1),
	totaalPrijs DECIMAL(10,2) 	 NULL,
	betaald BIT NOT NULL,
	CONSTRAINT PK_wishlist PRIMARY KEY (id)
)

;

CREATE TABLE WishlistItem
(
	id INT NOT NULL IDENTITY (1,1),
	wishlistId INT NOT NULL,
	activiteitId INT NOT NULL,
	aantalPersonen INT NOT NULL,
	CONSTRAINT PK_wishlistItem PRIMARY KEY (id)
)

;

CREATE TABLE Activiteit
(
	id INT NOT NULL IDENTITY (1,1),
	beginTijd DATETIME NOT NULL,
	eindTijd DATETIME 	 NULL,
	prijs DECIMAL(10,2) NOT NULL,
	CONSTRAINT PK_Activiteit PRIMARY KEY (id)
)

;

CREATE TABLE Film
(
	id INT NOT NULL IDENTITY (1,1),
	titel VARCHAR(50) NOT NULL,
	regiseur VARCHAR(50) NOT NULL,
	beschrijvingNL VARCHAR(50) NOT NULL,
	beschrijvingEN VARCHAR(50) 	 NULL,
	jaar DATETIME NOT NULL,
	CONSTRAINT PK_Film PRIMARY KEY (id)
)

;

CREATE TABLE Restaurant
(
	id INT NOT NULL IDENTITY (1,1),
	activiteitId INT 	 NULL,
	beschrijvingNL VARCHAR(50) 	 NULL,
	beschrijvingEN VARCHAR(50) 	 NULL,
	lunchStart DATETIME 	 NULL,
	lunchEind DATETIME 	 NULL,
	dinnerStart DATETIME 	 NULL,
	dinnerEind DATETIME 	 NULL,
	locatieId INT 	 NULL,
	CONSTRAINT PK_Restaurant PRIMARY KEY (id)
)

;

CREATE TABLE Filmvoorstelling
(
	id INT NOT NULL IDENTITY (1,1),
	activiteitId INT NOT NULL,
	filmId INT NOT NULL,
	maxAantal INT NOT NULL,
	locatieId INT NOT NULL,
	CONSTRAINT PK_Filmvoorstelling PRIMARY KEY (id)
)

;

CREATE TABLE Locatie
(
	id INT NOT NULL IDENTITY (1,1),
	naam VARCHAR(50) 	 NULL,
	straat VARCHAR(50) NOT NULL,
	postcode VARCHAR(50) NOT NULL,
	plaats VARCHAR(50) NOT NULL,
	gebouw VARCHAR(50) NOT NULL,
	CONSTRAINT PK_Locatie PRIMARY KEY (id)
)

;

CREATE TABLE Special
(
	id INT NOT NULL IDENTITY (1,1),
	activiteitId INT 	 NULL,
	titel VARCHAR(50) NOT NULL,
	spreker VARCHAR(50) NOT NULL,
	beschrijvingNL VARCHAR(50) NOT NULL,
	beschrijvingEN VARCHAR(50) 	 NULL,
	maxAantal VARCHAR(50) NOT NULL,
	locatieId INT NOT NULL,
	CONSTRAINT PK_Special PRIMARY KEY (id)
)

;

CREATE TABLE Reservering
(
	id INT 	 NOT NULL IDENTITY (1,1),
	wishlistId INT 	 NULL,
	naam VARCHAR(50) 	 NULL,
	email VARCHAR(50) 	 NULL,
	betaalmethode VARCHAR(50) 	 NULL
)

;

/* Create Indexes, Uniques, Checks */


/* Create Foreign Key Constraints */

ALTER TABLE WishlistItem 
 ADD CONSTRAINT FK_WishlistItem_Activiteit
	FOREIGN KEY (activiteitId) REFERENCES Activiteit (id) 
;

ALTER TABLE WishlistItem 
 ADD CONSTRAINT FK_WishlistItem_Wishlist
	FOREIGN KEY (wishlistId) REFERENCES Wishlist (id) 
;

ALTER TABLE Restaurant 
 ADD CONSTRAINT FK_Restaurant_Activiteit
	FOREIGN KEY (activiteitId) REFERENCES Activiteit (id) 
;

ALTER TABLE Restaurant 
 ADD CONSTRAINT FK_Restaurant_Locatie
	FOREIGN KEY (locatieId) REFERENCES Locatie (id) 
;

ALTER TABLE Filmvoorstelling 
 ADD CONSTRAINT FK_Filmvoorstelling_Activiteit
	FOREIGN KEY (activiteitId) REFERENCES Activiteit (id) 
;

ALTER TABLE Filmvoorstelling 
 ADD CONSTRAINT FK_Filmvoorstelling_Film
	FOREIGN KEY (filmId) REFERENCES Film (id) 
;

ALTER TABLE Filmvoorstelling 
 ADD CONSTRAINT FK_Filmvoorstelling_Locatie
	FOREIGN KEY (locatieId) REFERENCES Locatie (id) 
;

ALTER TABLE Special 
 ADD CONSTRAINT FK_Special_Activiteit
	FOREIGN KEY (activiteitId) REFERENCES Activiteit (id) 
;

ALTER TABLE Special 
 ADD CONSTRAINT FK_Special_Locatie
	FOREIGN KEY (locatieId) REFERENCES Locatie (id) 
;

ALTER TABLE Reservering 
 ADD CONSTRAINT FK_Reservering_Wishlist
	FOREIGN KEY (id) REFERENCES Wishlist (id) 
;
