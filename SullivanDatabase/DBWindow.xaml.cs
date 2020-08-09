//Patrik Sullivan psullivan8@cnm.edu
//SullivanDatabase
//08-05-20

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

//TODO:  Your database does not look like a DB and i can't run your program -5
//TODO:  your code looks good.  score 95/100

namespace SullivanDatabase
{
    /// <summary>
    /// Interaction logic for DBWindow.xaml
    /// </summary>
    public partial class DBWindow : Window
    {
        //Instantiate BindingList
        BindingList<Book> books = new BindingList<Book>();
        //Create a book object
        Book book;
        //Instantiate DBManager class
        DBManager dbManger = new DBManager();
        public DBWindow()
        {
            InitializeComponent();
            //Get data from Database and display
            dbManger.GetBooks(books);
            lbBooks.ItemsSource = books;
        }

        public void RefreshBooks()
        {
            books.Clear();
            lbBooks.ItemsSource = null;
            dbManger.GetBooks(books);
            lbBooks.ItemsSource = books;
            lbBooks.Items.Refresh();
        }

        private void UpdateManageControls()
        {
            if (lbBooks.SelectedItem != null)
            {
                tabControlForm.SelectedIndex = 1;
                lbBooks.SelectedItem = book;
                txtBookID.Text = book.BookID.ToString();
                txtTitle.Text = book.Title;
                txtAuthor.Text = book.Author;
                txtCopyright.Text = book.Copyright.ToString();
                switch (book.Hardback)
                {
                    case true:
                        ckbxHrdbck.IsChecked = true;
                        break;
                    case false:
                        ckbxHrdbck.IsChecked = false;
                        break;
                }
            }
        }

        private void ClearForm()
        {
            txtBookID.Clear();
            txtTitle.Clear();
            txtAuthor.Clear();
            txtCopyright.Clear();
            ckbxHrdbck.IsChecked = false;
        }

        private void GetBookFromControls()
        {
            try
            {
                book.BookID = int.Parse(txtBookID.Text);
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.Copyright = int.Parse(txtCopyright.Text);
                switch (ckbxHrdbck.IsChecked)
                {
                    case true:
                        book.Hardback = true;
                        break;
                    case false:
                        book.Hardback = false;
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry something went wrong.");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            tabControlForm.SelectedIndex = 1;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbBooks.SelectedItem != null)
            {
                tabControlForm.SelectedIndex = 1;
                UpdateManageControls();
            }
            else
            {
                MessageBox.Show("The book could not be found.");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbBooks.SelectedItem != null)
            {
                lbBooks.SelectedItem = book;
                dbManger.RemoveBook(book);
                RefreshBooks();
            }
            else
            {
                MessageBox.Show("The book could not be found.");
            }
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            book = new Book();
            GetBookFromControls();
            dbManger.Update(book);
            RefreshBooks();
            ClearForm();
            tabControlForm.SelectedIndex = 0;
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            if (txtAuthor.Text != "" ||
                txtBookID.Text != "" ||
                txtCopyright.Text != "" ||
                txtTitle.Text != "")
            {
                RefreshBooks();
                book = new Book();
                GetBookFromControls();
                dbManger.AddBook(book);
                RefreshBooks();
                ClearForm();
                tabControlForm.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("The book could not be added.");
            }
            tabControlForm.SelectedIndex = 0;
        }
    }
}
