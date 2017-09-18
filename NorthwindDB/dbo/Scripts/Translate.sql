/*
	Northwind translate
	Use this script to translate data from Northwind Database. (use Northwind.sql to make Northwind Database)
*/

---------------------------------------------------------------------------------------
--		Initializare
use Northwind;
go

---------------------------------------------------------------------------------------
--		Employees

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

if(not exists (SELECT * FROM Region where RegionDescription = 'Maramures' or RegionID = '5'))
	insert into Region values ('5', 'Maramures');
if(not exists (SELECT * FROM Region where RegionDescription = 'Moldova' or RegionID = '6'))
	insert into Region values ('6', 'Moldova')
if(not exists (SELECT * FROM Region where RegionDescription = 'Muntenia' or RegionID = '7'))
	insert into Region values ('7', 'Muntenia')
if(not exists (SELECT * FROM Region where RegionDescription = 'Oltenia' or RegionID = '8'))
	insert into Region values ('8', 'Oltenia')
if(not exists (SELECT * FROM Region where RegionDescription = 'Transilvania' or RegionID = '9'))
	insert into Region values ('9', 'Transilvania')
go


---------------------------------------------------------------------------------------
--		EmployeeTerritories truncate

truncate table EmployeeTerritories

---------------------------------------------------------------------------------------
--		Territories
--select * from Territories
/*Banat*/
update Territories
set TerritoryID = '1',
	TerritoryDescription = 'Timis',
	RegionID = 1
where TerritoryID = 01581;
update Territories
set TerritoryID = '2',
	TerritoryDescription = 'Caras-Severin',
	RegionID = 1
where TerritoryID = 01730;
update Territories
set TerritoryID = '3',
	TerritoryDescription = 'Botosani',
	RegionID = 2
where TerritoryID = 01833;
/*Bucovina*/
update Territories
set TerritoryID = '4',
	TerritoryDescription = 'Suceava',
	RegionID = 2
where TerritoryID = 02116;
update Territories
set TerritoryID = '5',
	TerritoryDescription = 'Bihor',
	RegionID = 3
where TerritoryID = 02139;
/*Crisana*/
update Territories
set TerritoryID = '6',
	TerritoryDescription = 'Arad',
	RegionID = 3
where TerritoryID = 02184;
update Territories
set TerritoryID = '7',
	TerritoryDescription = 'Tulcea',
	RegionID = 4
where TerritoryID = 02903;
/*Dobrogea*/
update Territories
set TerritoryID = '8',
	TerritoryDescription = 'Constanta',
	RegionID = 4
where TerritoryID = 03049;
update Territories
set TerritoryID = '9',
	TerritoryDescription = 'Satu-Mare',
	RegionID = 5
where TerritoryID = 03801;
/*Maramures*/
update Territories
set TerritoryID = '10',
	TerritoryDescription = 'Maramures',
	RegionID = 5
where TerritoryID = 06897;
update Territories
set TerritoryID = '11',
	TerritoryDescription = 'Neamt',
	RegionID = 6
where TerritoryID = 07960;
/*Moldova*/
update Territories
set TerritoryID = '12',
	TerritoryDescription = 'Iasi',
	RegionID = 6
where TerritoryID = 08837;
update Territories
set TerritoryID = '13',
	TerritoryDescription = 'Bacau',
	RegionID = 6
where TerritoryID = 10019;
update Territories
set TerritoryID = '14',
	TerritoryDescription = 'Vaslui',
	RegionID = 6
where TerritoryID = 10038;
update Territories
set TerritoryID = '15',
	TerritoryDescription = 'Vrancea',
	RegionID = 6
where TerritoryID = 11747;
update Territories
set TerritoryID = '16',
	TerritoryDescription = 'Galati',
	RegionID = 6
where TerritoryID = 14450;
update Territories
set TerritoryID = '17',
	TerritoryDescription = 'Braila',
	RegionID = 7
where TerritoryID = 19428;
/*Muntenia*/
update Territories
set TerritoryID = '18',
	TerritoryDescription = 'Buzau',
	RegionID = 7
where TerritoryID = 19713;
update Territories
set TerritoryID = '19',
	TerritoryDescription = 'Calarasi',
	RegionID = 7
