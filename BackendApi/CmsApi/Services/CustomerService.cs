using CmsApi.DatabaseContext;
using CmsApi.Iservices;
using CmsApi.Models;
using CmsApi.ModelsDto;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CmsApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CmsDbContext _dbCmsContext;
        public CustomerService(CmsDbContext dbCmsContext)
        {
            _dbCmsContext = dbCmsContext;
        }
        public async Task<ResponseDto> AddCustomer(CustomerDto customerInput)
        {
            ResponseDto responce = new ResponseDto();
            try
            {
                if (customerInput != null)
                {
                    var cusotmerObj =await _dbCmsContext.Customers.AsNoTracking().Where(c => c.Email== customerInput.Email).FirstOrDefaultAsync();
                    if (cusotmerObj != null)
                    {
                        responce.Message = "This Email For Customer Already Found";
                        responce.Success = false;
                        responce.Status = StatusCodes.Status200OK.ToString();
                    }
                    else
                    {
                        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                            Match matchEmail = regex.Match(customerInput.Email);
                        if (matchEmail.Success)
                        {
                            Customer customer = new Customer(customerInput.FirstName, customerInput.LastName, customerInput.Email, customerInput.Phone,
                                                       customerInput.Address, customerInput.CreatedBy);

                            _dbCmsContext.Add<Customer>(customer);
                            _dbCmsContext.SaveChanges();

                            var customerAdded = _dbCmsContext.Customers.AsNoTracking().Where(c => c.Email == customerInput.Email).FirstOrDefault();


                            responce.Message = "save customer successful";
                            responce.Success = true;
                            responce.Status = StatusCodes.Status200OK.ToString();
                            responce.Data = customerAdded;
                        }
                        else
                        {
                            responce.Message = "The Email For Customer Not Valid";
                            responce.Success = false;
                            responce.Status = StatusCodes.Status200OK.ToString();
                        }
                          
                    }
                }

            }
            catch (Exception ex)
            {
                responce.Message = ex.Message;
                responce.Success = false;
                responce.Status = StatusCodes.Status500InternalServerError.ToString();

            }

            return responce;
        }

        public async Task<ResponseDto> DeleteCustomer(int customerId)
        {
            ResponseDto responce = new ResponseDto();
            try
            {
                var customerObj = await _dbCmsContext.Customers.AsNoTracking().Where(c => c.CustomerId == customerId).FirstOrDefaultAsync();
                if (customerObj != null)
                {
                    _dbCmsContext.Customers.Remove(customerObj);
                    _dbCmsContext.SaveChanges();

                    responce.Message = "the Customer Deleted Successfully";
                    responce.Success = true;
                }
                else
                {
                    responce.Message = "The Customer Not Found ";
                    responce.Success = false;
                }
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
            }
            return responce;
        }

        public async Task<ResponseDto> GetCustomer(int customerId)
        {
            ResponseDto responce = new ResponseDto();
            var customerData = await _dbCmsContext.Customers.FindAsync(customerId);
           
            if (customerData != null)
            {
                responce.Message = "The customer is Found";
                responce.Success = true;
                responce.Data = customerData;
            }
            else
            {
                responce.Message = "The customer is Not Found";
                responce.Success = false;
            }
           
            responce.Status = StatusCodes.Status200OK.ToString();
            return responce;
        }

        public async Task<ResponseDto> GetCustomerList()
        {
            ResponseDto responce = new ResponseDto();
           
            var customerList = await _dbCmsContext.Customers.AsNoTracking().ToListAsync();

            responce.Message = "List Of Customer";
            responce.Success = true;
            responce.Status = StatusCodes.Status200OK.ToString();
            responce.Data = customerList;
            return responce;
        }

        public async Task<ResponseDto> UpdateCustomer(CustomerDto customerInput, int id)
        {
            ResponseDto responce = new ResponseDto();
            if (customerInput != null)
            {
                var CustomerObj = await _dbCmsContext.Customers.AsNoTracking().Where(c => c.CustomerId == id).FirstOrDefaultAsync();
                if (CustomerObj is not null)
                {
                    CustomerObj.FirstName = customerInput.FirstName;
                    CustomerObj.LastName = customerInput.LastName;
                    CustomerObj.Email = customerInput.Email;
                    CustomerObj.Phone = customerInput.Phone;
                    CustomerObj.Address = customerInput.Address;
                    CustomerObj.UpdatedBy = customerInput.CreatedBy;
                    CustomerObj.UpdatedAt = DateTime.Now;
                    CustomerObj.CustomerId= id;

                    var customerToUpdate = _dbCmsContext.Customers.Find(id);
                    _dbCmsContext.Entry(customerToUpdate).CurrentValues.SetValues(CustomerObj);
                    //_dbCmsContext.Update<Customer>(CustomerObj);
                    _dbCmsContext.SaveChanges();

                    responce.Message = "Update customer successful";
                    responce.Success = true;
                    responce.Status = StatusCodes.Status200OK.ToString();
                    responce.Data = customerInput;
                }
                else
                {
                    responce.Message = "the Customer Not Found";
                    responce.Success = false;
                    responce.Status = StatusCodes.Status200OK.ToString();
                }
            }
            return responce;
        }

        public async Task<ResponseDto> GetAnalytic()
        {

            ResponseDto responce = new ResponseDto();

            var customerList = await _dbCmsContext.Customers.AsNoTracking()
                .GroupBy(x =>x.Address)
                .Select(g => new
                {
                    City = g.Select(address=>address.Address).FirstOrDefault(),
                    TotalCustomers = g.Count()
                })
                .ToListAsync();

            responce.Message = "List Of Customer ";
            responce.Success = true;
            responce.Status = StatusCodes.Status200OK.ToString();
            responce.Data = customerList;
            return responce;
        }
    }
}
