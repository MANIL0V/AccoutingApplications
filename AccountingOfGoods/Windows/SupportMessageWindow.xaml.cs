using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccountingOfGoods.Windows
{
    /// <summary>
    /// Логика взаимодействия для SupportMessageWindow.xaml
    /// </summary>
    public partial class SupportMessageWindow : Window
    {
        List<string> GoalOfRequestList = new List<string>() {"Ошибка в работе программы", "Вопрос по использованию программы", "Другая цель" };
        public SupportMessageWindow()
        {
            InitializeComponent();
            cmbGoalOfRequest.ItemsSource = GoalOfRequestList;
            cmbGoalOfRequest.SelectedIndex = 0;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            string smtpServer = "smtp.mail.ru";
            int smtpPort = 587;
            string smtpUsername = "mv_feedback@internet.ru";
            string smtpPassword = "YyteNQFEEehSP1fgEC77";

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add("anonimusov1@yandex.ru");
                    mailMessage.Subject = "FeedBack";

                    // Проверяем, заполнены ли поля перед созданием тела сообщения
                    if (!string.IsNullOrWhiteSpace(cmbGoalOfRequest.Text) && !string.IsNullOrWhiteSpace(Mail.Text) && !string.IsNullOrWhiteSpace(appeals.Text))
                    {
                        mailMessage.Body = $"Причина: {cmbGoalOfRequest.Text} \r\nEmail для связи: {Mail.Text} \r\nОбращение: {appeals.Text}";

                        try
                        {
                            smtpClient.Send(mailMessage);
                            MessageBox.Show("Сообщение отправлено.", "Поддержка", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка отправки: {ex.Message}", "Поддержка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, заполните все поля перед отправкой.");
                    }
                }
            }
        }
    }
}
