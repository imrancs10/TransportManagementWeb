/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />
'use strict';
$(document).ready(function () {
    FillInvoiceDetail();
    function FillInvoiceDetail() {
        let dropdown = $('#InvoiceDropdown');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetAllInvoiceDetail',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.ClientId).text(entry.InvoiceNumber));
                });
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    $('#InvoiceDropdown').on('change', function (e) {
        var invoiceNo = this.value;
        var url = "/Masters/BillReport?invoiceNo=" + invoiceNo;
        window.location.href = url; 
        //FillBillDetail(valueSelected);
    });
    //function FillBillDetail(invoiceId) {
    //    $.ajax({
    //        dataType: 'json',
    //        type: 'POST',
    //        url: '/Masters/GetBillDetailByInvoiceId',
    //        data: '{invoiceId: "' + invoiceId + '" }',
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data) {
              
    //        },
    //        failure: function (response) {
    //            alert(response);
    //        },
    //        error: function (response) {
    //            alert(response.responseText);
    //        }
    //    });
    //}
});