where TerritoryID = 20852;
update Territories
set TerritoryID = '20',
	TerritoryDescription = 'Prahova',
	RegionID = 7
where TerritoryID = 27403;
update Territories
set TerritoryID = '21',
	TerritoryDescription = 'Dambovita',
	RegionID = 7
where TerritoryID = 27511;
update Territories
set TerritoryID = '22',
	TerritoryDescription = 'Arges',
	RegionID = 7
where TerritoryID = 29202;
update Territories
set TerritoryID = '23',
	TerritoryDescription = 'Ialomita',
	RegionID = 7
where TerritoryID = 30346;
update Territories
set TerritoryID = '24',
	TerritoryDescription = 'Calarasi',
	RegionID = 7
where TerritoryID = 31406;
update Territories
set TerritoryID = '25',
	TerritoryDescription = 'Ilfov',
	RegionID = 7
where TerritoryID = 32859;
update Territories
set TerritoryID = '26',
	TerritoryDescription = 'Bucuresti',
	RegionID = 7
where TerritoryID = 33607;
update Territories
set TerritoryID = '27',
	TerritoryDescription = 'Giurgiu',
	RegionID = 7
where TerritoryID = 40222;
update Territories
set TerritoryID = '28',
	TerritoryDescription = 'Teleorman',
	RegionID = 7
where TerritoryID = 44122;
update Territories
set TerritoryID = '29',
	TerritoryDescription = 'Gorj',
	RegionID = 8
where TerritoryID = 45839;
/*Oltenia*/
update Territories
set TerritoryID = '30',
	TerritoryDescription = 'Valcea',
	RegionID = 8
where TerritoryID = 48075;
update Territories
set TerritoryID = '31',
	TerritoryDescription = 'Olt',
	RegionID = 8
where TerritoryID = 48084;
update Territories
set TerritoryID = '32',
	TerritoryDescription = 'Dolj',
	RegionID = 8
where TerritoryID = 48304;
update Territories
set TerritoryID = '33',
	TerritoryDescription = 'Mehedinti',
	RegionID = 8
where TerritoryID = 53404;
update Territories
set TerritoryID = '34',
	TerritoryDescription = 'Salaj',
	RegionID = 9
where TerritoryID = 55113;
/*Transilvania*/
update Territories
set TerritoryID = '35',
	TerritoryDescription = 'Bistrita-Nasaud',
	RegionID = 9
where TerritoryID = 55439;
update Territories
set TerritoryID = '36',
	TerritoryDescription = 'Cluj',
	RegionID = 9
where TerritoryID = 60179;
update Territories
set TerritoryID = '37',
	TerritoryDescription = 'Mures',
	RegionID = 9
where TerritoryID = 60601;
update Territories
set TerritoryID = '38',
	TerritoryDescription = 'Harghita',
	RegionID = 9
where TerritoryID = 72716;
update Territories
set TerritoryID = '39',
	TerritoryDescription = 'Covasna',
	RegionID = 9
where TerritoryID = 75234;
update Territories
set TerritoryID = '40',
	TerritoryDescription = 'Brasov',
	RegionID = 9
where TerritoryID = 78759;
update Territories
set TerritoryID = '41',
	TerritoryDescription = 'Sibiu',
	RegionID = 9
where TerritoryID = 80202;
update Territories
set TerritoryID = '42',
	TerritoryDescription = 'Alba',
	RegionID = 9
where TerritoryID = 80909;
update Territories
set TerritoryID = '43',
	TerritoryDescription = 'Hunedoara',
	RegionID = 9
where TerritoryID = 85014;
go

delete from Territories where TerritoryID = 90405;
delete from Territories where TerritoryID = 94025;
delete from Territories where TerritoryID = 94105;
delete from Territories where TerritoryID = 95008;
delete from Territories where TerritoryID = 95054;
delete from Territories where TerritoryID = 95060;
delete from Territories where TerritoryID = 98004;
delete from Territories where TerritoryID = 98052;
delete from Territories where TerritoryID = 98104;
delete from Territories where TerritoryID = 85251;

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
--	Product

