using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AccountingOfGoods.EF;

namespace AccountingOfGoods.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddtrafficProductPage.xaml
    /// </summary>
    public partial class AddtrafficProductPage : Page
    {
        private readonly List<string> actionList = new List<string>();
        private Product product;
        private int initialInStockNow;
        private int initialCountProduct;

        public AddtrafficProductPage()
        {
            InitializeComponent();
            InitializeControls();
        }

        public AddtrafficProductPage(Product editProduct) : this()
        {
            product = editProduct;

            cmbAction.ItemsSource = actionList;
            cmbAction.SelectedIndex = 0;

            cmbStorageOut.ItemsSource = AppData.Context.Storage.ToList();
            cmbStorageOut.DisplayMemberPath = "NumberStorage";

            cmbStorageOut.SelectedItem = editProduct.Storage;

            txtIDProduct.Text = editProduct.ID.ToString();
            txtInStock.Text = editProduct.InStock.ToString();
            txtNameProduct.Text = editProduct.NameProduct;
            txtStorage.Text = editProduct.IDStorage == null ? "Не определено" : editProduct.Storage.NumberStorage;
            txtUnit.Text = editProduct.Unit.NameUnit;
            dpDateAction.SelectedDate = DateTime.Now;
            txtInStockNow.Text = txtInStock.Text;



        }

        private void InitializeControls()
        {
            actionList.AddRange(AppData.Context.TypeChange.Select(i => i.NameTypeChange));
        }

        private void txtCountProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num1 = 0, num2 = 0;

            txtCountProduct.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5F3504"));
            txtCountProduct.BorderThickness = new Thickness(0.5);

            if (int.TryParse(txtCountProduct.Text, out num1) && int.TryParse(txtCountProduct.Text, out num2))
            {
                initialInStockNow = num1;
                initialCountProduct = num2;
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCountProduct.Text) || string.IsNullOrWhiteSpace(txtInStockNow.Text))
            {
                MessageBox.Show("Введите значения в поля количество товара и остаток на складе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Извлекаем введенные пользователем значения
            string inputCountProduct = txtCountProduct.Text.Trim();
            string inputInStockNow = txtInStockNow.Text.Trim();

            // Проверяем, является ли первый символ числом или знаком "+" или "-"
            char firstChar = inputCountProduct.FirstOrDefault();
            bool isNumber = char.IsDigit(firstChar);
            bool isPositive = firstChar == '+';
            bool isNegative = firstChar == '-';

            // Извлекаем числовые значения из строк
            int num1, num2;
            if (isNumber || isPositive || isNegative)
            {
                inputCountProduct = isPositive || isNegative ? inputCountProduct.Substring(1) : inputCountProduct;
                if (!int.TryParse(inputCountProduct, out num1) || !int.TryParse(inputInStockNow, out num2))
                {
                    MessageBox.Show("Введите корректные значения в поля количество товара и остаток на складе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Выполняем операции в зависимости от знака перед числом
                if (isNumber || isPositive)
                {
                    num1 = isPositive ? num1 : 0;
                }
                else
                {
                    num1 = -num1;
                }
            }
            else
            {
                MessageBox.Show("Введите корректные значения в поля количество товара и остаток на складе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int res = num1 + num2; // выполняем операцию сложения

            // Проверяем, хватает ли товара на складе для операции
            if (res < 0)
            {
                MessageBox.Show("Товара на складе недостаточно для проведения данной операции", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Ваш остальной код
            product.InStock = res;
            product.IDStorage = (cmbStorageOut.SelectedItem as Storage)?.ID ?? 0;

            ChangeQuantityProduct quantityProduct = new ChangeQuantityProduct();
            quantityProduct.DateChange = dpDateAction.DisplayDate;
            quantityProduct.IDProduct = product.ID;
            quantityProduct.IDTypeChange = cmbAction.SelectedIndex + 1;
            quantityProduct.NumberDoc = txtNumberDoc.Text;
            quantityProduct.Quantity = res;
            quantityProduct.Note = txtNote.Text;
            quantityProduct.StartQuantity = Convert.ToInt32(inputInStockNow); // Ошибка была здесь.Надо было просто конвертировать в int

            if (num1 < 0)
            {
                quantityProduct.OutQuantity = Math.Abs(num1);
                quantityProduct.InQuantity = 0;
            }
            else
            {
                quantityProduct.OutQuantity = 0;
                quantityProduct.InQuantity = num1;
            }

            // Присваиваем значение предыдущего конечного остатка начальному остатку

            AppData.Context.ChangeQuantityProduct.Add(quantityProduct);
            AppData.Context.SaveChanges();

            MessageBox.Show("Товар успешно перемещен", "", MessageBoxButton.OK, MessageBoxImage.Information);
            ClassHelper.NavigateClass.MainFrame.GoBack();
        }

        private void txtNumberDoc_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNumberDoc.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5F3504"));
            txtNumberDoc.BorderThickness = new Thickness(0.5);
        }

        private void txtCountProduct_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCountProduct.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5F3504"));
            txtCountProduct.BorderThickness = new Thickness(0.5);
        }

        private void cmbStorageOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbStorageOut.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5F3504"));
        }

        private void cmbAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbAction.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5F3504"));
        }

        private void btnDeleteSection1_Click(object sender, RoutedEventArgs e)
        {
            Storage selectedStorage = cmbStorageOut.SelectedItem as Storage;

            if (selectedStorage != null)
            {
                // Проверяем, есть ли ссылки на выбранный элемент Storage
                bool hasReferences = AppData.Context.Storage.Any(entity => entity.ID == selectedStorage.ID);

                if (hasReferences)
                {
                    MessageBox.Show("Нельзя удалить данный элемент, так как на него ссылаются другие объекты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Если ссылок нет, удаляем выбранный элемент из базы данных
                AppData.Context.Storage.Remove(selectedStorage);
                AppData.Context.SaveChanges();

                // Обновляем ComboBox
                cmbStorageOut.ItemsSource = AppData.Context.Storage.ToList();

                // Показываем сообщение об успешном удалении
                MessageBox.Show("Элемент успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
/*
 * Это отрывок который нормально высчитывает но не сохраняет данные в таблицу
 * private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumberDoc.Text)) // если пустой номер документа
            {
                txtNumberDoc.BorderBrush = Brushes.Red;
                txtNumberDoc.BorderThickness = new Thickness(2);
                return;
            }           

            if (string.IsNullOrWhiteSpace(txtCountProduct.Text)) // если пусто количество товаров
            {
                txtCountProduct.BorderBrush = Brushes.Red;
                txtCountProduct.BorderThickness = new Thickness(2);
                return;
            }

            if (cmbStorageOut.SelectedIndex == -1) // если не выбрана секция
            {
                cmbStorageOut.BorderBrush = Brushes.Red;
                return;
            }

            if (cmbAction.SelectedIndex == 1) // если выбран Расход товаров проверяем их остаток на складе
            {
                int num1 = Int32.Parse(txtInStock.Text);
                int num2 = Int32.Parse(txtCountProduct.Text);
                int res = num1 - num2;
                if (res < 0)
                {
                    MessageBox.Show("Товара на складе недостаточно для проведения данной операции","",MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            

            product.InStock = Int32.Parse(txtInStockNow.Text);
            product.IDStorage = cmbStorageOut.SelectedIndex + 1;         

            ChangeQuantityProduct quantityProduct = new ChangeQuantityProduct();

            quantityProduct.DateChange = dpDateAction.DisplayDate;
            quantityProduct.IDProduct = product.ID;
            quantityProduct.IDTypeChange = cmbAction.SelectedIndex + 1;
            quantityProduct.NumberDoc = txtNumberDoc.Text;
            quantityProduct.Quantity = Int32.Parse(txtInStockNow.Text);
            quantityProduct.Note = txtNote.Text;
            quantityProduct.IDUser = ClassHelper.UserData.activUser.ID;
            quantityProduct.StartQuantity = Int32.Parse(txtInStock.Text);

            if (cmbAction.SelectedIndex == 0)
            {
                quantityProduct.InQuantity = Int32.Parse(txtCountProduct.Text);
                quantityProduct.OutQuantity = 0;
            }
            else
            {
                quantityProduct.OutQuantity = Int32.Parse(txtCountProduct.Text);
                quantityProduct.InQuantity = 0;
            }
            



            AppData.Context.ChangeQuantityProduct.Add(quantityProduct);
            AppData.Context.SaveChanges();

            MessageBox.Show("товар успешно перемещен", "", MessageBoxButton.OK, MessageBoxImage.Information);
            ClassHelper.NavigateClass.MainFrame.GoBack();
            
        }
 */
