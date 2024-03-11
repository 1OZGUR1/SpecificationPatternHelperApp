
# Specification Design Pattern

English

The Specification design pattern is a software design pattern that defines a flexible way to specify business rules. It allows us to encapsulate complex logic in objects that can be easily reused and combined. This makes code more readable, maintainable, and testable.

Français:

Le modèle de conception Specification est un modèle de conception logicielle qui définit une manière flexible de spécifier les règles métier. Il nous permet d'encapsuler une logique complexe dans des objets qui peuvent être facilement réutilisés et combinés. Cela rend le code plus lisible, maintenable et testable.

Deutsch:

Das Specification-Entwurfsmuster ist ein Software-Entwurfsmuster, das eine flexible Möglichkeit zur Spezifizierung von Geschäftsregeln definiert. Es ermöglicht uns, komplexe Logik in Objekten zu kapseln, die einfach wiederverwendet und kombiniert werden können. Dies macht den Code lesbarer, wartbarer und testbarer.

Português:

O padrão de design Specification é um padrão de design de software que define uma maneira flexível de especificar regras de negócios. Ele nos permite encapsular lógica complexa em objetos que podem ser facilmente reutilizados e combinados. Isso torna o código mais legível, sustentável e testável.

Español:

El patrón de diseño Specification es un patrón de diseño de software que define una forma flexible de especificar reglas comerciales. Nos permite encapsular lógica compleja en objetos que se pueden reutilizar y combinar fácilmente. Esto hace que el código sea más legible, mantenible y testable.

العربية:

نمط تصميم المواصفات هو نمط تصميم برمجي يحدد طريقة مرنة لتحديد قواعد العمل. يسمح لنا بكبسولة المنطق المعقد في كائنات يمكن إعادة استخدامها ودمجها بسهولة. هذا يجعل الكود أكثر قابلية للقراءة والصيانة والاختبار.

Türkçe:

Specification Tasarım Desenı, iş kurallarını tanımlamak için esnek bir yol tanımlayan bir yazılım tasarım desenidir. Karmaşık mantığı kolayca yeniden kullanılabilir ve birleştirilebilen nesnelerde kapsüllememize olanak tanır. Bu, kodu daha okunabilir, sürdürülebilir ve test edilebilir hale getirir.

## Install 
### Step 1: Install the NuGet package

dotnet add package my-project

### Step 2: Navigate to the project

cd my-project

### Step 3: Run the project

dotnet run

## Technologies Used

**Orm:** Microsoft.EntityFrameworkCore Version=8.0.2

**Third Party Library:** System.Linq.Dynamic.Core Version=1.3.10


### Kullanım/Örnekler

```CSharp
public interface IEntity
{
    int Id { get; set; }
    string Name { get; set; }
    bool IsActive { get; set; }
}
public abstract class Entity : IEntity
{
    public abstract int Id { get; set; }
    public abstract string Name { get; set; }
    public abstract bool IsActive { get; set; }
}
public class Product : Entity
{

    public override int Id { get; set; }
    public override string Name { get; set; }
    public int Price { get; set; }
    public override bool IsActive { get; set; }


    #region Category Fk
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    #endregion
}
public interface IProductSpecification
{
    List<Product> GetProductsWithCategoryByPaginate(int pageNumber, int pageSize);
}

public class ProductSpecification : BaseSpecificationService<Product, AppMsSqlContext>, IProductSpecification

{
    private int _pageNumber;
    private int _pageSize;

    public ProductSpecification(AppMsSqlContext context) : base(context)
    {
    }


    public List<Product> GetProductsWithCategoryByPaginate(int pageNumber, int pageSize)
    {
        _pageNumber = pageNumber;
        _pageSize = pageSize;

        // Specification oluşturma ( Specification Create )
        Specification = new Specification<Product>(null);

        // İlişkili varlıkları dahil etme ( Includes Add )
        Specification.Includes.Add(p => p.Category);


        // Sayfalama ekleme ( Paging Add  )
        var query = SpecificationBuilder<Product>.GetQuery(_context.Products.AsQueryable(), Specification)
            .Skip((_pageNumber - 1) * _pageSize)
            .Take(_pageSize);

        // Ürünleri alma
        var products = query.ToList();
        return products;
    }
}
public class ProductRepository : EfCoreGenericRepository<Product, AppInMemoryContext>, IProductRepository
{
    private readonly IProductSpecification _iProductSpecification;

    public ProductRepository(IProductSpecification iProductSpecification)
    {
        _iProductSpecification = iProductSpecification;
    }

    /// <summary>
    ///     In SPesifection Method
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public List<Product> GetListProductWithIncludeByPaginate(int pageNumber, int pageSize)
    {
        return _iProductSpecification.GetProductsWithCategoryByPaginate(pageNumber, pageSize);
    }
 }
```
