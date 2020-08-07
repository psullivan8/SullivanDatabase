//Patrik Sullivan psullivan8@cnm.edu
//SullivanDatabase
//08-05-20

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SullivanDatabase
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public int Copyright { get; set; }

        public bool Hardback { get; set; }

        public Book() : this(0, "", "", 0, false)
        {

        }

        public Book(int bookID, string title, string author, int copyright, bool hardback)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            Copyright = copyright;
            Hardback = hardback;
        }

        public override string ToString()
        {
            return BookID + " " + Title;
        }
    }
}
