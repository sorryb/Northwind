---------------------------------------------------------------------------------------
--		Initializare
use Northwind;
go

--create procedure getImg @pathImage nvarchar(255)
--AS
--	declare	
--		@pathFolder nvarchar(255)=N'C:\Users\M4rYu5\Documents\GitHub\SQL_Translate\SQL_Scripts\Images',
--		@sql nvarchar(255);
--		set @sql=N'SELECT BulkColumn FROM Openrowset( Bulk '''+@pathFolder+@pathImage+''', Single_Blob) as img'
--exec sp_executesql @sql;
--go

---------------------------------------------------------------------------------------
--		Employees
alter table Employees
	drop column Extension,
		column Region
go

update Employees
Set LastName = 'Danciu', 
	FirstName = 'Nicoleta', 
	Title = 'Reprezentant Vanzari',
	TitleOfCourtesy = 'Dra.',
	[Address] = 'Spl. Independentei, nr. 27, ap 27, Bucuresti Sector 6',
	City = 'Bucuresti',
	PostalCode = '050082',
	Country = 'Romania',
	HomePhone = '0751292371',
	Notes = 'Are o diploma de licenta in psihologie de la Universitatea de Stat din Colorado in 1980. Ea a completat, de asemenea, "Arta apelului rece". Nicoleta este membru al Toastmasters International.'
where LastName = 'Davolio';

update Employees
Set LastName = 'Florea', 
	FirstName = 'Andrei', 
	Title = 'Vice Presedinte, Vanzari',
	TitleOfCourtesy = 'Dr.',
	[Address] = 'Str. Hornului, nr. 22, ap 3, Bucuresti Sector 4',
	City = 'Bucuresti',
	PostalCode = '031317',
	Country = 'Romania',
	HomePhone = '0753255371',
	Notes = 'Andrei a primit diploma de tehnician superior in 1984 si un doctorat in marketing international de la Universitatea din Dallas in 1991. Este fluent in franceza si italiana si citeste limba germana. A intrat in companie ca reprezentant de vanzari, a fost promovat in functia de director de vanzari in ianuarie 2002 si vicepresedinte al vanzarilor in martie 2003. Andrew este membru al camerei de comert din Seattle.'
where LastName = 'Fuller';

update Employees
Set LastName = 'Leca', 
	FirstName = 'Ioana', 
	Title = 'Reprezentant Vanzari',
	TitleOfCourtesy = 'Dra.',
	[Address] = 'Str. Cupolei, nr 54, ap 15, Bucuresti Sector 6',
	City = 'Bucuresti',
	PostalCode = '061158',
	Country = 'Romania',
	HomePhone = '0723292441',
	Notes = 'Ioana are o diploma in chimie din Boston College (1994). A finalizat un program de certificare in domeniul managementul vanzarii cu amanuntul a produselor alimentare. Ioana a fost angajata ca vanzatoare in 2001 si a fost promovata pe postul reprezentant vanzari in februarie 2002.'
where LastName = 'Leverling';

update Employees
Set LastName = 'Pescaru', 
	FirstName = 'Margareta', 
	Title = 'Reprezentant Vanzari',
	TitleOfCourtesy = 'Dra',
	[Address] = 'Str. Hornului, nr. 22, ap 4, Bucuresti Sector 4',
	City = 'Bucuresti',
	PostalCode = '031317',
	Country = 'Romania',
	HomePhone = '0752212371',
	Notes = 'Margareta detine o diploma de licenta in literatura engleza de la Concordia College (1978) si o diploma de licenta de la Institutul American de Arta culinara (1986).'
where LastName = 'Peacock';

update Employees
Set LastName = 'Bucurescu', 
	FirstName = 'Stefan', 
	Title = 'Manager Vanzari',
	TitleOfCourtesy = 'Dl.',
	[Address] = 'Str. Preciziei, nr 2, ap 32, Bucuresti Sector 6',
	City = 'Bucuresti',
	PostalCode = '062203',
	Country = 'Romania',
	HomePhone = '0712292991',
	Notes = 'Stefan a absolvit Universitatea din St. Andrews, Scotia, cu diploma in stiinte in 1986. Dupa ce s-a alaturat companiei ca reprezentant de vanzari in 2002, a petrecut 6 luni intr-un program de orientare la biroul din Seattle si apoi a revenit la postul sau permanent in Londra . A fost promovat manager de vanzari in martie 1993. Domnul Buchanan a absolvit cursurile "Telemarketing de succes" si "Managementul vanzarilor internationale". Este fluent in franceza.'
where LastName = 'Buchanan';

update Employees
Set LastName = 'Surdu', 
	FirstName = 'Mihai', 
	Title = 'Reprezentant Vanzari',
	TitleOfCourtesy = 'Dl.',
	[Address] = 'Str Ghirlandei, nr 5, ap 40, Bucuresti Sector 6',
	City = 'Bucuresti',
	PostalCode = '062242',
	Country = 'Romania',
	HomePhone = '0751217471',
	Notes = 'Mihai este absolvent al Universitatii din Sussex (MA, economie, 1993) si al Universitatii din California, Los Angeles (MBA, marketing, 1996). De asemenea, a luat cursurile "Vanzari multiculturale" si "Managementul timpului pentru profesionistii in vanzari". Este fluent in limba japoneza si poate citi si scrie franceza, portugheza si spaniola.'
where LastName = 'Suyama';

update Employees
Set LastName = 'Kiritescu', 
	FirstName = 'Robert', 
	Title = 'Reprezentant Vanzari',
	TitleOfCourtesy = 'Dl.',
	[Address] = 'Str. Fabricii, nr 4, ap 4, Bucuresti Sector 6',
	City = 'Bucuresti',
	PostalCode = '060823',
	Country = 'Romania',
	HomePhone = '0718642371',
	Notes = 'Robert a fost in Corpul Pacii si a calatorit mult inaintae de a-si incheia studiile in limba engleza la Universitatea din Michigan in 2002, anul in care sa alatura companiei. Dupa finalizarea cursului de "Vanzarea in Europa" a fost transferat la biroul din Londra in martie 2003.'
where LastName = 'King';

update Employees
Set LastName = 'Cojocaru', 
	FirstName = 'Laura', 
	Title = 'Coordonator Vanzari Intern',
	TitleOfCourtesy = 'Dra.',
	[Address] = 'Str. Lugoj, nr 2, ap 12, Bucuresti Sector 6',
	City = 'Bucuresti',
	PostalCode = '012212',
	Country = 'Romania',
	HomePhone = '0712392335',
	Notes = 'Laura a obtinut o diploma in psihologie la Universitatea din Washington. De asemenea, a absolvit un curs de afaceri francez. Citeste si scrie franceza.'
where LastName = 'Callahan';

update Employees
Set LastName = 'Dobrescu', 
	FirstName = 'Ana', 
	Title = 'Reprezentant Vanzari',
	TitleOfCourtesy = 'Dra.',
	[Address] = 'Str. Comana, nr 2, ap 23, Bucuresti Sector 6',
	City = 'Bucuresti',
	PostalCode = '011274',
	Country = 'Romania',
	HomePhone = '0764882331',
	Notes = 'Ana are o diploma de licenta in limba engleza de la Colegiul St. Lawrence. Este fluenta in franceza si germana.'
where LastName = 'Dodsworth';

---------------------------------------------------------------------------------------
--  Region

--select * from Region

update Region
set RegionDescription = 'Banat'
where RegionID = '1';

update Region
set RegionDescription = 'Bucovina'
where RegionID = '2';

update Region
set RegionDescription = 'Crisana'
where RegionID = '3';

update Region
set RegionDescription = 'Dobrogea'
where RegionID = '4';

insert into Region values ('5', 'Maramures')
insert into Region values ('6', 'Moldova')
insert into Region values ('7', 'Muntenia')
insert into Region values ('8', 'Oltenia')
insert into Region values ('9', 'Transilvania')
go
---------------------------------------------------------------------------------------
--		Territories

/*#1. drop FOREIGN key*/
alter table EmployeeTerritories
drop constraint FK_EmployeeTerritories_Territories
go

truncate table EmployeeTerritories
truncate table Territories
go

/*#1. create FOREIGN key */
ALTER TABLE [dbo].[EmployeeTerritories]
ADD  CONSTRAINT [FK_EmployeeTerritories_Territories] FOREIGN KEY(TerritoryID)
REFERENCES [dbo].[Territories] ([TerritoryID])
go

/*Banat*/
insert into Territories values('1', 'Timis', 1);
insert into Territories values('2', 'Caras-Severin', 1);
/*Bucovina*/
insert into Territories values('3', 'Botosani', 2);
insert into Territories values('4', 'Suceava', 2);
/*Crisana*/
insert into Territories values('5', 'Bihor', 3);
insert into Territories values('6', 'Arad', 3);
/*Dobrogea*/
insert into Territories values('7', 'Tulcea', 4);
insert into Territories values('8', 'Constanta', 4);
/*Maramures*/
insert into Territories values('9', 'Satu-Mare', 5);
insert into Territories values('10', 'Maramures', 5);
/*Moldova*/
insert into Territories values('11', 'Neamt', 6);
insert into Territories values('12', 'Iasi', 6);
insert into Territories values('13', 'Bacau', 6);
insert into Territories values('14', 'Vaslui', 6);
insert into Territories values('15', 'Vrancea', 6);
insert into Territories values('16', 'Galati', 6);
/*Muntenia*/
insert into Territories values('17', 'Braila', 7);
insert into Territories values('18', 'Buzau', 7);
insert into Territories values('19', 'Calarasi', 7);
insert into Territories values('20', 'Prahova', 7);
insert into Territories values('21', 'Dambovita', 7);
insert into Territories values('22', 'Arges', 7);
insert into Territories values('23', 'Ialomita', 7);
insert into Territories values('24', 'Calarasi', 7);
insert into Territories values('25', 'Ilfov', 7);
insert into Territories values('26', 'Bucuresti', 7);
insert into Territories values('27', 'Giurgiu', 7);
insert into Territories values('28', 'Teleorman', 7);
/*Oltenia*/
insert into Territories values('29', 'Gorj', 8);
insert into Territories values('30', 'Valcea', 8);
insert into Territories values('31', 'Olt', 8);
insert into Territories values('32', 'Dolj', 8);
insert into Territories values('33', 'Mehedinti', 8);
/*Transilvania*/
insert into Territories values('34', 'Salaj', 9);
insert into Territories values('35', 'Bistrita-Nasaud', 9);
insert into Territories values('36', 'Cluj', 9);
insert into Territories values('37', 'Mures', 9);
insert into Territories values('38', 'Harghita', 9);
insert into Territories values('39', 'Covasna', 9);
insert into Territories values('40', 'Brasov', 9);
insert into Territories values('41', 'Sibiu', 9);
insert into Territories values('42', 'Alba', 9);
insert into Territories values('43', 'Hunedoara', 9);
go

---------------------------------------------------------------------------------------
--		EmployeeTerritories

insert into EmployeeTerritories values (1, 9);
insert into EmployeeTerritories values (1, 10);
insert into EmployeeTerritories values (1, 5);
insert into EmployeeTerritories values (1, 34);
insert into EmployeeTerritories values (1, 35);
insert into EmployeeTerritories values (1, 36);
insert into EmployeeTerritories values (2, 12);
insert into EmployeeTerritories values (2, 23);
insert into EmployeeTerritories values (2, 19);
insert into EmployeeTerritories values (2, 26);
insert into EmployeeTerritories values (2, 27);
insert into EmployeeTerritories values (3, 3);
insert into EmployeeTerritories values (3, 4);
insert into EmployeeTerritories values (3, 11);
insert into EmployeeTerritories values (3, 12);
insert into EmployeeTerritories values (3, 37);
insert into EmployeeTerritories values (3, 38);
insert into EmployeeTerritories values (3, 35);
insert into EmployeeTerritories values (4, 11);
insert into EmployeeTerritories values (4, 12);
insert into EmployeeTerritories values (4, 13);
insert into EmployeeTerritories values (4, 14);
insert into EmployeeTerritories values (4, 16);
insert into EmployeeTerritories values (5, 6);
insert into EmployeeTerritories values (5, 1);
insert into EmployeeTerritories values (5, 2);
insert into EmployeeTerritories values (5, 42);
insert into EmployeeTerritories values (5, 43);
insert into EmployeeTerritories values (5, 29);
insert into EmployeeTerritories values (5, 33);
insert into EmployeeTerritories values (6, 29);
insert into EmployeeTerritories values (6, 30);
insert into EmployeeTerritories values (6, 31);
insert into EmployeeTerritories values (6, 32);
insert into EmployeeTerritories values (6, 33);
insert into EmployeeTerritories values (6, 28);
insert into EmployeeTerritories values (6, 22);
insert into EmployeeTerritories values (7, 36);
insert into EmployeeTerritories values (7, 37);
insert into EmployeeTerritories values (7, 38);
insert into EmployeeTerritories values (7, 39);
insert into EmployeeTerritories values (7, 40);
insert into EmployeeTerritories values (7, 41);
insert into EmployeeTerritories values (7, 42);
insert into EmployeeTerritories values (8, 15);
insert into EmployeeTerritories values (8, 16);
insert into EmployeeTerritories values (8, 40);
insert into EmployeeTerritories values (8, 39);
insert into EmployeeTerritories values (8, 18);
insert into EmployeeTerritories values (8, 17);
insert into EmployeeTerritories values (8, 20);
insert into EmployeeTerritories values (8, 21);
insert into EmployeeTerritories values (8, 25);
insert into EmployeeTerritories values (8, 26);
insert into EmployeeTerritories values (8, 7);
insert into EmployeeTerritories values (9, 7);
insert into EmployeeTerritories values (9, 8);
insert into EmployeeTerritories values (9, 17);
insert into EmployeeTerritories values (9, 23);
insert into EmployeeTerritories values (9, 18);
insert into EmployeeTerritories values (9, 20);
insert into EmployeeTerritories values (9, 21);
insert into EmployeeTerritories values (9, 27);
insert into EmployeeTerritories values (9, 24);
insert into EmployeeTerritories values (9, 26);
go

---------------------------------------------------------------------------------------
--		Customers

update Customers
	set CompanyName = 'Vinyl Fever',
	ContactName = 'Danut Gogean',
	[Address] = 'STR. 10 MAI nr. 15, DaMBOVIŢA',
	City = 'Targoviste',
	Region = 'Muntenia',
	PostalCode = '130062',
	Country = 'Romania',
	Phone = '0245-216 446',
	Fax = NULL
where CompanyName = 'Alfreds Futterkiste';

update Customers
	set CompanyName = 'Kash n',
	ContactName = 'Gabriella  Anghelescu',
	[Address] = 'Strada Caraiman 3, Constanta',
	City = 'Constanta',
	Region = 'Dobrogea',
	PostalCode = '907021',
	Country = 'Romania',
	Phone = '0723-564 218',
	Fax = '0251.411688'
where CompanyName = 'Ana Trujillo Emparedados y helados';

update Customers
	set CompanyName = 'Children',
	ContactName = 'Eugenia  Costiniu',
	[Address] = 'Strada Stelelor 4, Timisoara',
	City = 'Timis',
	Region = 'Banat',
	PostalCode = '373572',
	Country = 'Romania',
	Phone = '+40(256)441727',
	Fax = '0251.411688'
where CompanyName = '';

update Customers
	set CompanyName = 'Tech Hifi',
	ContactName = 'Dorin  Butacu',
	[Address] = 'Piata Revolutiei 3/26, Maramures',
	City = 'Maramures',
	Region = 'Maramures',
	PostalCode = '873309',
	Country = 'Romania',
	Phone = '+40(262)260999',
	Fax = '+40(262)271338'
where CompanyName = 'Antonio Moreno Taquería';

update Customers
	set CompanyName = 'Beatties',
	ContactName = 'Ioana  Draghici',
	[Address] = 'STR. VULCAN SAMUIL nr. 16, BEIUS',
	City = 'BIHOR',
	Region = 'Crisana',
	PostalCode = '653271',
	Country = 'Romania',
	Phone = '0259-320 222',
	Fax = '0251.418803'
where CompanyName = 'Around the Horn';

update Customers
	set CompanyName = 'Cut Above',
	ContactName = 'Varujan  Puscas',
	[Address] = 'Bulevardul Ion Mihalache 148B, Bucuresti',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '666708',
	Country = 'Romania',
	Phone = '+40(21)2246714',
	Fax = '0251.413102'
where CompanyName = 'Berglunds snabbköp';

update Customers
	set CompanyName = 'Sears Homelife',
	ContactName = 'Stefan  Manole',
	[Address] = 'STR. 9 MAI, BACAU',
	City = 'Bacau',
	Region = 'Moldova',
	PostalCode = '546708',
	Country = 'Romania',
	Phone = '0740-082 824',
	Fax = '0251.413102'
where CompanyName = 'Blauer See Delikatessen';

update Customers
	set CompanyName = 'Century House',
	ContactName = 'Varujan  Teodorescu',
	[Address] = 'STR. BARNUŢIU SIMION nr. 67, SALAJ',
	City = 'SALAJ',
	Region = 'Transilvania',
	PostalCode = '437945',
	Country = 'Romania',
	Phone = '0260-616 920',
	Fax = '0251.418803'
where CompanyName = 'Blondesddsl père et fils';

update Customers
	set CompanyName = 'Matrix Interior Design',
	ContactName = 'Lavinia  Ciora',
	[Address] = 'NR. 91/A, COM. BOBOTA',
	City = 'SALAJ',
	Region = 'Transilvania',
	PostalCode = '626705',
	Country = 'Romania',
	Phone = '0260-652 491',
	Fax = '0251.418803'
where CompanyName = 'Bólido Comidas preparadas';

update Customers
	set CompanyName = 'Awthentikz',
	ContactName = 'Diona  Lascar',
	[Address] = 'STR. GIMNASTICII nr. 11, SIBIU',
	City = 'SIBIU',
	Region = 'Transilvania',
	PostalCode = '907892',
	Country = 'Romania',
	Phone = '0269-245 479',
	Fax = '0251.413102'
where CompanyName like 'Bon app%';

update Customers
	set CompanyName = 'Afforda Merchant Services',
	ContactName = 'Amelia  Raducanu',
	[Address] = 'STR. BERZEI nr. 21, Bucuresti - Sector 1',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '749447',
	Country = 'Romania',
	Phone = '0741-108 981',
	Fax = '0251.413102'
where CompanyName = 'Bottom-Dollar Markets';

update Customers
	set CompanyName = 'Prahject Planner',
	ContactName = 'Juana  Kogalniceaunu',
	[Address] = 'STR. EROILOR nr. 749, COM. CORNU',
	City = 'PRAHOVA',
	Region = 'Muntenia',
	PostalCode = '643518',
	Country = 'Romania',
	Phone = '0244-367 481',
	Fax = '0251.413102'
where CompanyName like '%s Beverages';

update Customers
	set CompanyName = 'Club Wholesale',
	ContactName = 'Sergiu  Stinga',
	[Address] = 'STR. PORUMBESCU CIPRIAN nr. 19 bl. A27 sc. A ap. 2, BRASOV',
	City = 'Brasov',
	Region = 'Transilvania',
	PostalCode = '155584',
	Country = 'Romania',
	Phone = '0723-474 265',
	Fax = '0251.413687'
where CompanyName = 'Cactus Comidas para llevar';

update Customers
	set CompanyName = 'Adaptas',
	ContactName = 'Mihail  Serbanescu',
	[Address] = 'NR. 180, COM. SACADAT',
	City = 'BIHOR',
	Region = 'Crisana',
	PostalCode = '835400',
	Country = 'Romania',
	Phone = '0721-517 140',
	Fax = '0251.413687'
where CompanyName = 'Centro comercial Moctezuma';

update Customers
	set CompanyName = 'Flagg Bros. Shoes',
	ContactName = 'Alin  Dita',
	[Address] = 'Strada Lunga 14, Brasov',
	City = 'Brasov',
	Region = 'Transilvania',
	PostalCode = '500058',
	Country = 'Romania',
	Phone = '+40(268)418770',
	Fax = '0251.415899'
where CompanyName = 'Chop-suey Chinese';

update Customers
	set CompanyName = 'Naturohair',
	ContactName = 'Dana  Lahovary',
	[Address] = 'STR. NEGOIU bl. 6, FAGARAS',
	City = 'Brasov',
	Region = 'Transilvania',
	PostalCode = '505200',
	Country = 'Romania',
	Phone = '0268-215 645',
	Fax = '0251.415899'
where CompanyName = 'Comércio Mineiro';

update Customers
	set CompanyName = 'Sunburst Garden Management',
	ContactName = 'Daciana  Diaconu',
	[Address] = 'STR. CALUGARENI nr. 7, BRASOV',
	City = 'Brasov',
	Region = 'Transilvania',
	PostalCode = '516337',
	Country = 'Romania',
	Phone = '0268-420 146',
	Fax = '0251.412479'
where CompanyName = 'Consolidated Holdings';

update Customers
	set CompanyName = 'Garden Management',
	ContactName = 'Tudor  Popescu',
	[Address] = 'Soseaua Dobroiesti 7, Complex Atlantis, Bl.A',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '466966',
	Country = 'Romania',
	Phone = '+40(21)3364941',
	Fax = '0251.412479'
where CompanyName = 'Drachenblut Delikatessen';

update Customers
	set CompanyName = 'Pro Garden Management',
	ContactName = 'Ioana  Izbasa',
	[Address] = 'STR. BRaNCOVEANU CONSTANTIN nr. 32, TIMIS',
	City = 'Timisoara',
	Region = 'Banat',
	PostalCode = '144913',
	Country = 'Romania',
	Phone = '0722-644 846',
	Fax = '0251.419689'
where CompanyName = 'Du monde entier';

update Customers
	set CompanyName = 'Datacorp',
	ContactName = 'Florin  Urzica',
	[Address] = 'STR. ROBESCU F. CONSTANTIN nr. 11, Bucuresti Sector 3',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '071465',
	Country = 'Romania',
	Phone = '021-316 6831',
	Fax = '0251.419689'
where CompanyName = 'Eastern Connection';

update Customers
	set CompanyName = 'Bombay Company',
	ContactName = 'Daria  Rotaru',
	[Address] = 'STR. CUZA I. AL. nr. 1, STREHAIA',
	City = 'MEHEDINŢI',
	Region = 'Oltenia',
	PostalCode = '253792',
	Country = 'Romania',
	Phone = '0722-262 210',
	Fax = '0251.437979'
where CompanyName = 'Ernst Handel';

update Customers
	set CompanyName = 'Tee Town',
	ContactName = 'Adrian  PIrvulescu',
	[Address] = 'STR. PIETAŢII nr. 1, BRAILA',
	City = 'Braila',
	Region = 'Moldova',
	PostalCode = '353968',
	Country = 'Romania',
	Phone = '0723-761 061',
	Fax = '0251.438210'
where CompanyName = 'Familia Arquibaldo';

update Customers
	set CompanyName = 'CompuAdd',
	ContactName = 'Florica  Radu',
	[Address] = 'STR. 9 MAI nr. 23C, MARAMURES',
	City = 'Baia Mare',
	Region = 'Maramures',
	PostalCode = '640213',
	Country = 'Romania',
	Phone = '0744-313 069',
	Fax = '0251.438210'
where CompanyName = 'FISSA Fabrica Inter. Salchichas S.A.';

update Customers
	set CompanyName = 'Isaly',
	ContactName = 'Oana  Mircea',
	[Address] = 'SAT PAUSESTI-OTASAU, COM. PAUSESTI',
	City = 'VaLCEA',
	Region = 'Oltenia',
	PostalCode = '524423',
	Country = 'Romania',
	Phone = '0751-204 789',
	Fax = '0251.411755'
where CompanyName = 'Folies gourmandes';

update Customers
	set CompanyName = 'John F. Lawhon',
	ContactName = 'Florenta  Balan',
	[Address] = 'Bulevardul Schitu Magureanu 39, Et.1, Ap.10',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '702828',
	Country = 'Romania',
	Phone = '+40(727)494463',
	Fax = '0251.413921'
where CompanyName = 'Folk och fä HB';

update Customers
	set CompanyName = 'Plan Smart',
	ContactName = 'Dana  Iagar',
	[Address] = 'STR. LINIEI nr. 13-15 bl. 15-16 sc. 3 ap. 91,Sector 6',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '530264',
	Country = 'Romania',
	Phone = '021-434 1043',
	Fax = '0251.413921'
where CompanyName = 'Frankenversand';

update Customers
	set CompanyName = 'Atlas Architectural Designs',
	ContactName = 'Liviu  Olinescu',
	[Address] = 'STR. MIRCEA CEL BATRaN nr. 102, CONSTANŢA',
	City = 'Constanta',
	Region = 'Dobrogea',
	PostalCode = '782841',
	Country = 'Romania',
	Phone = '0241-550 099',
	Fax = '0251.413405'
where CompanyName = 'France restauration';

update Customers
	set CompanyName = 'Signa Air',
	ContactName = 'Ion  Nita',
	[Address] = 'STR. LUNII nr. 8A, Cluj',
	City = 'Cluj',
	Region = 'Transilvania',
	PostalCode = '779951',
	Country = 'Romania',
	Phone = '0264-430 891',
	Fax = '0251.411753'
where CompanyName = 'Franchi S.p.A.';

update Customers
	set CompanyName = 'Strategic Profit',
	ContactName = 'Mazonn  PIrvulescu',
	[Address] = 'STR. OLTEŢ nr. 5, DOLJ',
	City = 'DOLJ',
	Region = 'Oltenia',
	PostalCode = '522951',
	Country = 'Romania',
	Phone = '0251-410 016',
	Fax = '0251.413405'
where CompanyName = 'Furia Bacalhau e Frutos do Mar';

update Customers
	set CompanyName = 'Gold Touch',
	ContactName = 'Mihaita  Cuza',
	[Address] = 'STR. BABA NOVAC nr. 11 bl. G1 sc. 2 et parter Ap. 46, Sec. 3',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '523951',
	Country = 'Romania',
	Phone = '021-324 0083',
	Fax = '0251.411753'
where CompanyName = 'Galería del gastrónomo';

update Customers
	set CompanyName = 'Luskin',
	ContactName = 'Maria  Barladeanu',
	[Address] = 'STR. TRAIAN nr. 53, IALOMIŢA',
	City = 'Slobozia',
	Region = 'Muntenia',
	PostalCode = '245951',
	Country = 'Romania',
	Phone = '0243-216 565',
	Fax = '0251.411752'
where CompanyName = 'Godos Cocina Típica';

update Customers
	set CompanyName = 'E-zhe Source ',
	ContactName = 'Octavia  Filotti',
	[Address] = 'Strada Nikolai Gogol 1A, et.1, Ap.2',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '479951',
	Country = 'Romania',
	Phone = '+40(21)2302390',
	Fax = '0251.411754'
where CompanyName = 'Gourmet Lanchonetes';

update Customers
	set CompanyName = 'KB Toys',
	ContactName = 'Maria  Tilea',
	[Address] = 'STR. URSU ION nr. 49 bl. N2A sc. A ap. 3, CONSTANŢA',
	City = 'Constanta',
	Region = 'Dobrogea',
	PostalCode = '919951',
	Country = 'Romania',
	Phone = '0241-684 848',
	Fax = '0251.417047'
where CompanyName = 'Great Lakes Food Market';

update Customers
	set CompanyName = 'Isaly',
	ContactName = 'Oana  Mircea',
	[Address] = 'SAT PAUSESTI-OTASAU, COM. PAUSESTI',
	City = 'VaLCEA',
	Region = 'Oltenia',
	PostalCode = '555951',
	Country = 'Romania',
	Phone = '0751-204 789',
	Fax = '0252.319303'
where CompanyName = 'GROSELLA-Restaurante';

update Customers
	set CompanyName = 'Pantry Pride',
	ContactName = 'Tabitha  Filipescu',
	[Address] = 'STR. LIVEZILOR nr. 33, COM. DIOSIG',
	City = 'BIHOR',
	Region = 'Crisana',
	PostalCode = '572351',
	Country = 'Romania',
	Phone = '0259-350 212',
	Fax = '0252.319303'
where CompanyName = 'Hanari Carnes';

update Customers
	set CompanyName = 'Compact Disc Center',
	ContactName = 'Jean  Olinescu',
	[Address] = 'Strada Zlatna 2, Brasov',
	City = 'Brasov',
	Region = 'Transilvania',
	PostalCode = '879951',
	Country = 'Romania',
	Phone = '+40(268)441192',
	Fax = '0252.317219'
where CompanyName = 'HILARION-Abastos';

update Customers
	set CompanyName = 'BASCO',
	ContactName = 'Georgeta  Draghicescu',
	[Address] = 'STR. ODOBESCU ALEXANDRU nr. 1, PRAHOVA',
	City = 'Ploiesti',
	Region = 'Muntenia',
	PostalCode = '779951',
	Country = 'Romania',
	Phone = '0244-512 820',
	Fax = '0252.317219'
where CompanyName = 'Hungry Coyote Import Store';

update Customers
	set CompanyName = 'Mansmann',
	ContactName = 'Eugen  Ciobanu',
	[Address] = 'STR. REPUBLICII nr. 7, VRANCEA',
	City = 'Focsani',
	Region = 'Moldova',
	PostalCode = '579951',
	Country = 'Romania',
	Phone = '0722-788 869',
	Fax = '0251.414021'
where CompanyName = 'Hungry Owl All-Night Grocers';

update Customers
	set CompanyName = 'Team Uno',
	ContactName = 'Ilie  Stanescu',
	[Address] = 'BD. MARASTI nr. 31, Bucuresti - Sector 1',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '799865',
	Country = 'Romania',
	Phone = '021-202 3642',
	Fax = null
where CompanyName = 'Island Trading';

update Customers
	set CompanyName = 'Walt',
	ContactName = 'Catalina  Proca',
	[Address] = 'STR. UNIRII nr. 2, TULCEA',
	City = 'TULCEA',
	Region = 'Dobrogea',
	PostalCode = '915207',
	Country = 'Romania',
	Phone = '0240-511 110',
	Fax = '0251.414021'
where CompanyName = 'Königlich Essen';

update Customers
	set CompanyName = 'Red Owl',
	ContactName = 'Cezar  Hutopila',
	[Address] = 'STR. 1 DECEMBRIE 1918 nr. 29',
	City = 'Galati',
	Region = 'Dobrogea',
	PostalCode = '555207',
	Country = 'Romania',
	Phone = '0236-820 290',
	Fax = '0251.414398'
where CompanyName like 'La corne d%' and CompanyName like '%abondance';

update Customers
	set CompanyName = 'Alladin Realty',
	ContactName = 'Costel  Enache',
	[Address] = 'STR. ALECSANDRI VASILE nr. 1, COMANESTI',
	City = 'Bacau',
	Region = 'Moldova',
	PostalCode = '732207',
	Country = 'Romania',
	Phone = '0234-374 219',
	Fax = '0251.414398'
where CompanyName like 'La maison d%';

update Customers
	set CompanyName = 'Channel Home Centers',
	ContactName = 'Margareta  Ionescu',
	[Address] = 'Strada Septimius Severus, Ap.10, Alba',
	City = 'Alba Iulia',
	Region = 'Transilvania',
	PostalCode = '234207',
	Country = 'Romania',
	Phone = '+40(368)445479',
	Fax = '0251.413844'
where CompanyName = 'Laughing Bacchus Wine Cellars';

update Customers
	set CompanyName = 'Handy City',
	ContactName = 'Flori  Ceausescu',
	[Address] = 'BD. LAPUSNEANU A. nr. 75 bl. LV4 Ap. 20, CONSTANŢA',
	City = 'Constanta',
	Region = 'Dobrogea',
	PostalCode = '125207',
	Country = 'Romania',
	Phone = '0730-006 188',
	Fax = '0251.413844'
where CompanyName = 'Lazy K Kountry Store';

update Customers
	set CompanyName = 'Ukrop',
	ContactName = 'Lucian  Brancoveanu',
	[Address] = 'SOS. NAŢIONALA nr. 64-66 et PARTER, IASI',
	City = 'Iasi',
	Region = 'Moldova',
	PostalCode = '323387',
	Country = 'Romania',
	Phone = '0232-245 471',
	Fax = '0251.419015'
where CompanyName = 'Lehmanns Marktstand';

update Customers
	set CompanyName = 'Widdmann',
	ContactName = 'Leunta  Teodorescu',
	[Address] = 'Strada Istriei 2D, Bucuresti',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '307217',
	Country = 'Romania',
	Phone = '+40(722)950706',
	Fax = '0251.419015'
where CompanyName like '%s Stop N Shop';

update Customers
	set CompanyName = 'Finast',
	ContactName = 'Nina  Georghiou',
	[Address] = 'BD. STIRBEI VODA nr. 19, DOLJ',
	City = 'Craiova',
	Region = 'Oltenia',
	PostalCode = '777287',
	Country = 'Romania',
	Phone = '0251-534 463',
	Fax = '0251.413102'
where CompanyName = 'LILA-Supermercado';

update Customers
	set CompanyName = 'Standard Sales',
	ContactName = 'Ionel  Neagoe',
	[Address] = 'STR. 1 DECEMBRIE 1918, MOLDOVA NOUA',
	City = 'CARAS-SEVERIN',
	Region = 'Banat',
	PostalCode = '707287',
	Country = 'Romania',
	Phone = '0745-610 526',
	Fax = '0251.413102'
where CompanyName = 'LINO-Delicateses';

update Customers
	set CompanyName = 'CAL. SEVERINULUI nr. 11, GORJ',
	ContactName = 'Ioana  Cernat',
	[Address] = 'STR. VINTILA VODA nr. 21, Olt',
	City = 'Targu Jiu',
	Region = 'Oltenia',
	PostalCode = '887287',
	Country = 'Romania',
	Phone = '0249-433 112',
	Fax = '0251.413844'
where CompanyName = 'Lonesome Pine Restaurant';

update Customers
	set CompanyName = 'Galyan',
	ContactName = 'Danut  Dumitrescu',
	[Address] = 'STR. OLTEŢ nr. 5, DOLJ',
	City = 'DOLJ',
	Region = 'Oltenia',
	PostalCode = '127287',
	Country = 'Romania',
	Phone = '0744-479 942',
	Fax = '0251.413844'
where CompanyName = 'Magazzini Alimentari Riuniti';

update Customers
	set CompanyName = 'Atlas Architectural Designs',
	ContactName = 'Liviu  Olinescu',
	[Address] = 'STR. MIRCEA CEL BATRaN nr. 102, CONSTANŢA',
	City = 'Constanta',
	Region = 'Dobrogea',
	PostalCode = '323287',
	Country = 'Romania',
	Phone = '0241-550 099',
	Fax = '0251.413102'
where CompanyName = 'Maison Dewey';

update Customers
	set CompanyName = 'Gold Touch',
	ContactName = 'Mihaita  Cuza',
	[Address] = 'STR. BABA NOVAC nr. 11 bl. G17 sc. 2 et 1 Ap. 46, Sect. 3',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '399287',
	Country = 'Romania',
	Phone = '021-324 0083',
	Fax = '0251.413102'
where CompanyName = 'Mère Paillarde';

update Customers
	set CompanyName = 'Man Business',
	ContactName = 'Diana  Blerinca',
	[Address] = 'CAL. LUI TRAIAN nr. 331, VaLCEA',
	City = 'VaLCEA',
	Region = 'Oltenia',
	PostalCode = '307287',
	Country = 'Romania',
	Phone = '+40(21)3162878',
	Fax = '0251.413102'
where CompanyName = 'Morgenstern Gesundkost';

update Customers
	set CompanyName = 'Circuit City',
	ContactName = 'Virgil  Nicolae',
	[Address] = 'STR. JIULUI nr. 1, GORJ',
	City = 'GORJ',
	Region = 'Oltenia',
	PostalCode = '594203',
	Country = 'Romania',
	Phone = '0728-177 249',
	Fax = '0251.413102'
where CompanyName = 'North/South';

update Customers
	set CompanyName = 'Grossman',
	ContactName = 'Ioan  Fieraru',
	[Address] = 'SOS. COLENTINA nr. 76 bl. 111 et parter, Sector 2',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '511675',
	Country = 'Romania',
	Phone = '539877',
	Fax = '0251.413102'
where CompanyName = 'Océano Atlántico Ltda.';

update Customers
	set CompanyName = 'Roberd',
	ContactName = 'Magdalena  Iliescu',
	[Address] = 'BD. REVOLUŢIEI nr. 45, ARAD',
	City = 'Arad',
	Region = 'Crisana',
	PostalCode = '138106',
	Country = 'Romania',
	Phone = '0257-253 767',
	Fax = '0251.413102'
where CompanyName = 'Old World Delicatessen';

update Customers
	set CompanyName = 'Sportswest',
	ContactName = 'Grigore  Barbu',
	[Address] = 'STR. OZANA nr. 31NEAMŢ, Piatra Neamt',
	City = 'Neamt',
	Region = 'Moldova',
	PostalCode = '822646',
	Country = 'Romania',
	Phone = '0742-028 490',
	Fax = '0251.413041'
where CompanyName = 'Ottilies Käseladen';

update Customers
	set CompanyName = 'National Lumber',
	ContactName = 'Nina  Banica',
	[Address] = 'Strada Simion Barnutiu 12, Bihor',
	City = 'Bihor',
	Region = 'Crisana',
	PostalCode = '138321',
	Country = 'Romania',
	Phone = '+40(259)475320',
	Fax = '0251.413041'
where CompanyName = 'Paris spécialités';

update Customers
	set CompanyName = 'Patterson-Fletcher',
	ContactName = 'Livia  Skutnik',
	[Address] = 'STR. PANU ANASTASIE nr. 13, IASI',
	City = 'Iasi',
	Region = 'Moldova',
	PostalCode = '453993',
	Country = 'Romania',
	Phone = '0232-212 162',
	Fax = '0251.413687'
where CompanyName = 'Pericles Comidas clásicas';

update Customers
	set CompanyName = 'The Flying Hippo ',
	ContactName = 'Anca  Toma',
	[Address] = 'SPL. UNIRII nr. 4 bl. B3 Ap. 20, Bucuresti - Sector 4',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '377738',
	Country = 'Romania',
	Phone = '031-805 3526',
	Fax = '0251.415899'
where CompanyName = 'Piccolo und mehr';

update Customers
	set CompanyName = 'Rainbow Life',
	ContactName = 'Stefana  Mironescu',
	[Address] = 'Bulevardul Iuliu Maniu 69, Bucuresti 6',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '688127',
	Country = 'Romania',
	Phone = '+40(31)4195861',
	Fax = '0251.415899'
where CompanyName = 'Princesa Isabel Vinhos';

update Customers
	set CompanyName = 'Superior Interactive',
	ContactName = 'Melania  Manescu',
	[Address] = 'Bulevardul Regiei 6D, et.2, Bucuresti 6',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '703948',
	Country = 'Romania',
	Phone = '+40(21)3166279',
	Fax = '0251.412479'
where CompanyName = 'Que Delícia';

update Customers
	set CompanyName = 'Just For Fun',
	ContactName = 'Sofia  Lucescu',
	[Address] = 'BD. TOMIS nr. 80, CONSTANŢA',
	City = 'Constanta',
	Region = 'Dobrogea',
	PostalCode = '696505',
	Country = 'Romania',
	Phone = '0241-559 979',
	Fax = '0251.412479'
where CompanyName = 'Queen Cozinha';

update Customers
	set CompanyName = 'All Wound Up',
	ContactName = 'Constantin  Chirila',
	[Address] = 'STR. HORIA nr. 5, DOLJ',
	City = 'DOLJ',
	Region = 'Oltenia',
	PostalCode = '567203',
	Country = 'Romania',
	Phone = '0251-544 521',
	Fax = '0251.598054'
where CompanyName = 'QUICK-Stop';

update Customers
	set CompanyName = 'Datacorp',
	ContactName = 'Florin  Urzica',
	[Address] = 'STR. ROBESCU F. CONSTANTIN nr. 11, Sector 3',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '059698',
	Country = 'Romania',
	Phone = '021-316 6831',
	Fax = '0251.598054'
where CompanyName = 'Rancho grande';

update Customers
	set CompanyName = 'Computer City',
	ContactName = 'Marinela  Cozma',
	[Address] = 'STR. HOREA nr. 40, CLUJ',
	City = 'Cluj-Napoca',
	Region = 'Transilvania',
	PostalCode = '238154',
	Country = 'Romania',
	Phone = '0264-442 044',
	Fax = null
where CompanyName = 'Rattlesnake Canyon Grocery';

update Customers
	set CompanyName = 'Ideal Garden Management',
	ContactName = 'Andrei  Barladeanu',
	[Address] = 'STR. REPUBLICII nr. 65-67, CLUJ',
	City = 'Cluj-Napoca',
	Region = 'Transilvania',
	PostalCode = '786220',
	Country = 'Romania',
	Phone = '0264-599 331',
	Fax = '0251.419689'
where CompanyName = 'Reggiani Caseifici';

update Customers
	set CompanyName = 'Buttrey Food & Drug',
	ContactName = 'Neculai  Calinescu',
	[Address] = 'STR. RARES PETRU nr. 50A, COM. ALEXANDRU CEL BUN',
	City = 'NEAMŢ',
	Region = 'Moldova',
	PostalCode = '070085',
	Country = 'Romania',
	Phone = '0730-330 481',
	Fax = '0251.419689'
where CompanyName = 'Ricardo Adocicados';

update Customers
	set CompanyName = 'Practi-Plan',
	ContactName = 'Corneliu  Preda',
	[Address] = 'Aleea Spitalului 28A, Arges',
	City = 'Arges',
	Region = 'Muntenia',
	PostalCode = '327141',
	Country = 'Romania',
	Phone = '+40(248)280101',
	Fax = '0251.413820'
where CompanyName = 'Richter Supermarkt';

update Customers
	set CompanyName = 'Adaptaz',
	ContactName = 'Traian  Ungur',
	[Address] = 'SOS. CHITILEI nr. 115, Bucuresti - Sector 1',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '798227',
	Country = 'Romania',
	Phone = '0722-414 016',
	Fax = '0251.413820'
where CompanyName = 'Romero y tomillo';

update Customers
	set CompanyName = 'Vinyl Fever',
	ContactName = 'Danut  Gogean',
	[Address] = 'STR. 10 MAI nr. 15, DaMBOVIŢA',
	City = 'Targoviste',
	Region = 'Muntenia',
	PostalCode = '453993',
	Country = 'Romania',
	Phone = '0245-216 446',
	Fax = '0251.437979'
where CompanyName = 'Santé Gourmet';

update Customers
	set CompanyName = 'Handy Andy Home Improvement Center',
	ContactName = 'Valeriu  Moldovanu',
	[Address] = 'BD. MANIU IULIU nr. 7 bl. Corp G ap. 18B, Sector 6',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '460609',
	Country = 'Romania',
	Phone = '0722-121 659',
	Fax = '0251.438844'
where CompanyName = 'Save-a-lot Markets';

update Customers
	set CompanyName = 'MegaSolutions',
	ContactName = 'Marta  Gaina',
	[Address] = 'INT. PATINOARULUI nr. 7 bl. 5 sc. 1 et 2 Ap. 3, Sector 3',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '156963',
	Country = 'Romania',
	Phone = '021-324 3772',
	Fax = '0251.510174'
where CompanyName = 'Seven Seas Imports';

update Customers
	set CompanyName = 'Total Sources',
	ContactName = 'Tara  Dumitru',
	[Address] = 'Strada Regiment 5 Vanatori, nr.4-6.jpg',
	City = 'Timisoara',
	Region = 'Banat',
	PostalCode = '410656',
	Country = 'Romania',
	Phone = '+40(256)43250',
	Fax = '0251.510174'
where CompanyName = 'Simons bistro';

update Customers
	set CompanyName = 'Jackhammer Technologies',
	ContactName = 'Vasile  Avramescu',
	[Address] = 'STR. CUZA I. AL. bl. 25 et PARTER ap. 3, GORJ',
	City = 'Targu Jiu',
	Region = 'Oltenia',
	PostalCode = '696505',
	Country = 'Romania',
	Phone = '0722-441 992',
	Fax = '0251.525290'
where CompanyName = 'Spécialités du monde';

update Customers
	set CompanyName = 'Vinyl Fever',
	ContactName = 'David  Stelymes',
	[Address] = 'STR. LAZAR GHEORGHE nr. 17, TIMIS',
	City = 'Timisoara',
	Region = 'Banat',
	PostalCode = '696505',
	Country = 'Romania',
	Phone = '0759926648',
	Fax = '0251.525290'
where CompanyName = 'Split Rail Beer & Ale';

update Customers
	set CompanyName = 'Gene Walter',
	ContactName = 'Nina  Cardei',
	[Address] = 'STR. ZORILOR nr. 4 sc. B, BOTOSANI',
	City = 'Botosani',
	Region = 'Bucovina',
	PostalCode = '696505',
	Country = 'Romania',
	Phone = '0231-582 776',
	Fax = '0251.438730'
where CompanyName = 'Suprêmes délices';

update Customers
	set CompanyName = 'Dream Home Improvements',
	ContactName = 'Vasilescu  Vlaicu',
	[Address] = 'DRUM. TABEREI nr. 94 bl. 519, Sector 6',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '090827',
	Country = 'Romania',
	Phone = '021-322 2289',
	Fax = '0251.438730'
where CompanyName = 'The Big Cheese';

update Customers
	set CompanyName = 'Brendle',
	ContactName = 'Marta  Suciu',
	[Address] = 'Strada Caldarari 1, Bucuresti 3',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '375093',
	Country = 'Romania',
	Phone = '+40(744)318414',
	Fax = '0251.417049'
where CompanyName = 'The Cracker Box';

update Customers
	set CompanyName = 'Rite Solution',
	ContactName = 'Ilie  Stanescu',
	[Address] = 'CAL. LUI TRAIAN nr. 331, VaLCEA',
	City = 'VaLCEA',
	Region = 'Oltenia',
	PostalCode = '224729',
	Country = 'Romania',
	Phone = '0250-746 501',
	Fax = '0251.417049'
where CompanyName = 'Toms Spezialitäten';

update Customers
	set CompanyName = 'Matrix Interior Design',
	ContactName = 'Sanda  Stelymes',
	[Address] = 'CAL. GALATA 340, Galati',
	City = 'Galati',
	Region = 'Dobrogea',
	PostalCode = '624857',
	Country = 'Romania',
	Phone = '0744-755 056',
	Fax = '0251.411755'
where CompanyName = 'Tortuga Restaurante';

update Customers
	set CompanyName = 'StopGrey',
	ContactName = 'Craita  Iliescu',
	[Address] = 'Strada Traian 57, Bucuresti',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '812861',
	Country = 'Romania',
	Phone = '+40(21)3260355',
	Fax = '0251.411755'
where CompanyName = 'Tradição Hipermercados';

update Customers
	set CompanyName = 'W. Bell & Co.',
	ContactName = 'Dorin  Butacu',
	[Address] = 'SOS. MIHAI BRAVU nr. 444 bl. V10 sc. 3 et 1 Ap. 66, Sector 3',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '584908',
	Country = 'Romania',
	Phone = '0734-961 412',
	Fax = '0251.413921'
where CompanyName like '%s Head Gourmet Provisioners';

update Customers
	set CompanyName = 'Sun Foods',
	ContactName = 'Matei  Pop',
	[Address] = 'BD. LACUL TEI nr. 77 bl. N et demisol ap. 17, Sector 2',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '879813',
	Country = 'Romania',
	Phone = '021-243 2721',
	Fax = '0251.414021'
where CompanyName = 'Vaffeljernet';

update Customers
	set CompanyName = 'Chandler',
	ContactName = 'Victoria  Andreescu',
	[Address] = 'STR. MUSCA VASILE nr. 5, Sector 5',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '812056',
	Country = 'Romania',
	Phone = '0723-611 818',
	Fax = '0251.414021'
where CompanyName = 'Victuailles en stock';

update Customers
	set CompanyName = 'Strength Gurus',
	ContactName = 'Livia  Gaina',
	[Address] = 'BD. LAPUSNEANU A. nr. 185, CONSTANŢA',
	City = 'Constanta',
	Region = 'Dobrogea',
	PostalCode = '579782',
	Country = 'Romania',
	Phone = '0726-201 736',
	Fax = '0251.411753'
where CompanyName = 'Vins et alcools Chevalier';

update Customers
	set CompanyName = 'Awthentikz',
	ContactName = 'Diona  Lascar',
	[Address] = 'STR. GIMNASTICII nr. 11, SIBIU',
	City = 'SIBIU',
	Region = 'Transilvania',
	PostalCode = '164270',
	Country = 'Romania',
	Phone = '0269-245 479',
	Fax = '0251.413405'
where CompanyName = 'Die Wandernde Kuh';

update Customers
	set CompanyName = 'Dream Home Improvements',
	ContactName = 'Vasilescu  Vlaicu',
	[Address] = 'DRUM. TABEREI nr. 94 bl. 519, Sector 6',
	City = 'Bucuresti',
	Region = 'Muntenia',
	PostalCode = '432841',
	Country = 'Romania',
	Phone = '021-322 2289',
	Fax = '0251.415032'
where CompanyName = 'Wartian Herkku';

update Customers
	set CompanyName = 'David Weis',
	ContactName = 'Ionus  Sandulescu',
	[Address] = 'nr. 153, COM. GEPIU',
	City = 'BIHOR',
	Region = 'Crisana',
	PostalCode = '250218',
	Country = 'Romania',
	Phone = '0744-963 516',
	Fax = '0251.415036'
where CompanyName = 'Wellington Importadora';

update Customers
	set CompanyName = 'Multi Tech Development',
	ContactName = 'Monica  Preda',
	[Address] = 'STR. MORII nr. 10-12, VALEA LUI MIHAI',
	City = 'BIHOR',
	Region = 'Crisana',
	PostalCode = '757011',
	Country = 'Romania',
	Phone = '0259-355 413',
	Fax = '0251.415036'
where CompanyName = 'White Clover Markets';

update Customers
	set CompanyName = 'Beaver Lumber',
	ContactName = 'Natalia  Zaituc',
	[Address] = 'STR. MIHAI VITEAZU nr. 31, MURES',
	City = 'Targu Mures',
	Region = 'Transilvania',
	PostalCode = '540004',
	Country = 'Romania',
	Phone = '0265-210 902',
	Fax = '0251.411752'
where CompanyName = 'Wilman Kala';

update Customers
	set CompanyName = 'Maxi-Tech',
	ContactName = 'Sebastian  Barnutiu',
	[Address] = 'STR. VALEANCA, COM. BUCOV',
	City = 'PRAHOVA',
	Region = 'Muntenia',
	PostalCode = '100003',
	Country = 'Romania',
	Phone = '0244-274 138',
	Fax = '0251.411754'
where CompanyName = 'Wolski  Zajazd';

update Customers
set ContactTitle = 'Manager Contabilitate'
where ContactTitle = 'Accounting Manager';

update Customers
set ContactTitle = 'Asistent Vanzari'
where ContactTitle = 'Assistant Sales Agent';

update Customers
set ContactTitle = 'Reprezentant Vanzari'
where ContactTitle = 'Assistant Sales Representative';

update Customers
set ContactTitle = 'Asistent Marketing'
where ContactTitle = 'Marketing Assistant';

update Customers
set ContactTitle = 'Director Marketing'
where ContactTitle = 'Marketing Manager';

update Customers
set ContactTitle = 'Administrator Comenzi'
where ContactTitle = 'Order Administrator';

update Customers
set ContactTitle = 'Patron'
where ContactTitle = 'Owner';

update Customers
set ContactTitle = 'Patron/Asistent Marketing'
where ContactTitle = 'Owner/Marketing Assistant';

update Customers
set ContactTitle = 'Agent Vanzari'
where ContactTitle = 'Sales Agent';

update Customers
set ContactTitle = 'Asociat'
where ContactTitle = 'Sales Associate';

update Customers
set ContactTitle = 'Manager Vanzari'
where ContactTitle = 'Sales Manager';

update Customers
set ContactTitle = 'Reprezentant Vanzari'
where ContactTitle = 'Sales Representative';


---------------------------------------------------------------------------------------
--		Shippers

update Shippers
set CompanyName = 'FedEx',
	Phone = '+40213034567 '
where ShipperID = 1;

update Shippers
set CompanyName = 'Urgent Cargus',
	Phone = '021 9330 '
where ShipperID = 2;

update Shippers
set CompanyName = 'FAN Courier',
	Phone = '+40742552233 '
where ShipperID = 3;
go

---------------------------------------------------------------------------------------
--		Orders

declare @count bigint = 1;
declare @customerNum int = (select count(Customers.CompanyName) from Customers);
declare @orderNum bigint = (select count(Orders.OrderID) from Orders);

while(@count <= @orderNum)
begin
	update Orders
	set Orders.CustomerID = (SELECT foo.CustomerID FROM (
									SELECT
										ROW_NUMBER() OVER (ORDER BY Customers.CompanyName ASC) AS rownumber,
										Customers.CustomerID, Customers.CompanyName, Customers.[Address], Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
									  FROM Customers
									) AS foo
								WHERE rownumber = (@count * 2) % @customerNum + 1),
		Orders.ShipName = (SELECT foo.CompanyName FROM (
									SELECT
										ROW_NUMBER() OVER (ORDER BY Customers.CompanyName ASC) AS rownumber,
										Customers.CustomerID, Customers.CompanyName, Customers.[Address], Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
									  FROM Customers
									) AS foo
								WHERE rownumber = (@count * 2) % @customerNum + 1),
		Orders.ShipAddress = (SELECT foo.[Address] FROM (
									SELECT
										ROW_NUMBER() OVER (ORDER BY Customers.CompanyName ASC) AS rownumber,
										Customers.CustomerID, Customers.CompanyName, Customers.[Address], Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
									  FROM Customers
									) AS foo
								WHERE rownumber = (@count * 2) % @customerNum + 1),
		Orders.ShipCity = (SELECT foo.City FROM (
									SELECT
										ROW_NUMBER() OVER (ORDER BY Customers.CompanyName ASC) AS rownumber,
										Customers.CustomerID, Customers.CompanyName, Customers.[Address], Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
									  FROM Customers
									) AS foo
								WHERE rownumber = (@count * 2) % @customerNum + 1),
		Orders.ShipRegion = (SELECT foo.Region FROM (
									SELECT
										ROW_NUMBER() OVER (ORDER BY Customers.CompanyName ASC) AS rownumber,
										Customers.CustomerID, Customers.CompanyName, Customers.[Address], Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
									  FROM Customers
									) AS foo
								WHERE rownumber = (@count * 2) % @customerNum + 1),
		Orders.ShipPostalCode = (SELECT foo.PostalCode FROM (
									SELECT
										ROW_NUMBER() OVER (ORDER BY Customers.CompanyName ASC) AS rownumber,
										Customers.CustomerID, Customers.CompanyName, Customers.[Address], Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
									  FROM Customers
									) AS foo
								WHERE rownumber = (@count * 2) % @customerNum + 1),
		Orders.ShipCountry = (SELECT foo.Country FROM (
									SELECT
										ROW_NUMBER() OVER (ORDER BY Customers.CompanyName ASC) AS rownumber,
										Customers.CustomerID, Customers.CompanyName, Customers.[Address], Customers.City, Customers.Region, Customers.PostalCode, Customers.Country
									  FROM Customers
									) AS foo
								WHERE rownumber = (@count * 2) % @customerNum + 1)
	where OrderID = (	SELECT OrderID FROM (
							SELECT
								ROW_NUMBER() OVER (ORDER BY CustomerID ASC) AS rownumber1,
								OrderID
							  FROM Orders
							) AS sss
						WHERE rownumber1 = @count);
set @count = @count +  1;
end;
go

----------------------------------------------------------------------------------
--	Product changes

/* Alter table pentu a il putea modifica*/
alter table [Order Details]
drop constraint FK_Order_Details_Products;
go
--supliers
alter table Products
drop constraint FK_Products_Categories;
go

alter table Products
drop constraint FK_Products_Suppliers;


	truncate table [dbo].[Suppliers];

	alter table Suppliers
	alter column [Address] nvarchar(100);
	--sup list
	insert into Suppliers values('EURO GSM IMPEX S.R.L.','Ion Vasilde','Proprietar','B-dul Muncii nr.18','CLUJ-NAPOCA',null,'400641','Romania','0264450450',null,'https://eurogsm.ro');
	insert into Suppliers values('GERSIM IMPEX S.R.L.','Mircea Daniel','Manager depozit','Strada Bilciurești 9A','BUCURESTI',null,'014012','Romania','0213264850','0213264851','http://www.gersim.ro');
	insert into Suppliers values('EMAG S.A.','Dumitru George','Agent Vanzari','Swan Office Park, Windsor Building Sos. Bucureşti Nord nr. 15-23','ILFOV',null,'077190','Romania','0722.25.00.00',null,'https://emag.ro');
	insert into Suppliers values('SC MEDIA GALAXY S.R.L.','Popescu Mihai','Reprezentant Vanzari','Bulevardul Poligrafiei Nr.1, Sector 1','Bucuresti',null,'400641','Romania','0212062000','0213199939','www.mediagalaxy.ro');

-- aici stergem produsele
truncate table Products

ALTER TABLE [dbo].Products
ADD  CONSTRAINT FK_Products_Suppliers FOREIGN KEY(SupplierID)
REFERENCES [dbo].Suppliers (SupplierID)
go

-- aici bagam produsele
alter table Products
alter column ProductName nvarchar(100);
go
alter table Products
add img text null,
	img1 text null,
	img2 text null,
	img3 text null;
go
--eBookReader select * from Products
insert into Products values('eBook Reader Kindle 6 Glare Touch Screen WiFi Black 140210',1,5,1,339,10,1,1,'true', '/Images/eBook/ebook-reader-kindle-6-glare-touch-screen-wifi-black.jpg', null, null, null);
insert into Products values('eBook Reader Kindle PaperWhite Wi-Fi 4GB New Model 2015 Black 111399',2,5,1,629,10,1,1,'true', '/Images/eBook/ebook-reader-kindle-paperwhite-wi-fi-4gb-new-model-2015.jpg', null, null, null);
insert into Products values('eBook Reader Kindle PaperWhite Wi-Fi 4GB New Model 2015 White 143087',2,5,1,599,10,1,1,'true', '/Images/eBook/ebook-reader-kindle-paperwhite-wifi-white.jpg', null, null, null);
insert into Products values('eBook Reader PocketBook Touch LUX 3 4GB Red pb626',1,5,1,569,10,1,1,'true', '/Images/eBook/ebook-reader-pocketbook-touch-lux-3-4gb-red.jpg', null, null, null);
insert into Products values('eBook Reader PocketBook Touch LUX 3 4GB White pb626',2,5,1,569,10,1,1,'true', '/Images/eBook/ebook-reader-pocketbook-touch-lux-3-4gb-white.jpg', '/Images/eBook/ebook-reader-pocketbook-touch-lux-3-4gb-white-3.jpg', null, null);
insert into Products values('eBook Reader PocketBook Touch LUX 3 4GB Grey pb626',1,5,1,569,10,1,1,'true', '/Images/eBook/ebook-reader-pocketbook-touch-lux-3-4gb-grey.jpg', '/Images/eBook/ebook-reader-pocketbook-touch-lux-3-4gb-grey-2.jpg', null, null);
insert into Products values('eBook Reader PocketBook Touch HD Black pb631',4,5,1,799,10,1,1,'true', '/Images/eBook/ebook-reader-pocketbook-touch-hd-black.jpg', null, null, null);
insert into Products values('eBook Reader Bookeen CybooK Muse FrontLight Black',3,5,1,699,10,1,1,'true', '/Images/eBook/e-book-reader-bookeen-cybook-muse-frontlight-black.jpg', null, null, null);
insert into Products values('eBook Reader Prestigio MultiReader SUPREME 4GB Black',2,5,1,549,10,1,1,'true', '/Images/eBook/ebook-reader-prestigio-multireader-supreme-4gb-black.jpg', null, null, null);
insert into Products values('eBook Reader Bookeen Cybook Muse HD 8GB Black',4,5,1,569,10,1,1,'true', '/Images/eBook/ebook-reader-bookeen-cybook-muse-hd-8gb-black.jpg', '/Images/eBook/ebook-reader-bookeen-cybook-muse-hd-8gb-black-6.jpg', null, null);
insert into Products values('eBook Reader Bookeen Cybook Muse Light 4GB Black ',3,5,1,579,10,1,1,'true', '/Images/eBook/ebook-reader-bookeen-cybook-muse-light-4gb-black.jpg', '/Images/eBook/ebook-reader-bookeen-cybook-muse-light-4gb-black-3.jpg', null, null);

--tel clasice
insert into Products values('Telefon Mobil CAT B25 Dual SIM Black',2,1,1,229,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-cat-b25-dual-sim-black.jpg', '/Images/telefoane_clasice/telefon-mobil-cat-b25-dual-sim-black-3.jpg', null, null);
insert into Products values('Telefon Mobil Nokia 3310 Dual SIM Dark Blue',1,1,1,249,10,1,1,'true','/Images/telefoane_clasice/telefon-mobil-nokia-3310-dual-sim-dark-blue.jpg', '/Images/telefoane_clasice/telefon-mobil-nokia-3310-dual-sim-dark-blue-1.jpg', null, null)
insert into Products values('Telefon Mobil Alcatel Tiger X3 1016G Black',4,1,1,69,10,1,1,'true','/Images/telefoane_clasice/telefon-mobil-alcatel-tiger-x3-1016g-black-2.jpg', '/Images/telefoane_clasice/telefon-mobil-alcatel-tiger-x3-1016g-black-4.jpg', null, null)
insert into Products values('Telefon Mobil Nokia 3310 Single Sim Orange',1,1,1,249,10,1,1,'true','/Images/telefoane_clasice/telefon-mobil-nokia-3310-orange.jpg', null, null, null)
insert into Products values('Telefon Mobil Nokia 130 Dual SIM Red',2,1,1,99,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-nokia-130-dual-sim-red.jpg', '/Images/telefoane_clasice/telefon-mobil-nokia-130-dual-sim-red-2.jpg', null, null);
insert into Products values('Telefon Mobil Alcatel 1054 White',1,1,1,83,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-alcatel-1054-white.jpg', null, null, null);
insert into Products values('Telefon Mobil Nokia 150 Single Sim White',3,1,1,141,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-nokia-150-dual-sim-white.jpg', null, null, null);
insert into Products values('Telefon Mobil MaxCom MM 141 Dual Sim Grey',2,1,1,101,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-maxcom-mm-141-dual-sim-4g-grey.jpg', '/Images/telefoane_clasice/telefon-mobil-maxcom-mm-141-dual-sim-4g-grey-1.jpg', '/Images/telefoane_clasice/telefon-mobil-maxcom-mm-141-dual-sim-4g-grey-2.jpg', null);
insert into Products values('Telefon Mobil Alcatel 2008G Black-Silver',1,1,1,165,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-alcatel-2008g-black-silver.jpg', null, null, null);
insert into Products values('Telefon Mobil Nokia 216 Dual Sim Black',3,1,1,156,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-nokia-216-single-sim-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Nokia 216 Dual SIM Grey',1,1,1,156,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-nokia-216-dual-sim-grey.jpg','/Images/telefoane_clasice/telefon-mobil-nokia-216-dual-sim-grey-1.jpg', null, null);
insert into Products values('Telefon Mobil Karbonn K-flip Dual Sim White',1,1,1,127,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-karbonn-k-flip-dual-sim-white.jpg', '/Images/telefoane_clasice/telefon-mobil-karbonn-k-flip-dual-sim-white-2.jpg', '/Images/telefoane_clasice/telefon-mobil-karbonn-k-flip-dual-sim-white-3.jpg', null);
insert into Products values('Telefon Mobil MyPhone Metro Red',2,1,1,209,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-myphone-metro-red.jpg', '/Images/telefoane_clasice/telefon-mobil-myphone-metro-red-1.jpg', '/Images/telefoane_clasice/telefon-mobil-myphone-metro-red-3.jpg', '/Images/telefoane_clasice/telefon-mobil-myphone-metro-red-2.jpg');
insert into Products values('Telefon Mobil MyPhone 6310 Dual Sim Black',3,1,1,104,10,1,1,'true', '/Images/telefoane_clasice/telefon-mobil-myphone-6310-dual-sim-black.jpg', '/Images/telefoane_clasice/telefon-mobil-myphone-6310-dual-sim-black-1.jpg', '/Images/telefoane_clasice/telefon-mobil-myphone-6310-dual-sim-black-4.jpg', null);


-- Accesorii
insert into Products values('Bratara Xiaomi Silicon pentru MiBand 2 - Roz',4,3,1,24,10,1,1,'true', '/Images/accesorii/bratara-xiaomi-silicon-pentru-miband-2---roz.jpg', null, null, null);
insert into Products values('Bratara Xiaomi Silicon pentru MiBand 2 - Verde',4,3,1,24,10,1,1,'true', '/Images/accesorii/bratara-xiaomi-silicon-pentru-miband-2---verde.jpg', null, null, null);
insert into Products values('Curea Ceas Garmin Forerunner 910XT GPS Negru',4,3,1,86,10,1,1,'true', '/Images/accesorii/curea-ceas-garmin-forerunner-910xt-gps-negru.jpg', null, null, null);
insert into Products values('Bratara Smartwatch Samsung Gear S3 Silicon Maron',4,3,1,127,10,1,1,'true', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-maron.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-maron-1.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-maron-2.jpg', null);
insert into Products values('Curea Apple Watch 38mm Piele Neagra MLHG2',4,3,1,964,10,1,1,'true', '/Images/accesorii/curea-apple-watch-38mm-piele-neagra-mlhg2-1.jpg', '/Images/accesorii/curea-apple-watch-38mm-piele-neagra-mlhg2-2.jpg', '/Images/accesorii/curea-apple-watch-38mm-piele-neagra-mlhg2.jpg', null);
insert into Products values('Dock Slate Native Union Pentru Apple Watch',4,3,1,217,10,1,1,'true', '/Images/accesorii/dock-slate-native-union-pentru-apple-watch.jpg', '/Images/accesorii/dock-slate-native-union-pentru-apple-watch-2.jpg', '/Images/accesorii/dock-slate-native-union-pentru-apple-watch-1.jpg', null);
insert into Products values('Dock Native Union Luxury Tech Marble Pentru Apple Watch',4,3,1,423,10,1,1,'true', '/Images/accesorii/dock-native-union-luxury-tech-marble-pentru-apple-watch.jpg', '/Images/accesorii/dock-native-union-luxury-tech-marble-pentru-apple-watch-1.jpg', null, null);
insert into Products values('Stand de incarcare pentru Huawei Watch W1 Argintiu',4,3,1,95,10,1,1,'true', '/Images/accesorii/stand-de-incarcare-pentru-huawei-watch-w1-argintiu.jpg', '/Images/accesorii/stand-de-incarcare-pentru-huawei-watch-w1-argintiu-1.jpg', null, null);
insert into Products values('Cablu de incarcare Fitbit Flex',4,3,1,25,10,1,1,'true', '/Images/accesorii/cablu-de-incarcare-fitbit-flex.jpg', null, null, null);
insert into Products values('Husa Bumper Cellularline pentru Apple Watch 38mm',4,3,1,49,10,1,1,'true', '/Images/accesorii/husa-bumper-cellularline-pentru-apple-watch-38mm.jpg', '/Images/accesorii/husa-bumper-cellularline-pentru-apple-watch-38mm-1.jpg', '/Images/accesorii/husa-bumper-cellularline-pentru-apple-watch-38mm-2.jpg', null);
insert into Products values('Bratara Smartwatch Samsung Gear S3 Silicon Argintiur',4,3,1,128,10,1,1,'true', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-argintiu.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-argintiu-1.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-argintiu-2.jpg', null);
insert into Products values('Bratara Smartwatch Samsung Gear S3 Piele Neagra',4,3,1,115 ,10,1,1,'true', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-piele-neagra.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-piele-neagra-1.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-piele-neagra-3.jpg', null);
insert into Products values('Bratara Smartwatch Samsung Gear S3 Silicon Khaki',4,3,1,86,10,1,1,'true', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-khaki.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-khaki-1.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-khaki-3.jpg', null);
insert into Products values('Bratara Smartwatch Samsung Gear S3 Silicon Blue Black',4,3,1,86,10,1,1,'true', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-blue-black.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-blue-black-1.jpg', '/Images/accesorii/bratara-smartwatch-samsung-gear-s3-silicon-blue-black-2.jpg', null);
insert into Products values('Folie Protectie Sticla Securizata Curbata Apple Watch 42 mm Negra',4,3,1,36,10,1,1,'true', '/Images/accesorii/folie-protectie-sticla-securizata-curbata-apple-watch-42-mm-negra.jpg', null, null, null);
insert into Products values('Folie Protectie Sticla Securizata Curbata Apple Watch 38 mm Negra',4,3,1,36,10,1,1,'true', '/Images/accesorii/folie-protectie-sticla-securizata-curbata-apple-watch-negra.jpg', null, null, null);


--smartphones
insert into Products values('Telefon Mobil Apple iPhone 7 32GB Black',1,2,1,2999,10,1,1,'true', '/Images/Smartphone/telefon-mobil-apple-iphone-7-32gb-black.jpg', null, null, null);
insert into Products values('Telefon Mobil OnePlus 5 A5000 64GB Dual SIM 4G Black',2,2,1,2599,10,1,1,'true', '/Images/Smartphone/telefon-mobil-oneplus-5-a5000-64gb-dual-sim-4g-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy A3(2017) A320 4G Black',3,2,1,1199,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-a3-2017-a320-4g-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy J5(2016) J510 Dual SIM Gold',4,2,1,849,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-j510-dual-sim-gold.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy S8 G950F 64GB 4G Black',1,2,1,2989,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-s8-g950f-64gb-4g-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Apple iPhone 6 32GB Space Gray',2,2,1,1899,10,1,1,'true', '/Images/Smartphone/telefon-mobil-apple-iphone-6-16gb-space-gray.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy J1 Mini Prime J106 Dual Sim 3G Black',3,2,1,349,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-j105-3g-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy J1 Mini Prime J106 Dual Sim 3G Gold',2,2,1,349,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-j105-3g-gold.jpg', null, null, null);
insert into Products values('Telefon Mobil Xiaomi Redmi 4A 32GB Dual Sim 4G Dark Grey',4,2,1,499,10,1,1,'true', '/Images/Smartphone/telefon-mobil-xiaomi-redmi-4a-16gb-dual-sim-4g-dark-grey.jpg', null, null, null);
insert into Products values('Telefon Mobil Lenovo Moto Z 32GB Dual Sim 4G Black',3,2,1,1659,10,1,1,'true', '/Images/Smartphone/telefon-mobil-lenovo-moto-z3-32gb-dual-sim-4g-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy S8 Plus G955F 64GB 4G Black',2,2,1,3549,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-s8-plus-g955f-64gb-4g-black.jpg', null, null, null);
insert into Products values('Telefon Mobil HTC 10 32GB 4G Gold',1,2,1,1799,10,1,1,'true', '/Images/Smartphone/telefon-mobil-htc-10-32gb-4g-gold.jpg', null, null, null);
insert into Products values('Telefon Mobil Huawei P10 Lite 32GB Dual Sim 4G Gold',3,2,1,1199,10,1,1,'true', '/Images/Smartphone/telefon-mobil-huawei-p10-lite-32gb-dual-sim-4g-gold.jpg', null, null, null);
insert into Products values('Telefon Mobil Apple iPhone SE 32GB Space Gray',1,2,1,1699,10,1,1,'true', '/Images/Smartphone/telefon-mobil-apple-iphone-se-16gb-space-gray.jpg', null, null, null);
insert into Products values('Telefon Mobil Huawei P10 Lite 32GB Dual Sim 4G Black',3,2,1,1199,10,1,1,'true', '/Images/Smartphone/telefon-mobil-huawei-p10-lite-32gb-dual-sim-4g-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy J1 Mini Prime J106 Dual Sim 3G White',1,2,1,339,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-j105-3g-white.jpg', '/Images/Smartphone/telefon-mobil-samsung-galaxy-j105-3g-black-cpyy-1.jpg', null, null);
insert into Products values('Telefon Mobil Huawei P10 Lite 32GB Dual Sim 4G Blue',4,2,1,1199,10,1,1,'true', '/Images/Smartphone/telefon-mobil-huawei-p10-lite-32gb-dual-sim-4g-blue.jpg', null, null, null);
insert into Products values('Telefon Mobil Samsung Galaxy S6 Edge G925 32GB Black',4,2,1,1899,10,1,1,'true', '/Images/Smartphone/telefon-mobil-samsung-galaxy-s6-edge-g925-32gb-black.jpg', null, null, null);
insert into Products values('Telefon Mobil Sony Xperia X Compact 32GB 4G Black xcompactblk',4,2,1,1599,10,1,1,'true', '/Images/Smartphone/telefon-mobil-sony-xperia-x-compact-32gb-4g-black.jpg', '/Images/Smartphone/telefon-mobil-sony-xperia-x-compact-32gb-4g-black-1.jpg', null, null);
insert into Products values('Telefon Mobil LG G5 SE H840 32GB Titanium Grey H840 Grey',4,2,1,1349,10,1,1,'true', '/Images/Smartphone/telefon-mobil-lg-g5-se-h840-32gb-titanium-grey.jpg', '/Images/Smartphone/telefon-mobil-lg-g5-se-h840-32gb-titanium-grey-1.jpg', '/Images/Smartphone/telefon-mobil-lg-g5-se-h840-32gb-titanium-grey-4.jpg', '/Images/Smartphone/telefon-mobil-lg-g5-se-h840-32gb-titanium-grey-5.jpg');
insert into Products values('Telefon Mobil Apple iPhone 6s 32GB Space Grey iphone 6s 32gb Space Grey',4,2,1,2599,10,1,1,'true', '/Images/Smartphone/telefon-mobil-apple-iphone-6s-32gb-space-grey.jpg', '/Images/Smartphone/telefon-mobil-apple-iphone-6s-32gb-space-grey-1.jpg', null, null);

--gadgeturi
insert into Products values('Boxa Portabila Emie Cybertron Wireless',1,4,1,749,10,1,1,'true','/Images/Gadgeturi/boxa-portabila-emie-cybertron-wireless-negru.jpg','/Images/Gadgeturi/boxa-portabila-emie-cybertron-wireless-negru-1.jpg',null,null)
insert into Products values('Ochelari Samsung Gear VR 2 SM-R323 Negru',1,4,1,209,10,1,1,'true','/Images/Gadgeturi/ochelari-vr-samsung-gear-vr-r323-negru.jpg','/Images/Gadgeturi/ochelari-vr-samsung-gear-vr-r323-negru-1.jpg','/Images/Gadgeturi/ochelari-vr-samsung-gear-vr-r323-negru-2.jpg','/Images/Gadgeturi/ochelari-vr-samsung-gear-vr-r323-negru-3.jpg')
insert into Products values('Manusi cu Casca Bluetooth Hi-Fun M Black',1,4,1,229,10,1,1,'true','/Images/Gadgeturi/manusi-bluetooth-hi-fun-m-black.jpg','/Images/Gadgeturi/manusi-bluetooth-hi-fun-m-black-1.jpg',null,null)
insert into Products values('Dispozitiv monitorizare somn SenSe Sleep',1,4,1,119,10,1,1,'true','/Images/Gadgeturi/dispozitiv-monitorizare-somn-sense-sleep-peanut.jpg','/Images/Gadgeturi/dispozitiv-monitorizare-somn-sense-sleep-peanut-1.jpg','/Images/Gadgeturi/dispozitiv-monitorizare-somn-sense-sleep-peanut-2.jpg',null)
insert into Products values('Telecomanda Bluetooth Esperanza',1,4,1,28,10,1,1,'true','/Images/Gadgeturi/telecomanda-bluetooth-esperanza-pentru-ochelari-3d-vr-emv100.jpg','/Images/Gadgeturi/telecomanda-bluetooth-esperanza-pentru-ochelari-3d-vr-emv100-3.jpg','/Images/Gadgeturi/telecomanda-bluetooth-esperanza-pentru-ochelari-3d-vr-emv100-4.jpg','/Images/Gadgeturi/telecomanda-bluetooth-esperanza-pentru-ochelari-3d-vr-emv100-6.jpg')
insert into Products values('Caciula Stereo Cellularline Music Cap cu Microfon Negru',1,4,1,37,10,1,1,'true', '/Images/Gadgeturi/caciula-stereo-cellularline-music-cap-cu-microfon-negru.jpg', '/Images/Gadgeturi/caciula-stereo-cellularline-music-cap-cu-microfon-negru-1.jpg', '/Images/Gadgeturi/caciula-stereo-cellularline-music-cap-cu-microfon-negru-2.jpg', null);
insert into Products values('Dispozitiv localizare cu Bluetooth Media-Tech BT Seeker',1,4,1,29,10,1,1,'true', '/Images/Gadgeturi/detector-de-chei-media-tech-bt-seeker-negru.jpg', '/Images/Gadgeturi/detector-de-chei-media-tech-bt-seeker-negru-1.jpg', null, null);
insert into Products values('Telecomanda Media-Tech Trigger BT pentru VR Matrix PRO Negru',1,4,1,39,10,1,1,'true', '/Images/Gadgeturi/telecomanda-media-tech-trigger-bt-pentru-vr-matrix-pro-negru.jpg', '/Images/Gadgeturi/telecomanda-media-tech-trigger-bt-pentru-vr-matrix-pro-negru-1.jpg', '/Images/Gadgeturi/telecomanda-media-tech-trigger-bt-pentru-vr-matrix-pro-negru-2.jpg', '/Images/Gadgeturi/telecomanda-media-tech-trigger-bt-pentru-vr-matrix-pro-negru-3.jpg');
insert into Products values('Drona Arcade Orbit',1,4,1,149,10,1,1,'true', '/Images/Gadgeturi/drona-arcade-orbit.jpg', '/Images/Gadgeturi/drona-arcade-orbit-1.jpg', '/Images/Gadgeturi/drona-arcade-orbit-2.jpg', '/Images/Gadgeturi/drona-arcade-orbit-3.jpg');
insert into Products values('Camera Video Fondi OnReal Negru',1,4,1,514,10,1,1,'true', '/Images/Gadgeturi/camera-video-fondi-onreal-negru.jpg', '/Images/Gadgeturi/camera-video-fondi-onreal-negru-1.jpg', null,null);
insert into Products values('Telecomanda Arcade Bluetooth pentru Arcade Horizon VR',1,4,1,74,10,1,1,'true', '/Images/Gadgeturi/telecomanda-arcade-bluetooth-pentru-arcade-horizon-vr.jpg',null,null,null);
insert into Products values('Robot Inteligent Interactiv Ubtech Alpha 1S Bluetooth',1,4,1,2369,10,1,1,'true', '/Images/Gadgeturi/robot-inteligent-interactiv-ubtech-alpha-1s-bluetooth.jpg', '/Images/Gadgeturi/robot-inteligent-interactiv-ubtech-alpha-1s-bluetooth-1.jpg', '/Images/Gadgeturi/robot-inteligent-interactiv-ubtech-alpha-1s-bluetooth-2.jpg', '/Images/Gadgeturi/robot-inteligent-interactiv-ubtech-alpha-1s-bluetooth-4.jpg');
insert into Products values('Robot Inteligent de Serviciu Uno',1,4,1,3249,10,1,1,'true', '/Images/Gadgeturi/robot-inteligent-uno-de-serviciu-bluetooth.jpg', '/Images/Gadgeturi/robot-inteligent-uno-de-serviciu-bluetooth-1.jpg', '/Images/Gadgeturi/robot-inteligent-uno-de-serviciu-bluetooth-2.jpg', '/Images/Gadgeturi/robot-inteligent-uno-de-serviciu-bluetooth-3.jpg');
insert into Products values('Boxa Portabila Bluetooth JBL Flip 4 Waterproof Black',1,4,1,579,10,1,1,'true', '/Images/Gadgeturi/boxa-portabila-jbl-flip-4.jpg', '/Images/Gadgeturi/boxa-portabila-jbl-flip-4-1.jpg', '/Images/Gadgeturi/boxa-portabila-jbl-flip-4-2.jpg', '/Images/Gadgeturi/boxa-portabila-jbl-flip-4-3.jpg');
insert into Products values('Boxa Portabila Bluetooth JBL Charge 2+ Wireless Cu Microfon',1,4,1,499,10,1,1,'true', '/Images/Gadgeturi/boxa-portabila-bluetooth-jbl-charge-2--wireless-cu-microfon-gri.jpg', '/Images/Gadgeturi/boxa-portabila-bluetooth-jbl-charge-2--wireless-cu-microfon-gri-1.jpg', '/Images/Gadgeturi/boxa-portabila-bluetooth-jbl-charge-2--wireless-cu-microfon-gri-2.jpg', '/Images/Gadgeturi/boxa-portabila-bluetooth-jbl-charge-2--wireless-cu-microfon-gri-3.jpg');

go
----------------------------------------------------------------------------------
--	Category Changes

/* Alter table pentu a il putea modifica*/

alter table Categories
drop column Picture;
go
alter table Categories
add img text null;
go
-- aici bagi categoriile
update Categories set CategoryName='Classic',Description='Telefoane cu butoane', img = 'Images/telefoane_clasice.jpg' where CategoryID=1
update Categories set CategoryName='Smartphone',Description='Touchscreen', img = 'Images/smartphone.jpg' where CategoryID=2
update Categories set CategoryName='Accesorii',Description='Selfie sticks, Incarcatoare, Casti, Baterii, Huse', img = 'Images/accesorii.png' where CategoryID=3
update Categories set CategoryName='Gadgeturi',Description='Boxe, Ochelari VR, Telecomenzi', img = 'Images/Gadgeturi.jpg' where CategoryID=4
update Categories set CategoryName='eBookreader',Description='Bookreader', img = 'Images/eBookreader.jpg' where CategoryID=5
delete from Categories where CategoryID between 6 and 8
go
/*#1. create FOREIGN key */
ALTER TABLE [dbo].Products
ADD  CONSTRAINT FK_Products_Categories FOREIGN KEY(CategoryID)
REFERENCES [dbo].Categories (CategoryID)
go
--alg :(

/*#1. create FOREIGN key */
ALTER TABLE [dbo].[Order Details]
ADD  CONSTRAINT FK_Order_Details_Products FOREIGN KEY(ProductID)
REFERENCES [dbo].Products (ProductID)
go

drop table Persons
go
create table Persons(ID int primary key identity(1,1), Nume varchar(50),Email nvarchar(50),Comentariu text)