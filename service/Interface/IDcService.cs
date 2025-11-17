using admin_customer_api.models;

public interface IDcServices
{
    Task<int> SaveDataCustomer(DataCustomer_save dataCustomer);

    Task<List<DataCustomer>> GetDataCustomer();

    Task<DataCustomer> GetDataCustomerById(int itemid);

    Task<List<Dataprovince>> GetProvice();

    Task<List<DataCity>> GetCity(int province_id);

    Task<List<DataDistrict>> GetDistrict(int province_id,int city_id);

    Task<List<DataZipcode>> GetZipcode(int province_id,int city_id,int district_id);


}