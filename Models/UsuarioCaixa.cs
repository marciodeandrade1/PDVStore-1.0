using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace PDVStore.Models
{
    public class UsuarioCaixa
    {
        internal string? FotoPath;
        internal bool Ativo;

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public DateTime CreatedAt { get; set; }

        private string? fotoPath;

        public string? GetFotoPath()
        {
            return fotoPath;
        }

        public void SetFotoPath(string? value)
        {
            fotoPath = value;
        }

        private bool ativo = true;

        public bool GetAtivo()
        {
            return ativo;
        }

        public void SetAtivo(bool value)
        {
            ativo = value;
        }

        // === NOVO: Sistema de Permissões ===
        public TipoPermissao Permissao { get; set; } = TipoPermissao.Operador;

        public bool Autenticar(string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, SenhaHash);
        }

        public void SetSenha(string senha)
        {
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
        }
        // Métodos de verificação de permissão
        public bool EhAdmin() => Permissao == TipoPermissao.Administrador;
        public bool PodeGerenciarUsuarios() => Permissao == TipoPermissao.Administrador;
    }

    public enum TipoPermissao
    {
        Operador = 1,      // Pode vender, ver relatórios básicos
        Administrador = 2  // Pode gerenciar usuários, configurações, etc.
    }


}

