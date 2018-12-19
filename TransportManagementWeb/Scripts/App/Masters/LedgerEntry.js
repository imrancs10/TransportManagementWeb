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
                })
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
    $('#ClientDropdown').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        FillLRDetail(valueSelected);
    });
    function FillLRDetail(clientId) {
        let dropdown = $('#ConsignmentDropdown');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{clientId: "' + clientId + '" }',
            url: '/Masters/GetAllLRDetails',
            success: function (data) {
                var jsonData = JSON.parse(data);
                $.each(jsonData, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.Id).text(entry.LRNumber));
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
    $('#ConsignmentDropdown').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        FillInvoiceDetail(valueSelected);
    });
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

});

