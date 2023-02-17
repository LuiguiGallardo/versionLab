CREATE DATABASE versionLabDb2
GO
USE versionLabDb
GO

-- Main tables
CREATE TABLE Users(
	userId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FirstName VARCHAR (25) NOT NULL,
	LastName VARCHAR (25) NOT NULL,
    email VARCHAR (25) NOT NULL
)

CREATE TABLE Projects(
	projectId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	projectName VARCHAR(50) NOT NULL
)

CREATE TABLE Documents(
    documentId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    locationUrl VARCHAR(100)
)

-- Intermediate tables
CREATE TABLE UserProject(
	userProjectId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	userId INT NOT NULL,
	projectId INT NOT NULL,
	CONSTRAINT FK_Users_UserProject FOREIGN KEY (userId) REFERENCES Users(userId),
	CONSTRAINT FK_Projects_UserProject FOREIGN KEY (projectId) REFERENCES Projects(projectId)
)

CREATE TABLE ProjectDocument(
	projectDocumentId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	projectId INT NOT NULL,
	documentId INT NOT NULL,
	CONSTRAINT FK_Projects_ProjectDocument FOREIGN KEY (projectId) REFERENCES Projects(projectId),
	CONSTRAINT FK_Documents_ProjectDocument FOREIGN KEY (documentId) REFERENCES Documents(documentId)
)

INSERT INTO	Users VALUES ('Luigui', 'Gallardo', 'bfllg77@gmail.com');

SELECT * FROM Users;