using System;
using System.Collections.Generic;

namespace LibraryManagement
{
    class Program
    {
        public class Book
        {
            public string Title { get; set; }
            public bool IsBorrowed { get; set; }

            public Book(string title)
            {
                Title = title;
                IsBorrowed = false;
            }
        }

        static void Main(string[] args)
        {
            List<Book> library = new List<Book>();
            bool exit = false;

            Console.WriteLine("Welcome to the Library Book Management System!");

            while (!exit)
            {
                try
                {
                    Console.WriteLine("\n1. Add a new book");
                    Console.WriteLine("2. Borrow a book");
                    Console.WriteLine("3. Return a book");
                    Console.WriteLine("4. Display all books");
                    Console.WriteLine("5. Exit");
                    Console.Write("Choose an action: ");

                    // Перетворення введення в число
                    int choice = int.Parse(Console.ReadLine());

                    // Перевірка, чи введене число в допустимому діапазоні
                    if (choice < 1 || choice > 5)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                        continue; // Повернення до меню
                    }

                    // Виконання вибраної дії
                    switch (choice)
                    {
                        case 1:
                            AddBook(library);
                            break;
                        case 2:
                            BorrowBook(library);
                            break;
                        case 3:
                            ReturnBook(library);
                            break;
                        case 4:
                            Display(library);
                            break;
                        case 5:
                            exit = true;
                            break;
                    }
                }
                catch (FormatException) // Якщо користувач ввів не число
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (Exception ex) // Інші можливі винятки
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }

            Console.WriteLine("Thank you for using the Library Book Management System. Goodbye!");
        }

        //метод щоб додати нову книгу
        static void AddBook(List<Book> library)
        {
            Console.Write("Enter the name of the book to add: ");
            string title = Console.ReadLine();

            if (!string.IsNullOrEmpty(title))
            {
                library.Add(new Book(title));
                Console.WriteLine($"Book \"{title}\" successfully added!");
            }
            else
            {
                Console.WriteLine("Book name cannot be empty.");
            }
        }

        //метод для того щоб взяти книгу в позику
        static void BorrowBook(List<Book> library)
        {
            Console.Write("Enter the name of the book to borrow: ");
            string title = Console.ReadLine();

            Book book = library.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (book != null)
            {
                if (!book.IsBorrowed)
                {
                    book.IsBorrowed = true;
                    Console.WriteLine($"You have successfully borrowed \"{title}\".");
                }
                else
                {
                    Console.WriteLine($"The book \"{title}\" is already borrowed.");
                }
            }
            else
            {
                Console.WriteLine($"The book \"{title}\" is not available in the library.");
            }
        }

        //метод щоб повернути книгу у бібліотеку
        static void ReturnBook(List<Book> library)
        {
            Console.WriteLine("Enter the name of the book to return: ");
            string title = Console.ReadLine();

            Book book = library.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (book != null)
            {
                if (book.IsBorrowed)
                {
                    book.IsBorrowed = false;
                    Console.WriteLine($"You have successfully returned \"{title}\".");
                }
                else
                {
                    Console.WriteLine($"The book \"{title}\" was not borrowed.");
                }
            }
            else
            {
                Console.WriteLine($"The book \"{title}\" is not available in the library.");
            }
        }

        //метод для відображення всіх книг які існують
        static void Display(List<Book> library)
        {
            if (library.Count == 0)
            {
                Console.WriteLine("No books in the library");
            }
            else
            {
                Console.WriteLine("\nList of books in the library:");
                foreach (var book in library)
                {
                    string status = book.IsBorrowed ? "Borrowed" : "Available";
                    Console.WriteLine($"- {book.Title} ({status})");
                }
            }
        }
    }
};
