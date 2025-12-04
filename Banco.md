--  drop database dbJoalheriaDuquesa;
create database dbJoalheriaDuquesa;
use dbJoalheriaDuquesa;

create table tbProduto(
IdProduto INT PRIMARY KEY,
NomeProduto VARCHAR(200) NOT NULL,
TipPedra VARCHAR(50),
Material VARCHAR(200) NOT NULL,
Marca VARCHAR(100),
Preco DECIMAL(10,2) NOT NULL,
Categoria VARCHAR(100) NOT NULL,
Tamanho VARCHAR(50) NOT NULL,
Peso DECIMAL(10,2) NOT NULL
);

create table tbEstoque(
IdProduto int primary key,
qtd int not null,
statusEstoque varchar(200) not null,
ultimaAtualizacao date not null,
foreign key(IdProduto) references tbProduto(IdProduto)
);
create table tbPais(
IdPais int primary key AUTO_INCREMENT,
Pais varchar(50) not null
);
create table tbEstado(
IdEstado int primary key AUTO_INCREMENT,
Estado varchar(50) not null
);
create table tbCidade(
IdCidade int primary key AUTO_INCREMENT,
Cidade varchar(50) not null
);
create table tbBairro(
IdBairro int primary key AUTO_INCREMENT,
Bairro varchar(50) not null
);
-- Criando tbEndereco
create table tbEndereco(
CEP varchar(9) not null primary key,
Logradouro varchar(200) not null,
Numero smallint not null, 
IdPais int not null,
IdEstado int not null,
IdCidade int not null,
IdBairro int not null,

foreign key (IdPais) references tbPais(IdPais),
foreign key (IdEstado) references tbEstado(IdEstado),
foreign key (IdCidade) references tbCidade(IdCidade),
foreign key (IdBairro) references tbBairro(IdBairro)
);
create table tbCliente(
IdCliente INT PRIMARY KEY,
NomeCliente VARCHAR (200) NOT NULL,
DataCadastro DATE NOT NULL,
CepCli varchar(9) not null, -- talvez seja melhor mudar para 10 pois alguns países utilizam CEP de 9 digitos + (-)
foreign key(CepCli) references tbEndereco(CEP)
);
create table tbClientePF(
CPF bigint primary key,
Id int not null,
foreign key(Id) references tbCliente(IdCliente)
);
create table tbClientePJ(
CNPJ bigint primary key,
Id int not null,
foreign key(Id) references tbCliente(IdCliente)
);

create table tbFuncionario(
IdFunc INT primary key,
Cargo VARCHAR(100) NOT NULL,
CPFfunc VARCHAR(11) NOT NULL,
NomeFunc VARCHAR(200) NOT NULL,
Salario DECIMAL(10,2) NOT NULL,
DataAdmissao DATE NOT NULL,
SenhaSistema varchar(200) not null,
CepFuncionario varchar(9) not null,
foreign key (CepFuncionario) references tbEndereco(CEP)
); 

create table tbEmail(
IdEmail int primary key AUTO_INCREMENT,
Email varchar(250) not null,
IdFunc int not null,
foreign key (IdFunc) references tbFuncionario(IdFunc)
);

create table tbVenda(
IdVenda INT PRIMARY KEY,
IdCliente int not null,
foreign key (IdCliente) references tbCliente (IdCliente),
DataPedido DATE NOT NULL,
StatusPedido VARCHAR(150) NOT NULL,
ValorTotal DECIMAL(10,2) NOT NULL
); 

create table tbNotaFiscal(
IdNota INT PRIMARY KEY,
IdVenda INT NOT NULL,
NumNota VARCHAR(50) UNIQUE NOT NULL,
EmissaoData DATE NOT NULL,
ValorNota DECIMAL(10,2) NOT NULL,
FOREIGN KEY (IdVenda) REFERENCES tbVenda (IdVenda)
);

create table tbItensPedido(
Quantidade INT NOT NULL,
IdProduto INT,
IdVenda INT,
PRIMARY KEY (IdProduto, IdVenda),
foreign key (IdProduto) references tbProduto(IdProduto),
foreign key (IdVenda) references tbVenda(IdVenda)
);


