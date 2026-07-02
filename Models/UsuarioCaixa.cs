using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;

namespace PDVStore.Models
{
    public class UsuarioCaixa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SenhaHash { get; set; }

        public bool Autenticar(string senha)
        {
            return BCrypt.Verify(senha, SenhaHash);
        }

        public void SetSenha(string senha)
        {
            SenhaHash = BCrypt.HashPassword(senha);
        }
    }
}
