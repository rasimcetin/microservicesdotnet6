using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepo repository;
    private readonly IMapper mapper;

    public GrpcPlatformService(IPlatformRepo repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public override Task<PlatfomResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new PlatfomResponse();
        var platforms = repository.GetAllPlatforms();

        foreach (var plat in platforms)
        {
            response.Platform.Add(mapper.Map<GrpcPlatformModel>(plat));
        }

        return Task.FromResult(response);

    }
}