-- tabela do fornecedor
create table tbFornecedor(
IdFornecedor int primary key,
NomeEmpresa varchar(200) not null,
CNPJ varchar(14) not null
);

-- tabela de ligação entre Produto e Fornecedor
create table tbFornecedorProduto(
IdFornecedor int,
IdProduto int primary key,
foreign key (IdFornecedor) references tbFornecedor(IdFornecedor),
foreign key (IdProduto) references tbProduto(IdProduto)
);



create table tbTelefone(
Idtel int primary key AUTO_INCREMENT,
IdFunc int ,
IdFornecedor int,
IdCli int ,
Numero varchar(15) not null,
foreign key (IdFunc) references tbFuncionario(IdFunc),
foreign key (IdFornecedor) references tbFornecedor(IdFornecedor),
foreign key (IdCli) references tbCliente(IdCliente),
CHECK(
(IdFunc is not null and IdFornecedor is null and IdCli is null) or
(IdFornecedor is not null and IdFunc is null and IdCli is null) or
(IdCli is not null and IdFunc is null and IdFornecedor is null)
)
);



/*
Mudanças feitas em Outubro
Ao invés de fazer um atributo tipo, é possível fazer 3 ids para cada um que são limitados por check
Porém também é possível fazer uma herança adicional nas tabelas, onde existiria uma tabelaPessoa, que estaria ligada a Cliente, Fornecedor e Funcionário.
 Dessa forma TbPessoa que se ligaria com telefone, porém mudaria totalmente a lógica do código. Por enquanto só fiz desse primeiro jeito
*/
/*
Mudanças feitas no momento:
1) Normalização de Tabela do Endereço, gerando mais tabelas(Pais, Estado, Cidade e Bairro)
2) Já citado acima, em tabelas que podem ter diferentes donos, como telefone que pode ser tanto cliente, como funcionario e fornecedor, eu coloquei um id para cada,
 e para nao dar erro em inserts usei check, mas há outras maneiras de fazer isso
 
Observações feitas:
1)Necessidade de mais PKs com Auto_Increment- pois não é necessário especificar muitos IDs de diversas tabelas, apenas das mais importantes: Cliente, Funcionario, Fornecedor, etc..
2)Possível necessidade de mudança de certas cardinalidades-Algumas tabelas não englobam muito sentido na cardinalidade ao meu ver, como: Endereço com Funcionario e Cliente,
pois apenas o endereço só pode ser de um Cliente ou Funcionario.
3)Pouco Possível interesse de colocar mais alguns atributos - Isso é bem pouco, mas talvez algumas tabelas queiram alguns atributos mais específicos, como não é projeto de tcc e tem que fazer
infinitas coisas não vou me preocupar com isso por enquanto. Estou com mais vontade de fazer a parte do C# e interações com o Razor-C#
4)Senha em tabelas de Usuario-Seria melhor colocar um atributo específico para a senha, mas já já verei isso
*/


-- procedure para cadastrar Fornecedor, seu endereço e seu telefone (Apenas testes, há muitas coisas que ainda vou mudar)
/*
Aviso:
Essa procedure ainda não é ideal usar no asp, pois muitas coisas não estão do jeito de uso 100% correto, seria necessário fazer mais linhas, mas farei em breve
*/


