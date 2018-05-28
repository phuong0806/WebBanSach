$('#CategoryID').chosen();
$('#AuthorID').chosen();
$("#AuthorID").chosen({ no_results_text: "Không tìm thấy :(( " });
$('#AuthorID').trigger('chosen:updated');
