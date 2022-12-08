using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using KotobuApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace KotobuApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookController : ApiController
    {
        db_a8f994_kotobudbEntities db = new db_a8f994_kotobudbEntities();

        //Add post request to add the book to db
        //api/book/
       
        public string Post(Book book)
        {
            //Book b1=new Book
            //{
            //    Name="Ghoorbaghe",
            //    Author="Ali mohammadzade",
            //    Categorys="Amuzeshi",
            //     CC="This is a test",
            //     CcDescription="This is a test for that",
            //     Cover="Cover link",
            //     Description="this is a description",
            //     IgLink="test link",
            //     MainLink="this is the main link",
            //     TwitterLink="This is a twiter link"
            //}
            db.Books.Add(book);
            db.SaveChanges();
            return "The book was added";

        }
        //api/book
        //Get All books
        public IEnumerable<Book> Get()
        {
            return db.Books.ToList();
        }
        //api/book/id
        //Get One book use id to search
        public Book Get(int id)
        {
         Book _book=db.Books.Find(id);
            return _book;
        }

        //Update all property of  a book with that id


        public string Put(int id,Book book)
        {
           
            Book _book = db.Books.Find(id);
            _book.Name = book.Name;
            _book.Author = book.Author;
            _book.Categorys = book.Categorys;
            _book.CC = book.CC;
            _book.CcDescription = book.CcDescription;
            _book.Cover = book.Cover;
            _book.Description = book.Description;
            _book.IgLink = book.IgLink;
            _book.MainLink = book.MainLink;
            _book.TwitterLink = book.TwitterLink;

            db.Entry(_book).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return "Book is Updated";
        }


        //Delete A book 
        public string Delete(int id)
        {
            Book _book = db.Books.Find(id);
            db.Entry(_book).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return "The book was deleted...";
        }
        [HttpPatch]
        public string Patch(int id,[FromBody] JsonPatchDocument<Book> vm)
        {
            Book book = db.Books.Find(id);
            vm.ApplyTo(book);
            db.Entry(book).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            return "It Is a succes patch";

        }
    }
}

