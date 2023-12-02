
List<Member> members = new List<Member>() {
    new() { Id = 1, FirstName = "Emre", LastName = "Polat", BrowwingBooks = new() },
    new() { Id = 2, FirstName = "Mehmet", LastName = "Yılmaz", BrowwingBooks = new() }
};

List<Book> books = new List<Book>()
{
    new() { Id = 1, Name = "Deneme", Author = "Yiğit Öztürk", PublicationYear = 2023 },
    new() { Id = 2, Name = "Test", Author = "Kerim Polat", PublicationYear = 2022 },
};

Library library = new Library(members, books);

library.PrintBooks();
library.PrintMembers();

while (true)
{
    Console.WriteLine("Kütüphane Yönetim Sistemi: ");
    Console.WriteLine("1. Kitapları listele");
    Console.WriteLine("2. Üyeleri listele");
    //Console.WriteLine("3. Üye ekle");
    //Console.WriteLine("4. Kitap ekle");
    Console.WriteLine("3. Kitap ödünç ver");
    Console.WriteLine("4. Kitap iade et");
    Console.WriteLine("5. Çıkış");
    Console.WriteLine("Lütfen yapmak istediğiniz işlemin numarasını giriniz: ");
    int readNumber = Convert.ToInt16(Console.ReadLine());

    switch (readNumber)
    {
        case 1:
            library.PrintBooks(); 
            break;
        case 2:
            library.PrintMembers(); 
            break;
        //case 3:
        //    Console.WriteLine("Üye Adı: ");
        //    string memberFirstName = Console.ReadLine();
        //    Console.WriteLine("Üye Soyadı: ");
        //    string memberLastName = Console.ReadLine();
        //    library.AddMember(new() { FirstName= memberFirstName, LastName = memberLastName}); 
        //    break;
        //case 4:
        //    Console.WriteLine("Kitap Adı: ");
        //    string bookName = Console.ReadLine();
        //    Console.WriteLine("Kitap Yazarı: ");
        //    string bookAuthor = Console.ReadLine();
        //    Console.WriteLine("Kitap Yayın Yılı: ");
        //    int bookPublicationYear = Convert.ToInt32(Console.ReadLine());
        //    library.AddBook(new() { Name = bookName, Author = bookAuthor, PublicationYear = bookPublicationYear });
        //    break;
        case 3:
            library.PrintBooks();
            library.PrintMembers();
            Console.WriteLine("Üye Id: ");
            var memberId = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Kitap Id: ");
            var bookId = Convert.ToInt16(Console.ReadLine());
            library.BorrowBook(bookId, memberId);
            break;
        case 4:
            library.PrintBooks();
            library.PrintMembers();
            Console.WriteLine("Üye Id: ");
            var returnMemberId = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Kitap Id: ");
            var returnBookId = Convert.ToInt16(Console.ReadLine());
            library.ReturnBook(returnBookId, returnMemberId);
            break;
        case 5:
            return 0;
        default:
            break;
    }
}

public class Book : IBook
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
}

public class Literature : Book, IBook
{
    public string Tone { get; set; }
    public string Style { get; set; }
    public string Characters { get; set; }
}

public class Member : IMember
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Book> BrowwingBooks { get; set; }
}

public class Library : ILibrary, IPrintable
{
    private List<Member> _members;
    private List<Book> _books;
    public Library(List<Member> members, List<Book> books)
    {
        _members = members;
        _books = books;
    }

    public List<Member> GetMembers()
    {
        return _members;
    }

    public List<Book> GetBooks()
    {
        return _books;
    }

    //public void AddBook(Book book)
    //{
    //    _books.Add(book);
    //}

    //public void AddMember(Member member)
    //{
    //    if (_members.Any()) 
    //    { 
        
    //    };

    //    member.Id = _members
    //    _members.Add(member);
    //}

    public void BorrowBook(int bookId, int memberId)
    {
        var book = _books.Find(x => x.Id == bookId);
        var member = _members.Find(x => x.Id == memberId);
        member.BrowwingBooks.Add(book);
    }

    public void ReturnBook(int bookId, int memberId)
    {
        var book = _books.Find(x => x.Id == bookId);
        var member = _members.Find(x => x.Id == memberId);
        member.BrowwingBooks.Remove(book);
    }

    public void PrintMembers()
    {
        Console.WriteLine("Üyeler: ");
        foreach (Member member in _members)
        {
            Console.WriteLine($"Id: {member.Id}, Name: {member.FirstName} {member.LastName}");
            Console.WriteLine("Ödünç alınan kitaplar: ");
            if(member.BrowwingBooks.Count < 1)
            {
                Console.Write("-");
                Console.WriteLine();
            }

            foreach (Book browwingBook in member.BrowwingBooks)
            {
                Console.WriteLine($"Id: {browwingBook.Id}, Name: {browwingBook.Name}, Author: {browwingBook.Author}, Publication Year: {browwingBook.PublicationYear}");
            }

            Console.WriteLine();
        }
    }

    public void PrintBooks()
    {
        Console.WriteLine("Kitaplar:");

        foreach (Book book in _books)
        {
            Console.WriteLine($"Id: {book.Id}, Name: {book.Name}, Author: {book.Author}, Publication Year: {book.PublicationYear}");
        }

        Console.WriteLine();
    }
}

public interface IBook { }
public interface IMember { }
public interface IPrintable
{
    void PrintMembers();
    void PrintBooks();
}

public interface ILibrary
{
    List<Book> GetBooks();
    List<Member> GetMembers();
    //void AddBook(Book book);
    //void AddMember(Member member);
    void ReturnBook(int bookId, int memberId);
    void BorrowBook(int bookId, int memberId);
}