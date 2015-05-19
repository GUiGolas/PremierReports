using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PremierReports_v_1_0
{
    public static class LanguageConstants
    {

        private static string[] Events = new string[2] { "Eventos", "Events" };

        private static string[] StartDate = new string[2] { "Data Inicial", "Start Date" };
        
        private static string[] FinalDate = new string[2] { "Data Final", "Final Date" };

        private static string[] OrderByID = new string[2] { "Ordenar por ID", "Order by ID" };

        private static string[] UserName = new string[2] { "Usuário", "User" };

        private static string[] Password = new string[2] { "Senha", "Password" };

        private static string[] ConfirmPassword = new string[2] { "Confirmação de Senha", "Confirm Password" };

        private static string[] DbAddress = new string[2] { "Endereço do Banco", "Database Address" };
        
        private static string[] DbInstace = new string[2] { "Instância do Banco", "Instance" };

        private static string[] DbPort  = new string[2] { "Porta", "Port" };

        private static string[] DbPortSeparator = new string[2] { "Separador de Porta", "Port Separator" };

        private static string[] DbName = new string[2] { "Nome do Banco", "Database Name" };

        private static string[] Update = new string[2] { "Atualizar", "Update" };

        private static string[] Save = new string[2] { "Salvar", "Save" };

        private static string[] Factory = new string[2] { "Condigurações de Fábrica", "Factory Reset" };

        private static string[] NewUser = new string[2] { "Novo Usuário", "New User" };

        private static string[] NewPassword = new string[2] { "Nova Senha", "New Password" };

        private static string[] ChartType = new string[2] { "Tipo de Gráfico", "Chart Type" };

        private static string[] Settings = new string[2] { "Configurações", "Settings" };

        private static string[] Language = new string[2] { "Idioma", "Language" };

        private static string[] Clear = new string[2] { "Limpar", "Clear" };

        private static string[] Audit = new string[2] { "Auditoria", "Auditing" };

        private static string[] Chart = new string[2] { "Gráfico", "Chart" };

        public static string getErrorMessage(string Error, Int16 pt)
        {

            switch (Error)
            {
                case "Language":
                    return Language[pt];
                case "Clear":
                    return Clear[pt];

                case "Audit":
                    return Audit[pt];
                case "Chart":
                    return Chart[pt];
                case "DbInstance":
                    return DbInstace[pt];
                case "Settings":
                    return Settings[pt];
                case "ChartType":
                    return ChartType[pt];
                case "DbAddress":
                    return DbAddress[pt];
                case "NewPassword":
                    return NewPassword[pt];
                case "ConfirmPassword":
                    return ConfirmPassword[pt];
                case "Password":
                    return Password[pt];
                case "UserName":
                    return UserName[pt];
                case "FinalDate":
                    return FinalDate[pt];
                case "OrderByID":
                    return OrderByID[pt];
                case "StartDate":
                    return StartDate[pt];
                case "Events":
                    return Events[pt];
               case "NewUser":
                    return NewUser[pt];
                case "Factory":
                    return Factory[pt];
                case "Save":
                    return Save[pt];
                case "Update":
                    return Update[pt];
                case "DbName":
                    return DbName[pt];
                case "DbPortSeparator":
                    return DbPortSeparator[pt];
                case "DbPort":
                    return DbPort[pt];
                default:
                    return "";
                    
            }

            
        }

    }

    

}