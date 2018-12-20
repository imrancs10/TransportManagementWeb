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
                    dropdown.append($('<option></option>').attr('value', entry.Id).text(entry.InvoiceNumber));
                });
                var invoiceId = getUrlParameter('invoiceNo');
                if (invoiceId !== null && typeof invoiceId !== 'undefined') {
                    $('#InvoiceDropdown').val(invoiceId);
                    //$('#InvoiceDropdown').change();
                }
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

    var getUrlParameter = function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
    };
    $(document).on('click', '#btnPrint', function () {
        var pageTitle = 'Bill Report',
            stylesheet = 'https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css',
            stylesheetCustom = '../styles/deamon.css',
            win = window.open('', 'Print', 'width=1000,height=1000');
        //$('#header').removeClass('hidden');
        //$('.table-responsive').removeClass('table-responsive');
        $('.table td').css('padding', '0px');
        $('#invoiceRow').addClass('hidden');
        $('#btnPrint').addClass('hidden');
        var dd = $('.content').clone();

        win.document.write('<html><head><title>' + pageTitle + '</title>' +
            '<link rel="stylesheet" href="' + stylesheet + '">' +
            '<link rel="stylesheet" href="' + stylesheetCustom + '">' +
            '</head><body><hr/>' + dd.html() + '</body></html > ');
        //win.document.close();
        //win.print();
        //win.close();
        //$('#header').addClass('hidden');
        //$('table.table-bordered').addClass('table-responsive');
        $('#invoiceRow').removeClass('hidden');
        $('.table td').css('padding', '8px');
        $('#btnPrint').removeClass('hidden');
        return false;
    });
});