--eBookReader select * from Products
update Products set ProductName='Kindle 6 Glare Touch Screen WiFi Black',	SupplierID=1,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=339	where ProductID=1
update Products set ProductName='Kindle PaperWhite Model 2015 Black',		SupplierID=2,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=629	where ProductID=2
update Products set ProductName='Kindle PaperWhite Model 2015 White',		SupplierID=2,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=599	where ProductID=3
update Products set ProductName='PocketBook Touch LUX 3 Red pb626',		  	SupplierID=1,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=569	where ProductID=4
update Products set ProductName='PocketBook Touch LUX 3 White pb626',	  	SupplierID=2,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=569	where ProductID=5
update Products set ProductName='PocketBook Touch LUX 3 Grey pb626',	  	SupplierID=1,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=569	where ProductID=6
update Products set ProductName='PocketBook Touch HD Black pb631',		  	SupplierID=4,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=799	where ProductID=7
update Products set ProductName='Bookeen CybooK Muse FrontLight Black',	  	SupplierID=3,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=699	where ProductID=8
update Products set ProductName='Prestigio MultiReader SUPREME 4GB Black',	SupplierID=2,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=549	where ProductID=9
update Products set ProductName='Bookeen Cybook Muse HD 8GB Black',		  	SupplierID=4,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=569	where ProductID=10
update Products set ProductName='Bookeen Cybook Muse Light 4GB Black ',	  	SupplierID=3,     CategoryID=5,     QuantityPerUnit=1,     UnitPrice=579	where ProductID=11
				  																																										
				  																																										
update Products set ProductName='CAT B25 Dual SIM Black',					SupplierID=2,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=229	where ProductID=12
update Products set ProductName='Nokia 3310 Dual SIM Dark Blue',			SupplierID=1,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=249	where ProductID=13
update Products set ProductName='Alcatel Tiger X3 1016G Black',				SupplierID=4,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=69		where ProductID=14
update Products set ProductName='Nokia 3310 Single Sim Orange',				SupplierID=1,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=249	where ProductID=15
update Products set ProductName='Nokia 130 Dual SIM Red',					SupplierID=2,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=99		where ProductID=16
update Products set ProductName='Alcatel 1054 White',						SupplierID=1,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=83		where ProductID=17
update Products set ProductName='Nokia 150 Single Sim White',				SupplierID=3,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=141	where ProductID=18
update Products set ProductName='MaxCom MM 141 Dual Sim Grey',				SupplierID=2,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=101	where ProductID=19
update Products set ProductName='Alcatel 2008G Black-Silver',				SupplierID=1,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=165	where ProductID=20
update Products set ProductName='Nokia 216 Dual Sim Black',					SupplierID=3,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=156	where ProductID=21
update Products set ProductName='Nokia 216 Dual SIM Grey',					SupplierID=1,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=156	where ProductID=22
update Products set ProductName='Karbonn K-flip Dual Sim White',			SupplierID=1,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=127	where ProductID=23
update Products set ProductName='MyPhone Metro Red',						SupplierID=2,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=209	where ProductID=24
update Products set ProductName='MyPhone 6310 Dual Sim Black',				SupplierID=3,     CategoryID=1,     QuantityPerUnit=1,     UnitPrice=104	where ProductID=25
				 																																										
				 																																										
				 																																										
update Products set ProductName='Bratara Xiaomi Silicon - Roz',				SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=24		where ProductID=26
update Products set ProductName='Bratara Xiaomi Silicon - Verde',			SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=24		where ProductID=27
update Products set ProductName='Curea Ceas 910XT GPS Negru',				SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=86		where ProductID=28
update Products set ProductName='Bratara Gear S3 Silicon Maron',			SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=127	where ProductID=29
update Products set ProductName='Curea Apple Watch 38mm Piele Neagra',		SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=964	where ProductID=30
update Products set ProductName='Dock Slate Union Pentru Apple Watch',		SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=217	where ProductID=31
update Products set ProductName='Dock Native Union Luxury Tech Marble',		SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=423	where ProductID=32
update Products set ProductName='Stand de incarcare Huawei Watch',			SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=95		where ProductID=33
update Products set ProductName='Cablu de incarcare Fitbit Flex',			SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=25		where ProductID=34
update Products set ProductName='Husa Apple Watch 38mm',					SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=49		where ProductID=35
update Products set ProductName='Bratara Smartwatch Silicon Argintiur',		SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=128	where ProductID=36
update Products set ProductName='Bratara Smartwatch Piele Neagra',			SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=115	where ProductID=37
update Products set ProductName='Bratara Smartwatch Silicon Khaki',			SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=86		where ProductID=38
update Products set ProductName='Bratara Smartwatch Silicon Blue Black',	SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=86		where ProductID=39
update Products set ProductName='Folie Protectie Curbata 42 mm Negra',		SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=36		where ProductID=40
update Products set ProductName='Folie Protectie Curbata 38 mm Negra',		SupplierID=4,     CategoryID=3,     QuantityPerUnit=1,     UnitPrice=36		where ProductID=41
																																														
																																														
																																														
