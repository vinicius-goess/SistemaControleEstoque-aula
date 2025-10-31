using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SistemaControleEstoque.Util
{
    public static class Seguranca
    {
        // Define o número de iterações. 100.000 é um bom equilíbrio. [cite: 32]
        private const int Iterations = 100_000;

        // Define o tamanho do "salt". 16 bytes (128 bits) é o padrão. [cite: 34]
        private const int SaltLength = 16;

        // MUDANÇA: Define o tamanho do hash.
        // Alterado de 32 (SHA-256) para 20 (SHA-1), que é o padrão do .NET 4.7
        private const int HashLength = 20;

        /// <summary>
        /// Gera um hash de senha seguro usando PBKDF2 (com HMAC-SHA1 no .NET 4.7)
        /// </summary>
        /// <param name="senha">A senha em texto plano.</param>
        /// <param name="salt">O salt gerado.</param>
        /// <returns>Uma string Base64 contendo {Salt + Hash}.</returns>
        public static string GerarHashSenha(string senha, out byte[] salt)
        {
            if (senha == null) throw new ArgumentNullException(nameof(senha));

            // 1. Gera um "salt" aleatório.
            salt = new byte[SaltLength];

            // MUDANÇA: Substituído 'RandomNumberGenerator.Fill(salt)'
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // 2. Cria o hash usando PBKDF2.
            // MUDANÇA: O construtor do .NET 4.7 não aceita 'HashAlgorithmName'.
            // Ele usará HMAC-SHA1 por padrão.
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations))
            {
                var hash = pbkdf2.GetBytes(HashLength); // Pega 20 bytes

                // 3. Combina o salt e o hash em um único array.
                var hashBytes = new byte[SaltLength + HashLength];
                Array.Copy(salt, 0, hashBytes, 0, SaltLength);
                Array.Copy(hash, 0, hashBytes, SaltLength, HashLength);

                // 4. Converte para Base64.
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Verifica se uma senha em texto plano corresponde a um hash armazenado.
        /// </summary>
        /// <param name="senha">A senha que o usuário digitou.</param>
        /// <param name="hashArmazenado">O hash (Base64) do banco.</param>
        /// <returns>True se a senha for válida, False caso contrário.</returns>
        public static bool VerificarSenha(string senha, string hashArmazenado)
        {
            if (senha == null) throw new ArgumentNullException(nameof(senha));
            if (hashArmazenado == null) throw new ArgumentNullException(nameof(hashArmazenado));

            byte[] hashBytes;
            try
            {
                // 1. Converte o hash Base64 de volta para bytes.
                hashBytes = Convert.FromBase64String(hashArmazenado);
            }
            catch (FormatException)
            {
                return false;
            }

            // 2. O array deve ter o tamanho exato de Salt + Hash (16 + 20).
            if (hashBytes.Length != SaltLength + HashLength) return false;

            // 3. Extrai o salt.
            var salt = new byte[SaltLength];
            Array.Copy(hashBytes, 0, salt, 0, SaltLength);

            // 4. Recalcula o hash usando os mesmos parâmetros.
            // MUDANÇA: Usando o construtor padrão (HMAC-SHA1).
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations))
            {
                var hash = pbkdf2.GetBytes(HashLength);

                // 5. Extrai o hash original.
                var storedHash = new byte[HashLength];
                Array.Copy(hashBytes, SaltLength, storedHash, 0, HashLength);

                // 6. Compara os dois hashes.
                // MUDANÇA: Substituído 'CryptographicOperations.FixedTimeEquals'
                return SlowEquals(storedHash, hash);
            }
        }

        /// <summary>
        /// Método de comparação em tempo constante para prevenir Timing Attacks.
        /// (Substituto do CryptographicOperations.FixedTimeEquals)
        /// </summary>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }


        /// <summary>
        /// Valida a força de uma senha com base em um conjunto de regras.
        /// (Este método já era compatível com .NET 4.7)
        /// </summary>
        public static bool ValidarForcaSenha(string senha, out List<string> erros)
        {
            
            erros = new List<string>();
            if (string.IsNullOrEmpty(senha))
            {
                erros.Add("Senha vazia.");
                return false;
            }
            if (senha.Length < 12)
                erros.Add("A senha deve ter pelo menos 12 caracteres.");
            if (!Regex.IsMatch(senha, "[A-Z]"))
                erros.Add("A senha deve conter pelo menos 1 letra maiúscula.");
            if (!Regex.IsMatch(senha, "[a-z]"))
                erros.Add("A senha deve conter pelo menos 1 letra minúscula.");
            if (!Regex.IsMatch(senha, "[0-9]"))
                erros.Add("A senha deve conter pelo menos 1 dígito.");
            if (!Regex.IsMatch(senha, "[^a-zA-Z0-9]"))
                erros.Add("A senha deve conter pelo menos 1 caractere especial (ex: !@#$).");
            if (senha.Contains(" "))
                erros.Add("A senha não deve conter espaços.");
            var comuns = new[] { "123456", "password", "qwerty", "admin", "senha", "123456789", "12345678" };
            foreach (var c in comuns)
            {
                if (senha.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    erros.Add("A senha contém uma sequência comum ou óbvia.");
                    break;
                }
            }
            return erros.Count == 0;
        }
    }
}