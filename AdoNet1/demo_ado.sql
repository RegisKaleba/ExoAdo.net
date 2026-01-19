CREATE DATABASE demo_ado;

USE demo_ado;

CREATE TABLE Personne(
	id INT AUTO_INCREMENT,
    nom VARCHAR(100),
    prenom VARCHAR(100),
    age INT,
    email VARCHAR(255),
    PRIMARY KEY (id)
);