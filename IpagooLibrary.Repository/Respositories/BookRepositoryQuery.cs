﻿using IpagooLibrary.Models.DTO;
using IpagooLibrary.Repository.Respositories.Interfaces;
using System;
using System.Data;

namespace IpagooLibrary.Repository.Respositories
{
    public class BookRepositoryQuery : IBookRepositoryQuery
    {
        public IDbCommand FilterBooks(BookFilter bookFilter, IDbCommand command)
        {
            try
            {
                command.CommandText = @"exec [dbo].[GetBooks]  @ISBN,@Title,@Author";//,@Offset,@Limit";

                var parameter = command.CreateParameter();

                parameter.ParameterName = "@ISBN";
                parameter.Value = bookFilter.ISBN == null ? string.Empty : bookFilter.ISBN;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Title";
                parameter.Value = bookFilter.Title == null ? string.Empty : bookFilter.Title;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Author";
                parameter.Value = bookFilter.AuthorName == null ? string.Empty : bookFilter.AuthorName;
                command.Parameters.Add(parameter);

                return command;
            }
            catch(Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }     
        }

        public IDbCommand AddBook(BookDTO book, IDbCommand command)
        {
            command.Parameters.Clear();

            //This procedure will add the author and genre if it does not exist,and then with the author and genre id add the book
            command.CommandText = @"exec [dbo].[AddBook]
                                        @ISBN
                                       ,@Title
                                       ,@Genre
                                       ,@Author";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "@ISBN"; 
            parameter.DbType = DbType.String;
            parameter.Value = book.ISBN;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Title";
            parameter.DbType = DbType.String;
            parameter.Value = book.Title;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Genre";
            parameter.DbType = DbType.String;
            parameter.Value = book.Genre;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Author";
            parameter.DbType = DbType.String;
            parameter.Value = book.AuthorName;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            return command;
        }

        public void CreateBookLender(BookLender bookLender, IDbCommand command)
        {
            //This proc will go find the book id by the isbn number and then use that when creating a new lender
            command.CommandText = @"exec [dbo].[CreateBookLender]
                                        @LenderName
                                       ,@BookISBN
                                       ,@DateLent
                                       ,@Comment";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "@LenderName";
            parameter.DbType = DbType.String;
            parameter.Value = bookLender.FriendName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@BookISBN";
            parameter.DbType = DbType.String;
            parameter.Value = bookLender.BookISBN;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@DateLent";
            parameter.DbType = DbType.Date;
            parameter.Value = bookLender.BorrowedDateTime;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Comment";
            parameter.DbType = DbType.String;
            parameter.Value = bookLender.Comments;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();
        }

        public void ReturnBook(ReturnBook returnBook, IDbCommand command)
        {
            command.CommandText = @"exec [dbo].[ReturnBook]
                                        @BookISBN
                                       ,@LenderID";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "@BookISBN";
            parameter.DbType = DbType.String;
            parameter.Value = returnBook.ISBN;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@LenderID";
            parameter.DbType = DbType.Int32;
            parameter.Value = returnBook.LenderID;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();
        }
    }
}
