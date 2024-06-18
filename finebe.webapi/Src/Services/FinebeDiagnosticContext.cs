using finebe.webapi.Src.Interfaces;

namespace finebe.webapi.Src.Services;

public class FinebeDiagnosticContext : IFinebeDiagnosticContext
{
    public string CorrelationId { get; set; }
}
