//Patrik Sullivan psullivan8@cnm.edu
//SullivanDatabase
//08-05-20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.ComponentModel;
using System.Data;
using System.Windows;

namespace SullivanDatabase
{
    class DBManager
    {
        public void GetBooks(BindingList<Book> books)
        {
            try
            {
                //Clear list of books
                books.Clear();
                string sqlStr = "SELECT * FROM Books;";
                string connStr = ConfigurationManager.ConnectionStrings["MyLibrary"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand selectCmd = new SqlCommand(sqlStr, conn);

                    conn.Open();

                    SqlDataReader rd = selectCmd.ExecuteReader();

                    while (rd.Read())
                    {
                        //Instantiate book with data from the Database
                        Book book = new Book((int)rd[0], (string)rd[1], (string)rd[2], (int)rd[3], (bool)rd[4]);
                        //Add book to BindingList
                        books.Add(book);
                    }
                    conn.Close();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddBook(Book book)
        {
            try
            {
                string sqlStr = "INSERT INTO Books (Title, Author, Copyright, Hardback) VALUES(@Title, @Author, @Copyright, @Hardback);";
                string connStr = ConfigurationManager.ConnectionStrings["MyLibrary"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand addCmd = new SqlCommand(sqlStr, conn);

                    addCmd.Parameters.AddWithValue("Title", book.Title);
                    addCmd.Parameters.AddWithValue("Author", book.Author);
                    addCmd.Parameters.AddWithValue("Copyright", book.Copyright);
                    addCmd.Parameters.AddWithValue("Hardback", book.Hardback);

                    //open database connection
                    conn.Open();
                    //run command
                    addCmd.ExecuteNonQuery();
                    //close database connection
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RemoveBook(Book book)
        {
            try
            {
                string sqlStr = "DELETE FROM Books WHERE BookID = @BookID;";
                string connStr = ConfigurationManager.ConnectionStrings["MyLibrary"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand removeCmd = new SqlCommand(sqlStr, conn);

                    removeCmd.Parameters.AddWithValue("BookID", book.BookID);

                    //open database connection
                    conn.Open();
                    //run command
                    removeCmd.ExecuteNonQuery();
                    //close database connection
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update(Book book)
        {
            try
            {
                string sqlStr = "UPDATE Books SET Title = @Title, Author = @Author, Copyright = @Copyright, Hardback = @Hardback WHERE BookID = @BookID;";
                string connStr = ConfigurationManager.ConnectionStrings["MyLibrary"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand updateCmd = new SqlCommand(sqlStr, conn);

                    updateCmd.Parameters.AddWithValue("BookID", book.BookID);
                    updateCmd.Parameters.AddWithValue("Title", book.Title);
                    updateCmd.Parameters.AddWithValue("Author", book.Author);
                    updateCmd.Parameters.AddWithValue("Copyright", book.Copyright);
                    updateCmd.Parameters.AddWithValue("Hardback", book.Hardback);

                    conn.Open();

                    updateCmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
