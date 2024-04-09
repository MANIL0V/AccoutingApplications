using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        public AddProductPage()
        {
            InitializeComponent();
            var productList = AppData.Context.Product.ToList();

            if (productList.Any())
            {
                txtIdProduct.Text = (productList.Select(i => i.ID).Max() + 1).ToString();
            }
            else
            {
                txtIdProduct.Text = "1";
            }

            cmbNameCategory.ItemsSource = AppData.Context.CategoryProduct.ToList();
            cmbNameCategory.DisplayMemberPath = "NameCategory";
            cmbNameCategory.SelectedIndex = 0;

            cmbUnit.ItemsSource = AppData.Context.Unit.ToList();
            cmbUnit.DisplayMemberPath = "NameUnit";
            cmbUnit.SelectedIndex = 0;

        }



        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EF.Product product = new EF.Product();

                if (int.TryParse(txtIdProduct.Text, out int val))
                {
                    int idNewProduct = Int32.Parse(txtIdProduct.Text);
                    if (AppData.Context.Product.Where(i => i.ID == idNewProduct).FirstOrDefault() == null)
                    {
                        product.ID = Int32.Parse(txtIdProduct.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("Артикул занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Недопустимый артикул", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                product.NameProduct = txtNameProduct.Text;

                string categoryName = cmbNameCategory.Text;
                CategoryProduct category = AppData.Context.CategoryProduct.FirstOrDefault(c => c.NameCategory == categoryName);
                if (category == null)
                {
                    category = new CategoryProduct { NameCategory = categoryName };
                    AppData.Context.CategoryProduct.Add(category);
                    AppData.Context.SaveChanges();
                }
                product.IDCategory = category.ID;

                string unitName = cmbUnit.Text;
                Unit unit = AppData.Context.Unit.FirstOrDefault(u => u.NameUnit == unitName);
                if (unit == null)
                {
                    unit = new Unit { NameUnit = unitName };
                    AppData.Context.Unit.Add(unit);
                    AppData.Context.SaveChanges();
                }
                product.IDUnit = unit.ID;

                string storageNumber = txtStorage.Text.Trim();
                if (!string.IsNullOrEmpty(storageNumber))
                {
                    Storage storage = AppData.Context.Storage.FirstOrDefault(s => s.NumberStorage == storageNumber);
                    if (storage == null)
                    {
                        storage = new Storage { NumberStorage = storageNumber };
                        AppData.Context.Storage.Add(storage);
                        AppData.Context.SaveChanges();
                    }
                }

                var res = MessageBox.Show("Подтвердите добавление товара", "Подтверждение добавления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    AppData.Context.Product.Add(product);
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Товар успешно добавлен", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    ClassHelper.NavigateClass.MainFrame.GoBack();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }

                MessageBox.Show("Ошибка при валидации данных. Подробности в консоли.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteSection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CategoryProduct selectedCategory = cmbNameCategory.SelectedItem as CategoryProduct;

                if (selectedCategory != null)
                {
                    if (AppData.Context.Product.Any(p => p.IDCategory == selectedCategory.ID))
                    {
                        MessageBox.Show("Нельзя удалить категорию, так как с ней связаны товары", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int deletedCategoryId = selectedCategory.ID;

                    AppData.Context.CategoryProduct.Remove(selectedCategory);
                    AppData.Context.SaveChanges();


                    cmbNameCategory.ItemsSource = AppData.Context.CategoryProduct.ToList();

                    MessageBox.Show("Категория успешно удалена", "", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Выберите категорию для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении категории: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnDeleteSection2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Unit selectedUnit = cmbUnit.SelectedItem as Unit;

                if (selectedUnit != null)
                {
                    if (AppData.Context.Product.Any(p => p.IDUnit == selectedUnit.ID))
                    {
                        MessageBox.Show("Нельзя удалить единицу измерения, так как с ней связаны товары", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    AppData.Context.Unit.Remove(selectedUnit);
                    AppData.Context.SaveChanges();

                    AppData.ResetIdentity("Unit");

                    cmbUnit.ItemsSource = AppData.Context.Unit.ToList();

                    MessageBox.Show("Единица измерения успешно удалена", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберите единицу измерения для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении единицы измерения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
