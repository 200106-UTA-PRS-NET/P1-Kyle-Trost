using Microsoft.Data.SqlClient;
using System;

namespace PizzaBox.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = -1;

            var connection = new UserConnection();

            Console.WriteLine("Welcome to Pop-Pop Pizza, where we are a palindrome!");

            do
            {
                Console.WriteLine("What can we do for you today?");
                Console.WriteLine("1. Sign in");
                Console.WriteLine("2. View store locations");
                Console.WriteLine("3. Order pizza");
                Console.WriteLine("4. View order history");
                Console.WriteLine("0. Quit");
                Console.Write("Please enter the number of your choice: ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
         
                    switch (choice)
                    {
                        case 1:
                            SignIn(connection);
                            break;
                        case 2:
                            Console.WriteLine("Getting store locations...");
                            connection.ViewStoreLocations();
                            break;
                        case 3:
                            connection.CreateOrder();
                            break;
                        case 4:
                            connection.ViewOrderHistory();
                            break;
                        case 0:
                            Console.WriteLine("Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch(FormatException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch(NullReferenceException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch(SqlException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            } while (choice != 0);
        }

        static void SignIn(UserConnection connection)
        {
            string user, pass;

            Console.Write("Please enter your user name: ");
            user = Console.ReadLine();
            Console.Write("Please enter your password: ");
            pass = Console.ReadLine();

            connection.SignInAttempt(user, pass);
        }
    }
}
