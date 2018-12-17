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
                var ClientÌd = getUrlParameter('Client%C3%8Cd');
                if (ClientÌd !== null && typeof ClientÌd !== 'undefined') {
                    $('#ClientDropdown').val(ClientÌd);
                    $('#ClientDropdown').change();
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
            url: '/Masters/GetAllReferenceIdsForFreightPage',
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.Id).text(entry.ReferenceNumber));
                });
                var refId = getUrlParameter('referenceId');
                if (refId !== null && typeof refId !== 'undefined') {
                    $('#ReferenceDropdown').val(refId);
                    $('#ReferenceDropdown').change();
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
                if (jsonData.Transhipments.length === 1) {
                    $('#singleFromCityLabel').text(jsonData.Transhipments[0].FromCity);
                    $('#singleToCityLabel').text(jsonData.Transhipments[0].ToCity);
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
    $("#AdvanceFreight").blur(function () {
        var totalFreight = parseFloat($("#TotalFreight").val());
        var advanceFreight = parseFloat($(this).val());
        if (advanceFreight > totalFreight) {
            alert('Adance Freight can not greater than Total Freight');
            $(this).val('');
            return false;
        }
        $('#BalanceFreight').val(totalFreight - advanceFreight);
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
});

