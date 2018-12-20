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
        var valueSelected = this.value;
        FillInvoiceDetail(valueSelected);
    });
    function FillInvoiceDetail(lrId) {
        let dropdown = $('#InvoiceDropdown');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetAllInvoiceDetailByLRId',
            data: '{LRId: "' + lrId + '" }',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var jsonData = JSON.parse(data);
                $.each(jsonData, function (key, entry) {
                    dropdown.append($('<option data-total=' + entry.ClientBillDescriptions[0].TotalAmount + '></option>').attr('value', entry.Id).text(entry.InvoiceNumber));
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
        var valueSelected = this.value;
        var totalInvoiceAmount = parseFloat($("option:selected", this).data('total'));
        $('#TotalAmount').text(totalInvoiceAmount);
        var lrId = $('#ConsignmentDropdown').val();
        //Get paid amount in Ledger entry
        //100 50(Credit)=50, 100(Debit)=150  
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetLedgerAmountByLRIdAndInvoiceId',
            data: '{LRId: "' + lrId + '", InvoiceId:"' + valueSelected + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#AmountToPay').val(totalInvoiceAmount + data);
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });


    });

    $('#TransactionAmount').blur(function () {
        var selectedRadio = $('input[type="radio"]:checked');
        var totalAmount = parseFloat($('#AmountToPay').val());
        var balAmount = 0;
        if ($(selectedRadio).prop('id').indexOf('Credit') > -1) {
            if (parseFloat($(this).val()) > totalAmount) {
                alert('Amount can not greater than Total remianing amount');
                $(this).val('');
            }
            else {
                balAmount = totalAmount - parseFloat($(this).val());
                $('#BalenceAmount').text(balAmount);
                $('#BalenceAmountText').val(balAmount);
            }
        }
        else {
            balAmount = totalAmount + parseFloat($(this).val());
            $('#BalenceAmount').text(balAmount);
            $('#BalenceAmountText').val(balAmount);
        }


    });

    $('input[type="radio"]').click(function () {
        if ($(this).prop('id').indexOf('Debit') > -1) {
            $('#TransactionType').val('Debit');
        }
        else {
            $('#TransactionType').val('Credit');
        }
    });

});

