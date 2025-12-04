using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using projetoBancoDS.Models;
using System.Configuration;
using System.Data;
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
        [HttpGet]
        public IActionResult Cadastroproduto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastroproduto(Produto produto)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "call sp_insertProduto(@IdProduto, @NomeProduto, @TipoProduto, @MaterialProduto, @MarcaProduto, @PrecoProduto, @CategoriaProduto, @TamanhoProduto, @PesoProduto, @QtdProduto, @StatusProduto, @NomeFornecedor)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdProduto", produto.IdProduto);
            command.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            command.Parameters.AddWithValue("@TipoProduto", produto.TipoProduto);
            command.Parameters.AddWithValue("@MaterialProduto", produto.MaterialProduto);
            command.Parameters.AddWithValue("@MarcaProduto", produto.MarcaProduto);
            command.Parameters.AddWithValue("@PrecoProduto", produto.PrecoProduto);
            command.Parameters.AddWithValue("@CategoriaProduto", produto.CategoriaProduto);
            command.Parameters.AddWithValue("@TamanhoProduto", produto.TamanhoProduto);
            command.Parameters.AddWithValue("@PesoProduto", produto.PesoProduto);
            command.Parameters.AddWithValue("@QtdProduto", produto.QtdProduto);
            command.Parameters.AddWithValue("@StatusProduto", produto.StatusProduto);
            command.Parameters.AddWithValue("@NomeFornecedor", produto.NomeFornecedor);
            command.ExecuteNonQuery();

            if (!ModelState.IsValid)
            {
                return View(produto);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Login()
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

        public IActionResult AtualizarProduto(int id)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "select * from ProdutoRelate where IdProduto=@IdProduto";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdProduto", id);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataReader reader;
            Produto produto = new Produto();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                produto.IdProduto = Convert.ToInt32(reader["IdProduto"]);
                produto.NomeProduto = Convert.ToString(reader["NomeProduto"]);
                produto.TipoProduto = Convert.ToString(reader["TipoPedra"]);
                produto.MaterialProduto = Convert.ToString(reader["Material"]);
                produto.MarcaProduto = Convert.ToString(reader["Marca"]);
                produto.PrecoProduto = Convert.ToDecimal(reader["Preco"]);
                produto.CategoriaProduto = Convert.ToString(reader["Categoria"]);
                produto.TamanhoProduto = Convert.ToString(reader["Tamanho"]);
                produto.PesoProduto = Convert.ToDecimal(reader["Peso"]);
                produto.QtdProduto = Convert.ToInt32(reader["Qtd"]);
                produto.StatusProduto = Convert.ToString(reader["StatusEstoque"]);
                produto.NomeFornecedor = Convert.ToString(reader["NomeEmpresa"]);
            }

            return View(produto);
        }
        [HttpPost]
        public IActionResult AtualizarProduto(Produto produto)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "call sp_updateProduto(@IdProduto, @NomeProduto, @TipoProduto, @MaterialProduto, @MarcaProduto, @PrecoProduto, @CategoriaProduto, @TamanhoProduto, @PesoProduto, @QtdProduto, @StatusProduto, @NomeFornecedor)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdProduto", produto.IdProduto);
            command.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            command.Parameters.AddWithValue("@TipoProduto", produto.TipoProduto);
            command.Parameters.AddWithValue("@MaterialProduto", produto.MaterialProduto);
            command.Parameters.AddWithValue("@MarcaProduto", produto.MarcaProduto);
            command.Parameters.AddWithValue("@PrecoProduto", produto.PrecoProduto);
            command.Parameters.AddWithValue("@CategoriaProduto", produto.CategoriaProduto);
            command.Parameters.AddWithValue("@TamanhoProduto", produto.TamanhoProduto);
            command.Parameters.AddWithValue("@PesoProduto", produto.PesoProduto);
            command.Parameters.AddWithValue("@QtdProduto", produto.QtdProduto);
            command.Parameters.AddWithValue("@StatusProduto", produto.StatusProduto);
            command.Parameters.AddWithValue("@NomeFornecedor", produto.NomeFornecedor);
            command.ExecuteNonQuery();

            if (!ModelState.IsValid)
            {
                return View(produto);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult DeletarProduto(int id)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "call sp_deleteProduto(@IdProduto)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdProduto", id);
            command.ExecuteNonQuery();

            if (!ModelState.IsValid)
            {
                return View(id);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ListaProdutos()
        {
            List<Produto> produtos = new List<Produto>();
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                conn.Open();
                string sql = "Select * from ProdutoRelate ";
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                conn.Close();
                foreach (DataRow row in dataTable.Rows)
                {
                    produtos.Add(
                        new Produto
                        {
                            IdProduto = Convert.ToInt32(row["IdProduto"]),
                            NomeProduto = row["NomeProduto"].ToString(),
                            TipoProduto = row["TipoPedra"].ToString(),
                            MaterialProduto = row["Material"].ToString(),
                            MarcaProduto = row["Marca"].ToString(),
                            PrecoProduto = Convert.ToDecimal(row["Preco"]), // converter para tipo float
                            CategoriaProduto = row["Categoria"].ToString(),
                            TamanhoProduto = row["Tamanho"].ToString(),
                            PesoProduto = Convert.ToDecimal(row["Peso"]),
                            QtdProduto = Convert.ToInt32(row["Qtd"]),
                            StatusProduto = row["StatusEstoque"].ToString(),
                            NomeFornecedor = row["NomeEmpresa"].ToString()

                        });


                }
                return View(produtos);
            }
        }

        [HttpGet]
        public IActionResult LogFunc()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogFunc(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Ocorreu erro na validação!";
                return View();
            }

            using (var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                string sql = @"SELECT * FROM FuncRelate where NomeFuncionario = @NomeFuncionario AND EmailFuncionario = @EmailFuncionario AND SenhaFuncionario = @SenhaFuncionario";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NomeFuncionario", funcionario.NomeFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", funcionario.EmailFuncionario);
                cmd.Parameters.AddWithValue("@SenhaFuncionario", funcionario.SenhaFuncionario);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TempData["Success"] = "Login realizado com sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Email ou senha incorretos!";
                    return View();
                }
            }
        }
        public IActionResult LogCli()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LogCliPF()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogCliPF(LoginPF loginPF)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Ocorreu erro na validação!";
                return View();
            }

            using (var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                string sql = @"SELECT * FROM PFRelate where NomeCliente = @NomeCliente AND CPF = @CPF";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NomeCliente", loginPF.NomeCliente);
                cmd.Parameters.AddWithValue("@CPF", loginPF.CPF);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TempData["Success"] = "Login realizado com sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Email ou senha incorretos!";
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult LogCliPJ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogCliPJ(LoginPJ loginPJ)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Ocorreu erro na validação!";
                return View();
            }

            using (var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                string sql = @"SELECT * FROM PJRelate where NomeCliente = @NomeCliente AND CNPJ = @CNPJ";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NomeCliente", loginPJ.NomeCliente);
                cmd.Parameters.AddWithValue("@CNPJ", loginPJ.CNPJ);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TempData["Success"] = "Login realizado com sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Email ou senha incorretos!";
                    return View();
                }
            }
        }
    }
}
