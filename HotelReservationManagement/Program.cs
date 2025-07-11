// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Seja bem vindo ao sistema de reservas do hotel!");
Console.WriteLine("Escolha uma suíte para reservar:");

Reserva rs = new Reserva();

Console.Write("Iniciar a reserva? (S/N):");
string resposta = Console.ReadLine().ToUpper();
while (resposta == "S")
{
    rs.RealizarReserva();
}

Console.WriteLine("O programa se encerrou");
