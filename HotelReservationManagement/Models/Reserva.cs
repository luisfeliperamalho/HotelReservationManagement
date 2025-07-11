public class Reserva
{
    public List<Pessoa> Hospedes { get; set; }
    public Suite SuiteReservada { get; set; }
    public DateTime DataCheckIn { get; set; }
    public DateTime? DataCheckOut { get; set; }
    public decimal? ValorTotal { get; set; }

    public void RealizarReserva()
    {
        string resposta;

        do
        {
            Console.Write("Digite o nome do hóspede: ");
            string nome = Console.ReadLine();

            // Cria nova pessoa e adiciona à lista de hóspedes
            var pessoa = new Pessoa();
            pessoa.Nome = nome ?? "Hóspede Anônimo";

            AdicionarHospede(pessoa.Nome);

            Console.Write("Deseja adicionar mais um hóspede? (s/n): ");
            resposta = Console.ReadLine()?.Trim().ToLower();

        } while (resposta == "s");


        // Receber data de check-in
        Console.Write("Digite a data de check-in (ex: 2025-07-15): ");
        DateTime checkIn;
        while (!DateTime.TryParse(Console.ReadLine(), out checkIn))
        {
            Console.Write("Data inválida. Digite novamente (ex: 2025-07-15): ");
        }
        DefinirDataCheckIn(checkIn);

        // Receber data de check-out
        Console.Write("Digite a data de check-out (ex: 2025-07-20): ");
        DateTime checkOut;
        while (!DateTime.TryParse(Console.ReadLine(), out checkOut) || checkOut <= checkIn)
        {
            Console.Write("Data inválida. O check-out deve ser depois do check-in. Tente novamente: ");
        }
        DefinirDataCheckOut(checkOut);

        // Receber informações da suíte
        Console.Write("Digite o preço da suíte: ");
        decimal precoSuite;
        while (!decimal.TryParse(Console.ReadLine(), out precoSuite) || precoSuite <= 0)
        {
            Console.Write("Preço inválido. Digite novamente: ");
        }

        DefinirSuite(precoSuite);



        CalcularValorTotal();
    }


    private void DefinirDataCheckIn(DateTime checkIn)
    {
        DataCheckIn = checkIn;
    }

    private void DefinirDataCheckOut(DateTime? checkOut)
    {
        DataCheckOut = checkOut;
    }


    private void AdicionarHospede(string nome)
    {
        if (Hospedes == null)
        {
            Hospedes = new List<Pessoa>();
        }

        Pessoa hospede = new Pessoa
        {
            Nome = nome
        };

        Hospedes.Add(hospede);
    }

    private void DefinirSuite(decimal preco)
    {
        Suite suite = new Suite
        {
            Preco = preco,
        };

        SuiteReservada = suite;
    }

    private void CalcularValorTotal()
    {
        if (SuiteReservada == null)
        {
            Console.WriteLine("Nenhuma suíte foi definida para a reserva.");
            ValorTotal = null;
            return;
        }

        if (!DataCheckOut.HasValue)
        {
            Console.WriteLine("Data de check-out inválida ou não definida.");
            ValorTotal = null;
            return;
        }

        var diasHospedagem = (DataCheckOut.Value - DataCheckIn).Days;

        if (diasHospedagem <= 0)
        {
            Console.WriteLine("A data de check-out deve ser após o check-in.");
            ValorTotal = null;
            return;
        }

        decimal valorBruto = diasHospedagem * SuiteReservada.Preco;
        ValorTotal = valorBruto;

        Console.WriteLine($"\nTotal de dias: {diasHospedagem}");
        Console.WriteLine($"Valor da diária: R$ {SuiteReservada.Preco:F2}");

        if (diasHospedagem >= 10)
        {
            ValorTotal *= 0.9m; // aplica 10% de desconto
            Console.WriteLine("Desconto aplicado: 10% para estadias com 10 ou mais dias.");
        }

        Console.WriteLine($"Valor total da reserva: R$ {ValorTotal:F2}");
    }

}