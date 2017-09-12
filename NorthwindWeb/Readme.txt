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


#Users
Users are stored in the aspnet-Northwind-20170823114137.mdf database
User information is encoded with aspnet.Identity
First use of any user aspnet-Northwind-20170823114137.mdf database is initialized(ex:Login)
There are two static users: admin, tester; rest being done for employees from the northwind database
admin, tester are set in IdentityDatabaseInitializer appealed from ApplicationDataContext
they are set with username, password and email, and are assigned to the Admins role