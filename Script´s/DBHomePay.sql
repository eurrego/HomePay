create database DBHomePay

go

use DBHomePay

go

create table Usuario
(
 IdUsuario int constraint PK_Usuario primary key identity not null,
 Nombre varchar(50) not null,
 Apellidos varchar (50) not null,
 NickName varchar (30) not null,
 Pass varchar (50) not null,
 Email varchar (50) not null
)

create table SubCategoria
(
IdSubCategoria int constraint PK_SubCategoria primary key identity not null,
Nombre Varchar (50) not null,
)


create table Categoria
(
IdCategoria int constraint PK_Cateforia primary key identity not null,
Nombre varchar (50) not null,
Tipo char(1) not null,
IdSubCategoria int constraint FK_Categoria_SubCategoria foreign key references Subcategoria(IdSubCategoria)
)

create table Ingresos
(
IdIngresos int constraint PK_Ingresos primary key identity not null,
Valor money not null,
Fecha Date not null, 
Persona varchar (50) not null,
IdCategoria int constraint FK_Ingresos_Categoria foreign key references Categoria(IdCategoria)
)

alter table Ingresos
modify Valor float;

ALTER TABLE Ingresos
alter column Valor money;