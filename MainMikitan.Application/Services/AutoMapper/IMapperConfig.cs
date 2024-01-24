namespace MainMikitan.Application.Services.AutoMapper;

public interface IMapperConfig
{
    public TTo Map<TFrom, TTo>(TFrom from, TTo to) where TFrom : class where TTo : class;
}