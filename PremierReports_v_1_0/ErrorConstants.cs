using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PremierReports_v_1_0
{
    public static class ErrorConstants
    {

        private static string[] userNameMinLength = new string[2] { "*O nome do usuário deve conter pelo menos 3 caracteres!", "*The user name must have at least 3 characters!" };

        private static string[] passwordMinLength = new string[2] { "*A senha deve conter pelo menos 4 caracteres!", "*The password must have at least 4 characters!" };
        
        private static string[] loginFailed = new string[2] { "*Falha no login. Favor conferir o nome do usuário e senha.", "*Login failed. Please check the user name and password!" };

        private static string[] numberEventsExceded = new string[2] { "Só é possível escolher 3 eventos ao mesmo tempo.", "You can choose only 3 events at same time." };

        private static string[] passwordDontMatch = new string[2] { "As senhas não são iguais.", "The passwords are not the same." };

        private static string[] passwordlength = new string[2] { "A senha deve conter pelo menos 4 caracteres .", "The password must have at least 4 characters!" };

        private static string[] missingFields = new string[2] { "Está faltando preencher algum campo obrigatório.", "Some required field is missing." };

        private static string[] dateError = new string[2] { "A data final não pode ser menor que a data inicial.", "The final date cannot be greater then the start date." };
        
        
        public static string getMessage(string Error, Int16 pt)
        {

            switch (Error)
            {
                case "userNameMinLength":
                    return userNameMinLength[pt];
                case "passwordMinLength":
                    return passwordMinLength[pt];
                case "loginFailed":
                    return loginFailed[pt];
                case "numberEventsExceded":
                    return numberEventsExceded[pt];
                case "passwordDontMatch":
                    return passwordDontMatch[pt];
                case "passwordlength":
                    return passwordlength[pt];
                case "missingFields":
                    return missingFields[pt];
                case "dateError":
                    return dateError[pt];
                default:
                    return "";
                    
            }

            
        }

    }

    

}