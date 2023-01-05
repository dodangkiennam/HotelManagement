using HotelManagement.Data;
using HotelManagement.ViewModels;
using HotelManagement.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Mail;

namespace HotelManagement.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = "/")
        {
            LoginViewModel loginModel = new LoginViewModel() { ReturnUrl = ReturnUrl };

            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            if(User.Identity!.IsAuthenticated)
            {
                return LocalRedirect("/");
            }

            var account = _context.Accounts.FirstOrDefault(x => x.Username == loginModel.Username && x.Password == loginModel.Password);

            if (account == null)
            {
                ViewBag.Message = "Invalid username or password!";
                return View(loginModel);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(account.AccId)),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.RoleName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
            {
                IsPersistent = loginModel.RememberLogin
            });

            return LocalRedirect(loginModel.ReturnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var account = new Account() { RoleName = "customer" };
            var customer = new Customer() { Account = account };

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return View(customer);
            }

            var account = _context.Accounts.FirstOrDefault(x => x.Username == customer.Account.Username);
            if(account != null)
            {
                ModelState.AddModelError("Account.Username", "Username is already existed!");
                return View(customer);
            }

            customer.Account.RoleName = "customer";

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return LocalRedirect("/Account/Login");
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword()
        {
            //todo
            return LocalRedirect("/Account/Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return LocalRedirect("/");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var cus = _context.Customers.Where(c => c.Email.Equals(email)).FirstOrDefault();
            if (cus == null)
            {
                ViewBag.Message = "Invalid Email!";
                return View(email);
            }

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential("20521627@gm.uit.edu.vn", "Kn050320021627");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("20521627@gm.uit.edu.vn", "UIT Hotel");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "UIT Hotel - Reset password";
            mail.Body = "<h4>Click button below to reset your password!</h4></br><form method=\"post\" action=\"https://localhost:7116/Account/ResetPassword/\"><input type=\"hidden\" name=\"AccId\" value=\"" + cus.AccId.ToString() + "\"><button type=\"submit\">Reset</button></form>";
            mail.IsBodyHtml = true;
            smtpClient.Send(mail);
            return LocalRedirect("/Account/SendEmailSuccess");
        }

        public IActionResult SendEmailSuccess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(int AccId)
        {
            var account = _context.Accounts.Where(a => a.AccId == AccId).FirstOrDefault();

            if (account == null) {
                return LocalRedirect("/Account/Login");
            }

            return View(account);
        }

        [HttpPost]
        public IActionResult ConfirmedResetPassword(int AccId, string password, string repassword)
        {
            if(password != repassword)
            {
                ViewBag.Message = "Repassword incorrect!";
                return View();
            }

            var account = _context.Accounts.Where(a => a.AccId == AccId).FirstOrDefault();

            if (account == null)
            {
                return LocalRedirect("/Account/Login");
            }

            account.Password = password;
            _context.SaveChanges();

            return LocalRedirect("/Account/Login");
        }
    }
}
