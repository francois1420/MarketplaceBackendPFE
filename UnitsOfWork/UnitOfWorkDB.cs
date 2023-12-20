using marketplace_backend.Models;
using marketplace_backend.Repository.CartRepository;
using marketplace_backend.Repository.UserRepository;
using marketplace_backend.Repository.BaseItemRepository;
using marketplace_backend.Repository.CartLineRepository;
using marketplace_backend.Repository.CategoryRepository;
using marketplace_backend.Repository.ItemRepository;
using marketplace_backend.Repository.ItemImageRepository;
using marketplace_backend.Repository.AddressRepository;

namespace marketplace_backend.UnitsOfWork
{
    public class UnitOfWorkDB : IUnitOfWork
    {
        private readonly MarketplaceBackendDbContext context;

        private IUserRepository _userRepository;
        private IBaseItemRepository _baseItemRepository;
        private ICategoryRepository _categoryRepository;
        private IItemRepository _itemRepository;
        private IItemImageRepository _itemImageRepository;
        private ICartRepository _cartRepository;
        private ICartLineRepository _cartLineRepository;
        private IAddressRepository _addressRepository;

        public UnitOfWorkDB(MarketplaceBackendDbContext context)
        {
            this.context = context;
            this._userRepository = new UserRepository(context);
            this._categoryRepository = new CategoryRepository(context);
            this._baseItemRepository = new BaseItemRepository(context);
            this._itemRepository = new ItemRepository(context);
            this._itemImageRepository = new ItemImageRepository(context);
            this._cartRepository = new CartRepository(context);
            this._cartLineRepository = new CartLineRepository(context);
            this._addressRepository = new AddressRepository(context);
        }

        public IUserRepository UserRepository
        {
            get { return this._userRepository; }
        }

        public IBaseItemRepository BaseItemRepository
        {
            get { return this._baseItemRepository; }
        }

        public ICategoryRepository CategoryRepository
        {
            get { return this._categoryRepository; }
        }

        public IItemRepository ItemRepository
        {
            get { return this._itemRepository; }
        }

        public IItemImageRepository ItemImageRepository
        {
            get { return this._itemImageRepository; }
        }

        public ICartRepository CartRepository
        {
            get { return this._cartRepository; }
        }

        public ICartLineRepository CartLineRepository
        {
            get { return this._cartLineRepository; }
        }

        public IAddressRepository AddressRepository
        {
            get { return this._addressRepository; }
        }
    }
}
