using admin_customer_api.models;

public interface IDcServices
{
    Task<int> SaveDataCustomer(DataCustomer_save dataCustomer);

    Task<List<DataCustomer>> GetDataCustomer();

}