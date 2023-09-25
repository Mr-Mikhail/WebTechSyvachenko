using AutoMapper;
using Azure;
using Azure.Search.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OnlineShop.Application.Configurations;
using OnlineShop.Application.Models;
using OnlineShop.Controllers.Api.Dish.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers.Api.Dish;

[Route(Routes.DishManagementSystem)]
public class DishManagementSystemApiController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly IRepository<Domain.Models.Dish> _dishRepository;
    private readonly SearchClient? _searchClient;
    private readonly IMapper _mapper;

    private const string IndexName = "azuresql-index";

    public DishManagementSystemApiController(IOptions<SearchConfiguration> searchOptions,
        IRepository<Domain.Models.Dish> dishRepository, IMapper mapper, IMemoryCache memoryCache)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
        var searchConfiguration = searchOptions.Value;
        if (searchConfiguration != null)
            _searchClient = new SearchClient(new Uri(searchConfiguration.SearchServiceUri), IndexName, new AzureKeyCredential(searchConfiguration.SearchServiceQueryApiKey));
    }

    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllDishesAsync(CancellationToken token)
    {
        try
        {
            var data = await _memoryCache.GetOrCreateAsync<List<DishApiResponse>>("all_dishes",
                async entry =>
                {
                    var dishes = await _dishRepository.GetAllAsync(token);
                    var response = _mapper.Map<List<DishApiResponse>>(dishes);

                    entry.Value = response;
                    entry.SlidingExpiration = TimeSpan.FromMinutes(10);

                    return response;
                });
            return Ok(data);
        }
        catch
        {
            return NotFound("Failed to get all dishes");
        }
    }
    
    [HttpGet(Routes.Filtered)]
    public async Task<IActionResult> GetFilteredDishesAsync([FromBody] PaginationModel model, CancellationToken token)
    {
        try
        {
            var dishes = await _dishRepository.GetAsync(_ => true, new FilteringOptions(model), token);
            var response = _mapper.Map<List<DishApiResponse>>(dishes);

            return Ok(response);
        }
        catch
        {
            return NotFound("Failed to get all dishes");
        }
    }

    [HttpGet(Routes.Search)]
    public async Task<IActionResult> GetSearchDishesAsync([FromQuery] string searchTerm, CancellationToken token)
    {
        try
        {
            if (_searchClient == null)
                return BadRequest("Search failed to complete");
            
            var dishes = await _searchClient.SearchAsync<Domain.Models.Dish>(searchTerm, cancellationToken: token).ConfigureAwait(false);
            var results = dishes.Value.GetResults().Select(x => x.Document).ToList();
            var response = _mapper.Map<List<DishApiResponse>>(results);

            return Ok(response);
        }
        catch
        {
            return NotFound("Failed to get all dishes");
        }
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateDishAsync([FromBody] DishApiRequest dish, CancellationToken token)
    {
        try
        {
            var data = _mapper.Map<Domain.Models.Dish>(dish);
            await _dishRepository.CreateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to create a dish");
        }
    }

    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateDishAsync([FromBody] DishApiRequest dish, CancellationToken token)
    {
        if (dish.Id == Guid.Empty)
            return BadRequest("Dish not specified");
        
        try
        {
            var data = _mapper.Map<Domain.Models.Dish>(dish);
            await _dishRepository.UpdateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to update dish");
        }
    }

    [HttpDelete(Routes.Delete)]
    public async Task<IActionResult> DeleteDishAsync([FromQuery] Guid id, CancellationToken token)
    {
        try
        {
            if (id == Guid.Empty)
                return BadRequest("Incorrect Id specified");
            
            await _dishRepository.DeleteAsync(id, token);
            return Ok();
        }
        catch
        {
            return BadRequest("Failed to delete dish");
        }
    }
}