DELIMITER $$
     create procedure spInsertFuncGeral(
     vIdFunc int,
     vCargo varchar(100),
     vCPFfunc varchar(11),
     vNomeFunc varchar(200),
     vSalario decimal(10,2),
     vSenhaSistema varchar(200),
     vDataAdmissao date,
     vEmail varchar(250),
     vLogradouro varchar(200),
     vNumero smallint,
     vCEP varchar(9),
     vNumeroTel varchar(15),
     vPais varchar(50),
     vEstado varchar(50),
     vCidade varchar(50),
     vBairro varchar(50)
     )
     begin
        declare vIdPais int;
        declare vIdEstado int;
        declare vIdCidade int;
        declare vIdBairro int;
                    if not exists(select 1 from tbEndereco where CEP = vCEP)
                    then
                          if not exists(select 1 from tbFuncionario where IdFunc = vIdFunc)
                          then
                               if not exists(select 1 from tbPais where Pais = vPais)
                               then
                               insert into tbPais (Pais) values (vPais);
                               end if;
                               if not exists(select 1 from tbEstado where Estado = vEstado)
                               then
                               insert into tbEstado (Estado) values (vEstado);
                               end if;
                               if not exists(select 1 from tbCidade where Cidade = vCidade)
                               then
                               insert into tbCidade (Cidade) values (vCidade);
                               end if;
                               if not exists(select 1 from tbBairro where Bairro = vBairro)
                               then
                               insert into tbBairro (Bairro) values (vBairro);
                               end if;
                               select IdPais into vIdPais from tbPais where (Pais = vPais) limit 1;
                               select IdEstado into vIdEstado from tbEstado where(Estado = vEstado) limit 1;
                               select IdCidade into vIdCidade from tbCidade where(Cidade = vCidade) limit 1;
                               select IdBairro into vIdBairro from tbBairro where(Bairro = vBairro) limit 1;
                               insert into tbEndereco (Logradouro, Numero, IdPais, IdEstado, IdBairro, IdCidade, CEP) values (vLogradouro, vNumero, vIdPais, vIdEstado, vIdBairro, vIdCidade, vCEP);
                               insert into tbFuncionario (IdFunc, Cargo, CPFfunc, NomeFunc, Salario, SenhaSistema, DataAdmissao, CepFuncionario) values(vIdFunc, vCargo, vCPFfunc, vNomeFunc, vSalario, vSenhaSistema, vDataAdmissao, vCEP);
                               insert into tbEmail (Email, IdFunc) values (vEmail, vIdFunc);
                               insert into tbTelefone (IdFunc, Numero) values(vIdFunc, vNumeroTel);
                          end if;
                    end if;
     end
$$
-- drop procedure spInsertFuncGeral;
call spInsertFuncGeral(
    1,'Analista de Sistemas','12345678901','João da Silva',4500.75, 'Kj234fjg','2025-10-02','joao.silva@email.com','Rua das Flores',123,'12345-678','11987654321','Brasil','São Paulo','São Paulo','Centro'                             
);


select * from tbEndereco;
select * from tbFuncionario;
select * from tbEmail;
select * from tbTelefone;

select 
func.NomeFunc,
func.Cargo,
e.CEP,
e.Logradouro,
p.Pais,
es.Estado,
b.Bairro,
c.Cidade
from tbFuncionario func
inner join tbEndereco e
on func.CepFuncionario = e.CEP
inner join tbPais p
on e.IdPais = p.IdPais
inner join tbEstado es
on e.IdEstado = es.IdEstado
inner join tbCidade c
on e.IdCidade = c.IdCidade
inner join tbBairro b
on e.IdBairro = b.IdBairro
order by func.NomeFunc ASC
;

-- agora vou criar uma procedure para cadastrar o cliente

DELIMITER $$
  create procedure sp_insertClientPF(
  vIdCliente int,
  vNomeCliente varchar(200),
  vCEP varchar(9),
  vCPF bigint,
  vLogradouro varchar(200),
  vNumero smallint,
  vPais varchar(50),
  vEstado varchar(50),
  vCidade varchar(50),
  vBairro varchar(50),
  vNumeroTel varchar(15)
  )
  begin
      declare vidBairro int;
      declare vidCidade int;
      declare vidEstado int;
      declare vidPais int;
      if not exists(select 1 from tbCliente where IdCliente = vIdCliente)
      then
            if not exists(select 1 from tbTelefone where Numero = vNumeroTel)
            then
                  if not exists(select 1 from tbEndereco where CEP = vCEP)
                  then
                         if not exists(select 1 from tbBairro where Bairro = vBairro)
                         then 
                         insert into tbBairro (Bairro) values (vBairro);
                         end if;
                         if not exists(select 1 from tbCidade where Cidade= vCidade)
                         then
                         insert into tbCidade (Cidade) values(vCidade);
                         end if;
                         if not exists(select 1 from tbEstado where Estado = vEstado)
                         then
                         insert into tbEstado (Estado) values(vEstado);
                         end if;
                         if not exists(select 1 from tbPais where Pais = vPais)
                         then
                         insert into tbPais (Pais) values (vPais);
                         end if;
                         select IdBairro into vidBairro from tbBairro where Bairro = vBairro;
                         select IdCidade into vidCidade from tbCidade where Cidade = vCidade;
                         select IdEstado into vidEstado from tbEstado where Estado = vEstado;
                         select IdPais into vidPais from tbPais where Pais = vPais;
                         insert into tbEndereco (CEP, Logradouro, Numero, IdPais, IdEstado, IdCidade, IdBairro) values (vCEP, vLogradouro, vNumero, vidPais, vidEstado, vidCidade, vidBairro);
                  end if;
                  insert into tbCliente(IdCliente, NomeCliente, DataCadastro, CepCli) values(vIdCliente, vNomeCliente, CURRENT_DATE, vCEP);
                  insert into tbClientePF(CPF, Id) values(vCPF, vIdCliente);
                  insert into tbTelefone(IdCli, Numero) values(vIdCliente, vNumeroTel);
            end if;
      end if;
  end
