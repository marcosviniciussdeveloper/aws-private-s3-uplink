# 🚀 AWS Private S3 Uplink API

API desenvolvida em **.NET 8** com foco em **segurança e integração privada com o Amazon S3**, utilizando boas práticas de arquitetura em nuvem na AWS.

---

## 📌 Visão Geral

Este projeto fornece uma API para **upload e gerenciamento de objetos no S3**, operando dentro de uma infraestrutura segura, sem exposição pública.

A autenticação é feita via **IAM Roles (keyless)**, eliminando a necessidade de credenciais sensíveis no código.

---

## 🏗️ Arquitetura

A solução segue padrões de segurança utilizados em ambientes corporativos:

* 🔒 **Sub-rede privada (VPC):**
  A API roda em uma instância sem IP público

* 🔑 **IAM Instance Profile:**
  Permissões gerenciadas via Role (sem Access Keys)

* 🔐 **Acesso via AWS SSM:**
  Conexão segura usando *port forwarding*, sem abrir portas (SSH/HTTP)

---

## 🛠️ Tecnologias

* **Backend:** .NET 8 (C#)
* **Containerização:** Docker / Docker Compose
* **Cloud:** AWS (EC2, S3, IAM, SSM, VPC)
* **Documentação:** Swagger (OpenAPI)

---

## 📂 Estrutura do Projeto

```
.
├── aws-private-s3-uplink/   # Código da API
├── Img/                     # Imagens do projeto
├── compose.yaml            # Orquestração Docker
├── .dockerignore
├── global.json
└── aws-private-s3-uplink.sln
```

---

## 📸 Demonstração

![Swagger UI](./ImgImg/RetornoDaApi.png)

---

## 🚀 Como Executar

### 1. Subir o container

```bash
docker-compose up -d
```

---

### 2. Criar túnel com AWS SSM

Como a aplicação está em rede privada, utilize:

```powershell
aws ssm start-session --target SEU_INSTANCE_ID \
  --document-name AWS-StartPortForwardingSession \
  --parameters '{"portNumber":["5198"],"localPortNumber":["5198"]}' \
  --region us-east-1
```

---

### 3. Acessar a API

Abra no navegador:

```
http://localhost:5198/swagger/index.html
```

---

## 🛡️ Permissões IAM

A Role da instância precisa de permissões mínimas no S3:

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "s3:PutObject",
        "s3:ListBucket"
      ],
      "Resource": [
        "arn:aws:s3:::SEU-BUCKET/*",
        "arn:aws:s3:::SEU-BUCKET"
      ]
    }
  ]
}
```

---

## 📌 Boas Práticas Implementadas

* ✔️ Segurança sem exposição pública
* ✔️ Uso de autenticação sem credenciais hardcoded
* ✔️ Infraestrutura baseada em princípios de menor privilégio
* ✔️ Containerização para portabilidade

---

## 👨‍💻 Autor

**Marcos Vinicius**
Analista de Suporte N3
Cloud & AWS Enthusiast ☁️

---

## 📄 Licença

Este projeto está sob a licença MIT.
