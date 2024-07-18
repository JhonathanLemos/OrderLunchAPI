using AutoMapper;
using Lanches.Application.Commands.Orders.CreateOrder;
using Lanches.Application.Commands.Orders.UpdateOrder;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Entities;
using System.Diagnostics.CodeAnalysis;

public class OrderProfile : Profile
{
    [ExcludeFromCodeCoverage]
    public OrderProfile()
    {
        CreateMap<Order, OrderViewModel>();
        CreateMap<CreateOrderCommand, CreateOrUpdateOrderViewModel>();
        CreateMap<UpdateOrderCommand, CreateOrUpdateOrderViewModel>();
        CreateMap<Order, UpdateOrderCommand>();
    }
}
