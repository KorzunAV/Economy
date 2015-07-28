using System.Collections;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using DataGridFilterLibrary.Support;

namespace DataGridFilterLibrary.Querying
{
    public class QueryControllerFactory
    {
        public static QueryController 
            GetQueryController(
            DataGrid dataGrid,
            FilterData filterData, IEnumerable itemsSource)
        {
            QueryController query;

            query = DataGridExtensions.GetDataGridFilterQueryController(dataGrid);

            if (query == null)
            {
                //clear the filter if exisits begin
                ICollectionView view
                    = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
                if (view != null) view.Filter = null;
                //clear the filter if exisits end

                query = new QueryController();
                DataGridExtensions.SetDataGridFilterQueryController(dataGrid, query);
            }

            query.ColumnFilterData        = filterData;
            query.ItemsSource             = itemsSource;
            query.CallingThreadDispatcher = dataGrid.Dispatcher;
            query.UseBackgroundWorker     = DataGridExtensions.GetUseBackgroundWorkerForFiltering(dataGrid);

            return query;
        }
    }
}
