namespace WalmartDESModel
{
  class FutureCustomer
  {
    public int Id { get; set; }
    public double ArrivalTime { get; set; }
    public double CheckoutTime { get; set; }

    public FutureCustomer(int id, double arrivalTime, double checkoutTime)
    {
      Id = id;
      ArrivalTime = arrivalTime;
      CheckoutTime = checkoutTime;
    }
  }
}
