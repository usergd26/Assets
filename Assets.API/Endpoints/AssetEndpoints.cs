using Assets.API.Dtos;
using Assets.Domain;
using Assets.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assets.API.Endpoints
{
    public static class AssetEndpoints
    {
        private const string endpointGroup = "Assets";

        public static void MapAssetEndpoints(this IEndpointRouteBuilder app)
        {
            // Add AssetType
            app.MapPost("/assettypes", async ([FromServices] AppDbContext db, [FromBody] AssetTypeDto dto) =>
            {
                var assetType = new AssetType { Name = dto.Name };
                db.AssetTypes.Add(assetType);
                await db.SaveChangesAsync();
                dto.Id = assetType.Id;
                return Results.Created($"/assettypes/{dto.Id}", dto);
            }).WithTags(endpointGroup);

            // List AssetTypes
            app.MapGet("/assettypes", async ([FromServices] AppDbContext db) =>
            {
                var types = await db.AssetTypes
                    .Select(at => new AssetTypeDto
                    {
                        Id = at.Id,
                        Name = at.Name
                    })
                    .ToListAsync();
                return Results.Ok(types);
            }).WithTags(endpointGroup);

            // Add Asset
            app.MapPost("/assets", async ([FromServices] AppDbContext db, [FromBody] AssetDto dto) =>
            {
                var assetTypeExists = await db.AssetTypes.AnyAsync(at => at.Id == dto.AssetTypeId);
                if (!assetTypeExists)
                    return Results.BadRequest("AssetTypeId does not exist.");

                var asset = new Asset { Name = dto.Name, AssetTypeId = dto.AssetTypeId, Description = dto.Description, ModelNo = dto.ModelNo, Location = dto.Location, Status = dto.Status };
                db.Assets.Add(asset);
                await db.SaveChangesAsync();
                dto.Id = asset.Id;
                return Results.Created($"/assets/{dto.Id}", dto);
            }).WithTags(endpointGroup);

            // List Assets
            app.MapGet("/assets", async ([FromServices] AppDbContext db) =>
            {
                var assets = await db.Assets
                    .Include(a => a.AssetType)
                    .Select(a => new AssetDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        AssetTypeId = a.AssetTypeId,
                        Status = a.Status,
                        Description = a.Description,
                        Location = a.Location,
                        ModelNo = a.ModelNo
                    })
                    .ToListAsync();
                return Results.Ok(assets);
            }).WithTags(endpointGroup);

            app.MapGet("/assets/by-type/{assetTypeId:int}", async ([FromServices] AppDbContext db, int assetTypeId) =>
            {
                var assets = await db.Assets
                    .Where(a => a.AssetTypeId == assetTypeId)
                    .Select(a => new AssetDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        AssetTypeId = a.AssetTypeId,
                        ModelNo = a.ModelNo,
                        Location = a.Location,
                        Description = a.Description,
                        Status = a.Status
                    })
                    .ToListAsync();

                return Results.Ok(assets);
            }).WithTags(endpointGroup);
        }
    }
}