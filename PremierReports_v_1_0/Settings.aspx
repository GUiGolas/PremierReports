<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="PremierReports_v_1_0.Settings" %>

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

    <!-- Tab Control -->
    <script src="js/metro-tab-control.js"></script>


    <title>Premier Reports - Settings</title>

    <style>
        .container {
            width: 1040px;
        }

        .metro {
            background-color: #0F3B48;
            color: white;
        }

        .h1 {
            color: white;
        }
    </style>

</head>
<body class="metro">
    <form id="settingsForm" runat="server">
        <div id="mainHeader">
            <nav class="navigation-bar" style="background-color: #1B5465">

                <nav class="navigation-bar-content">
                    <button class="element image-button image-left place-left" type="button"  onclick="location.href='main.aspx'" >
                        Premier
            <img src="img/LogoPremierIco_short.ico" />
                    </button>
                    <item class="element-divider"></item>

                    <div class="element place-right">
                        <a class="dropdown-toggle" href="#">
                            <span class="icon-cog"></span>
                        </a>
                        <ul class="dropdown-menu place-right" data-role="dropdown">
                            <li><a href="#">Configurações</a></li>
                            <li><a href="settings.aspx">
                                <hr />
                            </a></li>
                            <li><a href="#">LogOff</a></li>
                        </ul>
                    </div>
                </nav>
            </nav>
        </div>

        <br />
        <br />
        <div id="content" style="padding-left:50px;">
            <div id="PasswordSettings">
                <h1 id="lblChangePassword" style="color: white">Trocar Senha:</h1>
                <br />
                <table style="background-color: transparent">
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" Text="Usuário: " runat="server" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserName" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Senha: " ID="lblPassword" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                            <br />
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label Text="Novo Usuário: " runat="server" ID="lblNewUserName" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNewUserName" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Nova Senha: " ID="lblNewPassword" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Confirmação de Senha: " ID="lblConfirmPassword" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" />
                        </td>
                    </tr>
                    <tr>
                         <td>
                            <button id="btnSavePassword" class="image-button bg-darkGreen fg-white image-left"/> Salvar <i class="icon-floppy on-left bg-green fg-white"></i>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <hr  style="width:1280px;"/>
            <br />
            <br />

            <div id="databaseSettings">
                <h1 id="lblChangeDbSettings" style="color: white">Configurações do Banco de Dados: </h1>
                <br />

                <table id="tableDbSettings" style="background-color: transparent" runat="server">
                    <tr>
                        <td>
                            <asp:Label Text="Endereço do Banco: " ID="lblDbAddress" runat="server" />
                        </td>
                        <td>
                            <asp:Label Text="Instância da Base de Dados: " ID="lblDbInstance" runat="server" />
                        </td>
                        <td>
                            <asp:Label Text="Nome da Base de Dados: " ID="lblDbName" runat="server" />
                        </td>
                        <td>
                            <asp:Label Text="Porta do Servidor" ID="lblPort" runat="server" />
                        </td>
                        <td>
                            <asp:Label Text="Separador de Porta:" ID="lblPortSeparator" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbAddress" Text="LOCALHOST" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbInstance" Text="SQLEXPRESS" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbName" Text="DvmsData" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbPort" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPortSeparator" Text="," />
                        </td>
                    </tr>S
                    
                </table>
              
                    <button id="btnSaveDbSettings" class="image-button bg-darkGreen fg-white image-left" /> Salvar <i class="icon-floppy on-left bg-green fg-white"></i>
               
                    <button id="btnFactory" class="image-button bg-darkOrange fg-white image-left" /> Configurações de Fábrica <i class="icon-redo on-left  bg-darkOrange fg-white" ></i>
               
                
            </div>
        </div>

    </form>
    <footer>
        <div style="position: fixed; bottom: 0; left: 100px; right: 0;">
            <nav class="navigation-bar" style="background-color: #0F3B48;">
                <h3 style="color: white">Copyright @ Premier Soluções em Segurança - 2015  <a href="mailto:contato@premier24h.com.br">contato@premier24h.com.br</a> </h3>
            </nav>

        </div>
    </footer>
</body>
</html>
