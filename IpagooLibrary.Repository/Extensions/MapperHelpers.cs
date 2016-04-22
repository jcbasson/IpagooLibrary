using IpagooLibrary.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace IpagooLibrary.Repository.Extensions
{
    public static class MapperHelpers
    {
        public static LibraryDTO MapToDto(this IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var books = new List<BookDTO>();
                
                while (reader.Read())
                {
                    try
                    {
                        books.Add(new BookDTO
                        {
                            ID = reader["ID"] != null ? (int)reader["ID"] : 0,
                            ISBN = reader["ISBN"] != null ? (string)reader["ISBN"] : string.Empty,
                            Title = reader["Title"] != null ? (string)reader["Title"] : string.Empty,
                            AuthorName = reader["AuthorName"] != null ? (string)reader["AuthorName"] : string.Empty,
                            Genre = reader["Genre"] != null ? (string)reader["Genre"] : string.Empty
                        });
                    }
                    catch (Exception ex)
                    {
                        //TODO lOG details of the record that failed to read and the exception
                        return null;
                    }
                }

                var libraryDTO = new LibraryDTO
                {
                    Books = books,
                    TotalBooks = books.Count
                };
                return libraryDTO;
            }
        }
    }
}
