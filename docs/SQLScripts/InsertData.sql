DELETE FROM FlightRoute;
DELETE FROM OtherRouteOperated;
DELETE FROM Company;
DELETE FROM Airport;

INSERT INTO Airport (iATA, name)
	VALUES 
		('CPH', 'Kastrup Lufthavn'),
		('LAX', 'Los Angeles International Airport'),
		('AAL', 'Ålborg Lufthavn'),
		('BEA', 'Berlin Airport'),
		('OSL', 'Oslo Airport'),
		('MCA', 'Moscow Airport'),
		('HSA', 'Helsinki Airport'),
		('BRA', 'Bristol Airport'),
		('BIA', 'Barcelona International Airport')



INSERT INTO Company (name) VALUES ('SAS'),('Fly Emirates'),('American Airlines'),('United Airlines'),('The Peoples Flight Company of China')

INSERT INTO FlightRoute (ownerID, airport_fk) VALUES
	(1, 'BIA'),
	(1, 'AAL'),	
	(1, 'OSL'),
	(3, 'LAX'),
	(3, 'BEA'),	
	(4, 'BRA'),
	(5, 'MCA'),
	(5, 'HSA')

INSERT INTO OtherRouteOperated (companyID, flightRoute_airport_fk) VALUES
	(1, 'BRA'),
	(1, 'MCA'),
	(2, 'LAX'),
	(2, 'BRA'),	
	(2, 'BIA'),
	(3, 'BRA'),
	(3, 'AAL'),
	(4, 'BEA'),
	(4, 'LAX'),	
	(5, 'BIA'),
	(5, 'BEA'),
	(5, 'OSL')