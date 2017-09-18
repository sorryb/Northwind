#Database Initialization
	First time, before starting the web server, we need to chose what DataBase we want to use.
	We have three options:
		A. Full initialization. This type of DataBase Initializtion allow us to start the site with a lot of Orders, Customers, Territories and more
			Implementation:
			1.Go in NorthwindDB project -> dbo -> Scripts
			2.Run Northwind.sql to make Northwind DataBase on your SQL Server
			3.Run Translate.sql to translate in Romanian the above data
			4.Make sure that in WebConfig -> connectionStrings -> NwModel connection string match the Northwind DataBase connection string from your SQL Server
		B. Small initialization. This will fill the database with Territories and Products, and with some portions of the Customers, Orders, (and others) from data that point A would add in DataBase
			Implementation:
			1.Change the connectionString in WebConfig -> connectionStrings -> NwModel to what you want (this is where the database will be made)
			2.Start the application. Now first time when the site will need something from Northwind DataBase the DataBase will be made
		C. Without initialization
			Implementation:
			1.Go in Models -> NorthwindModel.cs and in constructor comment this line: Database.SetInitializer(new NorthwindDatabaseInitializer());
			2.Start the application. Now first time when the site will need something from Northwind DataBase the empty DataBase will be made


#Users
	The main users are: username: admin, password: 123456;
						username: tester, password: Tester_1;

	Users are stored in the aspnet-Northwind-20170823114137.mdf database
	User information is created, encrypted and saved with aspnet.Identity
	The first use of any operation involving the user, the aspnet-Northwind-20170823114137.mdf database is initialized (ex: Login)
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
	             Managers: They have access to the list of all orders
				 Admins: They have access to the dashboard where they can manage the database and see company statistics

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
		userId: the report server requires a username and a password to display it's reports
		password: ...the password
		If it doesn't work and you don't remember setting up a username and a password try the ones you use to log
		in to windows.
