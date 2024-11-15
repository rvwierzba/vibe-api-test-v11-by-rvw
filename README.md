# Vibe API - Gestão e Filtragem de Placemarks em Arquivos KML

## 📄 Descrição

Bem-vindo ao **Vibe API**, uma API desenvolvida para gerenciar, filtrar e exportar placemarks de arquivos KML. Este projeto foi criado para resolver o desafio proposto por Rafael V. Wierzba, implementando uma solução robusta e eficiente para manipulação de dados geográficos em formato KML.

## 🚀 Funcionalidades

- **Carregamento de Arquivos KML:** Importa e processa arquivos KML contendo múltiplos placemarks.
- **Filtragem Avançada:** Permite filtrar placemarks com base em critérios como Cliente, Situação, Bairro, Referência e Rua/Cruzamento.
- **Validação de Filtros:** Garante que os filtros aplicados são válidos, evitando inconsistências nos resultados.
- **Exportação de KML Filtrado:** Gera novos arquivos KML contendo apenas os placemarks que atendem aos critérios de filtro.
- **Obtenção de Filtros Disponíveis:** Fornece listas de valores disponíveis para cada critério de filtro, facilitando a seleção adequada.
- **Documentação Interativa:** Utiliza Swagger para documentar e testar os endpoints da API de forma interativa.

## 🛠 Tecnologias Utilizadas

- **.NET 8.0**
- **C#**
- **ASP.NET Core Web API**
- **SharpKml.Core:** Biblioteca para manipulação de arquivos KML.
- **Swashbuckle.AspNetCore:** Ferramenta para documentação interativa da API (Swagger).
- **Microsoft.AspNetCore.OpenApi**

## 🏗 Estrutura do Projeto

vibe-api-test-v11-by-rvw/ ├── Controllers/ │ └── PlacemarksController.cs ├── Models/ │ ├── AvailableFilters.cs │ ├── PlacemarkFilter.cs │ ├── PlacemarkModel.cs │ └── ValidationResult.cs ├── Services/ │ ├── FilterValidator.cs │ ├── KmlService.cs │ └── Interfaces/ │ └── IFilterValidator.cs ├── Data/ │ └── DIRECIONADORES1.kml ├── Program.cs └── vibe-api-test-v11-by-rvw.csproj


## 📝 Como Utilizar

### 📋 Requisitos

