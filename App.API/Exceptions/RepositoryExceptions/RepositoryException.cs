using Microsoft.EntityFrameworkCore;

namespace App.API.Exceptions.RepositoryExceptions
{
    public class RepositoryException : DbUpdateException
    {
        public RepositoryException() { }
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception innerException) : base(message, innerException) { }
    }
}
