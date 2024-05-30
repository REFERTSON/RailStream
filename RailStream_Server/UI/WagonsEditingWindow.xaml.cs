﻿using Microsoft.EntityFrameworkCore;
using RailStream_Server.Models;
using RailStream_Server_Backend.Managers;
using System.Windows;

namespace RailStream_Server
{
    public partial class WagonsEditingWindow : Window
    {
        public WagonsEditingWindow()
        {
            // Инициализация компонентов из Xaml
            InitializeComponent();
        }

        // Редактирование записи
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Получение информации с кнопки
            var wagon = ((FrameworkElement)sender).DataContext as Wagon;

            if (wagon != null)
            {
                var wagonEdit = new WagonsEditAndAddWindow(wagon.Id);
                wagonEdit.Show();
                this.Close();
            }
        }

        // Добавление записи
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var wagonAdd = new WagonsEditAndAddWindow();
            wagonAdd.Show();
            this.Close();
        }

        // Удаление записи
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Выбор вагона и присваивание объекта в переменную
            if (GridWagons.SelectedItem is Wagon wagonToDelete)
            {

                var result = MessageBox.Show($"Вы уверены, что хотите удалить вагон с номером '{wagonToDelete.WagonNumber}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                // Удаление вагона
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new DatabaseManager())
                    {
                        db.Wagons.Remove(wagonToDelete);
                        db.SaveChanges();
                    }
                    MessageBox.Show("Вагон успешно удален.");
                    LoadWagons();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите вагон для удаления!");
            }
        }

        // Обновление данных при изменении страницы
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadWagons();
            }
        }

        // Метод для обновления данных
        private void LoadWagons()
        {
            using (var db = new DatabaseManager())
            {
                // Обновляем данные из базы и устанавливаем их в DataGrid
                var wagons = db.Wagons
                    .Include(r => r.WagonType)
                    .Include(r => r.Train)
                    .ToList();
                GridWagons.ItemsSource = wagons;

            }
        }

        /*
         * Кнопки для перемещения
         */

        // Страница маршрутов
        private void RoutesBtn_Click(object sender, RoutedEventArgs e)
        {
            var routesMove = new RoutesEditingWindow();
            routesMove.Show();
            this.Close();
        }

        // Страница пользователей
        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {
            var usersMove = new UsersEditingWindow();
            usersMove.Show();
            this.Close();
        }

        // Страница билетов
        private void TicketsBtn_Click(object sender, RoutedEventArgs e)
        {
            var ticketsMove = new TicketsEditingWindow();
            ticketsMove.Show();
            this.Close();
        }

        // Страница поездов
        private void TrainsBtn_Click(object sender, RoutedEventArgs e)
        {
            var trainsMove = new TrainsEditingWindow();
            trainsMove.Show();
            this.Close();
        }

        // Страница вагонов
        private void WagonsBtn_Click(object sender, RoutedEventArgs e)
        {
            var wagonsMove = new WagonsEditingWindow();
            wagonsMove.Show();
            this.Close();
        }
    }
}
