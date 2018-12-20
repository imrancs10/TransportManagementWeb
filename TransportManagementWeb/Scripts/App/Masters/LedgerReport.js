/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />
'use strict';
$(document).ready(function () {
    FillClientDetail();
    function FillClientDetail() {
        let dropdown = $('#ClientDropdown');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetAllClientDetail',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.ClientId).text(entry.ClientName));
                });
                var clientId = $('#HiddenClientId').val();
                var fromDate = $('#HiddenFromDate').val();
                var toDate = $('#HiddenToDate').val();
                if (clientId !== '') {
                    $('#ClientDropdown').val(clientId);
                }
                if (fromDate !== '') {
                    $('#FromDate').val(fromDate);
                }
                if (toDate !== '') {
                    $('#ToDate').val(toDate);
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

    $('#ToDate').blur(function () {
        var fromdate = $('#FromDate').val();
        var todate = $(this).val();
        if ((new Date(fromdate).getTime() > new Date(todate).getTime())) {
            utility.alert.setAlert(utility.alert.alertType.warning, "To Date Can not greater than from date");
            $(this).val('');
        }
    });
    $(document).on('click', '#btnPrint', function () {
        var pageTitle = 'Ledger Report',
            stylesheet = 'https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css',
            stylesheetCustom = '../styles/deamon.css',
            win = window.open('', 'Print', 'width=1000,height=1000');
        $('#header').removeClass('hidden');
        var htmlDOM = $('.table-responsive').clone();

        win.document.write('<html><head><title>' + pageTitle + '</title>' +
            '<link rel="stylesheet" href="' + stylesheet + '">' +
            '<link rel="stylesheet" href="' + stylesheetCustom + '">' +
            '</head><body><hr/>' + htmlDOM.html() + '</body></html > ');
        //win.document.close();
        //win.print();
        //win.close();
        $('#header').addClass('hidden');
        return false;
    });
});

