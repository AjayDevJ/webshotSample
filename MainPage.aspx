<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ImageDownload.MainPage" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="http://cdn.kendostatic.com/2014.2.716/js/kendo.all.min.js"></script>
    <script type="text/javascript">
        $(window).bind('load', function () {
            setTimeout(function () {
                var name = ['chart'];
                var innerVal = [];
                try {
                    for (i = 0; i < name.length; i++) {
                        var val = document.getElementById(name[i]);
                        if (val != null) {
                            var ID = val.id;
                            var HtmlDetails = val.innerHTML;
                            innerVal.push({ ID, HtmlDetails });
                        }
                    }
                }
                catch (err) {
                    alert(err);
                }
                var post = $.ajax({
                    type: "POST",
                    url: "MainPage.aspx/GetHtmlValuesFromUI",
                    contentType: "application/json; charset=utf-8",
                    data: "{DivValues:" + JSON.stringify(innerVal) + "}",
                    dataType: "json",
                    success: function (response) {
                        alert(response.d)
                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    statusCode: {
                        401: function () {
                            alert("Auth required");
                        }
                    },
                });
            }, 1000);
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="example">
            <div class="demo-section k-content wide">
                <div id="chart"></div>
            </div>
            <div class="demo-section k-content wide">
                <div id="graph">AJ testing, This content wont be in screenshot!!</div>
            </div>
            <script>
                function createChart() {
                    $("#chart").kendoChart({
                        title: {
                            text: "Gross domestic product growth \n /GDP annual %/"
                        },
                        legend: {
                            position: "bottom"
                        },
                        chartArea: {
                            background: ""
                        },
                        seriesDefaults: {
                            type: "column",
                            style: "smooth"
                        },
                        series: [{
                            name: "India",
                            data: [3.907, 7.943, 7.848, 9.284, 9.263, 9.801, 3.890, 8.238, 9.552, 6.855]
                        }, {
                            name: "World",
                            data: [1.988, 2.733, 3.994, 3.464, 4.001, 3.939, 1.333, -2.245, 4.339, 2.727]
                        }, {
                            name: "Russian Federation",
                            data: [4.743, 7.295, 7.175, 6.376, 8.153, 8.535, 5.247, -7.832, 4.3, 4.3]
                        }, {
                            name: "Haiti",
                            data: [-0.253, 0.362, -3.519, 1.799, 2.252, 3.343, 0.843, 2.877, -5.416, 5.590]
                        }],
                        valueAxis: {
                            labels: {
                                format: "{0}%"
                            },
                            line: {
                                visible: false
                            },
                            axisCrossingValue: -10
                        },
                        categoryAxis: {
                            categories: [2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011],
                            majorGridLines: {
                                visible: false
                            },
                            labels: {
                                rotation: "auto"
                            }
                        },
                        tooltip: {
                            visible: true,
                            format: "{0}%",
                            template: "#= series.name #: #= value #"
                        }
                    });
                }
                $(document).ready(createChart);
                $(document).bind("kendo:skinChange", createChart);
            </script>
        </div>
    </form>
</body>
</html>
