Tot ce trebuie sa faceti pentru a folosi un datatable este sa creati un tabel cu urmatoarea structura:
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

si apoi sa rulati urmatorul script:
<script>
	$("#example").DataTable();
</script>

Acesta va transforma tabelul vostru intr-un datatable cu setarile implicite, cum ar fi cautare si paginatie.
Pentru a va personaliza tabelul puteti sa adaugati optiuni in functia Datatable() in felul urmator:
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
O lista cu toate optiunile se gaseste aici:  https://datatables.net/reference/option/.

O alta modalitate usoara de a personaliza tabelul este sa adaugati clase in tag-ul <table>.
Urmatoarele clase afecteaza scriptul jquery:
	display
	cell-border
	compact
	hover
	nowrap
	order-column
	row-border
	stripe

O explicatie mai detaliata se gaseste aici: https://datatables.net/manual/styling/classes.