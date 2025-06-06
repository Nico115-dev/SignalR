# SignalR en .NET: Comunicación en Tiempo Real

## ¿Qué es SignalR?

**SignalR** es una biblioteca de ASP.NET Core que permite agregar **funcionalidad en tiempo real** a tus aplicaciones web. Esto significa que el **servidor puede enviar actualizaciones a los clientes instantáneamente**, sin que estos tengan que solicitar constantemente nueva información.

------

## ¿Cómo funciona SignalR?

SignalR abstrae la complejidad de mantener conexiones en tiempo real utilizando internamente:

- **WebSockets** (preferido cuando está disponible)
- **Server-Sent Events**
- **Long Polling**

SignalR selecciona automáticamente el mejor método de conexión según el navegador y el servidor.

------

## Estructura básica de SignalR

1. **Hub:** Es una clase en el servidor que gestiona la conexión y los métodos que los clientes pueden invocar.
2. **Cliente (JavaScript):** Usa la librería de SignalR JS para conectarse al Hub, invocar métodos y escuchar eventos.
3. **Transporte:** Mecanismo de comunicación (WebSockets, etc.).

# Aplicación De Signal R 

En este proyecto, SignalR se utiliza para crear un **chat básico en tiempo real** donde cualquier mensaje enviado por un usuario es transmitido instantáneamente a todos los usuarios conectados.

### 🔄 ¿Cómo funciona en este caso?

1. El servidor tiene una clase llamada `ChatHub`, que actúa como punto central de conexión y comunicación.
2. El frontend (HTML + JavaScript) se conecta a ese `Hub` mediante la librería de SignalR.
3. Cuando un usuario escribe un mensaje y lo envía:
   - El mensaje se envía al servidor usando `connection.invoke("SendMessage", ...)`.
   - El `Hub` recibe el mensaje y lo reenvía a **todos** los clientes usando `Clients.All.SendAsync(...)`.
   - Todos los clientes actualizan su interfaz mostrando el nuevo mensaje.

Este sistema evita tener que recargar la página o consultar constantemente al servidor, permitiendo una experiencia fluida y en tiempo real.

```c#
using ApiExpo.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5262")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors();
app.MapHub<ChatHub>("/chatHub");

app.Run();

```

Program.cs 

- Registra el servicio SignalR.
- Habilita CORS para permitir peticiones desde el frontend.
- Expone el hub en la ruta `/chatHub`.



```c#
using Microsoft.AspNetCore.SignalR;

namespace ApiExpo.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

```

Chathub.cs

- Expone el método `SendMessage` que los clientes llaman.
- Usa `Clients.All.SendAsync` para enviar el mensaje a todos los clientes conectados.

## ¿Cómo correr el proyecto?

1. Abre el proyecto en Visual Studio.
2. Ejecuta `ApiExpo` y copia la URL con el puerto (ej. `http://localhost:5262`).
3. Abre `index.html` en el navegador (o usa Live Server).
4. Escribe tu nombre y mensaje → presiona "Enviar".
5. Otros navegadores con la página abierta verán los mensajes al instante.
