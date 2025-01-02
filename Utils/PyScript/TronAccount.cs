using System.Diagnostics;
using System.Reflection;
using tgBotOrderV11.DbBot;

namespace tgBotOrderV11.Utils;

public class TronAccount
{
    public class WalletInfo
    {
        public string pubKey;
        public string privateKey;
        public WalletInfo(string info)
        {
            var tmp = info.Split("|");
            pubKey = tmp[0];
            privateKey = tmp[1];
        }
    }
    public static async Task<string?> BalanceTRX(string address)
    {
        try
        {
            // Получение относительного пути к скрипту Python
            string pythonScriptPath = FindPythonScript("", "CrpSystem.py");

            // Проверка наличия скрипта Python
            if (string.IsNullOrEmpty(pythonScriptPath) || !File.Exists(pythonScriptPath))
            {
                Console.WriteLine($"Ошибка: скрипт Python не найден по пути: {pythonScriptPath}");
                return null;
            }

            // Путь к интерпретатору Python в виртуальном окружении
            string pythonInterpreterPath = FindPythonInterpreter("", "myenv", "Scripts", "python.exe");

            // Проверка наличия интерпретатора Python
            if (string.IsNullOrEmpty(pythonInterpreterPath) || !File.Exists(pythonInterpreterPath))
            {
                Console.WriteLine($"Ошибка: интерпретатор Python не найден по пути: {pythonInterpreterPath}");
                return null;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInterpreterPath;
            start.Arguments = $"{pythonScriptPath} 4 {address}";
            start.WorkingDirectory = Path.GetDirectoryName(pythonScriptPath);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true; // Захват стандартного вывода ошибок
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    
                    
                }

                process.WaitForExit();
                int exitCode = process.ExitCode;
                Console.WriteLine($"Процесс завершился с кодом: {exitCode}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
        }
        return null;
    }
    public static async Task<bool?> TransactionUSDT(WalletInfo from, string to, string amount)
    {
        try
        {
            // Получение относительного пути к скрипту Python
            string pythonScriptPath = FindPythonScript("", "CrpSystem.py");

            // Проверка наличия скрипта Python
            if (string.IsNullOrEmpty(pythonScriptPath) || !File.Exists(pythonScriptPath))
            {
                Console.WriteLine($"Ошибка: скрипт Python не найден по пути: {pythonScriptPath}");
                return null;
            }

            // Путь к интерпретатору Python в виртуальном окружении
            string pythonInterpreterPath = FindPythonInterpreter("", "myenv", "Scripts", "python.exe");

            // Проверка наличия интерпретатора Python
            if (string.IsNullOrEmpty(pythonInterpreterPath) || !File.Exists(pythonInterpreterPath))
            {
                Console.WriteLine($"Ошибка: интерпретатор Python не найден по пути: {pythonInterpreterPath}");
                return null;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInterpreterPath;
            start.Arguments = $"{pythonScriptPath} 3 {from.pubKey} {from.privateKey} {to} {amount}";
            start.WorkingDirectory = Path.GetDirectoryName(pythonScriptPath);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true; // Захват стандартного вывода ошибок
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    
                    if (result == "SUCCESS")
                    {
                        return true;
                    }
                }

                process.WaitForExit();
                int exitCode = process.ExitCode;
                Console.WriteLine($"Процесс завершился с кодом: {exitCode}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
        }
        return null;
    }
    public static async Task<bool?> TransactionTRX(WalletInfo from, string to, string amount)
    {
        try
        {
            // Получение относительного пути к скрипту Python
            string pythonScriptPath = FindPythonScript("", "CrpSystem.py");

            // Проверка наличия скрипта Python
            if (string.IsNullOrEmpty(pythonScriptPath) || !File.Exists(pythonScriptPath))
            {
                Console.WriteLine($"Ошибка: скрипт Python не найден по пути: {pythonScriptPath}");
                return null;
            }

            // Путь к интерпретатору Python в виртуальном окружении
            string pythonInterpreterPath = FindPythonInterpreter("", "myenv", "Scripts", "python.exe");

            // Проверка наличия интерпретатора Python
            if (string.IsNullOrEmpty(pythonInterpreterPath) || !File.Exists(pythonInterpreterPath))
            {
                Console.WriteLine($"Ошибка: интерпретатор Python не найден по пути: {pythonInterpreterPath}");
                return null;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInterpreterPath;
            start.Arguments = $"{pythonScriptPath} 2 {from.pubKey} {from.privateKey} {to} {amount}";
            start.WorkingDirectory = Path.GetDirectoryName(pythonScriptPath);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true; // Захват стандартного вывода ошибок
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    
                    if (result == "SUCCESS")
                    {
                        return true;
                    }
                }

                process.WaitForExit();
                int exitCode = process.ExitCode;
                Console.WriteLine($"Процесс завершился с кодом: {exitCode}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
        }
        return null;
    }
    public static async Task<WalletInfo?> Create()
    {
        try
        {
            WalletInfo? walletInfo = null; 
            // Получение относительного пути к скрипту Python
            string pythonScriptPath = FindPythonScript("", "CrpSystem.py");

            // Проверка наличия скрипта Python
            if (string.IsNullOrEmpty(pythonScriptPath) || !File.Exists(pythonScriptPath))
            {
                Console.WriteLine($"Ошибка: скрипт Python не найден по пути: {pythonScriptPath}");
                return null;
            }

            // Путь к интерпретатору Python в виртуальном окружении
            string pythonInterpreterPath = FindPythonInterpreter("", "myenv", "Scripts", "python.exe");

            // Проверка наличия интерпретатора Python
            if (string.IsNullOrEmpty(pythonInterpreterPath) || !File.Exists(pythonInterpreterPath))
            {
                Console.WriteLine($"Ошибка: интерпретатор Python не найден по пути: {pythonInterpreterPath}");
                return null;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInterpreterPath;
            start.Arguments = $"{pythonScriptPath} 1";
            start.WorkingDirectory = Path.GetDirectoryName(pythonScriptPath);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true; // Захват стандартного вывода ошибок
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    
                    walletInfo = new WalletInfo(result);

                    return walletInfo;
                }

                process.WaitForExit();
                int exitCode = process.ExitCode;
                Console.WriteLine($"Процесс завершился с кодом: {exitCode}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
        }
        return null;
    }

    static string FindPythonScript(string directory, string scriptName)
    {
        return FindFile(directory, scriptName);
    }

    static string FindPythonInterpreter(string directory, string venvDirectory, string scriptsDirectory, string pythonExe)
    {
        return FindFile(directory, Path.Combine(venvDirectory, scriptsDirectory, pythonExe));
    }

    static string FindFile(string directory, string fileName)
    {
        // Получение начального каталога сборки
        string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        while (currentDirectory != null)
        {
            string foundPath = SearchDirectory(currentDirectory, directory, fileName);
            if (foundPath != null)
            {
                return foundPath;
            }

            // Переход в родительский каталог
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }

        // Если файл не найден, вернуть null
        return null;
    }

    static string SearchDirectory(string baseDirectory, string targetDirectory, string fileName)
    {
        // Получение полного пути к целевой директории
        string targetPath = Path.Combine(baseDirectory, targetDirectory);

        if (Directory.Exists(targetPath))
        {
            string filePath = Path.Combine(targetPath, fileName);
            if (File.Exists(filePath))
            {
                return filePath;
            }

            // Рекурсивный поиск в подкаталогах
            foreach (string subDirectory in Directory.GetDirectories(targetPath))
            {
                string foundPath = SearchDirectory(subDirectory, string.Empty, fileName);
                if (foundPath != null)
                {
                    return foundPath;
                }
            }
        }

        return null;
    }
}