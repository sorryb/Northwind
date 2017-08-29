$(document).ready(function () {
    var table=$('#MyTable').DataTable({
        paging: true,
        responsive: true,
        columnDefs: [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        orderClasses:true
    });
});