using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FIAPOficina.Domain.Clients.Utils
{
    internal static class ClientUtils
    {
        public static bool IsValidMail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = Regex.Replace(cpf, @"[^0-9]", "");

            if (cpf.Length != 11)
                return false;

            if (new string(cpf[0], cpf.Length) == cpf)
                return false;

            var numbers = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            for (int j = 9; j < 11; j++)
            {
                int sum = 0;
                for (int i = 0; i < j; i++)
                    sum += numbers[i] * (j + 1 - i);

                int mod = sum % 11;
                int check = mod < 2 ? 0 : 11 - mod;

                if (numbers[j] != check)
                    return false;
            }

            return true;
        }

        public static bool IsValidCnpj(string? cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = Regex.Replace(cnpj, @"[^0-9]", "");

            if (cnpj.Length != 14)
                return false;

            if (new string(cnpj[0], cnpj.Length) == cnpj)
                return false;

            var numbers = cnpj.Select(c => int.Parse(c.ToString())).ToArray();

            int[] weight1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weight2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            bool ValidateDigit(int[] weight, int length)
            {
                int sum = 0;
                for (int i = 0; i < length; i++)
                    sum += numbers[i] * weight[i];

                int mod = sum % 11;
                int check = mod < 2 ? 0 : 11 - mod;
                return numbers[length] == check;
            }

            return ValidateDigit(weight1, 12) && ValidateDigit(weight2, 13);
        }

        public static bool IsValidDocument(string value)
        {
            if(string.IsNullOrEmpty(value)) return false;

            var digits = Regex.Replace(value, @"[^0-9]", "");
            if (digits.Length == 11)
                return IsValidCpf(value);
            if (digits.Length == 14)
                return IsValidCnpj(value);
            return false;
        }
    }
}