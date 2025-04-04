# Documentação do Projeto

## Visão Geral

Este projeto implementa um sistema baseado em Kafka para envio e recebimento de mensagens. Ele é composto por:

1. **SendApi**: API responsável por enviar mensagens para o Kafka.
2. **ReceiveApi**: API responsável por consumir mensagens do Kafka.
3. **Kafka**: Sistema de mensagens que atua como intermediário entre as APIs.
4. **Frontend**: Interface para interação com o sistema (simulado no fluxo).

## Fluxo de Dados

1. O **Frontend** envia uma solicitação para a **SendApi** com o conteúdo da mensagem.
2. A **SendApi** publica a mensagem no tópico do Kafka.
3. A **ReceiveApi** consome as mensagens do tópico do Kafka.
4. O **ReceiveApi** processa e armazena as mensagens no banco de dados SQLite.

## Diagrama do Fluxo

![Diagrama do Fluxo](docs/diagrama-fluxo.png)

> **Nota**: O diagrama acima foi gerado a partir do código PlantUML. Para atualizá-lo, edite o arquivo `diagrama-fluxo.puml` e gere uma nova imagem.

## Estrutura do Projeto

- **src/Domain**: Contém as entidades do domínio, como `Message`.
- **src/Application**: Camada de aplicação (não implementada no fluxo atual).
- **src/Infrastructure**: Implementações de infraestrutura, como o `KafkaProducer` e `KafkaConsumer`.
- **src/Presentation**: APIs `SendApi` e `ReceiveApi` para interação com o sistema.
- **k8s**: Arquivos de configuração para deploy no Kubernetes.
- **docker-compose.yml**: Configuração para rodar o Kafka e Zookeeper localmente.

## Como Executar

1. Suba o Kafka e Zookeeper com `docker-compose up`.
2. Execute as APIs `SendApi` e `ReceiveApi`.
3. Use o arquivo `api-requests.http` para testar o envio e recebimento de mensagens.
4. Para deploy no Kubernetes, use os arquivos em `k8s` e o workflow do GitHub Actions.

## Tecnologias Utilizadas

- .NET 9.0
- Kafka
- SQLite
- Docker e Kubernetes
- GitHub Actions
