# Projeto >> PROVA HAVIRA GEOJSON

## Visão Geral

Este projeto visa desmonstrar os conhecimentos de desenvolvimento em c# e dotnet bem como aplicar padrões arquiteturais.

## Funcionalidades Principais

- Listagem de Localização
- Obger uma localização por meio do seu identificador
- Criar uma localização com um ponto geográfico
- Alterar os dados de uma localização
- Deletar uma localização

- Listagem de categorias 

## Tecnologias Utilizadas

### Backend
- Minimal API com c# e .NET 9
- EntityFrameworkCore
- GeoJSON
- Postgres com PostGIS
- Docker
- Docker Compose

## Instalação e Execução

Para instalar e executar o projeto, siga estes passos:

1. Clone o repositório:

```bash 
git clone https://github.com/apmleal/prova-havira-geojson.git
```

### Executar via Docker 

1. Navegue até o diretório do docker:

```bash 
cd .\prova-havira-geojson\docker
```

2. Execute um dos comandos abaixo para levarnta os containers de banco e da aplicação:

```bash 
cd .\up.ps1
```
ou 
```bash 
docker-compose -p docker-prova -f ./docker-compose.yml up --build -d
```

3. Apóss containers serem criados, pode acessar a aplicação backend pela url abxio
```bash 
http://localhost:8080/ROTA_APLICACAO
```

2. Para derrubar os containers execute um dos comandos abaixo:

```bash 
cd .\down.ps1
```
ou 
```bash 
docker-compose -p docker-prova -f .\docker-compose.yml down -v
```

### Execuiar via dotnetrun

1. Navegue até o diretório do projeto Prova.Api:

```bash 
cd .\presentation\Prova.Api\
```

2. Execute o comando abaixo:

```bash 
dotnet run Prova.Api.csproj urls=http://localhost:5555
```
3. Acesse o link abaixo para verificar as rotas disponiveis pelo Scalar:

```bash 
http://localhost:5555/scalar/v1
```


