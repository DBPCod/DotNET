namespace Backend.Services.Apis;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

}
