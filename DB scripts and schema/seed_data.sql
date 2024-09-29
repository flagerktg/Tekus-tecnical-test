-- Insertar datos en Providers
INSERT INTO Providers (Nit, Name, Email, PersonalizedFieldsJson) VALUES
('900123456-7', 'Provider 1', 'provider1@example.com', '{"contact": "John Doe"}'),
('900223456-8', 'Provider 2', 'provider2@example.com', '{"contact": "Jane Smith"}'),
('900323456-9', 'Provider 3', 'provider3@example.com', '{"contact": "James Brown"}'),
('900423456-0', 'Provider 4', 'provider4@example.com', '{"contact": "Emily Davis"}'),
('900523456-1', 'Provider 5', 'provider5@example.com', '{"contact": "Michael Johnson"}'),
('900623456-2', 'Provider 6', 'provider6@example.com', '{"contact": "Robert Wilson"}'),
('900723456-3', 'Provider 7', 'provider7@example.com', '{"contact": "William Taylor"}'),
('900823456-4', 'Provider 8', 'provider8@example.com', '{"contact": "Patricia Anderson"}'),
('900923456-5', 'Provider 9', 'provider9@example.com', '{"contact": "Linda Martinez"}'),
('901023456-6', 'Provider 10', 'provider10@example.com', '{"contact": "Christopher Harris"}');

-- Insertar datos en Countries
INSERT INTO Countries (Code, Name) VALUES
('US', 'United States'),
('MX', 'Mexico'),
('CO', 'Colombia'),
('BR', 'Brazil'),
('AR', 'Argentina'),
('PE', 'Peru'),
('CL', 'Chile'),
('VE', 'Venezuela'),
('EC', 'Ecuador'),
('BO', 'Bolivia');

-- Insertar datos en Services
INSERT INTO Services (Name, PriceByHour, ProviderId) VALUES
('Service 1', 100.00, 1),
('Service 2', 150.00, 2),
('Service 3', 120.00, 3),
('Service 4', 110.00, 4),
('Service 5', 140.00, 5),
('Service 6', 130.00, 6),
('Service 7', 170.00, 7),
('Service 8', 160.00, 8),
('Service 9', 180.00, 9),
('Service 10', 190.00, 10);

-- Insertar datos en CountryServices
INSERT INTO CountryServices (CountryId, ServiceId) VALUES
(1, 1), (2, 1), (3, 2), (4, 2), (5, 3),
(6, 3), (7, 4), (8, 5), (9, 6), (10, 7),
(1, 8), (2, 9), (3, 10), (4, 9), (5, 8);
