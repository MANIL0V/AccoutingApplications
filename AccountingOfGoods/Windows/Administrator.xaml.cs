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
using AccountingOfGoods.ClassHelper;
using AccountingOfGoods.EF;
using AccountingOfGoods.Pages;

namespace AccountingOfGoods.Windows
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        private Entities dbContext;

        public Administrator()
        {
            InitializeComponent();

            dbContext = new Entities();

            LoadUsers();


        }

        private void LoadUsers()
        {
            // Получаем список пользователей и их ролей из базы данных
            var usersWithRoles = GetUsersWithRoles();

            userListView.ItemsSource = usersWithRoles;
        }

        private List<UserWithRole> GetUsersWithRoles()
        {
            List<UserWithRole> usersWithRoles = new List<UserWithRole>();

            var query = from user in dbContext.User
                        join role in dbContext.Role on user.IDRole equals role.ID
                        select new UserWithRole
                        {
                            ID = user.ID,
                            Login = user.Login,
                            Password = user.Password,
                            RoleID = user.IDRole,
                            RoleName = role.name_role // Получаем название роли из таблицы Role
                        };

            return query.ToList();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, содержит ли frame.Content страницу AdministratorPage
            if (!(frame.Content is AdministratorPage))
            {
                // Если нет, создаем новый экземпляр страницы
                var page = new AdministratorPage();
                page.UpdateListViewRequested += AdministratorPage_UpdateListViewRequested;
                // Устанавливаем позицию страницы в Frame
                frame.Content = page;
            }
        }

        private void AdministratorPage_UpdateListViewRequested(object sender, EventArgs e)
        {
            // Обновляем ListView
            LoadUsers();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Получите координаты нажатия
            Point clickPoint = e.GetPosition(this);

            // Показать страницу AdministratorPage по этим координатам
            ShowPageAtPosition(clickPoint.X, clickPoint.Y);
        }

        // Метод для отображения страницы AdministratorPage в определенном месте
        private void ShowPageAtPosition(double x, double y)
        {
            // Создайте экземпляр вашей страницы
            var page = new AdministratorPage();

            // Установите позицию страницы в Frame
            frame.Content = page;
        }

        private void btnDeleteSection2_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, есть ли выбранная строка в ListView
            if (userListView.SelectedItem != null)
            {
                // Получаем выбранный объект из ListView
                var selectedUser = userListView.SelectedItem as UserWithRole;

                // Удаляем выбранную запись из базы данных
                var userToDelete = dbContext.User.FirstOrDefault(u => u.ID == selectedUser.ID);
                if (userToDelete != null)
                {
                    dbContext.User.Remove(userToDelete);
                    dbContext.SaveChanges();
                }

                // Обновляем ListView
                LoadUsers();
                MessageBox.Show("Учетная запись удалена", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
