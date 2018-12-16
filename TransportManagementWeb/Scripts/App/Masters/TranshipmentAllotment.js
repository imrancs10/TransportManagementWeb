/// <reference path="../../jquery-1.10.2.js" />
/// <reference path="../Global/App.js" />
/// <reference path="../Global/Utility.js" />
'use strict';
$(document).ready(function () {
    FillVendorDetail();
    function FillVendorDetail() {
        let dropdown = $('#VendorDropdown');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetAllVendorDetail',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.VendorId).text(entry.VendorName));
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
        FillReferenceIds(valueSelected);
    });
    function FillReferenceIds(clientId) {
        let dropdown = $('#ReferenceDropdown');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{clientId: "' + clientId + '" }',
            url: '/Masters/GetAllReferenceIds',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.Id).text(entry.ReferenceNumber));
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
    $('#ReferenceDropdown').on('change', function (e) {
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        FillServiceOrderDetail(valueSelected);
    });
    function FillServiceOrderDetail(serviceOrderId) {
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetServiceOrderDetail',
            data: '{Id: "' + serviceOrderId + '" }',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var jsonData = data;
                $('#deadlineDate').text(jsonData.VehicleRequirementDate);
                $('#clientName').text(jsonData.ClientName);
                if (jsonData.Transhipments.length === 1) {
                    $('#singleTranshipment').removeClass('hidden');
                    $('#multipleTranshipment').addClass('hidden');
                    $('#singleFromCityLabel').text(jsonData.Transhipments[0].FromCity);
                    $('#singleToCityLabel').text(jsonData.Transhipments[0].ToCity);
                }
                else {
                    $('#singleTranshipment').addClass('hidden');
                    $('#multipleTranshipment').removeClass('hidden');
                    $('#multiFromCityLabel').text(jsonData.Transhipments[0].FromCity);

                    $('#multiToCityLabel1').text(jsonData.Transhipments[0].ToCity);
                    $('#multiWeightLabel1').text(jsonData.Transhipments[0].Weight);
                    $('#multiUnitLabel1').text(jsonData.Transhipments[0].UnitName);

                    $('#multiToCityLabel2').text(jsonData.Transhipments[1].ToCity);
                    $('#multiWeightLabel2').text(jsonData.Transhipments[1].Weight);
                    $('#multiUnitLabel2').text(jsonData.Transhipments[1].UnitName);
                }
                $('#VehicleTypeLabel').text(jsonData.VehicleTypeName);
                $('#VehicleDetailLabel').text(jsonData.VehicleName);
                $('#GrossWeight').text(jsonData.GrossWeight);
                $('#NatureOfGoods').text(jsonData.NatureOfGoods);
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