update Products set ProductName='Apple iPhone 7 32GB Black',				SupplierID=1,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=2999	where ProductID=42
update Products set ProductName='OnePlus 5 A5000 64GB Dual SIM 4G Black',	SupplierID=2,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=2599	where ProductID=43
update Products set ProductName='Samsung Galaxy A3(2017) 4G Black',			SupplierID=3,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1199	where ProductID=44
update Products set ProductName='Samsung Galaxy J5(2016) Dual SIM Gold',	SupplierID=4,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=849	where ProductID=45
update Products set ProductName='Samsung Galaxy S8 G950F 64GB 4G Black',	SupplierID=1,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=2989	where ProductID=46
update Products set ProductName='Apple iPhone 6 32GB Space Gray',			SupplierID=2,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1899	where ProductID=47
update Products set ProductName='Samsung Galaxy J1 Mini Prime Black',		SupplierID=3,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=349	where ProductID=48
update Products set ProductName='Samsung Galaxy J1 Mini Prime Gold',		SupplierID=2,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=349	where ProductID=49
update Products set ProductName='Xiaomi Redmi 4A 32GB Dark Grey',			SupplierID=4,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=499	where ProductID=50
update Products set ProductName='Lenovo Moto Z 32GB Dual Sim 4G Black',		SupplierID=3,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1659	where ProductID=51
update Products set ProductName='Samsung Galaxy S8 Plus 64GB 4G Black',		SupplierID=2,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=3549	where ProductID=52
update Products set ProductName='HTC 10 32GB 4G Gold',						SupplierID=1,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1799	where ProductID=53
update Products set ProductName='Huawei P10 Lite 32GB Dual Sim 4G Gold',	SupplierID=3,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1199	where ProductID=54
update Products set ProductName='Apple iPhone SE 32GB Space Gray',			SupplierID=1,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1699	where ProductID=55
update Products set ProductName='Huawei P10 Lite 32GB Dual Sim 4G Black',	SupplierID=3,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1199	where ProductID=56
update Products set ProductName='Samsung Galaxy J1 Prime White',			SupplierID=1,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=339	where ProductID=57
update Products set ProductName='Huawei P10 Lite 32GB Blue',				SupplierID=4,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1199	where ProductID=58
update Products set ProductName='Samsung Galaxy S6 Edge 32GB Black',		SupplierID=4,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1899	where ProductID=59
update Products set ProductName='Sony Xperia X Compact 32GB 4G Black',		SupplierID=4,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1599	where ProductID=60
update Products set ProductName='LG G5 SE H840 32GB Titanium Grey',			SupplierID=4,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=1349	where ProductID=61
update Products set ProductName='iPhone 6s 32GB 32gb Space Grey',			SupplierID=4,     CategoryID=2,     QuantityPerUnit=1,     UnitPrice=2599	where ProductID=62
																																														
																																														
update Products set ProductName='Boxa Portabila Emie Cybertron Wireless',	SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=749	where ProductID=63
update Products set ProductName='Ochelari Samsung Gear VR 2 SM-R323 Negru',	SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=209	where ProductID=64
update Products set ProductName='Manusi cu Casca Bluetooth Hi-Fun M Black',	SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=229	where ProductID=65
update Products set ProductName='Dispozitiv monitorizare somn SenSe Sleep',	SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=119	where ProductID=66
update Products set ProductName='Telecomanda Bluetooth Esperanza',			SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=28		where ProductID=67
update Products set ProductName='Caciula Stereo cu Microfon Negru',			SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=37		where ProductID=68
update Products set ProductName='Dispozitiv localizare cu  Seeker',			SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=29		where ProductID=69
update Products set ProductName='Telecomanda Media-Tech pentru VR',			SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=39		where ProductID=70
update Products set ProductName='Drona Arcade Orbit',						SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=149	where ProductID=71
update Products set ProductName='Camera Video Fondi OnReal Negru',			SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=514	where ProductID=72
update Products set ProductName='Telecomanda Arcade Bluetooth',				SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=74		where ProductID=73
update Products set ProductName='Robot Inteligent Interactiv Ubtech Alpha',	SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=2369	where ProductID=74
update Products set ProductName='Robot Inteligent de Serviciu Uno',			SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=3249	where ProductID=75
update Products set ProductName='Boxa Portabila Flip 4 Waterproof Black',	SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=579	where ProductID=76
update Products set ProductName='Boxa Portabila  Wireless Cu Microfon',		SupplierID=1,     CategoryID=4,     QuantityPerUnit=1,     UnitPrice=499	where ProductID=77

