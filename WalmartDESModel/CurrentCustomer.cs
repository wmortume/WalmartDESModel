namespace WalmartDESModel
{
  class CurrentCustomer
  {
    public int Id { get; set; }
    public double ArrivalTime { get; set; }
    public double CheckoutTime { get; set; }
    public double AbsoluteTime { get; set; }
    public double WaitTime { get; set; }

    public CurrentCustomer(int id, double arrivalTime, double checkoutTime, double absoluteTime, double waitTime)
    {
      Id = id;
      ArrivalTime = arrivalTime;
      CheckoutTime = checkoutTime;
      AbsoluteTime = absoluteTime;
      WaitTime = waitTime;
    }
  }
}
