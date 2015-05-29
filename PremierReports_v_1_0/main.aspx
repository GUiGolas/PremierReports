<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="PremierReports_v_1_0.main" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
    <%--<script src="js/ga.js"></script>--%>

    <!-- Tab Control -->
    <script src="js/metro-tab-control.js"></script>
   

    <title>Relatorio SAM - Premier</title>

    <style>
        .container {
            width: 1040px;
        }

        .metro {
            background-color: #0F3B48;
            color: white;
        }
         .labelError{
            background-color:#870303;
            color:white;
            
        }
    </style>

</head>
<body class="metro">
    <form id="form1" runat="server">
        <div id="mainHeader">
            <nav class="navigation-bar" style="background-color: #1B5465">

                <nav class="navigation-bar-content">
                    <button class="element image-button image-left place-left" type="button"  onclick="location.href='main.aspx'">
                        Premier
            <img src="img/LogoPremierIco_short.ico" />
                    </button>
                    <item class="element-divider"></item>

                    <div class="element place-right">
                        <a class="dropdown-toggle" href="#">
                            <span class="icon-cog"></span>
                        </a>
                        <ul class="dropdown-menu place-right" data-role="dropdown">
                           <%-- <li><a href="settings.aspx">Configurações</a></li>--%>
                             <li>
                                 <asp:LinkButton ID="lnkButtonSettings" Text="Configurações" PostBackUrl="~/Settings.aspx" runat="server" meta:resourcekey="lnkButtonSettingsResource1"></asp:LinkButton>

                             </li>
                            <li><a href="#">
                                <hr />
                            </a></li>
                            <li>
                                <asp:LinkButton Text="LogOff"  OnClick="Logoff_Click" runat="server" meta:resourcekey="LinkButtonResource1" /></li>
                           <%-- <li><a href="#">LogOff</a></li>--%>
                        </ul>
                    </div>
                </nav>
            </nav>
        </div>
        <br />
       
        <div id="mainContent" style="padding-left:25px;">
            <div id="mainTab">
                <div class="tab-control" data-role="tab-control" data-effect="fade[slide]" style="width:1280px;">
                    <ul class="tabs">
                        <li class="active"><a href="#_page_1">
                            <asp:Label ID="lblEventTab" runat="server" Text="Eventos" meta:resourcekey="lblEventTabResource1"></asp:Label></a></li>
                        <li><a href="#_page_2">
                            <asp:Label ID="lblChartTab" runat="server" Text="Gráficos" meta:resourcekey="lblChartTabResource1"></asp:Label></a></li>
                        <li><a href="#_page_3">
                            <asp:Label ID="lblAuditTab" runat="server" Text="Auditoria" meta:resourcekey="lblAuditTabResource1"></asp:Label></a></li>
                    </ul>

                    <div class="frames" style="margin:0px;padding:0px;overflow:hidden">
                        <div class="frame" id="_page_1">
                            <div id="frameReport" >
                                <iframe src="ReportForm.aspx" style="width:1024px; height:630px; border:hidden;"></iframe>
                            </div>
                            

                        </div>
                        <div class="frame" id="_page_2">
                             <div id="frameChart" >
                                <iframe src="GraphicsForm.aspx" style="width:1024px; height:630px; border:hidden;"></iframe>
                            </div>

                        </div>
                        <div class="frame" id="_page_3">
                             <div id="frameAuditing" >
                                <iframe src="AuditForm.aspx" style="width:1024px; height:630px; border:hidden;"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <footer>
        <div style="position: fixed; bottom: 0; left: 100px; right: 0;">
            <nav class="navigation-bar" style="background-color: transparent">
                <h3 style="color: white">Copyright @ Premier Soluções em Segurança - 2015  <a href="mailto:contato@premier24h.com.br">contato@premier24h.com.br</a> </h3>
            </nav>

        </div>
    </footer>
    </form>
    
</body>
</html>