go

----------------------------------------------------------------------------------
--		Suppliers

update Suppliers 
	set CompanyName = 'EURO GSM IMPEX S.R.L.',
	ContactName = 'Ion Vasilde',
	ContactTitle = 'Proprietar',
	[Address] = 'B-dul Muncii nr.18',
	City = 'CLUJ-NAPOCA',
	Region = null,
	PostalCode = '400641',
	Country = 'Romania',
	Phone = '0264450450',
	Fax = 'null',
	HomePage = 'https://eurogsm.ro'
where CompanyName = 'Exotic Liquids';
update Suppliers 
	set CompanyName = 'GERSIM IMPEX S.R.L.',
	ContactName = 'Mircea Daniel',
	ContactTitle = 'Manager depozit',
	[Address] = 'Strada Bilciurești 9A',
	City = 'BUCURESTI',
	Region = null,
	PostalCode = '014012',
	Country = 'Romania',
	Phone = '0213264850',
	Fax = '0213264851',
	HomePage = 'http://www.gersim.ro'
where CompanyName = 'New Orleans Cajun Delights';
update Suppliers 
	set CompanyName = 'EMAG S.A.',
	ContactName = 'Dumitru George',
	ContactTitle = 'Agent Vanzari',
	[Address] = 'Windsor Building Sos. Bucureşti Nord nr. 15-23',
	City = 'ILFOV',
	Region = 'null',
	PostalCode = '077190',
	Country = 'Romania',
	Phone = '0722.25.00.00',
	Fax = null,
	HomePage = 'https://emag.ro'
where CompanyName like 'Grandma Kelly%' and CompanyName like '%s Homestead';
update Suppliers 
	set CompanyName = 'SC MEDIA GALAXY S.R.L.',
	ContactName = 'Popescu Mihai',
	ContactTitle = 'Reprezentant Vanzari',
	[Address] = 'Bulevardul Poligrafiei Nr.1, Sector 1',
	City = 'Bucuresti',
	Region = null,
	PostalCode = '400641',
	Country = 'Romania',
	Phone = '0212062000',
	Fax = '0213199939',
	HomePage = 'www.mediagalaxy.ro'
where CompanyName = 'Tokyo Traders';

delete from Suppliers where CompanyName like 'Cooperativa de Quesos%';


