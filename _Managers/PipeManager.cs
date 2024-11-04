using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Diagnostics;

namespace VCI_Forms_SPN._Managers
{
    public class PipeManager
    {
        private NamedPipeServerStream _pipeServer;
        private StreamWriter _writer;
        private StreamReader _reader;
        private CancellationTokenSource _cancellationTokenSource;
        public event Action<string> OnMessageReceived;

        public PipeManager()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartPipeServer()
        {
            try
            {
                _pipeServer = new NamedPipeServerStream("WaterCraftPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
                await _pipeServer.WaitForConnectionAsync();
                _writer = new StreamWriter(_pipeServer) { AutoFlush = true };
                _reader = new StreamReader(_pipeServer);
                _ = Task.Run(() => ReadFromPipe(_cancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting pipe server: " + ex.Message);
            }
        }

        private readonly object _pipeLock = new object();

        public void StopPipeServer()
        {
            lock (_pipeLock)
            {
                if (_pipeServer != null)
                {
                    _cancellationTokenSource.Cancel(); // Cancel the background task
                    _reader?.Dispose();
                    _writer?.Dispose();
                    _pipeServer.Close();
                    _pipeServer.Dispose();
                    _pipeServer = null;
                    _reader = null;
                    _writer = null;
                }

                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }



        // New async method for sending messages
        public async Task SendMessageAsync(string message)
        {
            if (_pipeServer != null && _pipeServer.IsConnected && _writer != null)
            {
                try
                {
                    await _writer.WriteLineAsync(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing to pipe: " + ex.Message);
                }
            }
            else
            {
                Debug.WriteLine("[DEBUG] Pipe server is not connected or writer is null.");
            }
        }


        private async Task ReadFromPipe(CancellationToken cancellationToken)
        {
            try
            {
                while (_pipeServer.IsConnected && !cancellationToken.IsCancellationRequested)
                {
                    string message = await _reader.ReadLineAsync();
                    if (message != null)
                    {
                        OnMessageReceived?.Invoke(message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Expected exception when the task is canceled
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DEBUG] Error in PipeManager: {ex.Message}");
            }
        }
    }
}
