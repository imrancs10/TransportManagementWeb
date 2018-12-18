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
            url: '/Masters/GetAllReferenceIdsForLRPage',
            success: function (data) {
                var jsonData = JSON.parse(data);
                $.each(jsonData, function (key, entry) {
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

    fillAllCity('#ConsignorCity');
    fillAllCity('#ConsigneeCity');
    function fillAllCity(dropdownId) {
        let dropdown = $(dropdownId);
        dropdown.empty();
        dropdown.append('<option value="">Select</option>');
        dropdown.prop('selectedIndex', 0);
        $.ajax({
            dataType: 'json',
            type: 'POST',
            url: '/Masters/GetAllCities',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $.each(data, function (key, entry) {
                    dropdown.append($('<option></option>').attr('value', entry.CityId).text(entry.CityName));
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
});

