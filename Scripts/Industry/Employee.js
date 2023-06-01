var Employee = function () {
    var init = function () {
        
        loaddata();
        GetCountry();
    }
    var GetCountry = function () {
        
        $.ajax({
            type: "POST",
            url: "/IndustryDemo/GetCountry",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
           
            success: function (data) {
                
                var strHtml = '<option value="-1">---Please Select---</option>';
                for (var i = 0; i < data.length; i++) {
                    strHtml += '<option value="' + data[i].CountryId + '">' + data[i].CountryName + '</option>';
                    //OnSuccess(data);
                }
                $("#ddlCountry").html(strHtml);
            },
            error: function (response) {
                alert(response.d);
            }
        })
    }
    $("#ddlCountry").change(function () {
        LoadStateData();
    })

    $("#close").click(function () {
        clearTextBox();
    })

    
    var LoadStateData = function () {
        debugger;
        $.ajax({
            type: "POST",
            url: "/IndustryDemo/GetState?CountryId=" + $("#ddlCountry").val(),
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var strHtml = '<option value="-1">---Please Select---</option>';
                for (var i = 0; i < data.length; i++) {
                    strHtml += '<option value="' + data[i].StateId + '">' + data[i].StateName + '</option>';
                }
                $("#ddlState").html(strHtml);
            },

            error: function (response) {
                alert(response.d);
            }
        })
    }
    $("#btnAdd").click(function () {
        if ($("#Name").val() == '') {
            alert("Please Enter Name")
            $("#Name").focus();
            return false;

        }
        else if ($("#Age").val() == '') {
            alert("Please Enter Age")
            $("#Age").focus();
            return false;
        }
        else if ($("#ddlCountry").val() == '-1') {
            alert("Please select Country")
            $("#ddlCountry").focus();
            return false;

        }
        else if ($("#ddlState").val() == '-1') {
            alert("Please select State")
            $("#ddlState").focus();
            return false;

        }
        var empObj = {
            id: $('#EmployeeId').val(),
            Name: $('#Name').val(),
            Age: $('#Age').val(),
            StateId: $('#ddlState').val(),
            CountryId: $('#ddlCountry').val()
        };
        $.ajax({
            url: "/IndustryDemo/InsertUpdateEmployee",
            type: "POST",
            data: JSON.stringify(empObj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert("Employee Added")
                clearTextBox();
                loaddata();
               
            },
           // success: function (response) {
               // OnSuccess(response)
               // alert("Employee Added")
               // clearTextBox();
               //loaddata();
           // },
            error: function (errormessage) {
                alert("Something went wrong");
            }
        });
    })
    var clearTextBox = function () {
        $('#EmployeeID').val('');
        $('#Name').val('');
        $('#Age').val('');
        $('#ddlState').val('');
        $('#ddlCountry').val('');
    };
    var loaddata = function () {
        $.ajax({
            type: "POST",
            url: "/IndustryDemo/LoadData",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //    success: OnSuccess(response),{

            //}
            success: function (response) {
                alert(response)
                OnSuccess(response)
            },

            failure: function (response) {
                alert(response);
            },
            error: function (response) {
                alert(response);
            }
        });

    };
    var OnSuccess = function (response) {

        alert(response);
        $("#tblEmployee").DataTable(
            {
               // bLengthChange: true,
                //lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                Filter: true,
                Sort: true,
                Paginate: true,
                data: response,
                //destroy: true,
                columns: [{ 'data': 'id' },
                { 'data': 'Name' },
                { 'data': 'Age' },
                { 'data': 'State' },
                { 'data': 'Country' },
                {
                    'data': 'id', "width": "150px", "render": function (id) {
                        return '<button type="button" data-toggle="modal" data target="#myModal" class=" btn btn-success" ><i class="fa fa-edit">Update</i></button><button type="button" class="btn btn-danger"><i class="fa fa-trash">Delete</i></button>';
                    }
                }]
            });

    };
    var loadEmployeeData = function (result) {
        $('#EmployeeId').val(result[0].id);
        $('#Name').val(result[0].Name);
        $('#Age').val(result[0].Age);
        $('#ddlCountry').val(result[0].CountryId);

        var stateid = result[0].StateId
        LoadStateData(stateid);
        $('#ddlState').val(result[0].StateId);
        $('#btnAdd').text("Update");
        $('#myModal').modal('show');
    };
    return  {
        Init: init,
        LoadEmployeeData: loadEmployeeData,
        LoadData:loaddata
       

    }
}();