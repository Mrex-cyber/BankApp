using System.Text;

namespace BankApp
{
    public static class CurrencyFileProvider
    {
        public static async Task WriteToFileAsync(string jsonData)
        {
            FileInfo file = new FileInfo("currencies.json");

            using (var stream = file.OpenWrite())
            {
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);
                await stream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
            }
        }

        public static async Task<string> ReadFromFileAsync()
        {
            Console.WriteLine("Зчитування збережених даних...");

            using (var reader = new StreamReader("currencies.json", Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
