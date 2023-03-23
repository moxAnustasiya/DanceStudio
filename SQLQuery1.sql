/*CREATE TABLE Hall
(
    Id_Hall INT PRIMARY KEY,
    Name_Hall NVARCHAR(20) NOT NULL
)

INSERT INTO Hall(Id_Hall, Name_Hall)
VALUES (1, 'Белый зал');
INSERT INTO Hall(Id_Hall, Name_Hall)
VALUES (2, 'Красный зал');
INSERT INTO Hall(Id_Hall, Name_Hall)
VALUES (3, 'Зелёный зал');
INSERT INTO Hall(Id_Hall, Name_Hall)
VALUES (4, 'Жёлтый зал');
INSERT INTO Hall(Id_Hall, Name_Hall)
VALUES (5, 'Синий зал');
INSERT INTO Hall(Id_Hall, Name_Hall)
VALUES (6, 'Чёрный зал');*/

CREATE TABLE Client
(
    Id_Client INT PRIMARY KEY,
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
	Id_Trainer INT NOT NULL FOREIGN KEY REFERENCES Trainer(Id_Trainer) ON UPDATE CASCADE
)

/*CREATE TABLE Trainer
(
    Id_Trainer INT PRIMARY KEY,
    FirstName NVARCHAR(20) NOT NULL,
    Dance_Style NVARCHAR(20) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
	Id_Hall INT NOT NULL FOREIGN KEY REFERENCES Hall(Id_Hall) ON UPDATE CASCADE
)*/

CREATE TABLE Subscription
(
    Id_Subscription INT PRIMARY KEY,
    Price INT NOT NULL,
    Dance_Style NVARCHAR(20) NOT NULL
)

CREATE TABLE Payment_Accounting
(
	Id_Subscription INT NOT NULL FOREIGN KEY REFERENCES Subscription(Id_Subscription) ON UPDATE CASCADE,
	Id_Client INT NOT NULL FOREIGN KEY REFERENCES Client(Id_Client) ON UPDATE CASCADE,
    Month_now NVARCHAR(20) NOT NULL,
    Payment BIT NOT NULL
	PRIMARY KEY(Id_Subscription, Id_Client, Month_now)
)

/*INSERT INTO Client(Id_Client, FirstName, LastName, Phone, Id_Trainer)
VALUES (100, 'Анастасия', 'Мохова' , '89229037424',  10);*/



