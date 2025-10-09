using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using projetoBancoDS.Models;
using System.Configuration;
namespace projetoBancoDS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;


        public AccountController(IConfiguration configuration, ILogger<AccountController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();
        }
        
        public IActionResult CadCli()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CadCliPF()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadCliPF(ClientePF clientePF)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "call sp_insertClientPF(@IdCliente, @NomeCliente, @CEP, @CPF, @Logradouro, @Numero, @Pais, @Estado, @Cidade, @Bairro, @NumeroTel)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdCliente", clientePF.IdCliente);
            command.Parameters.AddWithValue("@NomeCliente", clientePF.NomeCliente);
            command.Parameters.AddWithValue("@CEP", clientePF.CEP);
            command.Parameters.AddWithValue("@CPF", clientePF.CPF);
            command.Parameters.AddWithValue("@Logradouro", clientePF.Logradouro);
            command.Parameters.AddWithValue("@Numero", clientePF.Numero);
            command.Parameters.AddWithValue("@Pais", clientePF.Pais);
            command.Parameters.AddWithValue("@Estado", clientePF.Estado);
            command.Parameters.AddWithValue("@Cidade", clientePF.Cidade);
            command.Parameters.AddWithValue("@Bairro", clientePF.Bairro);
            command.Parameters.AddWithValue("@NumeroTel", clientePF.NumeroTel);
            command.ExecuteNonQuery();

            if (!ModelState.IsValid)
            {
                return View(clientePF);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpGet]
        public IActionResult CadCliPJ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadCliPJ(ClientePJ clientePJ)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "call sp_insertClientPJ(@IdCliente, @NomeCliente, @CEP, @CNPJ, @Logradouro, @Numero, @Pais, @Estado, @Cidade, @Bairro, @NumeroTel)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdCliente", clientePJ.IdCliente);
            command.Parameters.AddWithValue("@NomeCliente", clientePJ.NomeCliente);
            command.Parameters.AddWithValue("@CEP", clientePJ.CEP);
            command.Parameters.AddWithValue("@CNPJ", clientePJ.CNPJ);
            command.Parameters.AddWithValue("@Logradouro", clientePJ.Logradouro);
            command.Parameters.AddWithValue("@Numero", clientePJ.Numero);
            command.Parameters.AddWithValue("@Pais", clientePJ.Pais);
            command.Parameters.AddWithValue("@Estado", clientePJ.Estado);
            command.Parameters.AddWithValue("@Cidade", clientePJ.Cidade);
            command.Parameters.AddWithValue("@Bairro", clientePJ.Bairro);
            command.Parameters.AddWithValue("@NumeroTel", clientePJ.NumeroTel);
            command.ExecuteNonQuery();

            if (!ModelState.IsValid)
            {
                return View(clientePJ);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
