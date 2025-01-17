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

### Executar via Docker > Passo 2
### Execuiar via dotnet > Passo ?

2. Navegue até o diretório do docker:

```bash 
cd .\prova-havira-geojson\docker
```

3. Execute um dos comandos abaixo para levarnta os containers de banco e da aplicação:

```bash 
cd .\up.ps1
```
ou 
```bash 
docker-compose -p docker-prova -f ./docker-compose.yml up --build -d
```
4. Apóss containers serem criados, pode acessar a aplicação backend pela url abxio
```bash 
http://localhost:8080/ROTA_APLICACAO
```

