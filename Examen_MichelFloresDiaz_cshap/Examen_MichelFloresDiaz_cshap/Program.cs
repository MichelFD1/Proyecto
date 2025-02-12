using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

class program
{
    static void Main(string[] arg)
    {
        // Inicializa el WebDriver (Navegador Chrome)
        IWebDriver driver = new ChromeDriver();

        // Navega hacia MercadoLibre
        driver.Url = "https://www.mercadolibre.com/";

        // Espera explícita hasta que el enlace del selector de país esté visible
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        // Localiza el enlace de selección de país usando el ID
        IWebElement selectCountryButton = wait.Until(d => d.FindElement(By.Id("MX"))); 

        // Hacer clic en el enlace para mostrar el selector de países
        selectCountryButton.Click();





        // Una vez cargado el idioma, realiza la búsqueda de PlayStation 5
        IWebElement searchBox = wait.Until(d => d.FindElement(By.Id("cb1-edit")));  
        searchBox.SendKeys("PlayStation 5");

        // Encuentra el botón de búsqueda y haz clic
        IWebElement searchButton = wait.Until(d => d.FindElement(By.ClassName("nav-icon-search")));  
        searchButton.Click();

        // Espera a que los resultados se carguen
        wait.Until(d => d.Url.Contains("playstation-5"));

        IWebElement Button = wait.Until(d => d.FindElement(By.XPath("//span[@class='ui-search-filter-name' and contains(text(), 'Nuevo')]")));
        Button.Click();


        var cookieButton = driver.FindElement(By.CssSelector(".cookie-consent-banner-opt-out__container button"));
        cookieButton.Click();


        IWebElement CDMXButton = wait.Until(d => d.FindElement(By.XPath("//span[@class='ui-search-filter-name' and contains(text(), 'Distrito Federal')]")));
        CDMXButton.Click();


        // Espera a que el botón del dropdown sea visible
        IWebElement dropdownButton = wait.Until(d => d.FindElement(By.CssSelector(".andes-dropdown__trigger")));

       
        bool isDropdownOpen = dropdownButton.GetAttribute("aria-expanded") == "true";

        if (isDropdownOpen)
        {
            // Si el menú está abierto, hacemos clic en el botón para cerrarlo
            dropdownButton.Click();
            Console.WriteLine("El menú fue cerrado.");
        }

        // Esperar un poco para asegurar que el menú esté cerrado antes de abrirlo de nuevo
        System.Threading.Thread.Sleep(500);  // 500 ms de espera

        // Volver a hacer clic para abrir el menú
        dropdownButton.Click();
        Console.WriteLine("El menú fue abierto.");

        // Esperar hasta que las opciones del menú estén visibles
        IWebElement option = wait.Until(d => d.FindElement(By.XPath("//span[text()='Mayor precio']")));

        // Hacer clic en la opción "Menor precio"
        option.Click();
      Console.WriteLine("Opción 'Mayor precio' seleccionada.");






        IList<IWebElement> productos = wait.Until(d => d.FindElements(By.CssSelector(".poly-component__title")));

       if (productos.Count == 0)
        {
            Console.WriteLine("No se encontraron productos.");
            return;
        }

        int count = 0;

        // Recorrer los primeros 5 productos
        foreach (var producto in productos)
        {
           if (count >= 5) break; // Limitar a los primeros 5 productos

            try
            {
                string nombre = producto.Text;

                string precioOriginal = string.Empty;
                string precioConDescuento = string.Empty;
                string simboloMoneda = string.Empty;
                string descuento = string.Empty;

                // Intentar obtener el precio original
                try
                {
                    IWebElement precioOriginalElemento = producto.FindElement(By.XPath(".//ancestor::div[contains(@class, 'poly-card__content')]//s[contains(@class, 'andes-money-amount--previous')]"));
                    if (precioOriginalElemento != null)
                    {
                       precioOriginal = precioOriginalElemento.FindElement(By.CssSelector(".andes-money-amount__fraction")).Text;
                        simboloMoneda = precioOriginalElemento.FindElement(By.CssSelector(".andes-money-amount__currency-symbol")).Text;
                       Console.WriteLine($"Precio Original: {simboloMoneda}{precioOriginal}");
                    }
                }
                catch (NoSuchElementException)
                {
                   // Console.WriteLine("No se encontró precio original.");
                }

                // Intentar obtener el precio con descuento (si está presente)
                try
                {
                    IWebElement precioConDescuentoElemento = producto.FindElement(By.XPath(".//ancestor::div[contains(@class, 'poly-card__content')]//span[contains(@class, 'andes-money-amount--cents-superscript')]"));
                    if (precioConDescuentoElemento != null)
                    {
                        precioOriginal = precioConDescuentoElemento.FindElement(By.CssSelector(".andes-money-amount__fraction")).Text;
                        precioConDescuento = precioConDescuentoElemento.FindElement(By.CssSelector(".andes-money-amount__fraction")).Text;
                        simboloMoneda = precioConDescuentoElemento.FindElement(By.CssSelector(".andes-money-amount__currency-symbol")).Text;
                        Console.WriteLine($"Precio : {simboloMoneda}{precioConDescuento}");
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("No se encontró precio con descuento.");
                }

                // Intentar obtener el descuento (si está presente)
                try
                {
                    IWebElement descuentoElemento = producto.FindElement(By.XPath(".//ancestor::div[contains(@class, 'poly-card__content')]//span[contains(@class, 'andes-money-amount__discount')]"));
                    if (descuentoElemento != null)
                    {
                        descuento = descuentoElemento.Text;
                        Console.WriteLine($"Descuento: {descuento}");
                    }
                }
                catch (NoSuchElementException)
                {
                  //  Console.WriteLine("No se encontró descuento.");
                }

                 //Verificar si ambos precios (original y con descuento) están disponibles
                if (!string.IsNullOrEmpty(precioOriginal) && !string.IsNullOrEmpty(precioConDescuento))
                {
                   //  Si hay precio original y descuento
                    Console.WriteLine($"Producto: {nombre} | Precio Original: {simboloMoneda}{precioOriginal} | Precio con Descuento: {simboloMoneda}{precioConDescuento} | Descuento: {descuento}");
                }
                else if (!string.IsNullOrEmpty(precioOriginal) && string.IsNullOrEmpty(precioConDescuento))
                {
                     //Si solo tiene precio original (sin descuento)
                    precioConDescuento = precioOriginal;  // Asignar el precio original como precio con descuento
                    Console.WriteLine($"Producto: {nombre} | Precio : {simboloMoneda}{precioOriginal} | Precio con Descuento: {simboloMoneda}{precioConDescuento}");
                }
                else if (!string.IsNullOrEmpty(precioConDescuento))
                {
                  //   Si solo tiene precio con descuento (sin precio original)
                    Console.WriteLine($"Producto: {nombre} | Precio : {simboloMoneda}{precioConDescuento}");
                }
                else
                {
                    Console.WriteLine($"Producto: {nombre} | No se pudo obtener precio.");
                }
            }
            catch (NoSuchElementException)
            {

            }

            count++;
        }
    }



}