$$

call sp_insertClientPF(1,'Alexandra Winters','10001',5432101,'123 Maple Street', 42,'Estados Unidos','Nova York','Nova York','Manhattan','15551234567');
select * from tbEndereco;
select * from tbTelefone;
select * from tbCliente;

-- procedure cliente PJ

DELIMITER $$
  create procedure sp_insertClientPJ(
  vIdCliente int,
  vNomeCliente varchar(200),
  vCEP varchar(9),
  vCNPJ bigint,
  vLogradouro varchar(200),
  vNumero smallint,
  vPais varchar(50),
  vEstado varchar(50),
  vCidade varchar(50),
  vBairro varchar(50),
  vNumeroTel varchar(15)
  )
  begin
      declare vidBairro int;
      declare vidCidade int;
      declare vidEstado int;
      declare vidPais int;
      if not exists(select 1 from tbCliente where IdCliente = vIdCliente)
      then
            if not exists(select 1 from tbTelefone where Numero = vNumeroTel)
            then
                  if not exists(select 1 from tbEndereco where CEP = vCEP)
                  then
                         if not exists(select 1 from tbBairro where Bairro = vBairro)
                         then 
                         insert into tbBairro (Bairro) values (vBairro);
                         end if;
                         if not exists(select 1 from tbCidade where Cidade= vCidade)
                         then
                         insert into tbCidade (Cidade) values(vCidade);
                         end if;
                         if not exists(select 1 from tbEstado where Estado = vEstado)
                         then
                         insert into tbEstado (Estado) values(vEstado);
                         end if;
                         if not exists(select 1 from tbPais where Pais = vPais)
                         then
                         insert into tbPais (Pais) values (vPais);
                         end if;
                         select IdBairro into vidBairro from tbBairro where Bairro = vBairro;
                         select IdCidade into vidCidade from tbCidade where Cidade = vCidade;
                         select IdEstado into vidEstado from tbEstado where Estado = vEstado;
                         select IdPais into vidPais from tbPais where Pais = vPais;
                         insert into tbEndereco (CEP, Logradouro, Numero, IdPais, IdEstado, IdCidade, IdBairro) values (vCEP, vLogradouro, vNumero, vidPais, vidEstado, vidCidade, vidBairro);
                  end if;
                  insert into tbCliente(IdCliente, NomeCliente, DataCadastro, CepCli) values(vIdCliente, vNomeCliente, CURRENT_DATE, vCEP);
                  insert into tbClientePJ(CNPJ, Id) values(vCNPJ, vIdCliente);
                  insert into tbTelefone(IdCli, Numero) values(vIdCliente, vNumeroTel);
            end if;
      end if;
  end
$$
select * from tbCliente;
select * from tbClientePJ;
call sp_insertClientPJ(3, "Lusitânia Tech S.A", "1050064", 50988231000123, "Avenida da Liberdade", 245,  "Portugal", "Lisboa", "Lisboa", "Centro", "351912345678" );


