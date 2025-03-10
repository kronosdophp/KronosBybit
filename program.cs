using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

class AutomacaoSite
{
    static void Main()
    {
        string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

        while (true) // Loop infinito
        {
            // Encerra o processo do Chrome antes de iniciar uma nova instância
            foreach (var process in Process.GetProcessesByName("chrome"))
            {
                process.Kill();
            }

            // Aguarda um tempo para garantir que todos os processos do Chrome foram encerrados
            Thread.Sleep(1000);

            Process.Start(chromePath, "--remote-debugging-port=9222");
            Thread.Sleep(3000);

            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9222";

            using (IWebDriver driver = new ChromeDriver(options))
            {
                try
                {
                    driver.Manage().Window.FullScreen();
                    PrintAnimatedMessage("Navegador configurado para tela cheia.", ConsoleColor.Cyan);

                    string url = "https://www.bybit.com/pt-BR/trade/spot/PUFF/USDT";
                    driver.Navigate().GoToUrl(url);
                    PrintAnimatedMessage("Concluído " + url, ConsoleColor.Yellow);
                    Thread.Sleep(5000);

                    if (driver.Url == url)
                    {
                        PrintAnimatedMessage("Concluído URL atual: " + driver.Url, ConsoleColor.Green);
                    }
                    else
                    {
                        PrintAnimatedMessage("A navegação falhou. URL atual: " + driver.Url, ConsoleColor.Red);
                        return;
                    }

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    IWebElement botaoFerramentas = wait.Until(d => d.FindElement(By.CssSelector("#sfeFirstTabArea span:nth-of-type(2)")));
                    botaoFerramentas.Click();
                    PrintAnimatedMessage("Indo até Ferramentas...", ConsoleColor.Green);
                    Thread.Sleep(1000);

                    IWebElement entradaIceberg = wait.Until(d => d.FindElement(By.XPath("//div[@class='entry-item']//div[@class='title']//span[contains(text(),'Iceberg')]")));
                    PrintAnimatedMessage("Iceberg... " + entradaIceberg.Text, ConsoleColor.Yellow);
                    entradaIceberg.Click();
                    PrintAnimatedMessage("Concluído", ConsoleColor.Green);
                    Thread.Sleep(2000);

                    int valorFixo = 500;

                    IWebElement campoQuantidade = driver.FindElement(By.Id("total-qty"));
                    campoQuantidade.Clear();
                    campoQuantidade.SendKeys(valorFixo.ToString(CultureInfo.InvariantCulture));
                    PrintAnimatedMessage($"Valor fixo definido: {valorFixo}", ConsoleColor.Cyan);

                    Thread.Sleep(3000);

                    IWebElement campoPorPedido = driver.FindElement(By.Id("per-order-qty"));
                    campoPorPedido.Clear();
                    campoPorPedido.SendKeys(valorFixo.ToString(CultureInfo.InvariantCulture));
                    PrintAnimatedMessage($"Valor fixo definido: {valorFixo}", ConsoleColor.Cyan);

                    Thread.Sleep(3000);

                    IWebElement botaoComprarPrimeiro = wait.Until(d => d.FindElement(By.Id("sfeIceBergSubmitBtn")));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", botaoComprarPrimeiro);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", botaoComprarPrimeiro);
                    PrintAnimatedMessage("Comprando...", ConsoleColor.Green);

                    Thread.Sleep(5000);

                    IWebElement botaoComprarSegundo = wait.Until(d => d.FindElement(By.XPath("//div[@class='flex-row-space-between gap-24px']//button[contains(text(),'Comprar PUFF')]")));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", botaoComprarSegundo);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", botaoComprarSegundo);
                    PrintAnimatedMessage("Compra confirmada", ConsoleColor.Green);
                    Thread.Sleep(15000);

                    IWebElement botaoVender = wait.Until(d => d.FindElement(By.CssSelector("button.sell.flex-1 span")));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", botaoVender);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", botaoVender);
                    PrintAnimatedMessage("Iniciando Venda", ConsoleColor.Green);
                    Thread.Sleep(5000);

                    IWebElement campoQuantidadeFinal = driver.FindElement(By.Id("total-qty"));
                    campoQuantidadeFinal.Clear();
                    campoQuantidadeFinal.SendKeys(valorFixo.ToString(CultureInfo.InvariantCulture));
                    PrintAnimatedMessage($"Valor definido de Venda: {valorFixo}", ConsoleColor.Cyan);

                    Thread.Sleep(3000);

                    IWebElement campoPorPedidoFinal = driver.FindElement(By.Id("per-order-qty"));
                    campoPorPedidoFinal.Clear();
                    campoPorPedidoFinal.SendKeys(valorFixo.ToString(CultureInfo.InvariantCulture));
                    PrintAnimatedMessage($"Confirmando Valor de Venda: {valorFixo}", ConsoleColor.Cyan);

                    try
                    {
                        IWebElement botaoVenderPrimeiro = wait.Until(d => d.FindElement(By.Id("sfeIceBergSubmitBtn")));
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", botaoVenderPrimeiro);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", botaoVenderPrimeiro);
                        PrintAnimatedMessage("Vendendo...", ConsoleColor.Green);

                        IWebElement botaoVenderCarv = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'flex-row-space-between')]//button[contains(text(), 'Vender PUFF')]")));
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", botaoVenderCarv);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", botaoVenderCarv);
                        PrintAnimatedMessage("Vendido com sucesso!", ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        PrintAnimatedMessage("Erro ao tentar clicar nos botões de venda: " + ex.Message, ConsoleColor.Red);
                    }

                    Thread.Sleep(2000);
                }
                catch (Exception e)
                {
                    PrintAnimatedMessage("Erro: " + e.Message, ConsoleColor.Red);
                }
                finally
                {
                    driver.Quit();
                }
            }
        }
    }

    static void PrintAnimatedMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("╔════════════════════════════════════════════════╗");
        Console.WriteLine("║                Dev: Kronos Match               ║");
        Console.Write("║ ");

        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(30);
        }

        Console.WriteLine(" ║");
        Console.WriteLine("╚════════════════════════════════════════════════╝");
        Console.ResetColor();
    }
}
