namespace Kernel.Users;
public class AddressPayload : Payload
{
    public AddressPayload(Address address)
    {
        Address = address;
    }

    public Address? Address { get; set; }
}