delete from Suppliers where CompanyName like 'Cooperativa de Quesos %' and CompanyName like '%Las Cabras';
delete from Suppliers where CompanyName like 'Mayumi%';
delete from Suppliers where CompanyName = 'Pavlova, Ltd.';
delete from Suppliers where CompanyName = 'Specialty Biscuits, Ltd.';
delete from Suppliers where CompanyName = 'PB Knäckebröd AB';
delete from Suppliers where CompanyName = 'Refrescos Americanas LTDA';
delete from Suppliers where CompanyName = 'Heli Süßwaren GmbH & Co. KG';
delete from Suppliers where CompanyName = 'Plutzer Lebensmittelgroßmärkte AG';
delete from Suppliers where CompanyName = 'Nord-Ost-Fisch Handelsgesellschaft mbH';
delete from Suppliers where CompanyName = 'Formaggi Fortini s.r.l.';
delete from Suppliers where CompanyName = 'Norske Meierier';
delete from Suppliers where CompanyName = 'Bigfoot Breweries';
delete from Suppliers where CompanyName = 'Svensk Sjöföda AB';
delete from Suppliers where CompanyName = 'Aux joyeux ecclésiastiques';
delete from Suppliers where CompanyName = 'New England Seafood Cannery';
delete from Suppliers where CompanyName = 'Leka Trading';
delete from Suppliers where CompanyName = 'Lyngbysild';
delete from Suppliers where CompanyName = 'Zaanse Snoepfabriek';
delete from Suppliers where CompanyName = 'Karkki Oy';
delete from Suppliers where CompanyName like '%day, Mate';
delete from Suppliers where CompanyName = 'Ma Maison';
delete from Suppliers where CompanyName = 'Pasta Buttini s.r.l.';
delete from Suppliers where CompanyName = 'Escargots Nouveaux';
delete from Suppliers where CompanyName = 'Gai pâturage';
delete from Suppliers where CompanyName like 'Forêts d%' and CompanyName like '%érables';

----------------------------------------------------------------------------------
--	Category Changes

update Categories set CategoryName='Clasice',Description='Telefoane cu butoane' where CategoryID=1
update Categories set CategoryName='Smartphone',Description='Touchscreen' where CategoryID=2
update Categories set CategoryName='Accesorii',Description='Selfie sticks, Incarcatoare, Casti, Baterii, Huse' where CategoryID=3
update Categories set CategoryName='Gadgeturi',Description='Boxe, Ochelari VR, Telecomenzi' where CategoryID=4
update Categories set CategoryName='eBook Reader',Description='Bookreader' where CategoryID=5
if(not exists (select * from Categories where Categories.CategoryID = 6 ))
begin
	SET IDENTITY_INSERT Categories  on;
	insert into Categories (CategoryID, CategoryName, [Description]) values (6, 'Servicii', 'Services that we offer')
	set identity_insert Categories off;
end
else
begin
	update Categories set CategoryName='Servicii',Description='Servicii oferite' where CategoryID=6
end
delete from Categories where CategoryID between 7 and 8
go

--Insert 10 services
if(not exists (select * from Products where ProductID = 78))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Diagnosticare', 1, 6, 1, 60, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Diagnosticare', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 60, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 78;
end

if(not exists (select * from Products where ProductID = 79))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Inlocuire Baterie', 1, 6, 1, 120, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Inlocuire Baterie', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 120, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 79;
end

if(not exists (select * from Products where ProductID = 80))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Inlocuire ecran', 1, 6, 1, 400, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Inlocuire ecran', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 400, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 80;
end

if(not exists (select * from Products where ProductID = 81))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Inlocuire folie de protectie', 1, 6, 1, 70, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Inlocuire folie de protectie', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 70, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 81;
end

if(not exists (select * from Products where ProductID = 82))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Inlocuire placa de baza', 1, 6, 1, 900, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Inlocuire placa de baza', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 900, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 82;
end

if(not exists (select * from Products where ProductID = 83))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Instalare android', 1, 6, 1, 80, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Instalare android', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 80, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 83;
end


if(not exists (select * from Products where ProductID = 84))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Instalare IOS', 1, 6, 1, 120, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Instalare IOS', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 120, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 84;
end


if(not exists (select * from Products where ProductID = 85))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Instalare windows phone', 1, 6, 1, 80, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Instalare windows phone', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 80, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 85;
end


if(not exists (select * from Products where ProductID = 86))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Recuperare date windows phone', 1, 6, 1, 140, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Recuperare date windows phone', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 140, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 86;
end


if(not exists (select * from Products where ProductID = 87))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Recuperare date android', 1, 6, 1, 120, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Recuperare date android', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 120, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 87;
end


if(not exists (select * from Products where ProductID = 88))
begin
	insert into Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
	values ('Recuperare date iOS', 1, 6, 1, 160, 0, 0, 1, 0);
