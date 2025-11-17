namespace admin_customer_api.models
{
    public class DataCustomer_save
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string birthday { get; set; }
        public string Address { get; set; }

        public int province_id { get; set; }

        public int city_id { get; set; }

        public int district_id { get; set; }
        public string zipcode { get; set; }
        public int zipcode_id { get; set; }
    }

    public class DataCustomer
    {
         public int itemid { get; set; } =0;
        public string firstname { get; set; } = "";
        public string lastname { get; set; } = "";

        public string birthday { get; set; } ="";
        public int age { get; set; } = 0;
        public string Address { get; set; } ="";
        public int province_id { get; set; } =0 ;
        public string province_th { get; set; } ="";
        public int city_id { get; set; } = 0;
        public string city_th { get; set; } ="";
        public int district_id { get; set; } =0;
        public string district_th { get; set; }="";
         public string zipcode { get; set; } ="";
        public int zipcode_id { get; set; } =0;
       

    }
}