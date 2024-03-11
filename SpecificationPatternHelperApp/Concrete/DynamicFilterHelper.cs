namespace SpecificationPatternHelperApp.Concrete;

public partial class DynamicFilterHelper
{
    public string Filter { get; private set; }
    public string OrderBy { get; private set; }
    public string OrderByDescending { get; private set; }
    public int? PageIndex { get; private set; }
    public int? PageSize { get; private set; }
}

public partial class DynamicFilterHelper
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public class DynamicFilterHelperBuilder
    {
        private readonly DynamicFilterHelper _iDynamicFilterHelper;

        public DynamicFilterHelperBuilder()
        {
            _iDynamicFilterHelper ??= new DynamicFilterHelper();
        }

        public DynamicFilterHelperBuilder Filter(string filter = null)
        {
            _iDynamicFilterHelper.Filter = filter;
            return this;
        }

        public DynamicFilterHelperBuilder OrderBy(string orderBy = null)
        {
            _iDynamicFilterHelper.OrderBy = orderBy;
            return this;
        }

        public DynamicFilterHelperBuilder OrderByDescending(string orderByDescending = null)
        {
            _iDynamicFilterHelper.OrderByDescending = orderByDescending;
            return this;
        }

        public DynamicFilterHelperBuilder PageIndex(int? pageIndex = null)
        {
            _iDynamicFilterHelper.PageIndex = pageIndex;
            return this;
        }

        public DynamicFilterHelperBuilder PageSize(int? pageSize = null)
        {
            _iDynamicFilterHelper.PageSize = pageSize;
            return this;
        }

        public DynamicFilterHelper Build()
        {
            return _iDynamicFilterHelper;
        }
    }
}