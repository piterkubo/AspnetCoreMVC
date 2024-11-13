1.Esse projeto foi desenvolvido com a arquitetura MVC (Model/View/Controller). Ele permite fazer o CRUD de usuários para login, além de contar com uma área de gerenciamento para o administrador.

1.Uma das funcionalidades mais interessantes que implementei foi uma solução para facilitar o acesso de clientes já cadastrados. Ao clicar em "É Cliente, mas não tem acesso", o sistema consulta o CPF/CNPJ e o e-mail do cliente em outro banco de dados. Caso os dados estejam cadastrados, um método coleta algumas informações e as salva no banco de dados principal.

1.Depois, o cliente recebe um e-mail com uma senha provisória criptografada. Ao fazer o primeiro login, ele precisará alterar essa senha. Essa medida foi necessária porque o sistema de login estava inicialmente preparado apenas para novos clientes, mas queríamos facilitar o acesso para os clientes que já tinham um vínculo com a empresa.
