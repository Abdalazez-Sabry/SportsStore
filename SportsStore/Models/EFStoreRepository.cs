namespace SportsStore.Models {
    public class EFStoreRepository : IStoreRepository {
        private StoreDbContext context;
        public IQueryable<Product> Products => context.Products;

        public EFStoreRepository(StoreDbContext ctx) {
            context = ctx;
        }
    }
}