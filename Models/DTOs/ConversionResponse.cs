namespace WalletApi.Models.DTOs
{
    public class ConversionResponse
    {
        public Meta Meta { get; set; }
        public Data Data { get; set; }
    }

    public class Meta
    {
        public string last_updated_at { get; set; }
    }

    public class Data
    {
        public Attr AUD { get; set; }
        public Attr CAD { get; set; }
        public Attr EUR { get; set; }
        public Attr NGN { get; set; }
        public Attr SAR { get; set; }
        public Attr USD { get; set; }
    }

    public struct Attr
    {
        public string code { get; set; }
        public string value { get; set; }
    }
}