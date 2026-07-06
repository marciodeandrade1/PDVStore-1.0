using System;

namespace PDVStore.Models
{
    // Simple global session holder for the currently authenticated user.
    // This is a minimal approach for a WinForms app; consider a better
    // scoped/session design for larger applications.
    public static class Session
    {
        public static UsuarioCaixa? CurrentUser { get; set; }
    }
}
