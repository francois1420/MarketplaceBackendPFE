using marketplace_backend.Models;
using marketplace_backend.Repositories;
using marketplace_backend.Repository.BaseItemRepository;
using marketplace_backend.Repository.CartLineRepository;
using marketplace_backend.Repository.CartRepository;
using marketplace_backend.Repository.CategoryRepository;
using marketplace_backend.Repository.ItemImageRepository;
using marketplace_backend.Repository.ItemRepository;
using marketplace_backend.Repository.UserRepository;

namespace marketplace_backend.UnitsOfWork
{
    interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IBaseItemRepository BaseItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IItemRepository ItemRepository { get; }
        IItemImageRepository ItemImageRepository { get; }
        ICartRepository CartRepository { get; }
        ICartLineRepository CartLineRepository { get; }
    }
}
