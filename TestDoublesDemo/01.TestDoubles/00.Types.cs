using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDoublesDemo.TestDoubles
{
    public interface IPhoneNumberValidator
    {
        bool IsValid(string phoneNumber);
    }

    public class Customer
    {
        public Guid Id { get; }
        public string Name { get; }
        public string PhoneNumber { get; }

        public Customer(string name, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }

    public class CustomerRepository
    {
        private readonly Dictionary<Guid, Customer> _customers = new Dictionary<Guid, Customer>();
        private readonly IPhoneNumberValidator _phoneNumberValidator;

        public CustomerRepository(IPhoneNumberValidator phoneNumberValidator)
        {
            if (phoneNumberValidator == null)
                throw new ArgumentNullException(nameof(phoneNumberValidator));

            _phoneNumberValidator = phoneNumberValidator;
        }

        public Customer Create(string name, string phoneNumber)
        {
            if (!_phoneNumberValidator.IsValid(phoneNumber))
            {
                throw new ArgumentException("Phone number is invalid");
            }

            var customer = new Customer(name, phoneNumber);
            _customers.Add(customer.Id, customer);
            return customer;
        }

        public IReadOnlyList<Customer> GetAll()
            => _customers.Values.ToList();
    }
}
