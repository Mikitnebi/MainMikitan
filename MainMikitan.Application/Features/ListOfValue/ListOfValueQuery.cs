using System.Diagnostics;
using AutoMapper;
using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Database.Features.ListOfValue.Intefaces;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.ListOfValueResponses;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.ListOfValue;

public class ListOfValueQuery(int id) : IQuery<ListOfValueModel>
{
    public readonly int Id = id;
}

public class ListOfValueQueryHandler(IListOfValueQueryRepository listOfValueQueryRepository, IMapperConfig mapperConfig) : ResponseMaker<ListOfValueModel>,
    IQueryHandler<ListOfValueQuery, ListOfValueModel>
{
    public async Task<ResponseModel<ListOfValueModel>> Handle(ListOfValueQuery request,
        CancellationToken cancellationToken)
    {
        var stopWatch = new Stopwatch(); stopWatch.Start();
        var dictionaries = await listOfValueQueryRepository.GetDictionaryBySectorId(request.Id, cancellationToken);
        if(dictionaries.Count == 0)
            return Fail(ErrorType.Dictionary.NotFoundBySectorId);
        var dictionaryModel = dictionaries.Select(t => new DictionaryModel
        {
            Id = t.Id,
            NameEng = t.NameEng,
            NameGeo = t.NameGeo
        }).ToList(); 
        var sector = await listOfValueQueryRepository.GetSectorById(request.Id, cancellationToken);
        stopWatch.Stop(); Console.WriteLine(stopWatch.ElapsedMilliseconds);
        return Success(new ListOfValueModel
        {
            Dictionaries = dictionaryModel,
            Sector = sector
        });
    }
}