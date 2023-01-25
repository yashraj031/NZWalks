namespace NZWalksAPI.Model.DTO
{
    public class AddRegionRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double last { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
