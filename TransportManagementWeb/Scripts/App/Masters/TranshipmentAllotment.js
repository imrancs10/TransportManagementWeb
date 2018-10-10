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
    FillReferenceIds();
    function FillReferenceIds() {
        let dropdown = $('#ReferenceDropdown');
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
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
        //let dropdown = $('#VehicleDetailDropdown');
        //dropdown.empty();
        //dropdown.append('<option value="">Select</option>');
        //dropdown.prop('selectedIndex', 0);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetServiceOrderDetail',
            data: '{Id: "' + serviceOrderId + '" }',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var jsonData = jQuery.parseJSON(data);
                $('#deadlineDate').text(jsonData.VehicleRequirementDate);
                $('#clientName').text(jsonData.ClientDetail.ClientName);
                if (jsonData.TranshipmentDetails.length === 1) {
                    $('#singleTranshipment').removeClass('hidden');
                    $('#multipleTranshipment').addClass('hidden');
                    $('#singleFromCityLabel').text(jsonData.TranshipmentDetails[0].City.CityName);
                    $('#singleToCityLabel').text(jsonData.TranshipmentDetails[0].City1.CityName);
                }
                else {
                    $('#singleTranshipment').addClass('hidden');
                    $('#multipleTranshipment').removeClass('hidden');
                    $('#multiFromCityLabel').text(jsonData.TranshipmentDetails[0].City.CityName);

                    $('#multiToCityLabel1').text(jsonData.TranshipmentDetails[0].City1.CityName);
                    $('#multiWeightLabel1').text(jsonData.TranshipmentDetails[0].WeightLookup.Weight);
                    $('#multiUnitLabel1').text(jsonData.TranshipmentDetails[0].UnitDetail.Name);

                    $('#multiToCityLabel2').text(jsonData.TranshipmentDetails[1].City1.CityName);
                    $('#multiWeightLabel2').text(jsonData.TranshipmentDetails[1].WeightLookup.Weight);
                    $('#multiUnitLabel2').text(jsonData.TranshipmentDetails[1].UnitDetail.Name);
                }
                $('#VehicleTypeLabel').text(jsonData.VehicleDetail.VehicleType.VehicleTypeName);
                $('#VehicleDetailLabel').text(jsonData.VehicleDetail.VehicleName);
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
    $('#radioTranshipmentSingle').on('change', function (e) {
        $('#singleTranshipment').removeClass('hidden');
        $('#multipleTranshipment').addClass('hidden');
    });
    $('#radioTranshipmentMulti').on('change', function (e) {
        $('#singleTranshipment').addClass('hidden');
        $('#multipleTranshipment').removeClass('hidden');
    });
});