end
else
begin
	update Products	set ProductName = 'Recuperare date iOS', SupplierID = 1, CategoryID = 6, QuantityPerUnit = 1, UnitPrice = 160, UnitsOnOrder = 0, ReorderLevel = 1 where ProductID = 88;
end


-- add 20 order for services
if(not exists (
	select * from [Order Details]
	left join Products on [Order Details].ProductID = Products.ProductID
	left join Categories on Products.CategoryID = Categories.CategoryID
	where Categories.CategoryName = 'Servicii'
))
begin
	begin transaction
		--declare variable for orders
		declare @orderID int;
		declare @serviceID int = 0;
		declare @customerRow int = 0;
		declare @employeeID int = 0;
		declare @shipperID int = 0;

		SELECT Customers.CustomerID,Customers.[Address], City, Region, PostalCode, Country, ROW_NUMBER() OVER (ORDER BY CustomerID) AS 'RowNumber' into #CustomersIDSelect
		FROM Customers;

		select ProductID, UnitPrice, ROW_NUMBER() OVER (ORDER BY ProductID) AS 'RowNumber' into #ServicesIDRow 
		from Products left join Categories on Products.CategoryID = Categories.CategoryID where CategoryName = 'Servicii';
				
		--with this while we add orders
		declare @i int = 0;
		while(@i < 20)
		begin
			
			--insert 20 orders
			insert into Orders Values(
				(select CustomerID from #CustomersIDSelect where RowNumber = @customerRow%(select count(CustomerID) from Customers) + 1), --customerID
				(@employeeID%(select count(Employees.EmployeeID) from Employees) + 1), --EmployeeID
				'1998-01-01', --orderDate
				'1998-02-01', --RequiredDate
				'1998-06-01', --ShippedDate
				(@shipperID%(select count(Shippers.ShipperID) from Shippers) + 1), --ShiperID
				1, --incarcatura
				'Servicii', --nume transport
				(select [Address] from #CustomersIDSelect where RowNumber = (@customerRow%(select count(CustomerID) from #CustomersIDSelect) + 1)),
				(select City from #CustomersIDSelect where RowNumber = (@customerRow%(select count(CustomerID) from #CustomersIDSelect) + 1)),
				(select Region from #CustomersIDSelect where RowNumber = (@customerRow%(select count(CustomerID) from #CustomersIDSelect) + 1)),
				(select PostalCode from #CustomersIDSelect where RowNumber = (@customerRow%(select count(CustomerID) from #CustomersIDSelect) + 1)),
				(select Country from #CustomersIDSelect where RowNumber = (@customerRow%(select count(CustomerID) from #CustomersIDSelect) + 1))
			);
			
			
			--declare variable for order detail 
			set @orderID = (select max(Orders.OrderID) from Orders);
			--while for order details
			declare @ii int = 0;
			while(@ii < 5)
			begin
				insert into [Order Details] values(
				@orderID,
				(select ProductID from #ServicesIDRow where RowNumber = (@serviceID%(select count(ProductID) from #ServicesIDRow) + 1)),
				(select UnitPrice from #ServicesIDRow where RowNumber = (@serviceID%(select count(ProductID) from #ServicesIDRow) + 1)),
				1,
				0
				)
				update Products 
					set UnitsOnOrder = UnitsOnOrder + 1 
					where ProductID = (select ProductID from [Order Details] where OrderID = @orderID and ProductID = (select ProductID from #ServicesIDRow where RowNumber = (@serviceID%(select count(ProductID) from #ServicesIDRow) + 1)));
				set @ii = @ii + 1;
				set @serviceID = @serviceID + 1;
			end

			--increment
			set @shipperID = @shipperID + 1;
			set @employeeID = @employeeID + 1;
			set @customerRow = @customerRow + 1;

			set @i = @i + 1;
		end
		if(@@ERROR != 0)
		begin
			rollback transaction
		end
	commit transaction;
end

UPDATE orders set OrderDate=DATEADD(YEAR,19,OrderDate),RequiredDate=DATEADD(YEAR,19,RequiredDate),ShippedDate=DATEADD(YEAR,19,ShippedDate);

