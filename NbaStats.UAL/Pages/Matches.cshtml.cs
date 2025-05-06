using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;
using NbaStats.BLL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NbaStats.UAL.Pages
{
    public class Matches : PageModel
    {
        private readonly IMatchService _matchService;
        private const int PageSize = 10;

        public List<DAL.Data.Match> MatchesByDate { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public Matches(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task OnGetAsync([FromQuery]int page = 1, DateTime? searchDate = null)
        {
            var allMatches = await _matchService.GetAllWithTeamsAsync();
            var filteredMatches = searchDate.HasValue
                ? allMatches.Where(m => m.Date.DayOfYear == searchDate.Value.DayOfYear && m.Date.Year == searchDate.Value.Year)
                : allMatches;
        
            var orderedMatches = filteredMatches.OrderByDescending(m => m.Date).ToList();
        
            TotalPages = (int)System.Math.Ceiling(orderedMatches.Count / (double)PageSize);
            CurrentPage = page;
        
            MatchesByDate = orderedMatches
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
        public async Task<IActionResult> OnGetExportAsync(string searchDate = null)
        {
            // Filter matches as in OnGetAsync
var allMatches = await _matchService.GetAllWithTeamsAsync();
var filteredMatches = string.IsNullOrWhiteSpace(searchDate)
    ? allMatches
    : allMatches.Where(m => m.Date.ToString("yyyy-MM-dd") == searchDate);
var orderedMatches = filteredMatches.OrderByDescending(m => m.Date).ToList();
var matches = orderedMatches.Take(PageSize).ToList();
            var csv = new StringBuilder();
            csv.AppendLine("Date,Home Team,Away Team,Home Score,Away Score");
            foreach (var match in matches)
            {
                csv.AppendLine($"{match.Date:yyyy-MM-dd},{match.HomeTeam.Name},{match.AwayTeam.Name},{match.HomeScore},{match.AwayScore}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "matches.csv");
        }
    }
}