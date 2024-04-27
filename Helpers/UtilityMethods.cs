using WalletApi.Models.DTOs;

namespace WalletApi.Helpers
{
    public class UtilityMethods
    {
        public static bool IsValidCurrencyCode(string currencyCode)
        {

            List<string> validCurrencyCodes = new List<string> {
            "ADA", "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARB", "ARS", "AUD",
            "AVAX", "AWG", "AZN", "BAM", "BBD", "BDT", "BGN", "BHD", "BIF", "BMD",
            "BNB", "BND", "BOB", "BRL", "BSD", "BTC", "BTN", "BWP", "BYN", "BYR",
            "BZD", "CAD", "CDF", "CHF", "CLF", "CLP", "CNY", "COP", "CRC", "CUC",
            "CUP", "CVE", "CZK", "DAI", "DJF", "DKK", "DOP", "DOT", "DZD", "EGP",
            "ERN", "ETB", "ETH", "EUR", "FJD", "FKP", "GBP", "GEL", "GGP", "GHS",
            "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL", "HRK", "HTG", "HUF",
            "IDR", "ILS", "IMP", "INR", "IQD", "IRR", "ISK", "JEP", "JMD", "JOD",
            "JPY", "KES", "KGS", "KHR", "KID", "KMF", "KPW", "KRW", "KWD", "KYD",
            "KZT", "LAK", "LBP", "LKR", "LRD", "LSL", "LTC", "LTL", "LVL", "LYD",
            "MAD", "MATIC", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRO", "MRU",
            "MUR", "MVR", "MWK", "MXN", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK",
            "NPR", "NZD", "OMR", "P", "PAB", "PEN", "PGK", "PHP", "PKR", "PLN",
            "PYG", "QAR", "RON", "RSD", "RUB", "RWF", "SAR", "SBD", "SCR", "SDG",
            "SEK", "SGD", "SHP", "SLL", "SOL", "SOS", "SRD", "STD", "STN", "SVC",
            "SYP", "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD", "TWD",
            "TZS", "UAH", "UGX", "USD", "USDC", "USDT", "UYU", "UZS", "VEF", "VES",
            "VND", "VUV", "WST", "XAF", "XAG", "XAU", "XCD", "XDR", "XOF", "XPD",
            "XPF", "XPT", "XRP", "YER", "ZAR", "ZMK", "ZMW", "ZWL"
        };

            // Check if the provided currency code exists in the list of valid codes
            return validCurrencyCodes.Contains(currencyCode.ToUpper());
        }

      

    }
}
