using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartDESModel
{
  class Program
  {
    static double clock = 0;
    const bool secondCashier = true;
    static int meanArrivalTime = 7;
    static int meanCheckoutTime = 3;
    static Random rand = new Random();
    static Queue<FutureCustomer> futureCustomers = new Queue<FutureCustomer>();
    static List<CurrentCustomer> currentCustomers = new List<CurrentCustomer>();
    static void Main(string[] args)
    {
      for (int i = 0; i < 100000; i++) //nextdouble = random decimal between 0 and 1
      {
        futureCustomers.Enqueue(new FutureCustomer(i + 1, -Math.Log(1 - rand.NextDouble()) * meanArrivalTime, -Math.Log(1 - rand.NextDouble()) * meanCheckoutTime));
      }

      while (futureCustomers.Any())
      {
        FutureCustomer customer = futureCustomers.Dequeue();
        Console.WriteLine($"Customer Id: {customer.Id}, Timestamp: {Math.Round(clock, 2)} Mins, Description: Arrived");
        double absoluteTime = customer.ArrivalTime;
        double waitTime = 0;

        Console.WriteLine($"Customer Id: {customer.Id}, Timestamp: {Math.Round(clock, 2)} Mins, Description: Acquires Cashier");

        if (currentCustomers.Any())
        {
          absoluteTime = currentCustomers.Last().AbsoluteTime + customer.ArrivalTime;
        }

        if (currentCustomers.Any() && clock > absoluteTime)
        {
          waitTime = clock - absoluteTime;
        }

        CurrentCustomer leavingCustomer = new CurrentCustomer(customer.Id, customer.ArrivalTime, customer.CheckoutTime, absoluteTime, waitTime);
        currentCustomers.Add(leavingCustomer);
        clock = absoluteTime + customer.CheckoutTime + waitTime;

        Console.WriteLine($"Customer Id: {leavingCustomer.Id}, Arrival Time: {Math.Round(leavingCustomer.ArrivalTime, 2)} Mins, Absolute Time: {Math.Round(leavingCustomer.AbsoluteTime, 2)} Mins, " +
          $"Checkout Time: {Math.Round(leavingCustomer.CheckoutTime, 2)} Mins, Wait Time: {Math.Round(leavingCustomer.WaitTime, 2)} Mins, Timestamp: {Math.Round(clock, 2)} Mins, Description: Checkout Completed");
      }

      double firstCashierAverageWaitTime = Math.Round(currentCustomers.Average(x => x.WaitTime), 2);

      if (secondCashier)
      {
        for (int i = 0; i < 80000; i++)
        {
          futureCustomers.Enqueue(new FutureCustomer(i + 1 + 100000, -Math.Log(1 - rand.NextDouble()) * meanArrivalTime, -Math.Log(1 - rand.NextDouble()) * meanCheckoutTime));
        }

        while (futureCustomers.Any())
        {
          FutureCustomer customer = futureCustomers.Dequeue();
          Console.WriteLine($"Customer Id: {customer.Id}, Timestamp: {Math.Round(clock, 2)} Mins, Description: Arrived");
          double absoluteTime = customer.ArrivalTime;
          double waitTime = 0;

          Console.WriteLine($"Customer Id: {customer.Id}, Timestamp: {Math.Round(clock, 2)} Mins, Description: Acquires Cashier");

          if (currentCustomers.Any())
          {
            absoluteTime = currentCustomers.Last().AbsoluteTime + customer.ArrivalTime;
          }

          if (currentCustomers.Any() && clock > absoluteTime)
          {
            waitTime = clock - absoluteTime;
          }

          CurrentCustomer leavingCustomer = new CurrentCustomer(customer.Id, customer.ArrivalTime, customer.CheckoutTime, absoluteTime, waitTime);
          currentCustomers.Add(leavingCustomer);
          clock = absoluteTime + customer.CheckoutTime + waitTime;

          Console.WriteLine($"Customer Id: {leavingCustomer.Id}, Arrival Time: {Math.Round(leavingCustomer.ArrivalTime, 2)} Mins, Absolute Time: {Math.Round(leavingCustomer.AbsoluteTime, 2)} Mins, " +
            $"Checkout Time: {Math.Round(leavingCustomer.CheckoutTime, 2)} Mins, Wait Time: {Math.Round(leavingCustomer.WaitTime, 2)} Mins, Timestamp: {Math.Round(clock, 2)} Mins, Description: Checkout Completed");
        }

        Console.WriteLine($"Average Waited Time For Each Customer: {(firstCashierAverageWaitTime + Math.Round(currentCustomers.Average(x => x.WaitTime), 2)) / 2} Mins");
      }
      else
      {
        Console.WriteLine($"Average Waited Time For Each Customer: {firstCashierAverageWaitTime} Mins");
      }
    }
  }
}
