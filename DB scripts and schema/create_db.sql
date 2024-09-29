CREATE TABLE Providers (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Nit NVARCHAR(50) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PersonalizedFieldsJson NVARCHAR(MAX) NULL
);

CREATE TABLE Countries (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(5) NOT NULL,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Services (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    PriceByHour DECIMAL(18,2) NOT NULL,
    ProviderId BIGINT NOT NULL,
    FOREIGN KEY (ProviderId) REFERENCES Providers(Id) ON DELETE CASCADE
);

-- Tabla de relación muchos a muchos entre Services y Countries
CREATE TABLE CountryServices (
    CountryId BIGINT NOT NULL,
    ServiceId BIGINT NOT NULL,
    PRIMARY KEY (CountryId, ServiceId),
    FOREIGN KEY (CountryId) REFERENCES Countries(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceId) REFERENCES Services(Id) ON DELETE CASCADE
);
