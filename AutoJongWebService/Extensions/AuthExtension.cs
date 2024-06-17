using AutoJongWebService;
using AutoJongWebService.Models;

public class AuthExtension
{
    private readonly string _username;
    private readonly string _password;
    private readonly AppDbContext _context;

    public AuthExtension(string username, string password, AppDbContext context)
    {
        _username = username;
        _password = password;
        _context = context;
    }

    public AdminItem Authorization()
    {
        var admin = _context.AdminItems
                            .FirstOrDefault(a => a.Username == _username && a.Password == _password);

        if (admin != null)
        {
            return admin;
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }
}
