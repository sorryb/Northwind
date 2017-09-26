#Database Initialization
	First time, before starting the web server, we need to chose what DataBase we want to use.
	We have three options:
		A. Test Initialisation. This will fill the database with test data
			Implementation:
			1.Change the connectionString in WebConfig -> connectionStrings -> NorthwindDatabaseConnection to what you want (this is where the database will be created)
			2.Go in Context -> NorthwindDatabaseInitializer.cs and make sure that the following functions are not commented: NorthwindReferencedTableInitializer.InsertNorthwindReferencedData(context);
																															 NorthwindTestDatabaseInitializer.InsertNorthwindTestData(context);
			3.Start the application. Now first time when the site will need something from Northwind DataBase the DataBase will be created
		B. Empty Initialisation:
			1.Change the connectionString in WebConfig -> connectionStrings -> NorthwindDatabaseConnection to what you want (this is where the database will be created)
			2.Go in Context -> NorthwindDatabaseInitializer.cs and comment: 
				-b if you want to keep the referenced database data
				-b and a if you want an empty database
					a) NorthwindReferencedTableInitializer.InsertNorthwindReferencedData(context);
					b) NorthwindTestDatabaseInitializer.InsertNorthwindTestData(context);
			3.Start the application. Now first time when the site will need something from Northwind DataBase the DataBase will be created
		Note:
		You can also create a test database by using the following two scripts.
			Implementation:
			1.Go in NorthwindDB project -> dbo -> Scripts
			2.Run Northwind.sql to make Northwind DataBase on your SQL Server
			3.Run Translate.sql to translate in Romanian the above data
			4.Make sure that in WebConfig -> connectionStrings -> NwModel connection string match the Northwind DataBase connection string from your SQL Server


#Users
	The main users are: username: admin, password: 123456;
						username: tester, password: Tester_1;

	Users are stored in the Northwind database
	User information is created, encrypted and saved with aspnet.Identity
	The first use of any operation involving the user, the Northwind database is initialized with user tables(ex: Login)
	There are two static users: admin, tester; rest being done for employees from the northwind database
	The default roles created at initialization are:Admins, Employees, Managers, Guest, Customers they offer different degrees of access
	admin, tester and users for employees are set in IdentityDatabaseInitializer called from ApplicationDataContext if it does not exist aspnet-Northwind-20170823114137.mdf database
	They are set with username, password and email, and are assigned to the Admins role

#Roles
    There are five roles, they offer different degrees of access for two categories of Customers and Employees
	Anyone logged in or out has access to the eCommerce interface

	  Customers: Guest: They have the lowest degree of access, they only have access to change the password
	             Customers: When a user places his first order, he becomes Customers and can see their orders

      Employees: Employeers: They have access to the list of orders they handle
	             Managers: They have access to the list of all orders and dashboard
				 Admins: They are the only ones who have access to security administration

#DataTable
	All you need to do to enable datatables is to create a table with the following structure:
	<table id="example" class="">
		<thead>
			<tr>
				<th>
				</th>
				.
				.
				.
			</tr>
		</thead>
		<tbody>
			<tr>
				<td>
				</td>
				.
				.
				.
			</tr>
		</tbody>
	<table>

	and then run the following script:
	<script>
		$("#example").DataTable();
	</script>

	This will transform your table into a dataTable with the default options, such as search and pagination.
	To further customize your datatable you can add options to the DataTable() function like so:
	<script>
		$("#example").DataTable(
		{
			"autowidth":true,
			"info":true,
			.
			.
			.
		});
	</script>
	The options reference can be found here https://datatables.net/reference/option/.

	Another simple way to customize your datatable is to add classes to the <table> tag.
	The following classes affect the jquery script:
		display
		cell-border
		compact
		hover
		nowrap
		order-column
		row-border
		stripe

	A more detailed explanation can be found here https://datatables.net/manual/styling/classes.


#WebConfig
	Reports configuration
		ReportServer: the link to your report server
		ReportServerDirectory: the folder in which your reports are stored on your report server
	Key pageSize represents the number of items in the paging list returned by the ToPagedList function