# Solução

Para a resolução do desafio proposto, foi criado aplicações responsáveis pelo cadastro, armazenamento das informações e realização de operações de câmbio,  bem como a e emissão de relatórios constando as mesmas.

Todos os cadastros, operações e obtenção de relatórios se dão pelo armazenamento e obtenção dos valores armazenados num Banco de dados simulado pelo conteúdo de arquivo JSON.
As aplicações foram desenvolvidas em C-Sharp.
Foi-se utilizado a ferramenta Postman, objetivando testar os serviços de WEB APIs por meio do envio de requisições HTTP, possibilitando assim  avaliar as respostas das requisições.

# Execução da solução

Para efetuar os testes, deve-se executar o sistema no Visual Studio Code, e posteriormente aplicar as ações no Postman.

### Inserir Cliente
```json
url = http://localhost:5001/cliente
method = POST
body = 
{
    "Nome": "Cliente"
}
```

### Consultar todos Clientes
```json
url = https://localhost:5001/cliente
method = GET
header ={}
Body ={}
```

### Inserir Moeda
```json
url = http://localhost:5001/cliente
method = POST
body = 
{
    "Descricao": "Euro",
    "Valor": 6,54
}
```

### Inserir operações
```json
url = https://localhost:5001/operacao
method = POST
body = 
{
    "valorOriginal": 300,
    "dataOperacao": "2021-05-01",
    "moedaOrigemId": 2,
    "moedaDestinoId": 1,
    "clienteId": 1
}
```

### Obter todas operações
```json
url = https://localhost:5001/operacao 
method = POST
header ={}
Body ={}
```

### Consulta por intervalo de data e cliente
```json
url = https://localhost:5001/operacao/2021-05-06T00:00:00/2021-05-12T00:00:00/1 
method = POST
header ={}
Body ={}
```
