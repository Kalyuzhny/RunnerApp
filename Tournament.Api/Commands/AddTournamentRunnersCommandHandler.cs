using MediatR;
using Polly.Retry;
using Polly;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Tournament.Api.Commands
{
    public class AddTournamentRunnersCommandHandler : IRequestHandler<AddTournamentRunnersCommand, bool>
    {
        private const int _maxApiCallRetries = 3;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _asyncRetryPolicy =
            Policy.
                HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode).
                WaitAndRetryAsync(
                _maxApiCallRetries,
                retryAttempt => TimeSpan.FromSeconds(retryAttempt),
                    (exception, timeSpan, retryCount, context) => {
                        // Logic that runs before each retry
                        // Write Logging Code Here
                    }
                );

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AddTournamentRunnersCommandHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(IHttpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }

        public async Task<bool> Handle(AddTournamentRunnersCommand request, CancellationToken cancellationToken)
        {
            var requestJson = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                Application.Json);

            var url = _configuration.GetValue<string>("RunnerApi");

            var httpResponseMessage = await _asyncRetryPolicy.ExecuteAsync(
                () =>
                {
                    var httpClient = _httpClientFactory.CreateClient();
                    return httpClient.PostAsync(url, requestJson);
                });

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<bool>(contentStream);
            }

            return false;
        }
    }
}
