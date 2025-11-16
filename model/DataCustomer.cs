namespace admin_customer_api.models
{
    public class DataCustomer_save
    {
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string birthday { get; set; }
        public string Address { get; set; }
    }

    public class DataCustomer
    {
         public int itemid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string Address { get; set; }
    }
}