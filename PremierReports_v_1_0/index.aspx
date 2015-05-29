<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PremierReports_v_1_0.index" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
   <%-- <script src="js/ga.js"></script>--%>

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

         .loginField{
             background-color:#0F3B48;
             color:white;
             border-color:white;
             font-family: 'Century Gothic';
             border:solid thin;
             
         }
    </style>


</head>
<body class="metro">
    <form id="form1" runat="server" defaultfocus="txtUserName" defaultbutton="btnLogin">

        <div id="content">
            <div id="divLogin">
                <table id="tblLogin" style="position: relative; left: 200px; width: 960px; height: 600px; background-color: transparent;">
                    <tr>
                        <td id="tdLogo" style="width: 430px; height: auto;">
                            <img src="img/LogosPremierHoriz.BOX.azul_edit.jpg" style="height: 250px;" />
                        </td>
                        <td>
                            <table style="width: auto; background-color: transparent;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLanguage" runat="server" Text="Idioma: " meta:resourcekey="lblLanguageResource1" Font-Names="Century Gothic"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLanguage" Width="153px" runat="server" CssClass="loginField" meta:resourcekey="ddlLanguageResource1" AutoPostBack="True" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                                            <asp:ListItem Value="pt-BR" meta:resourcekey="ListItemResource1">Português(Brasil)</asp:ListItem>
                                            <asp:ListItem Value="en-US" meta:resourcekey="ListItemResource2">English(EUA)</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblUserName" runat="server" Font-Names="Century Gothic" Text="Usuário:" meta:resourcekey="lblUserNameResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" meta:resourcekey="txtUserNameResource1" CssClass="loginField"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90px;">
                                        <asp:Label ID="lblPassword" runat="server" Font-Names="Century Gothic" Text="Senha:" meta:resourcekey="lblPasswordResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" runat="server" Text="Password" TextMode="Password" meta:resourcekey="txtPasswordResource1" CssClass="loginField"></asp:TextBox>
                                        
                                    </td>
                                    <td>
                                        
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Button ID="btnLogin" runat="server" Font-Names="Century Gothic" Font-Bold="True" Font-Size="Large" Width="245px" Text="LOGIN" ForeColor="#0F3B48" BackColor="#C5D300"  meta:resourcekey="btnLoginResource1" OnClick="btnLogin_Click"/>
                                        <asp:RegularExpressionValidator ID="valPassword" runat="server"
                                            ControlToValidate="txtPassword"
                                            ErrorMessage="Minimum password length is 4"
                                            ValidationExpression=".{4}.*" />
                                    </td>
                                </tr>
                                
                            </table>
                            <asp:Label ID="lblLoginError" runat="server" Text="[Erro]" Visible="False"  CssClass="labelError" meta:resourcekey="lblLoginErrorResource1"></asp:Label>
                        </td>
                    </tr>

                </table>
            </div>
        </div>

    </form>
    <footer>
        <div style="position: fixed; bottom: 0; left: 250px; right: 0;">
            <nav class="navigation-bar" style="background-color: #0F3B48;">
                <h3 style="color: white">Copyright @ Premier Soluções em Segurança - 2015  <a href="mailto:contato@premier24h.com.br">contato@premier24h.com.br</a> </h3>
            </nav>

        </div>
    </footer>
</body>
</html>
