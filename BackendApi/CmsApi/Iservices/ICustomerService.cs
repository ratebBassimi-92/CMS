using CmsApi.ModelsDto;

namespace CmsApi.Iservices
{
    public interface ICustomerService
    {
        Task<ResponseDto> AddCustomer(CustomerDto customerInput);
        Task<ResponseDto> UpdateCustomer(CustomerDto customerInput, int id);
        Task<ResponseDto> DeleteCustomer(int customerId);
        Task<ResponseDto> GetCustomer(int customerId);
        Task<ResponseDto> GetCustomerList();
        Task<ResponseDto> GetAnalytic();
    }
}
