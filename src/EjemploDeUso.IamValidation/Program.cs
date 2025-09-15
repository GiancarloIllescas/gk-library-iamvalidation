using System;
using System.Threading.Tasks;
using Yape.Library.IamValidation.Application.Interactors;
using YapeGeneralLimits.Application.Models.Dtos;

namespace EjemploDeUso.IamValidation
{
    internal class Program
    {
        static async Task Main()
        {
            var reqTest = new AuthValidateDto
            {
                Authorization = "Basic QkNQX1RFU1RfVVNFUjpJQWVMWnRPWU8wR0VIRUdD",
                PublicToken = "E39B8A04-EBCE-4AD6-BAFB-9BB851141299",
                AppUserId = "BC2400-test",
                Channel = "ADES-test"
            };

            Console.WriteLine("Llamada 1 (se llama a IAM)");
            var ok1 = await IamValidator.Validate(reqTest);
            Console.WriteLine($"Resultado 1: {ok1}");

            Console.WriteLine("Llamada 2 inmediata (debería estar en cache)");
            var ok2 = await IamValidator.Validate(reqTest);
            Console.WriteLine($"Resultado 2: {ok2}");

            Console.ReadKey();
        }
    }
}
