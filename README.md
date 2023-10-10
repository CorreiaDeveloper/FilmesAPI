# Filmes CRUD API

Esta é uma API RESTful simples para gerenciar uma coleção de filmes. A API oferece operações CRUD básicas (Create, Read, Update, Delete) para recursos de filmes. Ela foi projetada para ser amigável, flexível e fácil de integrar em várias aplicações.

## Recursos

- **Criar:** Adicionar novos filmes à coleção.
- **Ler:** Recuperar informações sobre todos os filmes ou um filme específico.
- **Atualizar:** Modificar detalhes de filmes existentes.
- **Excluir:** Remover um filme da coleção.

## Início Rápido

### Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/CorreiaDeveloper/FilmesAPI.git
   cd movie-crud-api

2. Instale as dependências:
npm install

3. Configure a conexão do banco de dados em config.js com suas credenciais.

4. Execute a aplicação:
npm start

A API estará acessível em http://localhost:3000 por padrão.

Uso
A API pode ser testada usando ferramentas como Postman ou integrada à sua aplicação.

Endpoints
Adicionar Filme: POST /filmes
Listar Filmes: GET /filmes
Listar Filme por ID: GET /filmes/:id
Atualizar Filme: PUT /filmes/:id
Atualizar Dados Parciais do Filme: PATCH /filmes/:id
Deletar Filme: DELETE /filmes/:id
Exemplos de Requisições e Respostas
1. Adicionar Filme (POST):
{
  "title": "Inception",
  "director": "Christopher Nolan",
  "year": 2010
}
2. Listar Todos os Filmes (GET):
[
  {
    "id": 1,
    "title": "Inception",
    "director": "Christopher Nolan",
    "year": 2010
  },
  {
    "id": 2,
    "title": "The Shawshank Redemption",
    "director": "Frank Darabont",
    "year": 1994
  }
]
3. Atualizar Filme (PUT):
{
  "director": "Christopher Nolan",
  "year": 2010
}
Tratamento de Erros
HTTP 404 Not Found: Se o ID do filme solicitado não existir.
HTTP 400 Bad Request: Se a carga útil da solicitação estiver malformada.
HTTP 500 Internal Server Error: Para erros inesperados.
Informações Adicionais
Documentação Adicional: Consulte o código-fonte para obter informações detalhadas sobre cada endpoint, formato de dados esperado e exemplos de uso.
Licença
Este projeto está licenciado sob a Licença MIT - consulte o arquivo LICENSE para obter detalhes.







