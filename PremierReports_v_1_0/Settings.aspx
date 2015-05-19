<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="PremierReports_v_1_0.Settings" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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

    <script type="text/javascript">

        function enableConnectionString() {

            var checked = document.getElementById("chkConnectionString").checked;
            if (checked) {
               <%--document.getElementById('<%= txtConnectionString.ClientID %>' ).enabled = true;--%>
                <%-- document.getElementById('<%= txtConnectionString.ClientID %>').style.display = 'inline';--%>
                document.getElementById("inpConnectionString").style.display = 'inline';
                document.getElementById("inpConnectionString").style.visibility = 'visible';
           }

           else {
                <%--document.getElementById('<%= txtConnectionString.ClientID %>' ).enabled = true;--%>
<%--                document.getElementById('<%= txtConnectionString.ClientID %>').style.display = 'none';--%>
                document.getElementById("inpConnectionString").style.display = 'none';
                document.getElementById("inpConnectionString").style.visibility = 'hidden';
            }
            
            
        }

    </script>

</head>
<body class="metro">
    <form id="settingsForm" runat="server">
        <div id="mainHeader">
            <nav class="navigation-bar" style="background-color: #1B5465">

                <nav class="navigation-bar-content">
                    <button class="element image-button image-left place-left" type="button" onclick="location.href='main.aspx'">
                        Premier
            <img src="img/LogoPremierIco_short.ico" />
                    </button>
                    <item class="element-divider"></item>

                    <div class="element place-right">
                        <a class="dropdown-toggle" href="#">
                            <span class="icon-cog"></span>
                        </a>
                        <ul class="dropdown-menu place-right" data-role="dropdown">
                            <%--<li><a href="#">Configurações</a></li>--%>
                             <li>
                                 <asp:LinkButton ID="lnkButtonSettings" Text="Configurações" PostBackUrl="~/Settings.aspx" runat="server" meta:resourcekey="lnkButtonSettingsResource1"></asp:LinkButton>

                             </li>
                            <li><a href="settings.aspx">
                                <hr />
                            </a></li>
                            <li>
                                <asp:LinkButton Text="LogOff" OnClick="Logoff_Click" runat="server" meta:resourcekey="LinkButtonResource1" /></li>
                            <%--<li><a href="#" runat="server" onclick="Logoff_Click">LogOff</a></li>--%>
                        </ul>
                    </div>
                </nav>
            </nav>
        </div>

        <br />
        <br />
        <div id="content" style="padding-left: 50px;">
            <div id="PasswordSettings">
                <h1 id="lblChangePassword" style="color: white">Trocar Senha:</h1>
                <br />
                <table style="background-color: transparent">
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" Text="Usuário: " runat="server" meta:resourcekey="lblUserNameResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserName" CssClass="textBox" meta:resourcekey="txtUserNameResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Senha: " ID="lblPassword" runat="server" meta:resourcekey="lblPasswordResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="textBox" TextMode="Password" meta:resourcekey="txtPasswordResource1" />
                            <br />
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label Text="Novo Usuário: " runat="server" ID="lblNewUserName" meta:resourcekey="lblNewUserNameResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="textBox" ID="txtNewUserName" meta:resourcekey="txtNewUserNameResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Nova Senha: " ID="lblNewPassword" runat="server" meta:resourcekey="lblNewPasswordResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="textBox" ID="txtNewPassword" TextMode="Password" meta:resourcekey="txtNewPasswordResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Confirmação de Senha:   " ID="lblConfirmPassword" runat="server" meta:resourcekey="lblConfirmPasswordResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtConfirmPassword" CssClass="textBox" TextMode="Password" meta:resourcekey="txtConfirmPasswordResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button Text="Salvar" ID="btnSavePassword" OnClick="btnSavePassword_Click" CssClass="btnSavePassword" runat="server" meta:resourcekey="btnSavePasswordResource1" />

                        </td>
                    </tr>
                </table>
                <div>
                    <asp:Label Text="[Error]" ID="lblError" CssClass="labelError" Visible="False" runat="server" meta:resourcekey="lblErrorResource1" />
                </div>
            </div>
            <br />
            <hr style="width: 1280px;" />
            <br />
            <br />

            <div id="databaseSettings">
                <h1 id="lblChangeDbSettings" style="color: white">Configurações do Banco de Dados: </h1>
                <br />
                <asp:Label Text="[Error]" ID="lblErrorDb" CssClass="labelError" Visible="False" runat="server" meta:resourcekey="lblErrorDbResource1" />
                <br />
                <table id="tableDbSettings" style="background-color: transparent" runat="server">
                    <tr>
                        <td>
                            <asp:Label Text="Endereço do Banco: " ID="lblDbAddress" runat="server" meta:resourcekey="lblDbAddressResource1" />
                        </td>
                        <td>
                            <asp:Label Text="Instância da Base de Dados: " ID="lblDbInstance" runat="server" meta:resourcekey="lblDbInstanceResource1" />
                        </td>
                        <td>
                            <asp:Label Text="Nome da Base de Dados: " ID="lblDbName" runat="server" meta:resourcekey="lblDbNameResource1" />
                        </td>
                        <td>
                            <asp:Label Text="Porta do Servidor" ID="lblPort" runat="server" meta:resourcekey="lblPortResource1" />
                        </td>
                        <td>
                            <asp:Label Text="Separador de Porta:" ID="lblPortSeparator" runat="server" meta:resourcekey="lblPortSeparatorResource1" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbAddress" CssClass="textBox" Text="LOCALHOST" meta:resourcekey="txtDbAddressResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbInstance" CssClass="textBox" Text="SQLEXPRESS" meta:resourcekey="txtDbInstanceResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbName" CssClass="textBox" Text="DvmsData" meta:resourcekey="txtDbNameResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDbPort" CssClass="textBox" meta:resourcekey="txtDbPortResource1" />
                        </td>
                        <td style="padding-left: 20px;">
                            <asp:TextBox runat="server" ID="txtPortSeparator" Font-Size="Larger" Font-Bold="True" Text="," CssClass="textBox" MaxLength="1" Width="40px" meta:resourcekey="txtPortSeparatorResource1" />
                        </td>

                    </tr>
                    <tr>
                        <td>

                            <div class="input-control switch">
                                <label>
                                    Connection String 
                                    <input type="checkbox" id="chkConnectionString" onclick="enableConnectionString()" runat="server" />
                                    <span class="check"></span>
                                </label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div class="input-control text" id="inpConnectionString" style="display:none; height:27px; visibility:hidden;">
                               <%-- <input type="text" value=""   class="textBox" style="height:27px;"   />
                                <button class="btn-clear"></button>--%>
                                <asp:TextBox runat="server"  Height="27px" Width="750px" CssClass="textBox" ID="txtConString" meta:resourcekey="txtConStringResource1"/>
                            </div>
                            
                            <%--<asp:TextBox  ID="txtConnectionString" CssClass="textBox no-display" TextMode="SingleLine" Width="750px" />--%>
                        </td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Usuário: " ID="lbldbUserName" runat="server" meta:resourcekey="lbldbUserNameResource1" />
                        </td>
                        <td>
                            <asp:Label Text="Senha: " ID="lbldbPassword" runat="server" meta:resourcekey="lbldbPasswordResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtdbUserName" CssClass="textBox" meta:resourcekey="txtdbUserNameResource1" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtdbPassword" TextMode="Password" CssClass="textBox" meta:resourcekey="txtdbPasswordResource1" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button Text="Salvar" ID="btnSaveDbSettings" CssClass="btnSavePassword" runat="server" OnClick="btnSaveDbSettings_Click"  OnClientClick="saveConString" meta:resourcekey="btnSaveDbSettingsResource1"/>

                <asp:Button Text="Configurações de fábrica" ID="btnFactoryReset" CssClass="btnFactory" runat="server" meta:resourcekey="btnFactoryResetResource1" />

                <br />

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
