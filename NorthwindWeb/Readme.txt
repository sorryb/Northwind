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


