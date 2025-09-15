# Yape.Library.IamValidation

NuGet para **validar apps** contra el servicio **IAM**.  
Expuesto como **clase estática** con un único método **Validate**:

```csharp
public static Task<bool> Validate(AuthValidateDto dto);
```

Devuelve **`true`** si el IAM valida las credenciales; **`false`** si no.  
**Quien llama siempre debe verificar el booleano y manejar las excepciones**.

---

## Instalación

1. Agregar el paquete desde YapeFeed.
2. Configurar los `appSettings` en el `web.config` / `app.config` del **proyecto host**.

```xml
<configuration>
  <appSettings>
    <add key="IAM.Validation.BaseUrl"              value="https://devboc02:4940" />
    <add key="IAM.Validation.EndpointAuthValidate" value="api/v1/IAM/channel/Auth/Validate" />
    <add key="IAM.Validation.TimeOutInSeconds"     value="30" />
    <add key="IAM.Validation.CacheMinutesTTL"      value="60" />
  </appSettings>
</configuration>
```

---

## DTO de entrada

```csharp
public class AuthValidateDto
{
    public string Authorization  { get; set; } = string.Empty;
    public string PublicToken    { get; set; } = string.Empty;
    public string AppUserId      { get; set; } = string.Empty;
    public string Channel        { get; set; } = string.Empty;
}
```

---

## Uso rápido

```csharp
using Yape.Library.IamValidation; // namespace del paquete

var dto = new AuthValidateDto
{
    Authorization = "password",
    PublicToken   = "abc123",
    AppUserId     = "appUserId",
    Channel       = "DevTest"
};

bool autorizado;
try
{
    autorizado = await IamValidator.Validate(dto);
}
catch (Exception ex)
{
    // Manejar la excepción (log, mapeo a dominio, etc.)
    throw;
}

if (!autorizado)
{
    // 401/forbidden de la app, o el flujo que corresponda
}
```

---

## Manejo de errores

El método puede lanzar excepciones si ocurre un problema **no funcional**:

- `HttpRequestException` (fallas de red / 5xx / DNS, etc.)
- `TaskCanceledException` (timeout)
- `InvalidOperationException` (contenido inesperado / JSON inválido)

---

## Caching

- El resultado **true/false** se guarda en cache internamente por `IAM.CacheMinutesTTL` minutos.  
- El cache **no** se usa si la llamada falló (excepción).

---
