
using ApiClients.Models.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace ApiClients.Base
{
    public abstract class BaseApiClient(HttpClient http)
    {
        protected readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
        // GET: Para traer datos
        protected async Task<ApiResult<T>> TryGetAsync<T>(string url, CancellationToken ct = default)
        {
            try
            {
                var resp = await http.GetAsync(url, ct);
                return await ProcessResponse<T>(resp, ct);
            }
            catch (Exception ex) { return ApiResult<T>.Failure(ex.Message); }
        }

        // POST: Para crear nuevos registros (Envía un Body)
        protected async Task<ApiResult<TResponse>> TryPostAsync<TRequest, TResponse>(string url, TRequest payload, CancellationToken ct = default)
        {
            try
            {
                var resp = await http.PostAsJsonAsync(url, payload, _jsonOptions, ct);
                return await ProcessResponse<TResponse>(resp, ct);
            }
            catch (Exception ex) { return ApiResult<TResponse>.Failure(ex.Message); }
        }

        // PUT: Para actualizar registros existentes
        protected async Task<ApiResult<TResponse>> TryPutAsync<TRequest, TResponse>(string url, TRequest payload, CancellationToken ct = default)
        {
            try
            {
                var resp = await http.PutAsJsonAsync(url, payload, _jsonOptions, ct);
                return await ProcessResponse<TResponse>(resp, ct);
            }
            catch (Exception ex) { return ApiResult<TResponse>.Failure(ex.Message); }
        }

        // DELETE: Para eliminar
        protected async Task<ApiResult<bool>> TryDeleteAsync(string url, CancellationToken ct = default)
        {
            try
            {
                var resp = await http.DeleteAsync(url, ct);
                return resp.IsSuccessStatusCode ? ApiResult<bool>.Success(true) : ApiResult<bool>.Failure($"Error {resp.StatusCode}");
            }
            catch (Exception ex) { return ApiResult<bool>.Failure(ex.Message); }
        }

        // Método privado para no repetir código de lectura de respuesta
        private async Task<ApiResult<T>> ProcessResponse<T>(HttpResponseMessage resp, CancellationToken ct)
        {
            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync(ct);
                return ApiResult<T>.Failure($"Error {resp.StatusCode}: {error}");
            }
            var data = await resp.Content.ReadFromJsonAsync<T>(_jsonOptions, ct);
            return ApiResult<T>.Success(data!);
        }
    }
}
