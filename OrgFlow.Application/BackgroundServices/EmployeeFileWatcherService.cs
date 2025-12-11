using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Interfaces;
using OrgFlow.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.BackgroundServices
{
    public class EmployeeFileWatcherService : BackgroundService
    {
        private readonly ILogger<EmployeeFileWatcherService> _logger;
        //private readonly IEmployeeImportService _importService;
        private readonly IServiceScopeFactory _scopeFactory;

        private FileSystemWatcher _watcher;
        private readonly string _watchPath = @"C:\OrgFlow\Inbound";

        private readonly TimeSpan _debounceTime = TimeSpan.FromMilliseconds(500);
        private DateTime _lastTriggerTime = DateTime.MinValue;

        public EmployeeFileWatcherService(
            ILogger<EmployeeFileWatcherService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            //_importService = importService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!Directory.Exists(_watchPath))
                Directory.CreateDirectory(_watchPath);

            _watcher = new FileSystemWatcher(_watchPath, "*.csv");

            _watcher.Created += async (sender, args) =>
            {
                // Debounce logic
                if (DateTime.Now - _lastTriggerTime < _debounceTime)
                    return;

                _lastTriggerTime = DateTime.Now;

                _logger.LogInformation($"Detected new CSV file: {args.FullPath}");

                try
                {
                    // Wait until file is fully written
                    await Task.Delay(1000);

                    using var stream = new FileStream(args.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var scope = _scopeFactory.CreateScope();
                    var repo = scope.ServiceProvider.GetRequiredService<IEmployeeImportService>();

                    var result = await repo.ImportEmployeesFromCsvFileAsync(args.FullPath);

                    if (result.Success)
                    {
                        _logger.LogInformation($"Import completed: {result.Message}");
                    }
                    else
                    {
                        _logger.LogWarning($"Import failed: {result.Message}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to import CSV file.");
                }
            };

            _watcher.EnableRaisingEvents = true;

            _logger.LogInformation("EmployeeFileWatcherService started.");

            return Task.CompletedTask;
        }
        private async Task<FileStream> TryOpenFileWithRetry(string path)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch (IOException)
                {
                    await Task.Delay(200);
                }
                catch 
                {
                    await Task.Delay(200);
                }
            }

            throw new FileNotFoundException($"Could not open file after retries: {path}");
        }



        public override void Dispose()
        {
            _watcher?.Dispose();
            base.Dispose();
        }
    }
}
