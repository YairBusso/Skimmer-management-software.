﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BO;
using BlApi;
using System.Collections.ObjectModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private IBL blGui;
        ObservableCollection<CustomerToList> CustomersToListView;
        private Customer customer;
        private CustomerToList customerToList;
        Actions actions;
        string action;

        public CustomerWindow(IBL bL, string _action = "")
        {
            blGui = bL;
            actions = new();
            action = _action;
            CustomersToListView = new();
            customerToList = new();
            InitList();
            InitializeComponent();
            switch (action)
            {
                case "List":
                    ListWindow();
                    break;
                case "Updating":
                    BorderEnterNumber.Visibility = Visibility.Visible;
                    update.Visibility = Visibility.Hidden;
                    addButton.Content = "הצג";
                    Close.Content = "סגור";
                    actions = Actions.UPDATING;
                    break;
                case "Add":
                    AddWindow();
                    break;
                default:
                    break;
            }

        }
        private void AddWindow()
        {
            actions = Actions.ADD;
            addButton.Content = "הוסף";
            Close.Content = "סגור";
            List.Visibility = Visibility.Hidden;
            Updating.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
            customer = new();
            customer.location = new();
            DataContext = customer;
        }

        private void ListWindow()
        {

            CustomerListView.Items.Refresh();
            actions = Actions.LIST;
            addButton.Content = "הוסף לקוח";
            Close.Content = "סגור";
            List.Visibility = Visibility.Visible;
            Updating.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Hidden;
            CustomerListView.ItemsSource = CustomersToListView;
        }
        private void UpdatingWindow(int id)
        {
            actions = Actions.UPDATING;
            addButton.Content = "עדכן";
            Close.Content = "סגור";
            List.Visibility = Visibility.Hidden;
            Updating.Visibility = Visibility.Visible;
            Add.Visibility = Visibility.Hidden;
            customer = blGui.GetCustomer(id);
            fromCustomer.ItemsSource = customer.fromCustomer.ToList();
            toCustomer.ItemsSource = customer.toCustomer.ToList();
            DataContext = customer;
        }

        public void InitList()//
        {
            List<CustomerToList> temp = blGui.DisplaysIistOfCustomers().ToList();
            foreach (CustomerToList item in temp)
            {
                CustomersToListView.Add(item);
            }
        }


        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            switch (actions)
            {
                case Actions.LIST:
                    AddWindow();
                    break;
                case Actions.ADD:

                    if (customer.Id != default && customer.name != default && customer.location.longitude != default && customer.location.latitude != default && customer.phone != default)//להעביר את הבדיקה לאיז אנעבלעד 
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("האם ברצונך לאשר הוספה זו", "אישור", MessageBoxButton.OKCancel);//לשפר סטייל של ההודעה
                        switch (messageBoxResult)
                        {

                            case MessageBoxResult.OK:
                                _ = blGui.addCustomer(customer);
                                CustomersToListView.Add(blGui.DisplaysIistOfCustomers().First(i => i.Id == customer.Id));
                                MessageBox.Show("הלקוח נוצר  בהצלחה\n מיד תוצג רשימת הלקוחות", "אישור");
                                ListWindow();
                                break;
                            case MessageBoxResult.Cancel:
                                break;
                            default:
                                break;

                        }
                    }
                    else
                        MessageBox.Show("נא השלם את השדות החסרים", "אישור");
                    break;
                case Actions.UPDATING:
                    if (addButton.Content == "הצג")
                    {
                        var idFind = CustomersToListView.ToList().Find(i => i.Id == int.Parse(TxtBx_ID.Text.ToString()));
                        if (idFind != default)
                        {
                            BorderEnterNumber.Visibility = Visibility.Hidden;
                            update.Visibility = Visibility.Visible;
                            UpdatingWindow(idFind.Id);
                        }
                        else
                        {
                            MessageBox.Show("הלקוח המבוקש לא נמצא", "אישור");
                            Close();
                        }
                        break;
                    }
                    break;
                case Actions.REMOVE:
                    break;
                default:
                    break;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            switch (action)
            {
                case "List":
                    if (actions != Actions.LIST)
                    {
                        ListWindow();
                        CustomerListView.SelectedItem = null;
                    }
                    else
                        Close();
                    break;

                default:
                    Close();
                    break;
            }
        }
        
        private void fromCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idParcel = ((ParcelInCustomer)fromCustomer.SelectedItem).Id;
            new ParcelWindow(blGui, "ByCustomer", idParcel).Show();
            Close();
        }

        private void toCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idParcel = ((ParcelInCustomer)toCustomer.SelectedItem).Id;
            new ParcelWindow(blGui, "ByCustomer", idParcel).Show();
            Close();
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           customerToList  = (CustomerToList)CustomerListView.SelectedItem;
            if (customerToList != null)
            {
                UpdatingWindow(customerToList.Id);
            }
        }
    }
}