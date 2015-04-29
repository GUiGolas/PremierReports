<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportForm.aspx.cs" Inherits="PremierReports_v_1_0.ReportForm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=yes" />
    <meta name="product" content="Premier Reports" />
    <meta name="keywords" content="Reports, Relatorio, Premier, Segurança" />
    <meta name="description" content="Simple Report to Premier security system" />
    <meta name="author" content="Guilherme B. Almeida, Belo Horizonte, Brazil, 2015" />

    <link href="css/metro-bootstrap.css" rel="stylesheet" />
    <!--<link href="css/metro-bootstrap-responsive.css" rel="stylesheet">-->
    <link href="css/iconFont.css" rel="stylesheet" />

    <!-- Load JavaScript Libraries -->
    <script src="js/jquery/jquery.min.js"></script>
    <script src="js/jquery/jquery.widget.min.js"></script>

    <!-- Metro UI CSS JavaScript plugins -->
    <script src="js/load-metro.js"></script>

    <!-- Local JavaScript -->
    <script src="js/github.info.js"></script>
    <script src="js/ga.js"></script>

    <!-- Calendar -->
    <script src="js/metro-calendar.js"></script>

    <!-- Date picker -->
    <script src="js/metro-datepicker.js"></script>

    <style>
        .container {
            width: 1040px;
        }

        .metro {
            background-color: #0F3B48;
            color: white;
        }
    </style>

    <title></title>
