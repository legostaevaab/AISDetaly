CREATE TABLE [dbo].[Details] (
[code] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
[name] [varchar] (50) NOT NULL ,
[material] [varchar] (50) NOT NULL ,
[color] [varchar] (50) NULL ,
[other] [varchar] (50) NULL ,
[size] [varchar] (50) NOT NULL ,
[provider_code] [int] NOT NULL FOREIGN KEY REFERENCES dbo.Providers(code),
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Providers] (
[code] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
[name] [varchar] (50) NOT NULL ,
[employee] [varchar] (50) NOT NULL ,
[city] [varchar] (50) NOT NULL ,
[phone] [varchar] (50) NOT NULL ,

) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Supplies] (
[code] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
[delivery_date] [date] NOT NULL ,
[detail_code] [int] NOT NULL FOREIGN KEY REFERENCES dbo.Details(code),
[provider_code] [int] NOT NULL  FOREIGN KEY REFERENCES dbo.Providers(code),
[price] [money] NOT NULL ,
[count] [int] NOT NULL ,
[amount] [money] NOT NULL ,
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Leftovers] (
[detail_code] [int] NOT NULL FOREIGN KEY REFERENCES dbo.Details(code),
[count] [int] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users] (
[login] [varchar] (50) NOT NULL PRIMARY KEY,
[password] [varchar] (50) NOT NULL ,
) ON [PRIMARY]
GO

GO
----Details----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------—
insert into [dbo].[Details] values('Карабин пластмассовый','пластмасса','черный', NULL, '25/40мм', 2)
insert into [dbo].[Details] values('Карабин металл','металл','никель', NULL, '55мм', 1)
insert into [dbo].[Details] values('Пряжка металл','металл','никель', NULL, '24мм', 1)
insert into [dbo].[Details] values('Полукольцо металл','металл','чёрный никель', NULL, '15мм', 1)
insert into [dbo].[Details] values('Резинка вязаная','40% полиэстер, 60% латекс','черный', 'плотность 6,71г/м, растяжимость 124%, стирка 80С', '9мм', 3)
insert into [dbo].[Details] values('Контактная лента','нейлон','синий', 'жесткая', '20мм', 3)
insert into [dbo].[Details] values('Тесьма вьюнок','полиэстер','розовый неоновый', NULL, '4мм', 3)
insert into [dbo].[Details] values('Цепочка декоративная','металл','золото', 'панцирное плетение "дутая"', '19мм', 1)
go


----Providers----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------—
insert into [dbo].[Providers] values('Компания по производству металлической фурнитуры','Иванов Иван Иванович','Москва','89123456789')
insert into [dbo].[Providers] values('Компания по производству пластиковой фурнитуры','Петров Пётр Петрович','Новокузнецк','89987654321')
insert into [dbo].[Providers] values('Компания по производству текстильной фурнитуры','Мосова Мария Михайловна','Лабытнанги','89794613852')
go


----Supplies----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------—
insert into Supplies values('02-19-2023',1,1,14.85,500,14.85*500)
insert into Supplies values('02-19-2023',2,1,129.49,700,129.49*700)
insert into Supplies values('02-19-2023',8,1,1326.28,10,1326.28*10)
go
----Leftovers----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------—
insert into Leftovers values(1,500)
insert into Leftovers values(1,700)
insert into Leftovers values(1,0)
insert into Leftovers values(1,0)
insert into Leftovers values(1,0)
insert into Leftovers values(1,0)
insert into Leftovers values(1,0)
insert into Leftovers values(1,10)

go
