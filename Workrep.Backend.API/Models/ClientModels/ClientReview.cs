using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Models
{
    public class ClientReview
    {

        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public long WorkplaceOrganizationNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string Position { get; set; }
        public DateTime? EmploymentStart { get; set; }
        public DateTime? EmploymentEnd { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; }

        public string WorkplaceName { get; set; }

        public ClientReview(Review review, Workplace workplace)
        {
            ConstructClientReview(review);
            WorkplaceName = workplace.Name;
        }

        public ClientReview(Review review, string workplaceName)
        {
            ConstructClientReview(review);
            WorkplaceName = workplaceName;
        }

        private void ConstructClientReview(Review review)
        {
            ReviewId = review.ReviewId;
            UserId = (int)review.UserId;
            WorkplaceOrganizationNumber = review.WorkplaceOrganizationNumber;
            Timestamp = (DateTime)review.Timestamp;
            Position = review.Position;
            EmploymentStart = review.EmploymentStart;
            EmploymentEnd = review.EmploymentEnd;
            Comment = review.Comment;
            Rating = review.Rating;
        }

    }
}
