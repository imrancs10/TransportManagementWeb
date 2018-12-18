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
        FillLRDescription(valueSelected);
    });

    function FillLRDescription(valueSelected) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{LRId: "' + valueSelected + '" }',
            url: '/Masters/GetLRDetailByLRId',
            success: function (data) {
                var jsonData = JSON.parse(data);
                if (jsonData.ServiceOrderDetail !== null) {
                    $('#txtLoadingCharge').val(jsonData.ServiceOrderDetail.ServiceOrderPaymentDetails[0].LoadingCharge);
                    $('#txtUnloadingCharge').val(jsonData.ServiceOrderDetail.ServiceOrderPaymentDetails[0].UnloadingCharge);
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

    $('#ConsignmentCharges').on('blur', function (e) {
        var chargePerUnit = $(this).val();
        var quantity = $('#ConsignmentQty').val();
        if (chargePerUnit !== '' && quantity !== '') {
            $('#ConsignmentTotal').val(parseFloat(chargePerUnit) * parseFloat(quantity));
        }
    });

    $('#ConsignmentDiscount').on('blur', function (e) {
        var percentageDiscount = $(this).val();
        if (percentageDiscount > 100) {
            utility.alert.setAlert(utility.alert.alertType.info, 'Discount can not more than 100');
            $(this).val('');
        }
        else {
            var discount = ($('#ConsignmentTotal').val() * parseFloat(percentageDiscount)) / 100;
            $('#ConsignmentDiscountAmount').val(discount);
            $('#ConsignmentGrandTotal').val(parseFloat($('#ConsignmentTotal').val()) - parseFloat(discount));

            //fill gst and charges
            var grandTotal = $('#ConsignmentGrandTotal').val();
            $('#txtAddCGST').val((parseFloat(grandTotal) * 2.5) / 100);
            $('#txtAddSGST').val((parseFloat(grandTotal) * 2.5 / 100));
            $('#txtAddIGST').val((parseFloat(grandTotal) * 5 / 100));
        }
    });

    $('#txtAddCess').on('blur', function (e) {
        if ($(this).val() !== '') {
            var totalTax = parseFloat($(this).val()) + parseFloat($('#txtAddCGST').val())
                                                    + parseFloat($('#txtAddSGST').val())
                                                    + parseFloat($('#txtAddIGST').val());
            $('#txtTotalTax').val(totalTax);
        }
    });

    $('#btnSave').click(function () {
        var BillJson = {
            ClientId: $('#ClientDropdown').val(),
            DocketCharge: $('#txtDocketCharge').val(),
            CGST: $('#txtAddCGST').val(),
            SGST: $('#txtAddSGST').val(),
            IGST: $('#txtAddIGST').val(),
            LoadingCharge: $('#txtLoadingCharge').val(),
            Cess: $('#txtAddCess').val(),
            Tax: $('#txtTotalTax').val(),
            RoundOff: $('#txtRoundOff').val(),
            InvoiceDate: $('#InvoiceDate').val(),
            UnloadingCharge: $('#txtUnloadingCharge').val(),
            InvoiceNumber: getInvoiceNumber(),
            AdvanceAmount: $('#txtAdvanceAmount').val(),
            ClientBillDescriptions: []
        };
        var Consignment = $('#ConsignmentDropdown').val();
        if (Consignment !== '') {
            BillJson.ClientBillDescriptions.push(
                {
                    Description: $('#ConsignmentDesc').val(),
                    SACCode: $('#ConsignmentSACCode').val(),
                    ConsighmentNumber: $('#ConsignmentDropdown').val(),
                    Quantity: $('#ConsignmentQty').val(),
                    Charges: $('#ConsignmentCharges').val(),
                    Total: $('#ConsignmentTotal').val(),
                    DiscountPercentage: $('#ConsignmentDiscount').val(),
                    Discount: $('#ConsignmentDiscountAmount').val(),
                    TotalAmount: $('#ConsignmentGrandTotal').val()
                });
        }

        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/BillEntrySave',
            data: JSON.stringify(BillJson),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                
            },
            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

    });

    function getInvoiceNumber() {
        return Math.random();
    }
});

