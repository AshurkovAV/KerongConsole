using KerongConsole;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

var ipAddress = builder.Configuration["Kerong:IpAddress"] ?? "192.168.0.178";
var defaultPort = builder.Configuration.GetValue<int?>("Kerong:Port") ?? 5000;
var ports = builder.Configuration.GetSection("Kerong:Ports").Get<int[]>() ?? new[] { 5000, 5001, 5002 };

var service = new KerongService(ipAddress, defaultPort, ports);

app.MapGet("/", () => Results.Redirect("/index.html"));

app.MapGet("/api/status", () =>
{
    var response = service.GetStatusData();

    return Results.Json(new
    {
        success = response != null,
        ip = ipAddress,
        port = defaultPort,
        ports = ports,
        raw = response == null ? null : BitConverter.ToString(response).Replace("-", " "),
        message = response == null ? "Контроллер не ответил или ответ невалиден." : "Статус получен."
    });
});

app.MapPost("/api/unlock", (UnlockRequest request) =>
{
    if (request.Cell <= 0 || request.Cell > 32)
    {
        return Results.BadRequest(new { success = false, message = "Номер ячейки должен быть от 1 до 32." });
    }

    var response = service.GetUnlockData(request.Cell);

    return Results.Json(new
    {
        success = response != null,
        cell = request.Cell,
        ip = ipAddress,
        port = defaultPort,
        raw = response == null ? null : BitConverter.ToString(response).Replace("-", " "),
        message = response == null ? "Команда не выполнена." : $"Ячейка {request.Cell} отправлена на открытие."
    });
});

app.Run();

public record UnlockRequest(int Cell);
