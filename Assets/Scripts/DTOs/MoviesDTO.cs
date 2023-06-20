using System.Collections.Generic;

namespace DTOs
{
    public class MoviesDTO
    {
        public int page;
        public List<MovieDTO> results;
        public int total_pages;
        public int total_results;
    }
}