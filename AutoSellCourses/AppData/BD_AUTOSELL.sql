BEGIN 
    IF DB_ID(N'AutoSell') IS NULL BEGIN
    CREATE DATABASE [AutoSell]
    END ELSE 
    PRINT N'Базы данных [Autosell] уже существует'
END
GO
BEGIN
USE [AutoSell]

	IF OBJECT_ID(N'Clients') IS NULL BEGIN
		CREATE TABLE Clients (
		[client_id] INT IDENTITY NOT NULL,
		[client_last_name] NVARCHAR(50) NOT NULL,
		[client_name] NVARCHAR(50),
		[client_middle_name] NVARCHAR (50) NOT NULL,
		[client_town] NVARCHAR(60) NOT NULL,
		[client_address] NVARCHAR (150) NOT NULL, 
		[client_number] NVARCHAR (20) NOT NULL,

		CONSTRAINT [PK_client_id] PRIMARY KEY ([client_id])
		)
		END ELSE
		PRINT (N'Таблица под именем [Clients] уже существует')

	IF OBJECT_ID(N'Dillers') IS NULL BEGIN
		CREATE TABLE Dillers (
		[dillers_id] INT IDENTITY(1,1) NOT NULL,
		[dillers_last_name] NVARCHAR(50) NOT NULL,
		[dillers_name] NVARCHAR(50),
		[dillers_middle_name] NVARCHAR (50) NOT NULL,
		[dillers_town] NVARCHAR(60) NOT NULL,
		[dillers_address] NVARCHAR (150) NOT NULL, 
		[dillers_number] NVARCHAR (20) NOT NULL,



		CONSTRAINT [PK_dillers_id] PRIMARY KEY ([dillers_id])
		)
		END ELSE 
		PRINT (N'Таблица под именем [Dillers] уже существует')


	IF OBJECT_ID(N'Contracts') IS NULL BEGIN
		CREATE TABLE Contracts (
		[contract_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
			[client_id] INT NOT NULL,  
			[diller_id] INT NOT NULL,
			[contract_date] DATE NOT NULL,
			[car_brand] NVARCHAR(50) NOT NULL,
			[car_photo] NVARCHAR(MAX), 
			[manufacture_date] DATE NOT NULL,
			[mileage] INT NOT NULL,
			[sale_date] DATE,
			[sale_price] DECIMAL(10, 2) NOT NULL,
			[commission] DECIMAL(10, 2) NOT NULL,
			[notes] NVARCHAR(500),
    
   
			CONSTRAINT FK_Contracts_Clients FOREIGN KEY ([client_id]) 
				REFERENCES Clients([client_id]) 
				ON DELETE CASCADE,  
    
			CONSTRAINT FK_Contracts_Dillers FOREIGN KEY ([diller_id]) 
				REFERENCES Dillers([dillers_id]) 
				ON DELETE NO ACTION  
		)
		END ELSE 
		PRINT(N'Таблица под именем [Contracts] уже существует')


	IF OBJECT_ID(N'ClientCars') IS NULL BEGIN
		CREATE TABLE ClientCars (
		[clientcar_id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		[client_id] INT NOT NULL,
		[car_brand] NVARCHAR(70) NOT NULL,
		[mileage] INT NOT NULL,
		[manufacture_date] DATE NOT NULL,
		[description] NVARCHAR(250) NOT NULL,
		[color_car] NVARCHAR(50) NOT NULL,
		[transmission_type] NVARCHAR(20) NOT NULL,
		[engine_capacity] NVARCHAR (50) NOT NULL,
		[price] MONEY NOT NULL

		CONSTRAINT FK_ClientsCar FOREIGN KEY ([client_id])
				REFERENCES Clients([client_id])

		CONSTRAINT [CK_ClientCarsPrice] CHECK(price >= 0) 
		)
		END ELSE
		PRINT (N'Таблица под именем [ClientCars] уже существует')
END
GO

			
BEGIN
INSERT INTO Clients 
    ([client_last_name], [client_name], [client_middle_name], [client_town], [client_address], [client_number])
VALUES
    (N'Иванов', N'Иван', N'Иванович', N'Москва', N'ул. Ленина, д. 10, кв. 25', '+7 (495) 123-45-67'),
    (N'Петрова', N'Мария', N'Сергеевна', N'Санкт-Петербург', N'Невский пр-т, д. 15', N'+7 (812) 987-65-43'),
    (N'Сидоров', N'Алексей', N'Владимирович', N'Казань', N'ул. Баумана, д. 5, кв. 12', N'+7 (843) 555-12-34'),
    (N'Кузнецова', N'Елена', N'Дмитриевна', N'Новосибирск', N'ул. Кирова, д. 33', N'+7 (383) 234-56-78'),
    (N'Фёдоров', N'Дмитрий', N'Алексеевич', N'Екатеринбург', N'ул. Малышева, д. 12, кв. 7', N'+7 (343) 876-54-32');
	
	
INSERT INTO ClientCars
    ([client_id], [car_brand], [mileage], [manufacture_date], [description], [color_car], [transmission_type], [engine_capacity], [price])
VALUES 
    (1, N'Toyota Camry', 45000, '2018-05-15', N'Превосходное состояние, один владелец, полная сервисная история, кожаный салон, подогрев сидений, мультимедийная система с навигацией', N'Бежевый', N'Автомат', N'2.0 л (150 л.с.)', 1850000),
    (1, N'BMW X5', 120000, '2015-11-20', N'Комплектация Luxury, полный привод, пакет M Sport, камера 360°, панорамная крыша, безаварийная история', N'Чёрный', N'Автомат', N'2.0 л (150 л.с.)', 3200000),
    (2, N'Hyundai Solaris', 78000, '2017-03-10', N'Экономичный вариант, кондиционер, ABS, ESP, легкий в управлении, идеальный первый автомобиль', N'Белый', N'Механика', N'2.0 л (150 л.с.)', 950000),
    (3, N'Kia Rio', 35000, '2020-08-05', N'Новый автомобиль с салона, гарантия до 2025 года, мультируль, датчики света и дождя, система контроля давления в шинах', N'Красный', N'Автомат', N'2.0 л (150 л.с.)', 1250000),
    (4, N'Volkswagen Tiguan', 92000, '2019-07-22', N'Премиум комплектация, адаптивный круиз-контроль, парктроник, подогрев руля и сидений, система мониторинга слепых зон', N'Синий', N'Автомат', N'2.0 л (150 л.с.)', 2100000),
    (5, N'Lada Vesta', 15000, '2022-01-10', N'Практически новый автомобиль, полная комплектация, мультимедиа с Apple CarPlay/Android Auto, гарантия производителя', N'Серый', N'Механика', N'2.0 л (150 л.с.)', 850000),
    (5, N'Mercedes-Benz E-Class', 65000, '2018-09-30', N'Элитный седан, пакет AMG, кожаный салон премиум-класса, система ночного видения, массаж сидений, биксеноновые фары', N'Белый', N'Автомат', N'2.0 л (150 л.с.)', 3800000);
END
GO
SELECT	car.clientcar_id as [Id],	
		c.client_name as [Client],
		c.client_number as [Client_number],
		car.car_brand as [CarBrand],
		FORMAT(car.mileage, N'0 км') as [Mileage],
		FORMAT(car.manufacture_date, N'yyyy г.') as [ManufactureDate],
		car.description as [Description],
		car.color_car as [CarsColor],
		car.transmission_type as [TransmissionType],
		car.engine_capacity as [EngineCapacity],
		FORMAT(car.price, N'C', N'ru-RU') as [Price]
		FROM [ClientCars] as [car]
		JOIN [Clients] as [c] 
					ON [car].[client_id] = [c].[client_id]

SELECT c.client_id,
		c.client_last_name,
		c.client_name,
		c.client_middle_name,
		c.client_number,
		c.client_town,
		c.client_address
		FROM [Clients] as c


--USE master; 
--GO
--DATABASE AutoSell SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--GO

--DROP DATABASE AutoSell;
--GO