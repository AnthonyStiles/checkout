namespace checkout;

public interface ICheckout
{
    int GetTotalPrice();
    void Scan(string item);
}