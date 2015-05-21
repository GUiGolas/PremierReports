<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="PremierReports_v_1_0.about" %>

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

    <!-- Custom style sheet -->
    <link href="css/custom.css" rel="stylesheet" />

    <!-- Load JavaScript Libraries -->
    <script src="js/jquery/jquery.min.js"></script>
    <script src="js/jquery/jquery.widget.min.js"></script>

    <!-- Metro UI CSS JavaScript plugins -->
    <script src="js/load-metro.js"></script>

    <!-- Local JavaScript -->
    <script src="js/github.info.js"></script>
    <script src="js/ga.js"></script>

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

        .btnSavePassword {
            background-color: green !important;
            padding-left: 30px !important;
            background: left no-repeat url(img/Save.png);
            background-size: 25px;
            color: white !important;
        }

        .btnFactory {
            background-color: #f85501 !important;
            padding-left: 30px !important;
            background: left no-repeat url(img/redo.png);
            background-size: 25px;
            color: white !important;
        }

        .labelError {
            background-color: #870303;
            color: white;
        }

        .customCheckBox {
            position: relative !important;
            vertical-align: middle !important;
            bottom: 1px !important;
        }

        .textBox {
            background-color: #cccccc;
            border: none;
            font-family: 'Segoe UI';
        }
    </style>

</head>
<body class="metro" style="padding-left:10px;">
    <form id="form1" runat="server">
        <div class="panel  bg-olive fg-white" style="padding-left:10px;">
            <h2 style="color: white;">
                <asp:Label ID="lblAbout" Text="Sobre" runat="server" /></h2>
            <div class="panel-header bg-transparent fg-white">
                Premier Soluções em Segurança LTDA
            </div>
            <div class="panel-content">
                
                <asp:TextBox ID="txtAbout" TextMode="MultiLine"  BackColor="Transparent" BorderWidth="0" ForeColor="White" Font-Names="Segoe UI" runat="server" />
                            
            </div>
        </div>
        <div class="panel  bg-steel fg-white" style="padding-left:10px;">
            <h2 style="color: white; font-weight:bold;">
                <asp:Label ID="lblContact" Text="Contato" runat="server" /></h2>
            
            <div class="panel-content">
                Rua Cristina, 170 - Belo Horizonte - MG -  Brasil,<br />
                30310-692
                <br />
                Tel: +55 31 3254-7000
                <br />
                <asp:LinkButton ID="lnkMails" PostBackUrl="mailto:contato@premier24h.com.br" runat="server" Text="contato@premier24h.com.br" Font-Size="Large"></asp:LinkButton>
                            
            </div>
        </div>
        <div class="panel  bg-steel fg-white" style="padding-left:10px; visibility:hidden">
            <h2 style="color: white;">
                <asp:Label ID="lblLicence" Text="Licença" runat="server" /></h2>
            <div class="panel-header bg-steel fg-white">
                Premier Soluções em Segurança LTDA - Belo horizonte - Brasil
            </div>
            <div class="panel-content">
                
                                Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.
                            
            </div>
        </div>

    </form>
</body>
</html>
