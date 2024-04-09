using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccountingOfGoods.EF;


namespace AccountingOfGoods.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductInStockPage.xaml
    /// </summary>
    public partial class ProductInStockPage : Page
    {
        public ProductInStockPage()
        {
            InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            List<ChangeQuantityProduct> listProduct = new List<ChangeQuantityProduct>();

            if (dpStartDate.SelectedDate != null && dpEndtDate.SelectedDate != null)
            {
                listProduct = AppData.Context.ChangeQuantityProduct.
                Where(i => i.DateChange >= dpStartDate.SelectedDate && i.DateChange <= dpEndtDate.SelectedDate).
                ToList();
                lvProductInStock.ItemsSource = listProduct;
            }
            else
            {
                listProduct = AppData.Context.ChangeQuantityProduct.ToList();
                var resultMessage = MessageBox.Show("Сформировать отчет за все время?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultMessage == MessageBoxResult.Yes)
                {
                    lvProductInStock.ItemsSource = listProduct;
                }

            }

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument document = CreateFlowDocumentFromListView(lvProductInStock);
                IDocumentPaginatorSource paginatorSource = document;
                printDialog.PrintDocument(paginatorSource.DocumentPaginator, $"Отчет движения товаров на {DateTime.UtcNow}");
            }
        }

        private FlowDocument CreateFlowDocumentFromListView(ListView listView)
        {
            FlowDocument document = new FlowDocument();
            Table table = new Table();
            TableRowGroup rowGroup = new TableRowGroup();
            table.RowGroups.Add(rowGroup);

            // Создаем заголовок таблицы
            TableRow headerRow = new TableRow();
            foreach (GridViewColumn column in ((GridView)listView.View).Columns)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run(column.Header.ToString()))));
            }
            rowGroup.Rows.Add(headerRow);

            // Создаем строки с данными
            foreach (object item in listView.Items)
            {
                TableRow dataRow = new TableRow();
                foreach (GridViewColumn column in ((GridView)listView.View).Columns)
                {
                    string cellValue = GetCellValue(item, column.DisplayMemberBinding as Binding);
                    dataRow.Cells.Add(new TableCell(new Paragraph(new Run(cellValue))));
                }
                rowGroup.Rows.Add(dataRow);
            }

            document.Blocks.Add(table);
            return document;
        }

        private string GetCellValue(object item, Binding binding)
        {
            if (binding == null)
            {
                return string.Empty;
            }

            string propertyName = binding.Path.Path;
            object propertyValue = item.GetType().GetProperty(propertyName)?.GetValue(item);
            return propertyValue?.ToString() ?? "";
        }

    }
}
