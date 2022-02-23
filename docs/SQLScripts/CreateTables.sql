USE CPHAirport

DROP TABLE OtherRouteOperated
DROP TABLE FlightRoute
DROP TABLE Airport
DROP TABLE Company

CREATE TABLE Airport (
	iATA varchar(3) NOT NULL PRIMARY KEY,
	name varchar(255) NOT NULL
)

CREATE TABLE Company (
	id int IDENTITY(1,1) PRIMARY KEY, 
	name varchar(255) NOT NULL
)


CREATE TABLE FlightRoute(
	ownerID int NOT NULL FOREIGN KEY REFERENCES Company(id),
	airport_fk varchar(3) NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Airport(iATA)
)

CREATE TABLE OtherRouteOperated(
	companyID int NOT NULL FOREIGN KEY REFERENCES Company(id),
	flightRoute_airport_fk varchar(3) FOREIGN KEY REFERENCES FlightRoute(airport_fk) NOT NULL,
	CONSTRAINT PK_OtherRouteOperated PRIMARY KEY (companyID, flightRoute_airport_fk)
)