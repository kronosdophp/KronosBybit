# KronosTradeBot

![GitHub repo size](https://img.shields.io/github/repo-size/kronosdophp/KronosBybit)
![GitHub last commit](https://img.shields.io/github/last-commit/kronosdophp/KronosBybit)

🚀 **Bot de automação para compra e venda de criptomoedas na Bybit, utilizando Selenium e a estratégia Iceberg.**

## 📌 Sobre o Projeto
KronosTradeBot é um bot desenvolvido em C# que automatiza a negociação da criptomoeda **PUFF/USDT** na plataforma **Bybit**. O bot:
- Fecha qualquer sessão do Chrome antes de iniciar;
- Abre o Chrome com modo **remote debugging**;
- Navega até a página de negociação da Bybit;
- Configura a estratégia **Iceberg**;
- Insere valores fixos para compra e venda;
- Executa as ordens automaticamente;
- Garante que todas as ações sejam registradas no console.

## ⚙️ Tecnologias Utilizadas
- **C#**
- **Selenium WebDriver**
- **Google Chrome (modo debugging)**

## 📦 Como Instalar

1. **Clone o repositório**:
   ```sh
   git clone https://github.com/seu-usuario/KronosTradeBot.git
   cd KronosTradeBot
   ```

2. **Instale as dependências do Selenium**:
   - No Visual Studio, instale via NuGet:
     ```sh
     Install-Package Selenium.WebDriver
     Install-Package Selenium.WebDriver.ChromeDriver
     ```

3. **Configure o caminho do Chrome**:
   - Edite a variável `chromePath` no código para apontar para o local correto do Chrome no seu sistema.

## 🚀 Como Usar

1. **Execute o programa:**
   ```sh
   dotnet run
   ```
2. O bot abrirá o Chrome, acessando a página de negociação na Bybit.
3. Ele definirá automaticamente os valores da estratégia Iceberg.
4. Ordens de compra e venda serão executadas conforme o código.
5. Todas as ações serão exibidas no console.

## 🛠 Melhorias Futuras
- Adicionar interface gráfica para facilitar a configuração dos valores.
- Implementar logs detalhados em arquivo.
- Melhorar tratamento de erros para evitar falhas inesperadas.

## 📜 Licença
Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

💡 **Desenvolvido por [Kronos Match](https://github.com/kronosdophp).**

