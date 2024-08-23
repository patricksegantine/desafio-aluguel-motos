# Projeto de Loca��o de Motos

## Descri��o

Este projeto � uma plataforma de loca��o de motos que permite aos administradores cadastrar motos e aos entregadores alugar motos. A aplica��o � composta por duas APIs: API Admin e API Deliverers e um Worker.

## Funcionalidades

### API Admin

- **Cadastrar uma nova moto**
  - Dados obrigat�rios: Identificador, Ano, Modelo e Placa.
  - A placa � um dado �nico e n�o pode se repetir.
  - Gera um evento de moto cadastrada.
  - Publica a notifica��o por mensageria.
  - Consumidor notifica quando o ano da moto for "2024" e armazena a mensagem no banco de dados para consulta futura.

- **Consultar motos existentes**
  - Permite filtrar as motos pela placa.

- **Modificar uma moto**
  - Permite alterar apenas a placa de uma moto cadastrada indevidamente.

- **Remover uma moto**
  - Permite remover uma moto que foi cadastrada incorretamente, desde que n�o tenha registro de loca��es.


### API Deliverers
- **Cadastrar entregador**
  - Dados do entregador: Identificador, Nome, CNPJ, Data de Nascimento, N�mero da CNH, Tipo da CNH, Imagem da CNH.
  - Tipos de CNH v�lidos: A, B ou ambas A+B.
  - O CNPJ � �nico e n�o pode se repetir.
  - O n�mero da CNH � �nico e n�o pode se repetir.

- **Enviar foto da CNH**
  - Formatos permitidos: PNG ou BMP.
  - A foto n�o � armazenada no banco de dados, mas em um servi�o de storage (disco local, Amazon S3, MinIO ou outros).

- **Alugar uma moto**
  - Planos dispon�veis:
    - 7 dias com um custo de R$30,00 por dia.
    - 15 dias com um custo de R$28,00 por dia.
    - 30 dias com um custo de R$22,00 por dia.
    - 45 dias com um custo de R$20,00 por dia.
    - 50 dias com um custo de R$18,00 por dia.
  - A loca��o deve ter uma data de in�cio, uma data de t�rmino e uma data de previs�o de t�rmino.
  - O in�cio da loca��o � o primeiro dia ap�s a data de cria��o.
  - Somente entregadores habilitados na categoria A podem efetuar uma loca��o.

- **Informar data de devolu��o e consultar valor total da loca��o**
  - Permite informar a data de devolu��o da moto e consultar o valor total da loca��o.

## Como Rodar Localmente

### Pr�-requisitos

- Docker
- Docker Compose

### Passos para Rodar

1. Clone o reposit�rio:
   ```bash
   git clone https://github.com/patricksegantine/desafio-aluguel-motos.git
   cd seu-repositorio
   ```
2. Configure o arquivo appsettings.json com as informa��es necess�rias, incluindo o caminho de armazenamento para as fotos da CNH.

3. Execute o Docker Compose para iniciar os servi�os:
   ```bash
   docker compose up -d
   ```

Isso ir� iniciar os servi�os do RabbitMQ, MongoDB, PostgreSQL e PgAdmin. O banco de dados ser� criado automaticamente e as migrations ser�o executadas automaticamente quando a API Admin subir.

4. Acesse a API Admin e a API Deliverers conforme necess�rio para utilizar as funcionalidades descritas acima.

### Contribui��o

Sinta-se � vontade para contribuir com o projeto. Para isso, fa�a um fork do reposit�rio, crie uma branch para suas altera��es e envie um pull request.

### Licen�a

Este projeto est� licenciado sob a licen�a MIT. Veja o arquivo LICENSE para mais detalhes