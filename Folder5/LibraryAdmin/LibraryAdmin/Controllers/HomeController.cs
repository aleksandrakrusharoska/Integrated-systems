using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using ClosedXML.Excel;
using ExcelDataReader;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using LibraryAdmin.Models;
using LibraryAdmin.Models.Dtos;

namespace LibraryAdmin.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private static readonly HttpClientHandler Handler = new()
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };

    private static readonly HttpClient Client = new(Handler);
    private const string BaseUrl = "https://localhost:7214/api";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
    }

    public async Task<IActionResult> Index()
    {
        var url = $"{BaseUrl}/Admin/GetAllOrders";
        var response = await Client.GetAsync(url);

        var result = await response.Content.ReadAsAsync<List<Order>>();

        //var orders = JsonSerializer.Deserialize<List<Order>>(result, new JsonSerializerOptions
        //{
        //    PropertyNameCaseInsensitive = true
        //});

        return View(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> OrderDetails(Guid id)
    {
        var url = $"{BaseUrl}/Admin/GetOrderById/{id}";
        var response = await Client.GetAsync(url);

        var result = await response.Content.ReadAsAsync<Order>();

        return View(result);
    }

    [HttpGet]
    public IActionResult ImportUsers()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImportUsers(IFormFile file)
    {
        // create a directory/folder if its not already created
        Directory.CreateDirectory($@"{Directory.GetCurrentDirectory()}\files\");

        var path = Path.Combine(Directory.GetCurrentDirectory(), "files", file.FileName);

        await using (var stream = System.IO.File.Create(path))
        {
            await file.CopyToAsync(stream);
        }

        var users = GetUsersFromFile(file.FileName);

        var url = $"{BaseUrl}/Admin/ImportUsers";

        //HttpContent content = new StringContent(JsonSerializer.Serialize(users), Encoding.UTF8, "application/json");

        var response = await Client.PostAsJsonAsync(url, users);

        var result = await response.Content.ReadAsAsync<bool>();

        return RedirectToAction("Index");
    }


    private List<UserDto> GetUsersFromFile(string fileName)
    {
        var users = new List<UserDto>();

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files", fileName);

        System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
        using var reader = ExcelReaderFactory.CreateReader(stream);

        while (reader.Read())
        {
            users.Add(new UserDto
            {
                Email = reader.GetValue(0)?.ToString(),
                Password = reader.GetValue(1)?.ToString(),
                ConfirmPassword = reader.GetValue(2)?.ToString()
            });
        }

        return users;
    }

    public async Task<IActionResult> ExportOrders()
    {
        var url = $"{BaseUrl}/Admin/GetAllOrders";
        var response = await Client.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Failed to fetch the orders from the API");
        }

        var orders = await response.Content.ReadAsAsync<List<Order>>();

        var fileName = $"Orders_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Orders");
        worksheet.Cell(1, 1).Value = "Order ID";
        worksheet.Cell(1, 2).Value = "Customer email";

        foreach (var (order, index) in orders.Select((v, i) => (v, i + 2)))
        {
            worksheet.Cell(index, 1).Value = order.Id.ToString();
            worksheet.Cell(index, 2).Value = order.LibraryUser.Email;

            foreach (var (book, j)in order.BooksInOrder.Select((v, i) => (v, i)))
            {
                worksheet.Cell(1, 3 + j).Value = $"Book #{j + 1}";
                worksheet.Cell(index, 3 + j).Value = $"{book.Book?.Title} - {book.Book?.Author} ({book.Quantity})";
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();

        const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return File(content, contentType, fileName);
    }

    public async Task<IActionResult> CreateInvoice(Guid id)
    {
        var url = $"{BaseUrl}/Admin/GetOrderById/{id}";
        var response = await Client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Failed to fetch the order from the API");
        }

        var order = await response.Content.ReadAsAsync<Order>();

        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
        var document = DocumentModel.Load(templatePath);

        document.Content.Replace("{{OrderNumber}}", order.Id.ToString());
        document.Content.Replace("{{UserName}}", $"{order.LibraryUser.FirstName} {order.LibraryUser.LastName} - {order.LibraryUser.Email}");

        var sb = new StringBuilder();

        var totalPrice = 0.0;
        foreach (var booksInOrder in order.BooksInOrder)
        {
            var bookPrice = (booksInOrder.Book?.Price ?? 0) * booksInOrder.Quantity;
            totalPrice += bookPrice * booksInOrder.Quantity;
            sb.AppendLine($"{booksInOrder.Book?.Title} - {booksInOrder.Book?.Author} (quantity: {booksInOrder.Quantity}) for the price of {bookPrice}");
        }

        document.Content.Replace("{{BookList}}", sb.ToString());
        document.Content.Replace("{{TotalPrice}}", totalPrice.ToString("C", new CultureInfo("en-US")));

        var stream = new MemoryStream();

        var pdfSaveOptions = new PdfSaveOptions();
        document.Save(stream, pdfSaveOptions);

        return File(stream.ToArray(), pdfSaveOptions.ContentType, $"Invoice_{order.Id}.pdf");
    }
}