-- Criando procedure para registro do Fornecedor(Apenas disponível dentro do MySql)
DELIMITER $$
   create procedure sp_insertFornecedor(
   vIdFornecedor int,
   vFornecedor varchar(200),
   vCNPJ varchar(14),
   vNumTel varchar(15)
   )
   begin
      if not exists(select 1 from tbFornecedor where idFornecedor = vIdFornecedor)
      then
          insert into tbFornecedor (IdFornecedor, NomeEmpresa, CNPJ) values(vIdFornecedor, vFornecedor, vCNPJ);
          if not exists(select 1 from tbTelefone where Numero = vNumTel)
          then
               insert into tbTelefone (IdFornecedor, Numero) values(vIdFornecedor, vNumTel);
          end if;
      end if;
   
   end
$$

call sp_insertFornecedor(1, "Marcenaria dos Andes", "12345678901234", "11778899543210");
call sp_insertFornecedor(2, "Artesanais Duquesa", "12345678911223", "99999998888888");
select * from tbFornecedor;
select * from tbTelefone;

-- Criando procedure para registro de produto
DELIMITER $$
    create procedure sp_insertProduto
    (
     vIdProduto int,
     vNomeProd varchar(200),
     vTipPedra varchar(50),
     vMaterial varchar(200),
     vMarca varchar(100),
     vPreco decimal(10,2),
     vCategoria varchar(100),
     vtamanho varchar(50),
     vpeso decimal(10,2),
     vqtd int,
     vstatus varchar(200),
     vFornecedor varchar(200)
    )
    begin
    declare vIdFornecedor int;
       if exists(select 1 from tbFornecedor where NomeEmpresa = vFornecedor)
       then
          if not exists(select 1 from tbProduto where IdProduto = vIdProduto)
          then
          insert into tbProduto (IdProduto, NomeProduto, TipPedra, Material, Marca, Preco, Categoria, Tamanho, Peso) values(vIdProduto, vNomeProd, vTipPedra, vMaterial, vMarca, vPreco, vCategoria, vTamanho, vPeso);
          insert into tbEstoque (IdProduto, qtd, statusEstoque, ultimaAtualizacao) values(vIdProduto, vqtd, vstatus, CURRENT_DATE());
          select IdFornecedor into vIdFornecedor from tbFornecedor where (NomeEmpresa = vFornecedor);
          insert into tbFornecedorProduto (IdFornecedor, IdProduto) values(vIdFornecedor, vIdProduto);
          end if;
       end if;
       
    end
$$
call sp_InsertProduto(1, "Anel solitário Elegance", "Diamante", "Ouro 18k", "Corte d'our", 3000.55, "anel", "16", 4.75, 12, "Disponível", "Marcenaria dos Andes" );
call sp_InsertProduto(2, "Brinco Reinado Verde", "Esmeralda", "Prata", "Prinz des Landes", 6800.99, "brinco", "10", 0.75, 40, "Disponível", "Marcenaria dos Andes");
call sp_InsertProduto(3, "Pulseira Rubi Astezca", "Rubi", "Prata", "Sucre Utilitárias", 8999.99, "pulseira", "30", 3.50, 20, "Disponível", "Marcenaria dos Andes");
select * from tbProduto;
select * from tbEstoque;
select * from tbFornecedorProduto;


-- atualizar produto
DELIMITER $$
   create procedure sp_updateProduto(
     vIdProduto int,
     vNomeProd varchar(200),
     vTipPedra varchar(50),
     vMaterial varchar(200),
     vMarca varchar(100),
     vPreco decimal(10,2),
     vCategoria varchar(100),
     vtamanho varchar(50),
     vpeso decimal(10,2),
     vqtd int,
     vstatus varchar(200),
     vFornecedor varchar(200)
      )
      begin
          declare vIdFornecedor int;
          declare vFornecedorOld varchar(200);
          declare vIdFornecedorNew int;
          select IdFornecedor into vIdFornecedor from tbFornecedorProduto where(IdProduto = vIdProduto);
          select NomeEmpresa into vFornecedorOld from tbFornecedor where(IdFornecedor = vIdFornecedor);
          
         if exists(select 1 from tbFornecedor where(NomeEmpresa = vFornecedor))
         then
              if exists(select 1 from tbProduto where(IdProduto = vIdProduto))
              then
              update tbProduto set 
              NomeProduto = vNomeProd, 
              TipPedra = vTipPedra,
              Material = vMaterial,
              Preco = vPreco,
              Categoria = vCategoria,
              Tamanho = vTamanho,
              Peso = vpeso
              where (IdProduto = vIdProduto);
              
              update tbEstoque set 
              qtd = vqtd,
              statusEstoque = vstatus,
              ultimaAtualizacao = CURRENT_DATE()
              where (IdProduto = vIdProduto);
              end if;
              if(not (vFornecedor = vFornecedorOld))
              then
              select IdFornecedor into vIdFornecedorNew from tbFornecedor where(NomeEmpresa = vFornecedor);
              update tbFornecedorProduto set
              IdFornecedor = vIdFornecedorNew
              where(IdProduto = vIdProduto);
              end if;
         end if;
      end

