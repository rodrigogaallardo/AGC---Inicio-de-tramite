var that = this;

var menuViewModel=
    {
        id_menu: ko.observable(0),
        descripcion_menu: ko.observable(0),
        aclaracion_menu: ko.observable(0),
        pagina_menu: ko.observable(0),
        iconCssClass_menu: ko.observable(0),
        id_menu_padre: ko.observable(0),
        nroOrden: ko.observable(0),
        visible: ko.observable(0)
    }

//Create ViewModel
var viewModel = {
    pageSize: ko.observable(10),
    pageIndex: ko.observable(0),
    sortCommand: ko.observable("ProductID asc"),
    dataRows: ko.observableArray([]),
    totalRows: ko.observable(0),
    sorted: function (e, data) {
        viewModel.sortCommand(data.sortCommand);
    },
    paged: function (e, data) {
        viewModel.pageIndex(data.newPageIndex);
    },
    load: function () {
        $.ajax({
            url: "http://services.odata.org/Northwind/Northwind.svc/Products",
            dataType: "jsonp",
            jsonp: "$callback",
            data: {
                $format: "json",
                $inlinecount: "allpages",
                $select: "ProductID,ProductName,UnitPrice,UnitsInStock",
                $orderby: viewModel.sortCommand(),
                $top: viewModel.pageSize(),
                $skip: viewModel.pageIndex() * viewModel.pageSize(),
                "paging[pageIndex]": viewModel.pageIndex(),
                "paging[pageSize]": viewModel.pageSize()
            },
            success: function (result) {
                var data = result.d.results;
                var arr = [];

                $.each(data, function (i) {
                    arr.push(new product(data[i]));
                });
                viewModel.totalRows(result.d.__count);
                viewModel.dataRows(arr);
            }
        });
    }

};

//Class constructor for grid row. Returns observable properties.
var product = function (data) {
    return {
        ProductID: ko.observable(data.ProductID),
        ProductName: ko.observable(data.ProductName),
        UnitPrice: ko.observable(data.UnitPrice),
        UnitsInStock: ko.observable(data.UnitsInStock)
    };
};