</head>
<body class="metro">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div id="reportFilter">
            <table id="tableFilter" style="color: white; font-family: 'Segoe UI Semibold_', 'Open Sans Bold', Verdana, Arial, Helvetica, sans-serif; background-color: transparent;">
                <tr>
                    <td>
                        <table style="font: inherit; color: inherit; background-color: transparent;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDateIniR" runat="server" Text="Data Inicial: *"></asp:Label>
                                </td>
                                <td>
                                    <div class="input-control text" data-role="datepicker"  data-effect="slide" data-other-days="1">
                                        <input type="text" readonly="readonly" id="inpDateIni"/>
                                        <button class="btn-date" type="button"></button>
                                        <div class="calendar calendar-dropdown" style="position: absolute; display: none; max-width: 260px; z-index: 1000; top: 100%; left: 0px;">
                                            <table class="bordered">
                                                <tbody>
                                                    <tr class="calendar-header">
                                                        <td class="text-center"><a class="btn-previous-year" href="#"><i class="icon-previous"></i></a></td>
                                                        <td class="text-center"><a class="btn-previous-month" href="#"><i class="icon-arrow-left-4"></i></a></td>
                                                        <td colspan="3" class="text-center"><a class="btn-select-month" href="#">November 2013</a></td>
                                                        <td class="text-center"><a class="btn-next-month" href="#"><i class="icon-arrow-right-4"></i></a></td>
                                                        <td class="text-center"><a class="btn-next-year" href="#"><i class="icon-next"></i></a></td>
                                                    </tr>
                                                    <tr class="calendar-subheader">
                                                        <td class="text-center day-of-week">Su</td>
                                                        <td class="text-center day-of-week">Mo</td>
                                                        <td class="text-center day-of-week">Tu</td>
                                                        <td class="text-center day-of-week">We</td>
                                                        <td class="text-center day-of-week">Th</td>
                                                        <td class="text-center day-of-week">Fr</td>
                                                        <td class="text-center day-of-week">Sa</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="empty"><small class="other-day">27</small></td>
                                                        <td class="empty"><small class="other-day">28</small></td>
                                                        <td class="empty"><small class="other-day">29</small></td>
                                                        <td class="empty"><small class="other-day">30</small></td>
                                                        <td class="empty"><small class="other-day">31</small></td>
                                                        <td class="text-center day"><a href="#">1</a></td>
                                                        <td class="text-center day"><a href="#">2</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">3</a></td>
                                                        <td class="text-center day"><a href="#">4</a></td>
                                                        <td class="text-center day"><a href="#">5</a></td>
                                                        <td class="text-center day"><a href="#">6</a></td>
                                                        <td class="text-center day"><a href="#">7</a></td>
                                                        <td class="text-center day"><a href="#">8</a></td>
                                                        <td class="text-center day"><a href="#">9</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">10</a></td>
                                                        <td class="text-center day"><a href="#">11</a></td>
                                                        <td class="text-center day"><a href="#" class="selected">12</a></td>
                                                        <td class="text-center day"><a href="#">13</a></td>
                                                        <td class="text-center day"><a href="#">14</a></td>
                                                        <td class="text-center day"><a href="#">15</a></td>
                                                        <td class="text-center day"><a href="#">16</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">17</a></td>
                                                        <td class="text-center day"><a href="#">18</a></td>
                                                        <td class="text-center day"><a href="#">19</a></td>
                                                        <td class="text-center day"><a href="#">20</a></td>
                                                        <td class="text-center day"><a href="#">21</a></td>
                                                        <td class="text-center day"><a href="#">22</a></td>
                                                        <td class="text-center day"><a href="#">23</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">24</a></td>
                                                        <td class="text-center day"><a href="#">25</a></td>
                                                        <td class="text-center day"><a href="#">26</a></td>
                                                        <td class="text-center day"><a href="#">27</a></td>
                                                        <td class="text-center day"><a href="#">28</a></td>
                                                        <td class="text-center day"><a href="#">29</a></td>
                                                        <td class="text-center day"><a href="#">30</a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    Datepicker
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHourIni" runat="server"></asp:DropDownList>
                                    HourPicker
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDateFinR" runat="server" Text="Data Final: *"></asp:Label>
                                </td>
                                <td>
                                    <div class="input-control text" data-role="datepicker"  data-effect="slide" data-other-days="1">
                                        <input type="text" readonly="readonly">
                                        <button class="btn-date" type="button"></button>
                                        <div class="calendar calendar-dropdown" style="position: absolute; display: none; max-width: 260px; z-index: 1000; top: 100%; left: 0px;">
                                            <table class="bordered">
                                                <tbody>
                                                    <tr class="calendar-header">
                                                        <td class="text-center"><a class="btn-previous-year" href="#"><i class="icon-previous"></i></a></td>
                                                        <td class="text-center"><a class="btn-previous-month" href="#"><i class="icon-arrow-left-4"></i></a></td>
                                                        <td colspan="3" class="text-center"><a class="btn-select-month" href="#">November 2013</a></td>
                                                        <td class="text-center"><a class="btn-next-month" href="#"><i class="icon-arrow-right-4"></i></a></td>
                                                        <td class="text-center"><a class="btn-next-year" href="#"><i class="icon-next"></i></a></td>
                                                    </tr>
                                                    <tr class="calendar-subheader">
                                                        <td class="text-center day-of-week">Su</td>
                                                        <td class="text-center day-of-week">Mo</td>
                                                        <td class="text-center day-of-week">Tu</td>
                                                        <td class="text-center day-of-week">We</td>
                                                        <td class="text-center day-of-week">Th</td>
                                                        <td class="text-center day-of-week">Fr</td>
                                                        <td class="text-center day-of-week">Sa</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="empty"><small class="other-day">27</small></td>
                                                        <td class="empty"><small class="other-day">28</small></td>
                                                        <td class="empty"><small class="other-day">29</small></td>
                                                        <td class="empty"><small class="other-day">30</small></td>
                                                        <td class="empty"><small class="other-day">31</small></td>
                                                        <td class="text-center day"><a href="#">1</a></td>
                                                        <td class="text-center day"><a href="#">2</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">3</a></td>
                                                        <td class="text-center day"><a href="#">4</a></td>
                                                        <td class="text-center day"><a href="#">5</a></td>
                                                        <td class="text-center day"><a href="#">6</a></td>
                                                        <td class="text-center day"><a href="#">7</a></td>
                                                        <td class="text-center day"><a href="#">8</a></td>
                                                        <td class="text-center day"><a href="#">9</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">10</a></td>
                                                        <td class="text-center day"><a href="#">11</a></td>
                                                        <td class="text-center day"><a href="#" class="selected">12</a></td>
                                                        <td class="text-center day"><a href="#">13</a></td>
                                                        <td class="text-center day"><a href="#">14</a></td>
                                                        <td class="text-center day"><a href="#">15</a></td>
                                                        <td class="text-center day"><a href="#">16</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">17</a></td>
                                                        <td class="text-center day"><a href="#">18</a></td>
                                                        <td class="text-center day"><a href="#">19</a></td>
                                                        <td class="text-center day"><a href="#">20</a></td>
                                                        <td class="text-center day"><a href="#">21</a></td>
                                                        <td class="text-center day"><a href="#">22</a></td>
                                                        <td class="text-center day"><a href="#">23</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center day"><a href="#">24</a></td>
                                                        <td class="text-center day"><a href="#">25</a></td>
                                                        <td class="text-center day"><a href="#">26</a></td>
                                                        <td class="text-center day"><a href="#">27</a></td>
                                                        <td class="text-center day"><a href="#">28</a></td>
                                                        <td class="text-center day"><a href="#">29</a></td>
                                                        <td class="text-center day"><a href="#">30</a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    DatePicker
                                </td>
                                <td>HourPicker
                                </td>
                            </tr>

                        </table>
                    </td>
                    <td>
                        <table style="background-color: transparent;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAddEventsR" runat="server" Text="Eventos"></asp:Label>
                                </td>
                                <td>Events DropDown
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Event List
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>Botao Submit
                    </td>
                </tr>
            </table>
        </div>
        <div id="reportShow">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
