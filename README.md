Nome da Dupla:
Nícolas Lima Schinagel do Nascimento
Palloma Tiodosio de Sousa

README principal explicando partes fundamentais no código, suas funções e alguns detalhes especiais:
Esse projeto tem como finalidade fazer uma conexão pelo Banco, permitindo cadastrar, remover e atualizar linhas dentro da aplicação, com base nisso vamos ver os princípios da aplicação:

Views: 
    Ela tem 3 pastas principais, "Account" contém as conexões principais, incluíndo cadastro de usuários e produtos, "Home" tem o index, a página inicial da aplicação, "Shared" é uma pasta que raramente modificamos e contém views compartilhadas entre várias views.
    Há vários arquivos cshtml dentro da pasta Account, vamos explicar a função de cada um:
    AtualizarProduto.cshtml - View para fazer update no produto, só pode ser acessada pela Lista dos produtos.
    CadastroProduto.cshtml - View para fazer cadastro de um produto, sendo acessada diretamente pelo header.
    CadCli.cshtml - View para selecionar o tipo de Cliente para cadastro, sendo acessada pelo Login.cshtml.
    CadCliPF.cshtml - View para cadastrar cliente de pessoa física.
    CadCliPJ.cshtml - View para cadastrar cliente de pessoa jurídica.
    ListaProdutos.cshtml - View que mostra todos os registros de Produtos do Banco, permitindo remover e atualizar.
    Login.cshtml - View que mostra seleção do Login entre Cliente ou Funcionário (Note-se que não há cadastro de funcionário por aqui, pois isso é feito pelo Banco de Dados)
    LogCli.cshtml - View que mostra seleção do Login entre os tipos de clientes, entre PF ou PJ.
    LogCliPF.cshtml - View que mostra o login de Cliente Pessoa Física
    LogCliPJ.cshtml - View que mostra o login de Cliente Pessoa Jurídica
    LogFunc.cshtml - View que mostra o login de Funcionário

Models:
     Feitas para passarem dados de formulários para o banco de dados, contendo várias classes que iremos explicar agora:
       Cliente.cs - Classe contendo Cliente, usando herança para conectar classes ClientePF e ClientePJ com Cliente
       ErrorViewModel.cs - Classe usada para exibir informações sobre erros, criada pelo ASP.NET
       Funcionario.cs - Classe contendo Funcionario, usado apenas para o Login de Adminstrador e não contém a tabela inteira do Banco
       LoginPF.cs - Classe especial para login de Cliente pessoa física, usada apenas para o login de Cliente PF
       LoginPJ.cs - Classe especial para login de Cliente pessoa jurídica, usada apenas para o login de Cliente PJ
       Produto.cs - Classe especial para o cadastro e atualização de Produto, sendo uma junção de tabelas do banco de dados.

Controllers:
       São usadas como um caminho de ligação entre views e models, veja as usadas abaixo:
      AccountController.cs - Controller que contém todas as conexões, referentes a clientes, funcionários e produtos, com diversas "IActionResult".
      HomeController.cs - Controller que contém direcionamento inicial do Index.cshtml e direcionamento para Login.cshtml


Além disso, é necessário informar sobre alguns detalhes importantes:
    Cultura: A cultura define a forma base como o ASP.NET irá definir alguns parâmetros, como números, isso é modificado dependendo da língua, ter a cultura como pt-br pode bagunçar o código com valores de número decimais com " . ", permitindo apenas " , ". Para isso, foi utilizado padrão do inglês, permitindo apenas valores decimais digitados com " . "
    Algumas tabelas, como as de fornecedor e funcionário, só podem ser registradas dentro do Banco, definindo como algo muito específico da Empresa, onde apenas alguns decidem.


Outros arquivos importantes:
     appsettings.json - Define algumas configurações específicas da aplicação, é nela onde a string de conexão com o banco de dados fica:
     "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;database=dbJoalheriaDuquesa;user=root;password=bug1drive"
     }
     É importante saber que os valores devem estar corretos para funcionar, ou seja, o banco de dados (database) precisa existir e a senha(password) precisa estar correta, então mude a senha se for necessário e use o arquivo --Banco.md-- para pegar o código do banco do MySql
     Já os valores do servidor e do usuário podem ser definidos como padrão, sendo respectivamente "localhost" e "root", funcionando para qualquer lugar.
