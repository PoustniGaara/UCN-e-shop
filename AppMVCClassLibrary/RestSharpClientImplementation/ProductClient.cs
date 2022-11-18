using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebAppMVC.Exceptions;

namespace WebApiClient.RestSharpClientImplementation
{
    public class ProductClient : IProductClient
    {

        RestClient _client;
        public ProductClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<IEnumerable<ProductDto>> GetAllByCategoryAsync(string? category)
        {
            var response = await _client.ExecuteGetAsync<IEnumerable<ProductDto>>(new RestRequest($"{category}"));
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error in client retrieving all products. Message was {response.Content}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var response = await _client.ExecuteGetAsync<IEnumerable<ProductDto>>(new RestRequest());
            //var response = await _client.RequestAsync<IEnumerable<ProductDto>>(Method.Get, $"products");
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error in client retrieving all products. Message was {response.Content}");
            }

            return response.Data;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _client.RequestAsync<ProductDto>(Method.Get, $"products/{id}");
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all orders. Message was {response.Content}");
            }
            return response.Data;
        }
        public async Task<bool> UpdateAsync(ProductDto entity)
        {

            var request = new RestRequest($"{entity.Id}");
            request.AddBody(entity);
            var response = await _client.PutAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException($"Bad request. Message was {response.Content}");
            }
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                throw new UnprocessableEntityException($"UnprocessableEntity. Message was {response.Content}");
            }
            else
            {
                throw new Exception($"Error updating product in Api client. Message was {response.Content}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _client.RequestAsync(Method.Delete, $"products/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error deleting product with id={id}. Message was {response.Content}");
            }
        }

        public async Task<int> CreateAsync(ProductDto entity)
        {
            var response = await _client.RequestAsync<int>(Method.Post, "products", entity);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new BadRequestException($"Bad request. Message was {response.Content}");
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new UnprocessableEntityException($"UnprocessableEntity. Message was {response.Content}");
            }
            else
            {
                throw new Exception($"Error creating product. Message was {response.Content}");
            }
        }


    }
}
