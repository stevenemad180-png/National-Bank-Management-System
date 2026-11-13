
using System;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace National_Bank_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using AppDbContext db = new AppDbContext();

            // ==================================
            // Part 02: Seeding
            // ==================================

            if (!db.Branches.Any(b => b.Code == 101))
            {
                db.Branches.Add(new Branch
                {
                    Code = 101,
                    Name = "Cairo Main Branch",
                    Address = "Cairo",
                    PhoneNumber = "01000000001"
                });
            }

            if (!db.Branches.Any(b => b.Code == 102))
            {
                db.Branches.Add(new Branch
                {
                    Code = 102,
                    Name = "Alexandria Branch",
                    Address = "Alexandria",
                    PhoneNumber = "01000000002"
                });
            }

            db.SaveChanges();

            if (!db.Managers.Any(m => m.BranchCode == 101))
            {
                db.Managers.Add(new Manager
                {
                    FullName = "Ahmed Ali",
                    Email = "ahmed@bank.com",
                    PhoneNumber = "01111111111",
                    HireDate = new DateTime(2022, 1, 10),
                    BranchCode = 101
                });
            }

            if (!db.Managers.Any(m => m.BranchCode == 102))
            {
                db.Managers.Add(new Manager
                {
                    FullName = "Sara Hassan",
                    Email = "sara@bank.com",
                    PhoneNumber = "01222222222",
                    HireDate = new DateTime(2023, 5, 15),
                    BranchCode = 102
                });
            }

            db.SaveChanges();

            // ==================================
            // Part 03: Interactive Menu
            // ==================================

            while (true)
            {
                Console.Clear();

                Console.WriteLine("==========================================");
                Console.WriteLine("       National Bank - Management");
                Console.WriteLine("==========================================");
                Console.WriteLine("1) Add a new Customer");
                Console.WriteLine("2) Open a new Account for a Customer");
                Console.WriteLine("3) Update Account Status (Active / Closed)");
                Console.WriteLine("4) Remove an Account from a Customer");
                Console.WriteLine("5) List all Customers (with accounts)");
                Console.WriteLine("0) Exit");
                Console.WriteLine("==========================================");
                Console.Write("Enter choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Enter a number.");
                    Console.ReadKey(true);
                    continue;
                }

                if (choice == 0)
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                try
                {
                    switch (choice)
                    {
                        // ==========================
                        // 1. Add Customer
                        // ==========================
                        case 1:
                            {
                                Console.WriteLine("\n--- Add New Customer ---");

                                string fullName;
                                do
                                {
                                    Console.Write("Full Name: ");
                                    fullName = Console.ReadLine() ?? "";
                                }
                                while (string.IsNullOrWhiteSpace(fullName));

                                string nationalId;

                                while (true)
                                {
                                    Console.Write("National ID: ");
                                    nationalId = Console.ReadLine() ?? "";

                                    if (string.IsNullOrWhiteSpace(nationalId)
                                        || !nationalId.All(char.IsDigit))
                                    {
                                        Console.WriteLine("Enter a valid numeric National ID.");
                                        continue;
                                    }

                                    if (db.Customers.Any(c => c.NationalId == nationalId))
                                    {
                                        Console.WriteLine("National ID already exists.");
                                        continue;
                                    }

                                    break;
                                }

                                DateTime dateOfBirth;

                                while (true)
                                {
                                    Console.Write("Date of Birth (yyyy-MM-dd): ");

                                    string input = Console.ReadLine() ?? "";

                                    if (DateTime.TryParseExact(
                                        input,
                                        "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out dateOfBirth)
                                        && dateOfBirth.Date < DateTime.Today)
                                    {
                                        break;
                                    }

                                    Console.WriteLine("Invalid Date of Birth.");
                                }

                                string email;

                                do
                                {
                                    Console.Write("Email: ");
                                    email = Console.ReadLine() ?? "";

                                    if (string.IsNullOrWhiteSpace(email)
                                        || !email.Contains('@')
                                        || !email.Contains('.'))
                                    {
                                        Console.WriteLine("Invalid email.");
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                while (true);

                                string phone;

                                do
                                {
                                    Console.Write("Phone: ");
                                    phone = Console.ReadLine() ?? "";

                                    if (phone.Length < 7 || !phone.All(char.IsDigit))
                                    {
                                        Console.WriteLine("Invalid phone number.");
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                while (true);

                                string address;

                                do
                                {
                                    Console.Write("Address: ");
                                    address = Console.ReadLine() ?? "";
                                }
                                while (string.IsNullOrWhiteSpace(address));

                                Console.WriteLine("Customer Type:");
                                Console.WriteLine("1) Individual");
                                Console.WriteLine("2) Business");

                                string customerChoice;

                                do
                                {
                                    Console.Write("Choice: ");
                                    customerChoice = Console.ReadLine() ?? "";
                                }
                                while (customerChoice != "1" && customerChoice != "2");

                                Customer customer = new Customer
                                {
                                    FullName = fullName,
                                    NationalId = nationalId,
                                    DateOfBirth = dateOfBirth,
                                    Email = email,
                                    PhoneNumber = phone,
                                    Address = address,
                                    cusstomerType = customerChoice == "1"
                                        ? "Individual"
                                        : "Business"
                                };

                                db.Customers.Add(customer);
                                db.SaveChanges();

                                Console.WriteLine(
                                    $"\nCustomer created successfully. CustomerId = {customer.Id}");

                                break;
                            }

                        // ==========================
                        // 2. Open Account
                        // ==========================
                        case 2:
                            {
                                Console.WriteLine("\n--- Open New Account ---");

                                int accountNumber;

                                while (true)
                                {
                                    Console.Write("Account Number: ");

                                    if (int.TryParse(Console.ReadLine(), out accountNumber)
                                        && accountNumber > 0)
                                        break;

                                    Console.WriteLine("Invalid account number.");
                                }

                                Console.WriteLine("Account Type:");
                                Console.WriteLine("1) Savings");
                                Console.WriteLine("2) Current");
                                Console.WriteLine("3) Business");

                                string typeChoice;

                                do
                                {
                                    Console.Write("Choice: ");
                                    typeChoice = Console.ReadLine() ?? "";
                                }
                                while (typeChoice != "1"
                                    && typeChoice != "2"
                                    && typeChoice != "3");

                                string accountType = typeChoice switch
                                {
                                    "1" => "Savings",
                                    "2" => "Current",
                                    _ => "Business"
                                };

                                int branchCode;

                                while (true)
                                {
                                    Console.Write("Branch Code: ");

                                    if (int.TryParse(Console.ReadLine(), out branchCode)
                                        && branchCode > 0)
                                        break;

                                    Console.WriteLine("Invalid branch code.");
                                }

                                int customerId;

                                while (true)
                                {
                                    Console.Write("Customer Id: ");

                                    if (int.TryParse(Console.ReadLine(), out customerId)
                                        && customerId > 0)
                                        break;

                                    Console.WriteLine("Invalid customer Id.");
                                }

                                Console.WriteLine("Ownership Role:");
                                Console.WriteLine("1) Primary");
                                Console.WriteLine("2) CoHolder");

                                string roleChoice;

                                do
                                {
                                    Console.Write("Choice: ");
                                    roleChoice = Console.ReadLine() ?? "";
                                }
                                while (roleChoice != "1" && roleChoice != "2");

                                string role = roleChoice == "1"
                                    ? "Primary"
                                    : "CoHolder";

                                Console.WriteLine("\nValidating branch and customer...");

                                Branch? branch = db.Branches.Find(branchCode);
                                Customer? owner = db.Customers.Find(customerId);

                                if (branch == null)
                                {
                                    Console.WriteLine("Branch not found.");
                                    break;
                                }

                                if (owner == null)
                                {
                                    Console.WriteLine("Customer not found.");
                                    break;
                                }

                                Account? account = db.Accounts.Find(accountNumber);

                                if (db.AccountCustomers.Any(ac =>
                                    ac.AccountNumber == accountNumber &&
                                    ac.CustomerId == customerId))
                                {
                                    Console.WriteLine("Customer already owns this account.");
                                    break;
                                }

                                if (account == null)
                                {
                                    if (role != "Primary")
                                    {
                                        Console.WriteLine(
                                            "A new account must start with a Primary owner.");
                                        break;
                                    }

                                    account = new Account
                                    {
                                        AccountNumber = accountNumber,
                                        AccountType = accountType,
                                        CurrentBalance = 0,
                                        OpeningDate = DateTime.Today,
                                        BranchCode = branchCode
                                    };

                                    db.Accounts.Add(account);
                                }
                                else
                                {
                                    if (account.BranchCode != branchCode
                                        || account.AccountType != accountType)
                                    {
                                        Console.WriteLine(
                                            "Existing account has a different branch or type.");
                                        break;
                                    }

                                    if (role == "Primary" &&
                                        db.AccountCustomers.Any(ac =>
                                            ac.AccountNumber == accountNumber &&
                                            ac.OwnershipType == "Primary"))
                                    {
                                        Console.WriteLine(
                                            "Account already has a Primary owner.");
                                        break;
                                    }
                                }

                                AccountCustomer accountCustomer = new AccountCustomer
                                {
                                    AccountNumber = accountNumber,
                                    CustomerId = customerId,
                                    OwnershipType = role,
                                    OwnershipStartDate = DateTime.Today,
                                    AccountStatus = "Active"
                                };

                                db.AccountCustomers.Add(accountCustomer);
                                db.SaveChanges();

                                Console.WriteLine(
                                    $"\nAccount '{accountNumber}' linked to customer {customerId} as {role}.");

                                break;
                            }

                        // ==========================
                        // 3. Update Account Status
                        // ==========================
                        case 3:
                            {
                                Console.WriteLine("\n--- Update Account Status ---");

                                Console.Write("Account Number: ");

                                if (!int.TryParse(Console.ReadLine(), out int accountNumber))
                                {
                                    Console.WriteLine("Invalid account number.");
                                    break;
                                }

                                Console.Write("Customer Id: ");

                                if (!int.TryParse(Console.ReadLine(), out int customerId))
                                {
                                    Console.WriteLine("Invalid customer Id.");
                                    break;
                                }

                                AccountCustomer? link = db.AccountCustomers
                                    .FirstOrDefault(ac =>
                                        ac.AccountNumber == accountNumber &&
                                        ac.CustomerId == customerId);

                                if (link == null)
                                {
                                    Console.WriteLine("Account ownership not found.");
                                    break;
                                }

                                Console.WriteLine("New Status:");
                                Console.WriteLine("1) Active");
                                Console.WriteLine("2) Closed");
                                Console.Write("Choice: ");

                                string statusChoice = Console.ReadLine() ?? "";

                                if (statusChoice != "1" && statusChoice != "2")
                                {
                                    Console.WriteLine("Invalid status.");
                                    break;
                                }

                                link.AccountStatus = statusChoice == "1"
                                    ? "Active"
                                    : "Closed";

                                db.SaveChanges();

                                Console.WriteLine(
                                    $"\nStatus updated to {link.AccountStatus}.");

                                break;
                            }

                        // ==========================
                        // 4. Remove Account from Customer
                        // ==========================
                        case 4:
                            {
                                Console.WriteLine("\n--- Remove Account From Customer ---");

                                Console.Write("Account Number: ");

                                if (!int.TryParse(Console.ReadLine(), out int accountNumber))
                                {
                                    Console.WriteLine("Invalid account number.");
                                    break;
                                }

                                Console.Write("Customer Id: ");

                                if (!int.TryParse(Console.ReadLine(), out int customerId))
                                {
                                    Console.WriteLine("Invalid customer Id.");
                                    break;
                                }

                                AccountCustomer? link = db.AccountCustomers
                                    .FirstOrDefault(ac =>
                                        ac.AccountNumber == accountNumber &&
                                        ac.CustomerId == customerId);

                                if (link == null)
                                {
                                    Console.WriteLine("Ownership link not found.");
                                    break;
                                }

                                int ownersCount = db.AccountCustomers.Count(ac =>
                                    ac.AccountNumber == accountNumber);

                                // Protect transaction history from accidental deletion
                                if (ownersCount == 1 &&
                                    db.Transactions.Any(t =>
                                        t.AccountNumber == accountNumber))
                                {
                                    Console.WriteLine(
                                        "Cannot remove the last owner while transactions exist.");
                                    break;
                                }

                                db.AccountCustomers.Remove(link);

                                if (ownersCount == 1)
                                {
                                    Account? account = db.Accounts.Find(accountNumber);

                                    if (account != null)
                                        db.Accounts.Remove(account);
                                }

                                db.SaveChanges();

                                Console.WriteLine("\nOwnership link deleted.");

                                if (ownersCount == 1)
                                {
                                    Console.WriteLine(
                                        $"That was the last owner - account '{accountNumber}' was also removed.");
                                }

                                break;
                            }

                        // ==========================
                        // 5. List Customers (Eager Loading)
                        // ==========================
                        case 5:
                            {
                                Console.WriteLine("\n--- All Customers ---");

                                var customers = db.Customers
                                    .Include(c => c.Accounts)
                                    .ThenInclude(ac => ac.Account)
                                    .ThenInclude(a => a.Branch)
                                    .AsNoTracking()
                                    .OrderBy(c => c.Id)
                                    .ToList();

                                if (customers.Count == 0)
                                {
                                    Console.WriteLine("No customers found.");
                                    break;
                                }

                                foreach (var c in customers)
                                {
                                    Console.WriteLine(
                                        $"#{c.Id} {c.FullName} ({c.cusstomerType})");

                                    if (!c.Accounts.Any())
                                    {
                                        Console.WriteLine("    (no accounts)");
                                        continue;
                                    }

                                    foreach (var ac in c.Accounts)
                                    {
                                        Account a = ac.Account;

                                        Console.WriteLine(
                                            $"    {a.AccountNumber}  {a.AccountType}  " +
                                            $"Balance: {a.CurrentBalance:N2}  " +
                                            $"{ac.OwnershipType}  {ac.AccountStatus}  " +
                                            $"@ {a.Branch.Name}");
                                    }
                                }

                                break;
                            }

                        default:
                            {
                                Console.WriteLine("Invalid menu choice.");
                                break;
                            }
                    }
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(
                        "\nDatabase Error: " +
                        (ex.InnerException?.Message ?? ex.Message));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nError: " + ex.Message);
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(true);
            }
        }
    }
}