- **.NET SDK 8.0** instalado. [Download .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Editor de Código:** Visual Studio 2022, Visual Studio Code ou qualquer IDE de sua preferência.

### 🔧 Instalação

1. **Clone o Repositório:**

   ```bash
   git clone https://github.com/seu-usuario/vibe-api-test-v11-by-rvw.git

#### Navegue até o Diretório do Projeto:

cd vibe-api-test-v11-by-rvw

#### Restaure as Dependências:

dotnet restore

### ⚙️ Configuração

   #### Arquivo KML: 
      Coloque o arquivo KML que deseja processar na pasta Data/ com o nome DIRECIONADORES1.kml


### ▶️ Executando a API

   #### Build e Execute o Projeto:

    dotnet run

   #### Acesse a Documentação Interativa (Swagger):
        Abra o navegador e navegue até https://localhost:5001/swagger (ou a URL indicada no terminal).
        Explore e teste os endpoints diretamente pela interface do Swagger.

### 🛤 Endpoints da API
1. Obter Todos os Placemarks

    URL: /api/placemarks
    Método HTTP: GET
    Descrição: Retorna uma lista completa de todos os placemarks presentes no arquivo KML.

Exemplo de Requisição:

GET /api/placemarks HTTP/1.1
Host: localhost:5001

Exemplo de Resposta:

[
  {
    "name": "Placemark 1",
    "description": "Descrição do Placemark 1",
    "latitude": -23.550520,
    "longitude": -46.633308,
    "cliente": "Cliente A",
    "situacao": "Ativo",
    "bairro": "Centro",
    "referencia": "Próximo ao Parque",
    "ruaCruzamento": "Rua A com Rua B"
  },
  {
    "name": "Placemark 2",
    "description": "Descrição do Placemark 2",
    "latitude": -23.551000,
    "longitude": -46.634000,
    "cliente": "Cliente B",
    "situacao": "Inativo",
    "bairro": "Jardins",
    "referencia": "Próximo ao Shopping",
    "ruaCruzamento": "Rua C com Rua D"
  }
]

2. Obter Filtros Disponíveis

    URL: /api/placemarks/filters
    Método HTTP: GET
    Descrição: Retorna os valores disponíveis para os critérios de filtro (Cliente, Situação, Bairro).

Exemplo de Requisição:

GET /api/placemarks/filters HTTP/1.1
Host: localhost:5001

Exemplo de Resposta:

{
  "cliente": ["Cliente A", "Cliente B", "Cliente C"],
  "situacao": ["Ativo", "Inativo", "Em Manutenção"],
  "bairro": ["Centro", "Jardins", "Pinheiros"]
}

3. Filtrar Placemarks

    URL: /api/placemarks/filter
    Método HTTP: POST
    Descrição: Retorna uma lista de placemarks que atendem aos critérios de filtro fornecidos.

Exemplo de Requisição:

POST /api/placemarks/filter HTTP/1.1
Host: localhost:5001
Content-Type: application/json

{
  "cliente": ["Cliente A", "Cliente B"],
  "situacao": ["Ativo"],
  "bairro": ["Centro"]
}

Exemplo de Resposta:

[
  {
    "name": "Placemark 1",
    "description": "Descrição do Placemark 1",
    "latitude": -23.550520,
    "longitude": -46.633308,
    "cliente": "Cliente A",
    "situacao": "Ativo",
    "bairro": "Centro",
    "referencia": "Próximo ao Parque",
    "ruaCruzamento": "Rua A com Rua B"
  }
]

4. Exportar Placemarks Filtrados para KML

    URL: /api/placemarks/export
    Método HTTP: POST
    Descrição: Exporta os placemarks filtrados para um arquivo KML e retorna o conteúdo do arquivo em formato binário.

Exemplo de Requisição:

POST /api/placemarks/export HTTP/1.1
Host: localhost:5001
Content-Type: application/json

{
  "cliente": ["Cliente A", "Cliente B"],
  "situacao": ["Ativo"],
  "bairro": ["Centro"]
}

Exemplo de Resposta:

    Conteúdo: Arquivo KML em formato binário para download.

#### 🧩 Como Abordei e Resolvi o Desafio

### 💡 1. Entendimento do Problema
  Iniciei o projeto compreendendo a necessidade de manipular arquivos KML, que contêm informações geográficas representadas por placemarks. O desafio envolvia não apenas a leitura e interpretação desses arquivos, mas também a implementação de funcionalidades de filtragem avançada e validação dos filtros aplicados.
### 🔨 2. Escolha das Tecnologias
  Optei por utilizar ASP.NET Core Web API devido à sua robustez e flexibilidade para construir APIs RESTful. A biblioteca SharpKml.Core foi selecionada para facilitar a manipulação de arquivos KML, permitindo a leitura, processamento e criação de novos arquivos com facilidade.

### 🏗 3. Estruturação do Projeto
Organizei o projeto seguindo boas práticas de desenvolvimento, segmentando funcionalidades em diferentes camadas:
    Controllers: Gerenciam as requisições HTTP e respondem aos clientes.
    Models: Contêm as definições de dados e estruturas utilizadas pela API.
    Services: Implementam a lógica de negócios, como processamento de KML e validação de filtros.
    Interfaces: Definem contratos para os serviços, promovendo desacoplamento e facilitando testes.

### 🧪 4. Implementação das Funcionalidades
    Carregamento de KML: Utilizou a SharpKml.Engine.Parser para ler e interpretar o arquivo KML, extraindo informações relevantes dos placemarks.
    Filtragem de Dados: Implementou métodos para filtrar os placemarks com base em critérios específicos, utilizando LINQ para consultas eficientes.
    Validação de Filtros: Criou o FilterValidator para garantir que os filtros aplicados são válidos, evitando inconsistências nos resultados filtrados.
    Exportação de KML: Desenvolveu a funcionalidade de exportar os placemarks filtrados para um novo arquivo KML, mantendo a estrutura e os dados personalizados.      
