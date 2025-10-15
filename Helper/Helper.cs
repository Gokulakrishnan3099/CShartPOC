using Merchant_Service.Model;

namespace Merchant_Service.Helper
{
    public static class PaymentHelper
    {
        public static bool SimpleApprovalLogic(PaymentRequest r)
        {
            if (!LuhnCheck(r.CardNumber)) return false;
            var last = r.CardNumber.Trim().Replace(" ", "");
            if (last.Length == 0) return false;
            return (last[^1] - '0') % 2 == 0;
        }


        public static string MaskCard(string card)
        {
            if (string.IsNullOrWhiteSpace(card)) return card;
            var digits = card.Replace(" ", "");
            if (digits.Length <= 4) return digits;
            return new string('*', digits.Length - 4) + digits.Substring(digits.Length - 4);
        }


        public static string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }


        private static bool LuhnCheck(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;
            var digits = number.Replace(" ", "");
            int sum = 0;
            bool alternate = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(digits[i])) return false;
                int n = digits[i] - '0';
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }
            return sum % 10 == 0;
        }
    }
}
