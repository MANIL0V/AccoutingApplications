using AccountingOfGoods.EF;
using System;
using System.Collections.Generic;
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

namespace AccountingOfGoods.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        private Entities dbContext;

        public event EventHandler UpdateListViewRequested;

        // Метод для вызова события
        protected virtual void OnUpdateListViewRequested(EventArgs e)
        {
            UpdateListViewRequested?.Invoke(this, e);
        }

        public AdministratorPage()
        {
            InitializeComponent();
            dbContext = new Entities();
        }

        private void btnSignIN_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли RadioButton Admin
            if (Admin.IsChecked == true)
            {
                // Создаем нового пользователя с ролью "Администратор"
                AddNewUser(1); // Передаем ID роли "Администратор"
            }
            // Проверяем, выбран ли RadioButton User
            else if (User.IsChecked == true)
            {
                // Создаем нового пользователя с ролью "Пользователь"
                AddNewUser(2); // Передаем ID роли "Пользователь"
            }
            else
            {
                MessageBox.Show("Выберите роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewUser(int roleId)
        {
            try
            {
                // Создаем новый экземпляр пользователя с введенными данными
                User newUser = new User
                {
                    Login = Login.Text,
                    Password = Password.Text,
                    IDRole = roleId // Устанавливаем ID роли в зависимости от выбранного RadioButton
                };

                // Добавляем пользователя в контекст данных
                dbContext.User.Add(newUser);

                // Сохраняем изменения в базе данных
                dbContext.SaveChanges();

                // Сообщение об успешном добавлении пользователя
                MessageBox.Show("Пользователь успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                OnUpdateListViewRequested(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при добавлении пользователя
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
