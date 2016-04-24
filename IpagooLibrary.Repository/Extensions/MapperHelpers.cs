using IpagooLibrary.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace IpagooLibrary.Repository.Extensions
{
    public static class MapperHelpers
    {
        public static LibraryDTO MapToLibraryDto(this IDbCommand command)
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
                            ID = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0,
                            ISBN = reader["ISBN"] != DBNull.Value ? (string)reader["ISBN"] : string.Empty,
                            Title = reader["Title"] != DBNull.Value ? (string)reader["Title"] : string.Empty,
                            AuthorName = reader["AuthorName"] != DBNull.Value ? (string)reader["AuthorName"] : string.Empty,
                            Genre = reader["Genre"] != DBNull.Value ? (string)reader["Genre"] : string.Empty,
                            LenderID = reader["LenderID"] != DBNull.Value ? int.Parse(reader["LenderID"].ToString()) : -1,

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

        public static BookDTO MapToBookDto(this IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                
                while (reader.Read())
                {
                    try
                    {
                        return new BookDTO
                        {
                            ID = reader["ID"] != DBNull.Value ? (int)reader["ID"] : 0,
                            ISBN = reader["ISBN"] != DBNull.Value ? (string)reader["ISBN"] : string.Empty,
                            Title = reader["Title"] != DBNull.Value ? (string)reader["Title"] : string.Empty,
                            AuthorName = reader["AuthorName"] != DBNull.Value ? (string)reader["AuthorName"] : string.Empty,
                            Genre = reader["Genre"] != DBNull.Value ? (string)reader["Genre"] : string.Empty,
                            LenderID = reader["LenderID"] != DBNull.Value ? int.Parse(reader["LenderID"].ToString()) : -1,

                        };
                    }
                    catch (Exception ex)
                    {
                        //TODO lOG details of the record that failed to read and the exception
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
