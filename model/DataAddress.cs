namespace admin_customer_api.models
{
    public class Dataprovince
    {
        public int province_id { get; set; }
        public string province_th { get; set; }

        public string province_en { get; set; }
    }

    public class DataCity
    {
        public int city_id { get; set; }
        public string city_th { get; set; }
        public string city_en { get; set; }
    }

    public class DataDistrict
    {
        public int district_id { get; set; }
        public string district_th { get; set; }
        public string district_en { get; set; }
    }

    public class DataZipcode
    {
        public int zipcode_id { get; set; }
        public string zipcode { get; set; }
    }
}