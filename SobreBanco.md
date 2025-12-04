Sobre o Banco de Dados(Pontos importantes):
Para fazer as conexões, eu decidi usar principalmente procedures e views.
Podemos dizer que a procedure é uma espécie de função armazenada no banco de dados, utilizei ela para ser a referência na conexão, ao invés de usar insert diretamente ou outros tipos de códigos.
Uma view é uma tabela "virtual", referente a tabelas específicas, nesse caso a view foi utilizada com inner join e é apenas utilizada com select(para as consultas nas conexões).
