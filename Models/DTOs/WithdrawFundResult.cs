namespace WalletApi.Models.DTOs
{
    public class WithdrawFundResult
    {
        public string UserId { get; set; } = "";
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }
}
