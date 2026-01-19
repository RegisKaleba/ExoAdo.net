CREATE DATABASE bibliotheque;
USE bibliotheque;

CREATE TABLE Livre (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Titre VARCHAR(255) NOT NULL,
    Auteur VARCHAR(255) NOT NULL,
    AnneePublication INT NOT NULL,
    Isbn VARCHAR(20) NOT NULL
);