$$
call sp_updateProduto(1, 'Anel Solitaire', 'Ouro', 'Ouro 18k', "Corte d'our", 4567.5, "anel", "16", 4.75, 12, "Disponível", "Artesanais Duquesa");
select * from tbProduto;
select * from tbEstoque;
select * from tbFornecedorProduto;

-- criando procedure para deletar um produto(pelo Id)
DELIMITER $$ 
   create procedure sp_deleteProduto(
   vIdProduto int
   )
   begin
   declare vIdFornecedor int;
      if exists(select 1 from tbProduto where (IdProduto = vIdProduto))
      then
      select IdFornecedor into vIdFornecedor from tbFornecedorProduto where(IdProduto = vIdProduto);
      delete from tbEstoque where(IdProduto = vIdProduto);
      delete from tbFornecedorProduto where(IdProduto = vIdProduto and IdFornecedor = vIdFornecedor);
      delete from tbProduto where(IdProduto = vIdProduto);
      end if;
   end
$$
call sp_deleteProduto(1);

select * from tbProduto;
select * from tbEstoque;
select * from tbFornecedorProduto;



-- criando view para usar na conexão depois
create view ProdutoRelate as
select 
prod.IdProduto as IdProduto,
prod.NomeProduto as NomeProduto,
prod.TipPedra as TipoPedra,
prod.Material as Material,
prod.Marca as Marca,
prod.Preco as Preco,
prod.Categoria as Categoria,
prod.Tamanho as Tamanho,
prod.Peso as Peso,
est.Qtd as Qtd,
est.StatusEstoque as StatusEstoque,
est.UltimaAtualizacao as UltimaAtualizacao, -- Essa coluna nunca será referenciada para edição, só poe ser modificado por triggers
forn.NomeEmpresa as NomeEmpresa
from tbProduto prod 
inner join tbEstoque est
on prod.IdProduto = est.IdProduto
inner join tbFornecedorProduto forprod
on prod.IdProduto = forprod.IdProduto
inner join tbFornecedor forn
on forprod.IdFornecedor = forn.IdFornecedor
order by prod.IdProduto ASC;

-- drop view ProdutoRelate;

select * from ProdutoRelate;


-- criando view para dados específicos do funcionário necessários para um login
create view FuncRelate as
select
func.NomeFunc as NomeFuncionario,
func.SenhaSistema as SenhaFuncionario,
em.Email as EmailFuncionario
from tbFuncionario func
inner join tbEmail em
on func.IdFunc = em.IdFunc
order by func.IdFunc ASC;

-- drop view FuncRelate;
select * from FuncRelate;



-- criando view para fazer o login da pessoa física
create view PFRelate as
select
cli.NomeCliente as NomeCliente,
pf.CPF as CPF
from tbCliente cli
inner join tbClientePF pf
on cli.IdCliente = pf.Id
order by cli.IdCliente ASC;
select * from PFRelate;

-- drop view PFRelate;
-- criando view para fazer o login da pessoa jurídica
create view PJRelate as
select
cli.NomeCliente as NomeCliente,
pj.CNPJ as CNPJ
from tbCliente cli
inner join tbClientePJ pj
on cli.IdCliente = pj.Id
order by cli.IdCliente ASC;

-- drop view PJRelate;

select * from PJRelate;



