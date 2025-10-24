using System;
using System.IO;

namespace SistemaControleEstoque.Util
{
    public static class Logger
    {
        // Registra um erro com detalhes opcionais da exceção
        // Os logs de erro são gravados em: %LocalAppData%/Sis_Estoque/logs/erros.txt
        // Exemplo típico: C:\Users\\SeuUsuario\\AppData\\Local\\Sis_Estoque\\logs\\erros.txt
        public static void LogError(string title, string message, Exception ex = null)
        {
            try
            {
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sis_Estoque", "logs");
                Directory.CreateDirectory(basePath);
                string file = Path.Combine(basePath, "erros.txt");

                using (var sw = new StreamWriter(file, true))
                {
                    string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sw.WriteLine($"[{ts}] ERROR: {title}");
                    sw.WriteLine($"[{ts}] {message}");
                    if (ex != null)
                    {
                        sw.WriteLine(ex.ToString());
                    }
                    sw.WriteLine();
                }
            }
            catch (Exception loggerEx)
            {
                // Caso ocorra erro ao registrar o log, salva em um arquivo de fallback
                FallbackLogger(loggerEx, "LogError");
            }
        }

        // Registra uma informação
        public static void LogInfo(string message)
        {
            try
            {
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sis_Estoque", "logs");
                Directory.CreateDirectory(basePath);
                string file = Path.Combine(basePath, "info.txt");

                using (var sw = new StreamWriter(file, true))
                {
                    string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sw.WriteLine($"[{ts}] {message}");
                }
            }
            catch (Exception loggerEx)
            {
                // Caso ocorra erro ao registrar o log, salva em um arquivo de fallback
                FallbackLogger(loggerEx, "LogInfo");
            }
        }

        // Registra avisos (problemas não fatais)
        public static void LogWarning(string message, string context = null)
        {
            try
            {
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sis_Estoque", "logs");
                Directory.CreateDirectory(basePath);
                string file = Path.Combine(basePath, "warnings.txt");

                using (var sw = new StreamWriter(file, true))
                {
                    string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sw.WriteLine($"[{ts}] WARNING: {message}");
                    if (!string.IsNullOrEmpty(context)) sw.WriteLine($"[{ts}] Contexto: {context}");
                    sw.WriteLine();
                }
            }
            catch (Exception loggerEx)
            {
                // Caso ocorra erro ao registrar o log, salva em um arquivo de fallback
                FallbackLogger(loggerEx, "LogWarning");
            }
        }

        // Registra exceções com contexto e usuário opcionais
        public static void LogException(Exception ex, string context = null, string user = null)
        {
            try
            {
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sis_Estoque", "logs");
                Directory.CreateDirectory(basePath);
                string file = Path.Combine(basePath, "exceptions.txt");

                using (var sw = new StreamWriter(file, true))
                {
                    string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sw.WriteLine($"[{ts}] EXCEPTION: {ex.Message}");
                    if (!string.IsNullOrEmpty(context)) sw.WriteLine($"[{ts}] Contexto: {context}");
                    if (!string.IsNullOrEmpty(user)) sw.WriteLine($"[{ts}] Usuário: {user}");
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine();
                }
            }
            catch (Exception loggerEx)
            {
                // Caso ocorra erro ao registrar o log, salva em um arquivo de fallback
                FallbackLogger(loggerEx, "LogException");
            }
        }

        /// <summary>
        /// Método auxiliar para registrar falhas do próprio logger em um arquivo de fallback.
        /// </summary>
        /// <param name="ex">Exceção capturada</param>
        /// <param name="origem">Método de origem do logger</param>
        private static void FallbackLogger(Exception ex, string origem)
        {
            try
            {
                string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sis_Estoque", "logs");
                Directory.CreateDirectory(basePath);
                string file = Path.Combine(basePath, "logger_failsafe.txt");
                using (var sw = new StreamWriter(file, true))
                {
                    string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sw.WriteLine($"[{ts}] FALHA NO LOGGER ({origem}): {ex.Message}");
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine();
                }
            }
            catch
            {
                // Se até o fallback do logger falhar, não há mais como registrar o erro.
                // Neste ponto, qualquer exceção será ignorada para não impactar a aplicação.
            }
        }
    }
}