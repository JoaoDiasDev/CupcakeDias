using CupcakeDias.Shared.Services.Implementations;
using CupcakeDias.Shared.Services.Interfaces;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load(options: new DotEnvOptions(ignoreExceptions: false, trimValues: true));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IOrderDetailService, OrderDetailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
